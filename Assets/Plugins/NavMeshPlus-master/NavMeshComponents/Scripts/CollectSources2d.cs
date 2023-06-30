using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace Plugins.NavMeshPlus_master.NavMeshComponents.Scripts
{
    [ExecuteAlways]
    [AddComponentMenu("Navigation/NavMesh CollectSources2d", 30)]
    public class CollectSources2d: NavMeshExtension
    {
        [FormerlySerializedAs("m_OverrideByGrid")] [SerializeField]
        bool mOverrideByGrid;
        public bool OverrideByGrid { get { return mOverrideByGrid; } set { mOverrideByGrid = value; } }

        [FormerlySerializedAs("m_UseMeshPrefab")] [SerializeField]
        GameObject mUseMeshPrefab;
        public GameObject UseMeshPrefab { get { return mUseMeshPrefab; } set { mUseMeshPrefab = value; } }

        [FormerlySerializedAs("m_CompressBounds")] [SerializeField]
        bool mCompressBounds;
        public bool CompressBounds { get { return mCompressBounds; } set { mCompressBounds = value; } }

        [FormerlySerializedAs("m_OverrideVector")] [SerializeField]
        Vector3 mOverrideVector = Vector3.one;
        public Vector3 OverrideVector { get { return mOverrideVector; } set { mOverrideVector = value; } }

        public override void CalculateWorldBounds(NavMeshSurface surface, List<NavMeshBuildSource> sources, NavMeshBuilderState navNeshState)
        {
            if (surface.CollectObjects != CollectObjects.Volume)
            {
                navNeshState.WorldBounds.Encapsulate(CalculateGridWorldBounds(surface, navNeshState.WorldToLocal, navNeshState.WorldBounds));
            }
        }

        private static Bounds CalculateGridWorldBounds(NavMeshSurface surface, Matrix4x4 worldToLocal, Bounds bounds)
        {
            var grid = FindObjectOfType<Grid>();
            var tilemaps = grid?.GetComponentsInChildren<Tilemap>();
            if (tilemaps == null || tilemaps.Length < 1)
            {
                return bounds;
            }
            foreach (var tilemap in tilemaps)
            {
                var lbounds = NavMeshSurface.GetWorldBounds(worldToLocal * tilemap.transform.localToWorldMatrix, tilemap.localBounds);
                bounds.Encapsulate(lbounds);
                if (!surface.HideEditorLogs)
                {
                    Debug.Log($"From Local Bounds [{tilemap.name}]: {tilemap.localBounds}");
                    Debug.Log($"To World Bounds: {bounds}");
                }
            }
            return bounds;
        }

        public override void CollectSources(NavMeshSurface surface, List<NavMeshBuildSource> sources, NavMeshBuilderState navNeshState)
        {
            if (!surface.HideEditorLogs)
            {
                if (!Mathf.Approximately(transform.eulerAngles.x, 270f))
                {
                    Debug.LogWarning("NavMeshSurface is not rotated respectively to (x-90;y0;z0). Apply rotation unless intended.");
                }
                if (Application.isPlaying)
                {
                    if (surface.UseGeometry == NavMeshCollectGeometry.PhysicsColliders && Time.frameCount <= 1)
                    {
                        Debug.LogWarning("Use Geometry - Physics Colliders option in NavMeshSurface may cause inaccurate mesh bake if executed before Physics update.");
                    }
                }
            }
            var builder = navNeshState.GetExtraState<NavMeshBuilder2dState>();
            builder.DefaultArea = surface.DefaultArea;
            builder.LayerMask = surface.LayerMask;
            builder.AgentID = surface.AgentTypeID;
            builder.UseMeshPrefab = UseMeshPrefab;
            builder.OverrideByGrid = OverrideByGrid;
            builder.CompressBounds = CompressBounds;
            builder.OverrideVector = OverrideVector;
            builder.CollectGeometry = surface.UseGeometry;
            builder.CollectObjects = (CollectObjects)(int)surface.CollectObjects;
            builder.Parent = surface.gameObject;
            builder.HideEditorLogs = surface.HideEditorLogs;
            builder.SetRoot(navNeshState.Roots);
            NavMeshBuilder2d.CollectSources(sources, builder);
        }
    }
}
