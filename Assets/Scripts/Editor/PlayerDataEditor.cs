using ScriptableObjects.Data.Character.Player;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(PlayerData))]
    public class PlayerDataEditor : UnityEditor.Editor
    {
        private Vector2 scroll;
    
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            PlayerData data = (PlayerData)target;

            scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(300));

            for (int i = 1; i < 25; i++)
            {
                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("Level " + i);
                EditorGUILayout.LabelField((int)data.ExperienceLevelDistribution.Evaluate(i) + "Xp");
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
        }
    }
}
