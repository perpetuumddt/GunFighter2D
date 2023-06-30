using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Plugins.NavMeshPlus_master.NavMeshComponents.Scripts
{
    [ExecuteAlways]
    [AddComponentMenu("Navigation/NavMesh RootSources2d", 30)]
    public class RootSources2d: NavMeshExtension
    {
        [SerializeField]
        private List<GameObject> _rootSources;

        public List<GameObject> RooySources { get => _rootSources; set => _rootSources = value; }

        protected override void Awake()
        {
            Order = -1000;
            base.Awake();
        }

        public override void CollectSources(NavMeshSurface surface, List<NavMeshBuildSource> sources, NavMeshBuilderState navNeshState)
        {
            navNeshState.roots = _rootSources;
        }
    }
}
