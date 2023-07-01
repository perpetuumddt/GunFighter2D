using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
using UnityEditor;
#endif

namespace Plugins.NavMeshPlus_master.NavMeshComponents.Scripts
{
    public enum CollectObjects
    {
        All = 0,
        Volume = 1,
        Children = 2,
    }

    [ExecuteAlways]
    [DefaultExecutionOrder(-102)]
    [AddComponentMenu("Navigation/Navigation Surface", 30)]
    [HelpURL("https://github.com/Unity-Technologies/NavMeshComponents#documentation-draft")]
    public class NavMeshSurface : MonoBehaviour
    {
        [FormerlySerializedAs("m_AgentTypeID")] [SerializeField]
        int mAgentTypeID;
        public int AgentTypeID { get { return mAgentTypeID; } set { mAgentTypeID = value; } }

        [FormerlySerializedAs("m_CollectObjects")] [SerializeField]
        CollectObjects mCollectObjects = CollectObjects.All;
        public CollectObjects CollectObjects { get { return mCollectObjects; } set { mCollectObjects = value; } }

        [FormerlySerializedAs("m_Size")] [SerializeField]
        Vector3 mSize = new Vector3(10.0f, 10.0f, 10.0f);
        public Vector3 Size { get { return mSize; } set { mSize = value; } }

        [FormerlySerializedAs("m_Center")] [SerializeField]
        Vector3 mCenter = new Vector3(0, 2.0f, 0);
        public Vector3 Center { get { return mCenter; } set { mCenter = value; } }

        [FormerlySerializedAs("m_LayerMask")] [SerializeField]
        LayerMask mLayerMask = ~0;
        public LayerMask LayerMask { get { return mLayerMask; } set { mLayerMask = value; } }

        [FormerlySerializedAs("m_UseGeometry")] [SerializeField]
        NavMeshCollectGeometry mUseGeometry = NavMeshCollectGeometry.RenderMeshes;
        public NavMeshCollectGeometry UseGeometry { get { return mUseGeometry; } set { mUseGeometry = value; } }

        [FormerlySerializedAs("m_DefaultArea")] [SerializeField]
        int mDefaultArea;
        public int DefaultArea { get { return mDefaultArea; } set { mDefaultArea = value; } }

        [FormerlySerializedAs("m_IgnoreNavMeshAgent")] [SerializeField]
        bool mIgnoreNavMeshAgent = true;
        public bool IgnoreNavMeshAgent { get { return mIgnoreNavMeshAgent; } set { mIgnoreNavMeshAgent = value; } }

        [FormerlySerializedAs("m_IgnoreNavMeshObstacle")] [SerializeField]
        bool mIgnoreNavMeshObstacle = true;
        public bool IgnoreNavMeshObstacle { get { return mIgnoreNavMeshObstacle; } set { mIgnoreNavMeshObstacle = value; } }

        [FormerlySerializedAs("m_OverrideTileSize")] [SerializeField]
        bool mOverrideTileSize;
        public bool OverrideTileSize { get { return mOverrideTileSize; } set { mOverrideTileSize = value; } }
        [FormerlySerializedAs("m_TileSize")] [SerializeField]
        int mTileSize = 256;
        public int TileSize { get { return mTileSize; } set { mTileSize = value; } }
        [FormerlySerializedAs("m_OverrideVoxelSize")] [SerializeField]
        bool mOverrideVoxelSize;
        public bool OverrideVoxelSize { get { return mOverrideVoxelSize; } set { mOverrideVoxelSize = value; } }
        [FormerlySerializedAs("m_VoxelSize")] [SerializeField]
        float mVoxelSize;
        public float VoxelSize { get { return mVoxelSize; } set { mVoxelSize = value; } }

        // Currently not supported advanced options
        [FormerlySerializedAs("m_BuildHeightMesh")] [SerializeField]
        bool mBuildHeightMesh;
        public bool BuildHeightMesh { get { return mBuildHeightMesh; } set { mBuildHeightMesh = value; } }

        [FormerlySerializedAs("m_HideEditorLogs")] [SerializeField]
        bool mHideEditorLogs;
        public bool HideEditorLogs { get { return mHideEditorLogs; } set { mHideEditorLogs = value; } }

