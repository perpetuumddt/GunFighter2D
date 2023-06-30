using Plugins.NavMeshPlus_master.NavMeshComponents.Scripts;
using UnityEditor;
using UnityEditor.AI;
using UnityEditor.IMGUI.Controls;
using UnityEditorInternal;
using UnityEngine;

namespace Plugins.NavMeshPlus_master.NavMeshComponents.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(NavMeshModifierVolume))]
    class NavMeshModifierVolumeEditor : UnityEditor.Editor
    {
        SerializedProperty _mAffectedAgents;
        SerializedProperty _mArea;
        SerializedProperty _mCenter;
        SerializedProperty _mSize;

        static Color _sHandleColor = new Color(187f, 138f, 240f, 210f) / 255;
        static Color _sHandleColorDisabled = new Color(187f * 0.75f, 138f * 0.75f, 240f * 0.75f, 100f) / 255;

        BoxBoundsHandle _mBoundsHandle = new BoxBoundsHandle();

        bool EditingCollider
        {
            get { return EditMode.editMode == EditMode.SceneViewEditMode.Collider && EditMode.IsOwner(this); }
        }

        void OnEnable()
        {
            _mAffectedAgents = serializedObject.FindProperty("m_AffectedAgents");
            _mArea = serializedObject.FindProperty("m_Area");
            _mCenter = serializedObject.FindProperty("m_Center");
            _mSize = serializedObject.FindProperty("m_Size");

            NavMeshVisualizationSettings.showNavigation++;
        }

        void OnDisable()
        {
            NavMeshVisualizationSettings.showNavigation--;
        }

        Bounds GetBounds()
        {
            var navModifier = (NavMeshModifierVolume)target;
            return new Bounds(navModifier.transform.position, navModifier.Size);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditMode.DoEditModeInspectorModeButton(EditMode.SceneViewEditMode.Collider, "Edit Volume",
                EditorGUIUtility.IconContent("EditCollider"), GetBounds, this);

            EditorGUILayout.PropertyField(_mSize);
            EditorGUILayout.PropertyField(_mCenter);

            NavMeshComponentsGUIUtility.AreaPopup("Area Type", _mArea);
            NavMeshComponentsGUIUtility.AgentMaskPopup("Affected Agents", _mAffectedAgents);
            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
        }

        [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
        static void RenderBoxGizmo(NavMeshModifierVolume navModifier, GizmoType gizmoType)
        {
            var color = navModifier.enabled ? _sHandleColor : _sHandleColorDisabled;
            var colorTrans = new Color(color.r * 0.75f, color.g * 0.75f, color.b * 0.75f, color.a * 0.15f);

            var oldColor = Gizmos.color;
            var oldMatrix = Gizmos.matrix;

            Gizmos.matrix = navModifier.transform.localToWorldMatrix;

            Gizmos.color = colorTrans;
            Gizmos.DrawCube(navModifier.Center, navModifier.Size);

            Gizmos.color = color;
            Gizmos.DrawWireCube(navModifier.Center, navModifier.Size);

            Gizmos.matrix = oldMatrix;
            Gizmos.color = oldColor;

            Gizmos.DrawIcon(navModifier.transform.position, "NavMeshModifierVolume Icon", true);
        }

        [DrawGizmo(GizmoType.NotInSelectionHierarchy | GizmoType.Pickable)]
        static void RenderBoxGizmoNotSelected(NavMeshModifierVolume navModifier, GizmoType gizmoType)
        {
            if (NavMeshVisualizationSettings.showNavigation > 0)
            {
                var color = navModifier.enabled ? _sHandleColor : _sHandleColorDisabled;
                var oldColor = Gizmos.color;
                var oldMatrix = Gizmos.matrix;

                Gizmos.matrix = navModifier.transform.localToWorldMatrix;

                Gizmos.color = color;
                Gizmos.DrawWireCube(navModifier.Center, navModifier.Size);

                Gizmos.matrix = oldMatrix;
                Gizmos.color = oldColor;
            }

            Gizmos.DrawIcon(navModifier.transform.position, "NavMeshModifierVolume Icon", true);
        }

        void OnSceneGUI()
        {
            if (!EditingCollider)
                return;

            var vol = (NavMeshModifierVolume)target;
            var color = vol.enabled ? _sHandleColor : _sHandleColorDisabled;
            using (new Handles.DrawingScope(color, vol.transform.localToWorldMatrix))
            {
                _mBoundsHandle.center = vol.Center;
                _mBoundsHandle.size = vol.Size;

                EditorGUI.BeginChangeCheck();
                _mBoundsHandle.DrawHandle();
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(vol, "Modified NavMesh Modifier Volume");
                    Vector3 center = _mBoundsHandle.center;
                    Vector3 size = _mBoundsHandle.size;
                    vol.Center = center;
                    vol.Size = size;
                    EditorUtility.SetDirty(target);
                }
            }
        }

        [MenuItem("GameObject/Navigation/NavMesh Modifier Volume", false, 2001)]
        static public void CreateNavMeshModifierVolume(MenuCommand menuCommand)
        {
            var parent = menuCommand.context as GameObject;
            var go = NavMeshComponentsGUIUtility.CreateAndSelectGameObject("NavMesh Modifier Volume", parent);
            go.AddComponent<NavMeshModifierVolume>();
            var view = SceneView.lastActiveSceneView;
            if (view != null)
                view.MoveToView(go.transform);
        }
    }
}
