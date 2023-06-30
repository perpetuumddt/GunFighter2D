using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace Plugins.NavMeshPlus_master.NavMeshComponents.Scripts
{
    class NavMeshBuilder2dState: IDisposable
    {
        public Dictionary<Sprite, Mesh> SpriteMeshMap;
        public Dictionary<uint, Mesh> ColiderMeshMap;
        public Action<UnityEngine.Object, NavMeshBuildSource> LookupCallback;
        public int DefaultArea;
        public int LayerMask;
        public int AgentID;
        public bool OverrideByGrid;
        public GameObject UseMeshPrefab;
        public bool CompressBounds;
        public Vector3 OverrideVector;
        public NavMeshCollectGeometry CollectGeometry;
        public CollectObjects CollectObjects;
        public GameObject Parent;
        public bool HideEditorLogs;
        
        protected IEnumerable<GameObject> _root;
        private bool _disposed;

        public IEnumerable<GameObject> Root => _root ?? GetRoot();

        public NavMeshBuilder2dState()
        {
            SpriteMeshMap = new Dictionary<Sprite, Mesh>();
            ColiderMeshMap = new Dictionary<uint, Mesh>();
            _root = null;
        }

        public Mesh GetMesh(Sprite sprite)
        {
            Mesh mesh;
            if (SpriteMeshMap.ContainsKey(sprite))
            {
                mesh = SpriteMeshMap[sprite];
            }
            else
            {
                mesh = new Mesh();
                NavMeshBuilder2d.Sprite2Mesh(sprite, mesh);
                SpriteMeshMap.Add(sprite, mesh);
            }
            return mesh;
        }

        public Mesh GetMesh(Collider2D collider)
        {
#if UNITY_2019_3_OR_NEWER
            Mesh mesh;
            uint hash = collider.GetShapeHash();
            if (ColiderMeshMap.ContainsKey(hash))
            {
                mesh = ColiderMeshMap[hash];
            }
            else
            {
                mesh = collider.CreateMesh(false, false);
                ColiderMeshMap.Add(hash, mesh);
            }
            return mesh;
#else
            throw new InvalidOperationException("PhysicsColliders supported in Unity 2019.3 and higher.");
#endif
        }
        public void SetRoot(IEnumerable<GameObject> root)
        {
            _root = root;
        }
        public IEnumerable<GameObject> GetRoot()
        {
            switch (CollectObjects)
            {
                case CollectObjects.Children: return new[] { Parent };
                case CollectObjects.Volume:
                case CollectObjects.All:
                default:
                    {
                        var list = new List<GameObject>();
                        var roots = new List<GameObject>();
                        for (int i = 0; i < SceneManager.sceneCount; ++i)
                        {
                            var s = SceneManager.GetSceneAt(i);
                            if (!s.isLoaded) continue;
                            s.GetRootGameObjects(list);
                            roots.AddRange(list);
                        }
                        return roots;
                    }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
                foreach (var item in SpriteMeshMap)
                {
#if UNITY_EDITOR
                    Object.DestroyImmediate(item.Value);
#else 
                    Object.Destroy(item.Value);
#endif
                }
                foreach (var item in ColiderMeshMap)
                {
#if UNITY_EDITOR
                    Object.DestroyImmediate(item.Value);
#else
                    Object.Destroy(item.Value);
#endif
                }
                SpriteMeshMap.Clear();
                ColiderMeshMap.Clear();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            _disposed = true;
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }

    class NavMeshBuilder2d
    {
        public static void CollectSources(List<NavMeshBuildSource> sources, NavMeshBuilder2dState builder)
        {
            foreach (var it in builder.Root)
            {
                CollectSources(it, sources, builder);
            }
            if (!builder.HideEditorLogs) Debug.Log("Sources " + sources.Count);
        }

        public static void CollectSources(GameObject root, List<NavMeshBuildSource> sources, NavMeshBuilder2dState builder)
        {
            foreach (var modifier in root.GetComponentsInChildren<NavMeshModifier>())
            {
                if (((0x1 << modifier.gameObject.layer) & builder.LayerMask) == 0)
                {
                    continue;
                }
                if (!modifier.AffectsAgentType(builder.AgentID))
                {
                    continue;
                }
                int area = builder.DefaultArea;
                //if it is walkable
                if (builder.DefaultArea != 1 && !modifier.IgnoreFromBuild)
                {
                    AddDefaultWalkableTilemap(sources, builder, modifier);
                }

                if (modifier.OverrideArea)
                {
                    area = modifier.Area;
                }
                if (!modifier.IgnoreFromBuild)
                {
                    CollectSources(sources, builder, modifier, area);
                }
            }
        }

        public static void CollectSources(List<NavMeshBuildSource> sources, NavMeshBuilder2dState builder, NavMeshModifier modifier, int area)
        {
            if (builder.CollectGeometry == NavMeshCollectGeometry.PhysicsColliders)
            {
                var collider = modifier.GetComponent<Collider2D>();
                if (collider != null)
                {
                    CollectSources(sources, collider, area, builder);
                }
            }
            else
            {
                var tilemap = modifier.GetComponent<Tilemap>();
                if (tilemap != null)
                {
                    CollectTileSources(sources, tilemap, area, builder);
                }
                var sprite = modifier.GetComponent<SpriteRenderer>();
                if (sprite != null)
                {
                    CollectSources(sources, sprite, area, builder);
                }
            }
        }

        private static void AddDefaultWalkableTilemap(List<NavMeshBuildSource> sources, NavMeshBuilder2dState builder, NavMeshModifier modifier)
        {
            var tilemap = modifier.GetComponent<Tilemap>();
            if (tilemap != null)
            {
                if (builder.CompressBounds)
                {
                    tilemap.CompressBounds();
                }

                if (!builder.HideEditorLogs) Debug.Log($"Walkable Bounds [{tilemap.name}]: {tilemap.localBounds}");
                var box = BoxBoundSource(NavMeshSurface.GetWorldBounds(tilemap.transform.localToWorldMatrix, tilemap.localBounds));
                box.area = builder.DefaultArea;
                sources.Add(box);
            }
        }

        public static void CollectSources(List<NavMeshBuildSource> sources, SpriteRenderer spriteRenderer, int area, NavMeshBuilder2dState builder)
        {
            if (spriteRenderer == null)
            {
                return;
            }
            Mesh mesh;
            mesh = builder.GetMesh(spriteRenderer.sprite);
            if (mesh == null)
            {
                if (!builder.HideEditorLogs) Debug.Log($"{spriteRenderer.name} mesh is null");
                return;
            }
            var src = new NavMeshBuildSource();
            src.shape = NavMeshBuildSourceShape.Mesh;
            src.component = spriteRenderer;
            src.area = area;
            src.transform = Matrix4x4.TRS(Vector3.Scale(spriteRenderer.transform.position, builder.OverrideVector), spriteRenderer.transform.rotation, spriteRenderer.transform.lossyScale);
            src.sourceObject = mesh;
            sources.Add(src);

            builder.LookupCallback?.Invoke(spriteRenderer.gameObject, src);
        }

        public static void CollectSources(List<NavMeshBuildSource> sources, Collider2D collider, int area, NavMeshBuilder2dState builder)
        { 
            if (collider.usedByComposite)
            {
                collider = collider.GetComponent<CompositeCollider2D>();
            }

            Mesh mesh;
            mesh = builder.GetMesh(collider);
            if (mesh == null)
            {
                if (!builder.HideEditorLogs) Debug.Log($"{collider.name} mesh is null");
                return;
            }

            var src = new NavMeshBuildSource();
            src.shape = NavMeshBuildSourceShape.Mesh;
            src.area = area;
            src.component = collider;
            src.sourceObject = mesh;
            if (collider.attachedRigidbody)
            {
                src.transform = Matrix4x4.TRS(Vector3.Scale(collider.attachedRigidbody.transform.position, builder.OverrideVector), collider.attachedRigidbody.transform.rotation, Vector3.one);
            }
            else
            {
                src.transform = Matrix4x4.identity;
            }

            sources.Add(src);

            builder.LookupCallback?.Invoke(collider.gameObject, src);
        }

        public static void CollectTileSources(List<NavMeshBuildSource> sources, Tilemap tilemap, int area, NavMeshBuilder2dState builder)
        {
            var bound = tilemap.cellBounds;

            var vec3INT = new Vector3Int(0, 0, 0);

            var size = new Vector3(tilemap.layoutGrid.cellSize.x, tilemap.layoutGrid.cellSize.y, 0);
            Mesh sharedMesh = null;
            Quaternion rot = default;

            var src = new NavMeshBuildSource();
            src.area = area;

            if (builder.UseMeshPrefab != null)
            {
                sharedMesh = builder.UseMeshPrefab.GetComponent<MeshFilter>().sharedMesh;
                size = builder.UseMeshPrefab.transform.localScale;
                rot = builder.UseMeshPrefab.transform.rotation;
            }
            for (int i = bound.xMin; i < bound.xMax; i++)
            {
                for (int j = bound.yMin; j < bound.yMax; j++)
                {
                    vec3INT.x = i;
                    vec3INT.y = j;
                    if (!tilemap.HasTile(vec3INT))
                    {
                        continue;
                    }

                    CollectTile(tilemap, builder, vec3INT, size, sharedMesh, rot, ref src);
                    sources.Add(src);

                    builder.LookupCallback?.Invoke(tilemap.GetInstantiatedObject(vec3INT), src);
                }
            }
        }

        private static void CollectTile(Tilemap tilemap, NavMeshBuilder2dState builder, Vector3Int vec3INT, Vector3 size, Mesh sharedMesh, Quaternion rot, ref NavMeshBuildSource src)
        {
            if (!builder.OverrideByGrid && tilemap.GetColliderType(vec3INT) == Tile.ColliderType.Sprite)
            {
                var sprite = tilemap.GetSprite(vec3INT);
                if (sprite != null)
                {
                    Mesh mesh = builder.GetMesh(sprite);
                    src.component = tilemap;
                    src.transform = GetCellTransformMatrix(tilemap, builder.OverrideVector, vec3INT);
                    src.shape = NavMeshBuildSourceShape.Mesh;
                    src.sourceObject = mesh;
                }
            }
            else if (builder.UseMeshPrefab != null || (builder.OverrideByGrid && builder.UseMeshPrefab != null))
            {
                src.transform = Matrix4x4.TRS(Vector3.Scale(tilemap.GetCellCenterWorld(vec3INT), builder.OverrideVector), rot, size);
                src.shape = NavMeshBuildSourceShape.Mesh;
                src.sourceObject = sharedMesh;
            }
            else //default to box
            {
                src.transform = GetCellTransformMatrix(tilemap, builder.OverrideVector, vec3INT);
                src.shape = NavMeshBuildSourceShape.Box;
                src.size = size;
            }
        }

        public static Matrix4x4 GetCellTransformMatrix(Tilemap tilemap, Vector3 scale, Vector3Int vec3INT)
        {
            return Matrix4x4.TRS(Vector3.Scale(tilemap.GetCellCenterWorld(vec3INT), scale) - tilemap.layoutGrid.cellGap, tilemap.transform.rotation, tilemap.transform.lossyScale) * tilemap.orientationMatrix * tilemap.GetTransformMatrix(vec3INT);
        }

        internal static void Sprite2Mesh(Sprite sprite, Mesh mesh)
        {
            Vector3[] vert = new Vector3[sprite.vertices.Length];
            for (int i = 0; i < sprite.vertices.Length; i++)
            {
                vert[i] = new Vector3(sprite.vertices[i].x, sprite.vertices[i].y, 0);
            }
            mesh.vertices = vert;
            mesh.uv = sprite.uv;
            int[] tri = new int[sprite.triangles.Length];
            for (int i = 0; i < sprite.triangles.Length; i++)
            {
                tri[i] = sprite.triangles[i];
            }
            mesh.triangles = tri;
        }

        static private NavMeshBuildSource BoxBoundSource(Bounds localBounds)
        {
            var src = new NavMeshBuildSource();
            src.transform = Matrix4x4.Translate(localBounds.center);
            src.shape = NavMeshBuildSourceShape.Box;
            src.size = localBounds.size;
            src.area = 0;
            return src;
        }
    }
}