        // Reference to whole scene navmesh data asset.
        [FormerlySerializedAs("m_NavMeshData")]
        [UnityEngine.Serialization.FormerlySerializedAs("m_BakedNavMeshData")]
        [SerializeField]
        NavMeshData mNavMeshData;
        public NavMeshData NavMeshData { get { return mNavMeshData; } set { mNavMeshData = value; } }

        // Do not serialize - runtime only state.
        NavMeshDataInstance _mNavMeshDataInstance;
        Vector3 _mLastPosition = Vector3.zero;
        Quaternion _mLastRotation = Quaternion.identity;

        static readonly List<NavMeshSurface> SNavMeshSurfaces = new List<NavMeshSurface>();
        public INavMeshExtensionsProvider NevMeshExtensions { get; set; } = new NavMeshExtensionsProvider();

        public static List<NavMeshSurface> ActiveSurfaces
        {
            get { return SNavMeshSurfaces; }
        }

        void OnEnable()
        {
            Register(this);
            AddData();
        }

        void OnDisable()
        {
            RemoveData();
            Unregister(this);
        }

        public void AddData()
        {
#if UNITY_EDITOR
            var isInPreviewScene = EditorSceneManager.IsPreviewSceneObject(this);
            var isPrefab = isInPreviewScene || EditorUtility.IsPersistent(this);
            if (isPrefab)
            {
                //Debug.LogFormat("NavMeshData from {0}.{1} will not be added to the NavMesh world because the gameObject is a prefab.",
                //    gameObject.name, name);
                return;
            }
#endif
            if (_mNavMeshDataInstance.valid)
                return;

            if (mNavMeshData != null)
            {
                _mNavMeshDataInstance = NavMesh.AddNavMeshData(mNavMeshData, transform.position, transform.rotation);
                _mNavMeshDataInstance.owner = this;
            }

            _mLastPosition = transform.position;
            _mLastRotation = transform.rotation;
        }

        public void RemoveData()
        {
            _mNavMeshDataInstance.Remove();
            _mNavMeshDataInstance = new NavMeshDataInstance();
        }

        public NavMeshBuildSettings GetBuildSettings()
        {
            var buildSettings = NavMesh.GetSettingsByID(mAgentTypeID);
            if (buildSettings.agentTypeID == -1)
            {
                if (!mHideEditorLogs) Debug.LogWarning("No build settings for agent type ID " + AgentTypeID, this);
                buildSettings.agentTypeID = mAgentTypeID;
            }

            if (OverrideTileSize)
            {
                buildSettings.overrideTileSize = true;
                buildSettings.tileSize = TileSize;
            }
            if (OverrideVoxelSize)
            {
                buildSettings.overrideVoxelSize = true;
                buildSettings.voxelSize = VoxelSize;
            }
            return buildSettings;
        }

        public void BuildNavMesh()
        {
            using var builderState = new NavMeshBuilderState() { };

            var sources = CollectSources(builderState);

            // Use unscaled bounds - this differs in behaviour from e.g. collider components.
            // But is similar to reflection probe - and since navmesh data has no scaling support - it is the right choice here.
            var sourcesBounds = new Bounds(mCenter, Abs(mSize));
            if (mCollectObjects == CollectObjects.All || mCollectObjects == CollectObjects.Children)
            {
                sourcesBounds = CalculateWorldBounds(sources);
            }
            builderState.WorldBounds = sourcesBounds;
            for (int i = 0; i < NevMeshExtensions.Count; ++i)
            {
                NevMeshExtensions[i].PostCollectSources(this, sources, builderState);
            }
            var data = NavMeshBuilder.BuildNavMeshData(GetBuildSettings(),
                    sources, sourcesBounds, transform.position, transform.rotation);

            if (data != null)
            {
                data.name = gameObject.name;
                RemoveData();
                mNavMeshData = data;
                if (isActiveAndEnabled)
                    AddData();
            }
        }

        // Source: https://github.com/Unity-Technologies/NavMeshComponents/issues/97#issuecomment-528692289
        public AsyncOperation BuildNavMeshAsync()
        {
            RemoveData();
            mNavMeshData = new NavMeshData(mAgentTypeID)
            {
                name = gameObject.name,
                position = transform.position,
                rotation = transform.rotation
            };

            if (isActiveAndEnabled)
            {
                AddData();
            }

            return UpdateNavMesh(mNavMeshData);
        }

