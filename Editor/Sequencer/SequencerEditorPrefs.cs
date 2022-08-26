using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.EditorPrefs
{
    internal class SequencerEditorPrefs : ScriptableObject
    {
        public Color clipNodeColor = Color.white;
        public Color clipNodeBackgroundColor = Color.white;
        
        public const string k_SequencerEditorPrefsPath = "Assets/Editor/SequencerEditorSettings.asset";

        private static SequencerEditorPrefs _instance;
        public static SequencerEditorPrefs Instance
        {
            get
            {
                if (_instance != null) return _instance;
                
                // load if possible
                _instance = AssetDatabase.LoadAssetAtPath<SequencerEditorPrefs>(k_SequencerEditorPrefsPath);
                if (_instance != null) return _instance;
                
                // create a new one
                _instance = CreateInstance<SequencerEditorPrefs>();
                if (!AssetDatabase.IsValidFolder("Assets/Editor")) // validate folder
                    AssetDatabase.CreateFolder("Assets", "Editor");
                AssetDatabase.CreateAsset(_instance, k_SequencerEditorPrefsPath);
                AssetDatabase.SaveAssets();
                return _instance;
            }
        }
        
        [SettingsProvider]
        private static SettingsProvider CreateSettingsProvider()
        {
            var provider = new SettingsProvider("AnimFlex/Sequencer", SettingsScope.Project)
            {
                label = "Sequencer",
                guiHandler = searchContext =>
                {
                    var settings = new SerializedObject(Instance);
                    var iter = settings.GetIterator();
                    iter.Next(true);
                    for (int i = 0; i < 9; i++) iter.Next(false);
                    while (iter.Next(false))
                    {
                        EditorGUILayout.PropertyField(iter, true);
                    }
                    settings.ApplyModifiedProperties();
                },
                keywords = new HashSet<string>(new[] {"animflex", "anim", "flex", "sequence" })
            };

            return provider;
        }
    }
}