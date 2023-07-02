using Gunfighter.Runtime.ScriptableObjects.Data.Character.Player;
using UnityEditor;
using UnityEngine;

namespace Gunfighter.Editor
{
    [CustomEditor(typeof(PlayerData))]
    public class PlayerDataEditor : global::UnityEditor.Editor
    {
        private Vector2 _scroll;
    
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            PlayerData data = (PlayerData)target;

            _scroll = EditorGUILayout.BeginScrollView(_scroll, GUILayout.MaxHeight(300));

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