        public AsyncOperation UpdateNavMesh(NavMeshData data)
        {
            using var builderState = new NavMeshBuilderState() { };

            var sources = CollectSources(builderState);

            // Use unscaled bounds - this differs in behaviour from e.g. collider components.
            // But is similar to reflection probe - and since navmesh data has no scaling support - it is the right choice here.
            var sourcesBounds = new Bounds(mCenter, Abs(mSize));
            if (mCollectObjects == CollectObjects.All || mCollectObjects == CollectObjects.Children)
            {
                sourcesBounds = CalculateWorldBounds(sources);
            }
            builderState.WorldBounds = sourcesBounds;
            for (int i = 0; i < NevMeshExtensions.Count; ++i)
            {
                NevMeshExtensions[i].PostCollectSources(this, sources, builderState);
            }
            return NavMeshBuilder.UpdateNavMeshDataAsync(data, GetBuildSettings(), sources, sourcesBounds);
        }

        static void Register(NavMeshSurface surface)
        {
#if UNITY_EDITOR
            var isInPreviewScene = EditorSceneManager.IsPreviewSceneObject(surface);
            var isPrefab = isInPreviewScene || EditorUtility.IsPersistent(surface);
            if (isPrefab)
            {
                //Debug.LogFormat("NavMeshData from {0}.{1} will not be added to the NavMesh world because the gameObject is a prefab.",
                //    surface.gameObject.name, surface.name);
                return;
            }
#endif
            if (SNavMeshSurfaces.Count == 0)
                NavMesh.onPreUpdate += UpdateActive;

            if (!SNavMeshSurfaces.Contains(surface))
                SNavMeshSurfaces.Add(surface);
        }

        static void Unregister(NavMeshSurface surface)
        {
            SNavMeshSurfaces.Remove(surface);

            if (SNavMeshSurfaces.Count == 0)
                NavMesh.onPreUpdate -= UpdateActive;
        }

        static void UpdateActive()
        {
            for (var i = 0; i < SNavMeshSurfaces.Count; ++i)
                SNavMeshSurfaces[i].UpdateDataIfTransformChanged();
        }

        void AppendModifierVolumes(ref List<NavMeshBuildSource> sources)
        {
#if UNITY_EDITOR
            var myStage = StageUtility.GetStageHandle(gameObject);
            if (!myStage.IsValid())
                return;
#endif
            // Modifiers
            List<NavMeshModifierVolume> modifiers;
            if (mCollectObjects == CollectObjects.Children)
            {
                modifiers = new List<NavMeshModifierVolume>(GetComponentsInChildren<NavMeshModifierVolume>());
                modifiers.RemoveAll(x => !x.isActiveAndEnabled);
            }
            else
            {
                modifiers = NavMeshModifierVolume.ActiveModifiers;
            }

            foreach (var m in modifiers)
            {
                if ((mLayerMask & (1 << m.gameObject.layer)) == 0)
                    continue;
                if (!m.AffectsAgentType(mAgentTypeID))
                    continue;
#if UNITY_EDITOR
                if (!myStage.Contains(m.gameObject))
                    continue;
#endif
                var mcenter = m.transform.TransformPoint(m.Center);
                var scale = m.transform.lossyScale;
                var msize = new Vector3(m.Size.x * Mathf.Abs(scale.x), m.Size.y * Mathf.Abs(scale.y), m.Size.z * Mathf.Abs(scale.z));

                var src = new NavMeshBuildSource();
                src.shape = NavMeshBuildSourceShape.ModifierBox;
                src.transform = Matrix4x4.TRS(mcenter, m.transform.rotation, Vector3.one);
                src.size = msize;
                src.area = m.Area;
                sources.Add(src);
            }
        }

