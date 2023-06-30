using System.Collections.Generic;
using System.IO;
using Plugins.NavMeshPlus_master.NavMeshComponents.Scripts;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

namespace Plugins.NavMeshPlus_master.NavMeshComponents.Editor
{
    public class NavMeshAssetManager : ScriptableSingleton<NavMeshAssetManager>
    {
        internal struct AsyncBakeOperation
        {
            public NavMeshSurface Surface;
            public NavMeshData BakeData;
            public AsyncOperation BakeOperation;
        }

        List<AsyncBakeOperation> _mBakeOperations = new List<AsyncBakeOperation>();
        internal List<AsyncBakeOperation> GetBakeOperations() { return _mBakeOperations; }

        struct SavedPrefabNavMeshData
        {
            public NavMeshSurface Surface;
            public NavMeshData NavMeshData;
        }

        List<SavedPrefabNavMeshData> _mPrefabNavMeshDataAssets = new List<SavedPrefabNavMeshData>();

        static string GetAndEnsureTargetPath(NavMeshSurface surface)
        {
            // Create directory for the asset if it does not exist yet.
            var activeScenePath = surface.gameObject.scene.path;

            var targetPath = "Assets";
            if (!string.IsNullOrEmpty(activeScenePath))
            {
                targetPath = Path.Combine(Path.GetDirectoryName(activeScenePath), Path.GetFileNameWithoutExtension(activeScenePath));
            }
            else
            {
                var prefabStage = PrefabStageUtility.GetPrefabStage(surface.gameObject);
                var isPartOfPrefab = prefabStage != null && prefabStage.IsPartOfPrefabContents(surface.gameObject);

                if (isPartOfPrefab)
                {
#if UNITY_2020_1_OR_NEWER
                    var assetPath = prefabStage.assetPath;
#else
                    var assetPath = prefabStage.prefabAssetPath;
#endif
                    if (!string.IsNullOrEmpty(assetPath))
                    {
                        var prefabDirectoryName = Path.GetDirectoryName(assetPath);
                        if (!string.IsNullOrEmpty(prefabDirectoryName))
                            targetPath = prefabDirectoryName;
                    }
                }
            }
            if (!Directory.Exists(targetPath))
                Directory.CreateDirectory(targetPath);
            return targetPath;
        }

        static void CreateNavMeshAsset(NavMeshSurface surface)
        {
            var targetPath = GetAndEnsureTargetPath(surface);

            var combinedAssetPath = Path.Combine(targetPath, "NavMesh-" + surface.name + ".asset");
            combinedAssetPath = AssetDatabase.GenerateUniqueAssetPath(combinedAssetPath);
            AssetDatabase.CreateAsset(surface.NavMeshData, combinedAssetPath);
        }

        NavMeshData GetNavMeshAssetToDelete(NavMeshSurface navSurface)
        {
            if (PrefabUtility.IsPartOfPrefabInstance(navSurface) && !PrefabUtility.IsPartOfModelPrefab(navSurface))
            {
                // Don't allow deleting the asset belonging to the prefab parent
                var parentSurface = PrefabUtility.GetCorrespondingObjectFromSource(navSurface) as NavMeshSurface;
                if (parentSurface && navSurface.NavMeshData == parentSurface.NavMeshData)
                    return null;
            }

            // Do not delete the NavMeshData asset referenced from a prefab until the prefab is saved
            var prefabStage = PrefabStageUtility.GetPrefabStage(navSurface.gameObject);
            var isPartOfPrefab = prefabStage != null && prefabStage.IsPartOfPrefabContents(navSurface.gameObject);
            if (isPartOfPrefab && IsCurrentPrefabNavMeshDataStored(navSurface))
                return null;

            return navSurface.NavMeshData;
        }

        void ClearSurface(NavMeshSurface navSurface)
        {
            var hasNavMeshData = navSurface.NavMeshData != null;
            StoreNavMeshDataIfInPrefab(navSurface);

            var assetToDelete = GetNavMeshAssetToDelete(navSurface);
            navSurface.RemoveData();

            if (hasNavMeshData)
            {
                SetNavMeshData(navSurface, null);
                EditorSceneManager.MarkSceneDirty(navSurface.gameObject.scene);
            }

            if (assetToDelete)
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(assetToDelete));
        }

