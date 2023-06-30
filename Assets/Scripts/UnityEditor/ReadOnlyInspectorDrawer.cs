using UnityEditor;
using UnityEngine;

namespace Gunfighter.UnityEditor
{
    [CustomPropertyDrawer(typeof(ReadOnlyInspectorAttribute))]

    public class ReadOnlyInspectorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position,property,label);
            GUI.enabled = true;
        }
    }
}