        List<NavMeshBuildSource> CollectSources(NavMeshBuilderState builderState)
        {
            var sources = new List<NavMeshBuildSource>();
            var markups = new List<NavMeshBuildMarkup>();

            List<NavMeshModifier> modifiers;
            if (mCollectObjects == CollectObjects.Children)
            {
                modifiers = new List<NavMeshModifier>(GetComponentsInChildren<NavMeshModifier>());
                modifiers.RemoveAll(x => !x.isActiveAndEnabled);
            }
            else
            {
                modifiers = NavMeshModifier.ActiveModifiers;
            }

            foreach (var m in modifiers)
            {
                if ((mLayerMask & (1 << m.gameObject.layer)) == 0)
                    continue;
                if (!m.AffectsAgentType(mAgentTypeID))
                    continue;
                var markup = new NavMeshBuildMarkup();
                markup.root = m.transform;
                markup.overrideArea = m.OverrideArea;
                markup.area = m.Area;
                markup.ignoreFromBuild = m.IgnoreFromBuild;
                markups.Add(markup);
            }

#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
            {
                if (mCollectObjects == CollectObjects.All)
                {
                    UnityEditor.AI.NavMeshBuilder.CollectSourcesInStage(
                        null, mLayerMask, mUseGeometry, mDefaultArea, markups, gameObject.scene, sources);
                }
                else if (mCollectObjects == CollectObjects.Children)
                {
                    UnityEditor.AI.NavMeshBuilder.CollectSourcesInStage(
                        transform, mLayerMask, mUseGeometry, mDefaultArea, markups, gameObject.scene, sources);
                }
                else if (mCollectObjects == CollectObjects.Volume)
                {
                    Matrix4x4 localToWorld = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
                    var worldBounds = GetWorldBounds(localToWorld, new Bounds(mCenter, mSize));

                    UnityEditor.AI.NavMeshBuilder.CollectSourcesInStage(
                        worldBounds, mLayerMask, mUseGeometry, mDefaultArea, markups, gameObject.scene, sources);
                }
                for (int i = 0; i < NevMeshExtensions.Count; ++i)
                {
                    NevMeshExtensions[i].CollectSources(this, sources, builderState);
                }
            }
            else
#endif
            {
                if (mCollectObjects == CollectObjects.All)
                {
                    NavMeshBuilder.CollectSources(null, mLayerMask, mUseGeometry, mDefaultArea, markups, sources);
                }
                else if (mCollectObjects == CollectObjects.Children)
                {
                    NavMeshBuilder.CollectSources(transform, mLayerMask, mUseGeometry, mDefaultArea, markups, sources);
                }
                else if (mCollectObjects == CollectObjects.Volume)
                {
                    Matrix4x4 localToWorld = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
                    var worldBounds = GetWorldBounds(localToWorld, new Bounds(mCenter, mSize));
                    NavMeshBuilder.CollectSources(worldBounds, mLayerMask, mUseGeometry, mDefaultArea, markups, sources);
                }
                for (int i = 0; i < NevMeshExtensions.Count; ++i)
                {
                    NevMeshExtensions[i].CollectSources(this, sources, builderState);
                }
            }

            if (mIgnoreNavMeshAgent)
                sources.RemoveAll((x) => (x.component != null && x.component.gameObject.GetComponent<NavMeshAgent>() != null));

            if (mIgnoreNavMeshObstacle)
                sources.RemoveAll((x) => (x.component != null && x.component.gameObject.GetComponent<NavMeshObstacle>() != null));

            AppendModifierVolumes(ref sources);

            return sources;
        }

        static Vector3 Abs(Vector3 v)
        {
            return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
        }

        public static Bounds GetWorldBounds(Matrix4x4 mat, Bounds bounds)
        {
            var absAxisX = Abs(mat.MultiplyVector(Vector3.right));
            var absAxisY = Abs(mat.MultiplyVector(Vector3.up));
            var absAxisZ = Abs(mat.MultiplyVector(Vector3.forward));
            var worldPosition = mat.MultiplyPoint(bounds.center);
            var worldSize = absAxisX * bounds.size.x + absAxisY * bounds.size.y + absAxisZ * bounds.size.z;
            return new Bounds(worldPosition, worldSize);
        }

