using UnityEditor;
using UnityEngine;

namespace AnimFlex.Clipper.Editor
{
    internal class ClipSequencerEditorPrefs : ScriptableObject
    {
        private const string ClipSequenceEditorPrefsPath = "Assets/AnimFlex/Clipper/Editor/ClipSequencerEditorPrefs.asset";
        public Color clipNodeColor = Color.white;
        public Color clipNodeBackgroundColor = Color.white;
        public Color clipColor = Color.white;
        public Color clipBackgroundColor = Color.white;

        internal static ClipSequencerEditorPrefs GetOrCreatePrefs()
        {
            var settings = AssetDatabase.LoadAssetAtPath<ClipSequencerEditorPrefs>(ClipSequenceEditorPrefsPath);
            if (settings == null)
            {
                settings = CreateInstance<ClipSequencerEditorPrefs>();
                AssetDatabase.CreateAsset(settings, ClipSequenceEditorPrefsPath);
                AssetDatabase.SaveAssets();
            }

            return settings;
        }

        [SettingsProvider]
        public static SettingsProvider PreferenceGUI()
        {
            var provider = new SettingsProvider("Preferences/Clipper", SettingsScope.User)
            {
                guiHandler = searchContext =>
                {
                    var prefs = new SerializedObject(GetOrCreatePrefs());
                    EditorGUILayout.PropertyField(prefs.FindProperty(nameof(clipColor)));
                    EditorGUILayout.PropertyField(prefs.FindProperty(nameof(clipBackgroundColor)));
                    EditorGUILayout.PropertyField(prefs.FindProperty(nameof(clipNodeColor)));
                    EditorGUILayout.PropertyField(prefs.FindProperty(nameof(clipNodeBackgroundColor)));
                    prefs.ApplyModifiedProperties();
                },
                keywords = new[] { "Clip", "Color" }
            };
            return provider;
        }
    }
}