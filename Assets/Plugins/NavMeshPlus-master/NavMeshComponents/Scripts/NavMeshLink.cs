using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Plugins.NavMeshPlus_master.NavMeshComponents.Scripts
{
    [ExecuteInEditMode]
    [DefaultExecutionOrder(-101)]
    [AddComponentMenu("Navigation/Navigation Link", 33)]
    [HelpURL("https://github.com/Unity-Technologies/NavMeshPlus#documentation-draft")]
    public class NavMeshLink : MonoBehaviour
    {
        [FormerlySerializedAs("m_AgentTypeID")] [SerializeField]
        int mAgentTypeID;
        public int AgentTypeID { get { return mAgentTypeID; } set { mAgentTypeID = value; UpdateLink(); } }

        [FormerlySerializedAs("m_StartPoint")] [SerializeField]
        Vector3 mStartPoint = new Vector3(0.0f, 0.0f, -2.5f);
        public Vector3 StartPoint { get { return mStartPoint; } set { mStartPoint = value; UpdateLink(); } }

        [FormerlySerializedAs("m_EndPoint")] [SerializeField]
        Vector3 mEndPoint = new Vector3(0.0f, 0.0f, 2.5f);
        public Vector3 EndPoint { get { return mEndPoint; } set { mEndPoint = value; UpdateLink(); } }

        [FormerlySerializedAs("m_Width")] [SerializeField]
        float mWidth;
        public float Width { get { return mWidth; } set { mWidth = value; UpdateLink(); } }

        [FormerlySerializedAs("m_CostModifier")] [SerializeField]
        int mCostModifier = -1;
        public int CostModifier { get { return mCostModifier; } set { mCostModifier = value; UpdateLink(); } }

        [FormerlySerializedAs("m_Bidirectional")] [SerializeField]
        bool mBidirectional = true;
        public bool Bidirectional { get { return mBidirectional; } set { mBidirectional = value; UpdateLink(); } }

        [FormerlySerializedAs("m_AutoUpdatePosition")] [SerializeField]
        bool mAutoUpdatePosition;
        public bool AutoUpdate { get { return mAutoUpdatePosition; } set { SetAutoUpdate(value); } }

        [FormerlySerializedAs("m_Area")] [SerializeField]
        int mArea;
        public int Area { get { return mArea; } set { mArea = value; UpdateLink(); } }

        NavMeshLinkInstance _mLinkInstance = new NavMeshLinkInstance();

        Vector3 _mLastPosition = Vector3.zero;
        Quaternion _mLastRotation = Quaternion.identity;

        static readonly List<NavMeshLink> STracked = new List<NavMeshLink>();

        void OnEnable()
        {
            AddLink();
            if (mAutoUpdatePosition && _mLinkInstance.valid)
                AddTracking(this);
        }

        void OnDisable()
        {
            RemoveTracking(this);
            _mLinkInstance.Remove();
        }

        public void UpdateLink()
        {
            _mLinkInstance.Remove();
            AddLink();
        }

        static void AddTracking(NavMeshLink link)
        {
#if UNITY_EDITOR
            if (STracked.Contains(link))
            {
                Debug.LogError("Link is already tracked: " + link);
                return;
            }
#endif

            if (STracked.Count == 0)
                NavMesh.onPreUpdate += UpdateTrackedInstances;

            STracked.Add(link);
        }

        static void RemoveTracking(NavMeshLink link)
        {
            STracked.Remove(link);

            if (STracked.Count == 0)
                NavMesh.onPreUpdate -= UpdateTrackedInstances;
        }

        void SetAutoUpdate(bool value)
        {
            if (mAutoUpdatePosition == value)
                return;
            mAutoUpdatePosition = value;
            if (value)
                AddTracking(this);
            else
                RemoveTracking(this);
        }

        void AddLink()
        {
#if UNITY_EDITOR
            if (_mLinkInstance.valid)
            {
                Debug.LogError("Link is already added: " + this);
                return;
            }
#endif

            var link = new NavMeshLinkData();
            link.startPosition = mStartPoint;
            link.endPosition = mEndPoint;
            link.width = mWidth;
            link.costModifier = mCostModifier;
            link.bidirectional = mBidirectional;
            link.area = mArea;
            link.agentTypeID = mAgentTypeID;
            _mLinkInstance = NavMesh.AddLink(link, transform.position, transform.rotation);
            if (_mLinkInstance.valid)
                _mLinkInstance.owner = this;

            _mLastPosition = transform.position;
            _mLastRotation = transform.rotation;
        }

        bool HasTransformChanged()
        {
            if (_mLastPosition != transform.position) return true;
            if (_mLastRotation != transform.rotation) return true;
            return false;
        }

        void OnDidApplyAnimationProperties()
        {
            UpdateLink();
        }

        static void UpdateTrackedInstances()
        {
            foreach (var instance in STracked)
            {
                if (instance.HasTransformChanged())
                    instance.UpdateLink();
            }
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            mWidth = Mathf.Max(0.0f, mWidth);

            if (!_mLinkInstance.valid)
                return;

            UpdateLink();

            if (!mAutoUpdatePosition)
            {
                RemoveTracking(this);
            }
            else if (!STracked.Contains(this))
            {
                AddTracking(this);
            }
        }
#endif
    }
}