        public Bounds CalculateWorldBounds(List<NavMeshBuildSource> sources)
        {
            // Use the unscaled matrix for the NavMeshSurface
            Matrix4x4 worldToLocal = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
            worldToLocal = worldToLocal.inverse;

            var result = new Bounds();
            var builderState = new NavMeshBuilderState() { WorldBounds = result, WorldToLocal = worldToLocal };
            for (int i = 0; i < NevMeshExtensions.Count; ++i)
            {
                NevMeshExtensions[i].CalculateWorldBounds(this, sources, builderState);
                result.Encapsulate(builderState.WorldBounds);
            }
            foreach (var src in sources)
            {
                switch (src.shape)
                {
                    case NavMeshBuildSourceShape.Mesh:
                    {
                        var m = src.sourceObject as Mesh;
                        result.Encapsulate(GetWorldBounds(worldToLocal * src.transform, m.bounds));
                        break;
                    }
                    case NavMeshBuildSourceShape.Terrain:
                    {
#if IS_TERRAIN_USED
                        // Terrain pivot is lower/left corner - shift bounds accordingly
                        var t = src.sourceObject as TerrainData;
                        result.Encapsulate(GetWorldBounds(worldToLocal * src.transform, new Bounds(0.5f * t.size, t.size)));
#endif
                        break;
                    }
                    case NavMeshBuildSourceShape.Box:
                    case NavMeshBuildSourceShape.Sphere:
                    case NavMeshBuildSourceShape.Capsule:
                    case NavMeshBuildSourceShape.ModifierBox:
                        result.Encapsulate(GetWorldBounds(worldToLocal * src.transform, new Bounds(Vector3.zero, src.size)));
                        break;
                }
            }
            // Inflate the bounds a bit to avoid clipping co-planar sources
            result.Expand(0.1f);
            return result;
        }

        bool HasTransformChanged()
        {
            if (_mLastPosition != transform.position) return true;
            if (_mLastRotation != transform.rotation) return true;
            return false;
        }

        void UpdateDataIfTransformChanged()
        {
            if (HasTransformChanged())
            {
                RemoveData();
                AddData();
            }
        }

#if UNITY_EDITOR
        bool UnshareNavMeshAsset()
        {
            // Nothing to unshare
            if (mNavMeshData == null)
                return false;

            // Prefab parent owns the asset reference
            var isInPreviewScene = EditorSceneManager.IsPreviewSceneObject(this);
            var isPersistentObject = EditorUtility.IsPersistent(this);
            if (isInPreviewScene || isPersistentObject)
                return false;

            // An instance can share asset reference only with its prefab parent
            var prefab = UnityEditor.PrefabUtility.GetCorrespondingObjectFromSource(this) as NavMeshSurface;
            if (prefab != null && prefab.NavMeshData == NavMeshData)
                return false;

            // Don't allow referencing an asset that's assigned to another surface
            for (var i = 0; i < SNavMeshSurfaces.Count; ++i)
            {
                var surface = SNavMeshSurfaces[i];
                if (surface != this && surface.mNavMeshData == mNavMeshData)
                    return true;
            }

            // Asset is not referenced by known surfaces
            return false;
        }

        void OnValidate()
        {
            if (UnshareNavMeshAsset())
            {
                if (!mHideEditorLogs) Debug.LogWarning("Duplicating NavMeshSurface does not duplicate the referenced navmesh data", this);
                mNavMeshData = null;
            }

            var settings = NavMesh.GetSettingsByID(mAgentTypeID);
            if (settings.agentTypeID != -1)
            {
                // When unchecking the override control, revert to automatic value.
                const float kMinVoxelSize = 0.01f;
                if (!mOverrideVoxelSize)
                    mVoxelSize = settings.agentRadius / 3.0f;
                if (mVoxelSize < kMinVoxelSize)
                    mVoxelSize = kMinVoxelSize;

                // When unchecking the override control, revert to default value.
                const int kMinTileSize = 16;
                const int kMaxTileSize = 1024;
                const int kDefaultTileSize = 256;

                if (!mOverrideTileSize)
                    mTileSize = kDefaultTileSize;
                // Make sure tilesize is in sane range.
                if (mTileSize < kMinTileSize)
                    mTileSize = kMinTileSize;
                if (mTileSize > kMaxTileSize)
                    mTileSize = kMaxTileSize;
            }
        }
#endif
    }
}
