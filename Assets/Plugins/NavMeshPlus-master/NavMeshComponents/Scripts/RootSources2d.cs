using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Plugins.NavMeshPlus_master.NavMeshComponents.Scripts
{
    [ExecuteAlways]
    [AddComponentMenu("Navigation/NavMesh RootSources2d", 30)]
    public class RootSources2d: NavMeshExtension
    {
        [FormerlySerializedAs("_rootSources")] [SerializeField]
        private List<GameObject> rootSources;

        public List<GameObject> RooySources { get => rootSources; set => rootSources = value; }

        protected override void Awake()
        {
            Order = -1000;
            base.Awake();
        }

        public override void CollectSources(NavMeshSurface surface, List<NavMeshBuildSource> sources, NavMeshBuilderState navNeshState)
        {
            navNeshState.Roots = rootSources;
        }
    }
}