        public void StartBakingSurfaces(UnityEngine.Object[] surfaces)
        {
            // Remove first to avoid double registration of the callback
            EditorApplication.update -= UpdateAsyncBuildOperations;
            EditorApplication.update += UpdateAsyncBuildOperations;

            foreach (NavMeshSurface surf in surfaces)
            {
                StoreNavMeshDataIfInPrefab(surf);

                var oper = new AsyncBakeOperation();

                oper.BakeData = InitializeBakeData(surf);
                oper.BakeOperation = surf.UpdateNavMesh(oper.BakeData);
                oper.Surface = surf;

                _mBakeOperations.Add(oper);
            }
        }

        static NavMeshData InitializeBakeData(NavMeshSurface surface)
        {
            var emptySources = new List<NavMeshBuildSource>();
            var emptyBounds = new Bounds();
            return UnityEngine.AI.NavMeshBuilder.BuildNavMeshData(surface.GetBuildSettings(), emptySources, emptyBounds
                , surface.transform.position, surface.transform.rotation);
        }

        void UpdateAsyncBuildOperations()
        {
            foreach (var oper in _mBakeOperations)
            {
                if (oper.Surface == null || oper.BakeOperation == null)
                    continue;

                if (oper.BakeOperation.isDone)
                {
                    var surface = oper.Surface;
                    var delete = GetNavMeshAssetToDelete(surface);
                    if (delete != null)
                        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(delete));

                    surface.RemoveData();
                    SetNavMeshData(surface, oper.BakeData);

                    if (surface.isActiveAndEnabled)
                        surface.AddData();
                    CreateNavMeshAsset(surface);
                    EditorSceneManager.MarkSceneDirty(surface.gameObject.scene);
                }
            }
            _mBakeOperations.RemoveAll(o => o.BakeOperation == null || o.BakeOperation.isDone);
            if (_mBakeOperations.Count == 0)
                EditorApplication.update -= UpdateAsyncBuildOperations;
        }

        public bool IsSurfaceBaking(NavMeshSurface surface)
        {
            if (surface == null)
                return false;

            foreach (var oper in _mBakeOperations)
            {
                if (oper.Surface == null || oper.BakeOperation == null)
                    continue;

                if (oper.Surface == surface)
                    return true;
            }

            return false;
        }

        public void ClearSurfaces(UnityEngine.Object[] surfaces)
        {
            foreach (NavMeshSurface s in surfaces)
                ClearSurface(s);
        }

        static void SetNavMeshData(NavMeshSurface navSurface, NavMeshData navMeshData)
        {
            var so = new SerializedObject(navSurface);
            var navMeshDataProperty = so.FindProperty("m_NavMeshData");
            navMeshDataProperty.objectReferenceValue = navMeshData;
            so.ApplyModifiedPropertiesWithoutUndo();
        }

        void StoreNavMeshDataIfInPrefab(NavMeshSurface surfaceToStore)
        {
            var prefabStage = PrefabStageUtility.GetPrefabStage(surfaceToStore.gameObject);
            var isPartOfPrefab = prefabStage != null && prefabStage.IsPartOfPrefabContents(surfaceToStore.gameObject);
            if (!isPartOfPrefab)
                return;

            // check if data has already been stored for this surface
            foreach (var storedAssetInfo in _mPrefabNavMeshDataAssets)
                if (storedAssetInfo.Surface == surfaceToStore)
                    return;

            if (_mPrefabNavMeshDataAssets.Count == 0)
            {
                PrefabStage.prefabSaving -= DeleteStoredNavMeshDataAssetsForOwnedSurfaces;
                PrefabStage.prefabSaving += DeleteStoredNavMeshDataAssetsForOwnedSurfaces;

                PrefabStage.prefabStageClosing -= ForgetUnsavedNavMeshDataChanges;
                PrefabStage.prefabStageClosing += ForgetUnsavedNavMeshDataChanges;
            }

            var isDataOwner = true;
            if (PrefabUtility.IsPartOfPrefabInstance(surfaceToStore) && !PrefabUtility.IsPartOfModelPrefab(surfaceToStore))
            {
                var basePrefabSurface = PrefabUtility.GetCorrespondingObjectFromSource(surfaceToStore) as NavMeshSurface;
                isDataOwner = basePrefabSurface == null || surfaceToStore.NavMeshData != basePrefabSurface.NavMeshData;
            }
            _mPrefabNavMeshDataAssets.Add(new SavedPrefabNavMeshData { Surface = surfaceToStore, NavMeshData = isDataOwner ? surfaceToStore.NavMeshData : null });
        }

        bool IsCurrentPrefabNavMeshDataStored(NavMeshSurface surface)
        {
            if (surface == null)
                return false;

            foreach (var storedAssetInfo in _mPrefabNavMeshDataAssets)
            {
                if (storedAssetInfo.Surface == surface)
                    return storedAssetInfo.NavMeshData == surface.NavMeshData;
            }

            return false;
        }

        void DeleteStoredNavMeshDataAssetsForOwnedSurfaces(GameObject gameObjectInPrefab)
        {
            // Debug.LogFormat("DeleteStoredNavMeshDataAsset() when saving prefab {0}", gameObjectInPrefab.name);

            var surfaces = gameObjectInPrefab.GetComponentsInChildren<NavMeshSurface>(true);
            foreach (var surface in surfaces)
                DeleteStoredPrefabNavMeshDataAsset(surface);
        }

        void DeleteStoredPrefabNavMeshDataAsset(NavMeshSurface surface)
        {
            for (var i = _mPrefabNavMeshDataAssets.Count - 1; i >= 0; i--)
            {
                var storedAssetInfo = _mPrefabNavMeshDataAssets[i];
                if (storedAssetInfo.Surface == surface)
                {
                    var storedNavMeshData = storedAssetInfo.NavMeshData;
                    if (storedNavMeshData != null && storedNavMeshData != surface.NavMeshData)
                    {
                        var assetPath = AssetDatabase.GetAssetPath(storedNavMeshData);
                        AssetDatabase.DeleteAsset(assetPath);
                    }

                    _mPrefabNavMeshDataAssets.RemoveAt(i);
                    break;
                }
            }

            if (_mPrefabNavMeshDataAssets.Count == 0)
            {
                PrefabStage.prefabSaving -= DeleteStoredNavMeshDataAssetsForOwnedSurfaces;
                PrefabStage.prefabStageClosing -= ForgetUnsavedNavMeshDataChanges;
            }
        }

        void ForgetUnsavedNavMeshDataChanges(PrefabStage prefabStage)
        {
            // Debug.Log("On prefab closing - forget about this object's surfaces and stop caring about prefab saving");

            if (prefabStage == null)
                return;

            var allSurfacesInPrefab = prefabStage.prefabContentsRoot.GetComponentsInChildren<NavMeshSurface>(true);
            NavMeshSurface surfaceInPrefab = null;
            var index = 0;
            do
            {
                if (allSurfacesInPrefab.Length > 0)
                    surfaceInPrefab = allSurfacesInPrefab[index];

                for (var i = _mPrefabNavMeshDataAssets.Count - 1; i >= 0; i--)
                {
                    var storedPrefabInfo = _mPrefabNavMeshDataAssets[i];
                    if (storedPrefabInfo.Surface == null)
                    {
                        // Debug.LogFormat("A surface from the prefab got deleted after it has baked a new NavMesh but it hasn't saved it. Now the unsaved asset gets deleted. ({0})", storedPrefabInfo.navMeshData);

                        // surface got deleted, thus delete its initial NavMeshData asset
                        if (storedPrefabInfo.NavMeshData != null)
                        {
                            var assetPath = AssetDatabase.GetAssetPath(storedPrefabInfo.NavMeshData);
                            AssetDatabase.DeleteAsset(assetPath);
                        }

                        _mPrefabNavMeshDataAssets.RemoveAt(i);
                    }
                    else if (surfaceInPrefab != null && storedPrefabInfo.Surface == surfaceInPrefab)
                    {
                        //Debug.LogFormat("The surface {0} from the prefab was storing the original navmesh data and now will be forgotten", surfaceInPrefab);

                        var baseSurface = PrefabUtility.GetCorrespondingObjectFromSource(surfaceInPrefab) as NavMeshSurface;
                        if (baseSurface == null || surfaceInPrefab.NavMeshData != baseSurface.NavMeshData)
                        {
                            var assetPath = AssetDatabase.GetAssetPath(surfaceInPrefab.NavMeshData);
                            AssetDatabase.DeleteAsset(assetPath);

                            //Debug.LogFormat("The surface {0} from the prefab has baked new NavMeshData but did not save this change so the asset has been now deleted. ({1})",
                            //    surfaceInPrefab, assetPath);
                        }

                        _mPrefabNavMeshDataAssets.RemoveAt(i);
                    }
                }
            } while (++index < allSurfacesInPrefab.Length);

            if (_mPrefabNavMeshDataAssets.Count == 0)
            {
                PrefabStage.prefabSaving -= DeleteStoredNavMeshDataAssetsForOwnedSurfaces;
                PrefabStage.prefabStageClosing -= ForgetUnsavedNavMeshDataChanges;
            }
        }
    }
}
