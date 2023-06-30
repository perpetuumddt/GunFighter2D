using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Plugins.NavMeshPlus_master.NavMeshComponents.Scripts
{
    [ExecuteInEditMode]
    [AddComponentMenu("Navigation/Navigation ModifierVolume", 31)]
    [HelpURL("https://github.com/Unity-Technologies/NavMeshComponents#documentation-draft")]
    public class NavMeshModifierVolume : MonoBehaviour
    {
        [FormerlySerializedAs("m_Size")] [SerializeField]
        Vector3 mSize = new Vector3(4.0f, 3.0f, 4.0f);
        public Vector3 Size { get { return mSize; } set { mSize = value; } }

        [FormerlySerializedAs("m_Center")] [SerializeField]
        Vector3 mCenter = new Vector3(0, 1.0f, 0);
        public Vector3 Center { get { return mCenter; } set { mCenter = value; } }

        [FormerlySerializedAs("m_Area")] [SerializeField]
        int mArea;
        public int Area { get { return mArea; } set { mArea = value; } }

        // List of agent types the modifier is applied for.
        // Special values: empty == None, m_AffectedAgents[0] =-1 == All.
        [FormerlySerializedAs("m_AffectedAgents")] [SerializeField]
        List<int> mAffectedAgents = new List<int>(new int[] { -1 });    // Default value is All

        static readonly List<NavMeshModifierVolume> SNavMeshModifiers = new List<NavMeshModifierVolume>();

        public static List<NavMeshModifierVolume> ActiveModifiers
        {
            get { return SNavMeshModifiers; }
        }

        void OnEnable()
        {
            if (!SNavMeshModifiers.Contains(this))
                SNavMeshModifiers.Add(this);
        }

        void OnDisable()
        {
            SNavMeshModifiers.Remove(this);
        }

        public bool AffectsAgentType(int agentTypeID)
        {
            if (mAffectedAgents.Count == 0)
                return false;
            if (mAffectedAgents[0] == -1)
                return true;
            return mAffectedAgents.IndexOf(agentTypeID) != -1;
        }
    }
}
