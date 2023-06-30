using Plugins.NavMeshPlus_master.NavMeshComponents.Scripts;
using UnityEditor;
using UnityEditor.AI;

namespace Plugins.NavMeshPlus_master.NavMeshComponents.Editor
{ 
    [CanEditMultipleObjects]
    [CustomEditor(typeof(NavMeshModifier))]
    class NavMeshModifierEditor : UnityEditor.Editor
    {
        SerializedProperty _mAffectedAgents;
        SerializedProperty _mArea;
        SerializedProperty _mIgnoreFromBuild;
        SerializedProperty _mOverrideArea;

        void OnEnable()
        {
            _mAffectedAgents = serializedObject.FindProperty("m_AffectedAgents");
            _mArea = serializedObject.FindProperty("m_Area");
            _mIgnoreFromBuild = serializedObject.FindProperty("m_IgnoreFromBuild");
            _mOverrideArea = serializedObject.FindProperty("m_OverrideArea");

            NavMeshVisualizationSettings.showNavigation++;
        }

        void OnDisable()
        {
            NavMeshVisualizationSettings.showNavigation--;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_mIgnoreFromBuild);

            EditorGUILayout.PropertyField(_mOverrideArea);
            if (_mOverrideArea.boolValue)
            {
                EditorGUI.indentLevel++;
                NavMeshComponentsGUIUtility.AreaPopup("Area Type", _mArea);
                EditorGUI.indentLevel--;
            }

            NavMeshComponentsGUIUtility.AgentMaskPopup("Affected Agents", _mAffectedAgents);
            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
