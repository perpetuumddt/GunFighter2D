#nullable enable


using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Plugins.CodeAssist.Editor
{
    public class StatusWindow : EditorWindow
    {
        GUIStyle? _styleLabel;

        public static void Display()
        {
            // Get existing open window or if none, make a new one:
            var window = GetWindow<StatusWindow>();
            window.Show();

            NetMqInitializer.Publisher?.SendConnectionInfo();

            Serilog.Log.Debug("Displaying status window");

            NetMqInitializer.Publisher?.SendAnalyticsEvent("Gui", "StatusWindow_Display");
        }

        private void OnEnable()
        {
            //**--icon
            //var icon = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Sprites/Gear.png");
            //titleContent = new GUIContent("Code Assist", icon);
            titleContent = new GUIContent(Assister.Title);
        }

        private void OnGUI()
        {
            var hasAnyClient = NetMqInitializer.Publisher?.Clients.Any() == true;

            _styleLabel ??= new GUIStyle(GUI.skin.label)
            {
                wordWrap = true,
                alignment = TextAnchor.MiddleLeft,
            };

            if (hasAnyClient)
            {
                EditorGUILayout.LabelField($"Code Assist is working!", _styleLabel, GUILayout.ExpandWidth(true));

                foreach (var client in NetMqInitializer.Publisher!.Clients)
                {
                    EditorGUILayout.LabelField($"Connected to {client.ContactInfo}", _styleLabel, GUILayout.ExpandWidth(true));
                }
            }
            else
            {
                EditorGUILayout.LabelField($"Code Assist isn't working!", _styleLabel, GUILayout.ExpandWidth(true));

                EditorGUILayout.LabelField($"No IDE found", _styleLabel, GUILayout.ExpandWidth(true));
            }

#if MERYEL_UCA_LITE_VERSION

            EditorGUILayout.LabelField($"", _styleLabel, GUILayout.ExpandWidth(true));
            EditorGUILayout.LabelField($"This is the lite version of Code Assist with limited features.", _styleLabel, GUILayout.ExpandWidth(true));
            EditorGUILayout.LabelField($"To unlock all of the features, get the full version.", _styleLabel, GUILayout.ExpandWidth(true));

            if (GUILayout.Button("Get full version"))
            {
                Application.OpenURL("http://u3d.as/2N2H");
            }

#endif // MERYEL_UCA_LITE_VERSION


        }
    }

}