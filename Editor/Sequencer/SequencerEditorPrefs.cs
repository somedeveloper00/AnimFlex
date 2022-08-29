using System.Collections.Generic;
using AnimFlex.Editor;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.EditorPrefs
{
    internal class SequencerEditorPrefs : ScriptableObject
    {
        public Color clipNodeColor = Color.white;
        public Color clipNodeBackgroundColor = Color.white;
        
        private static SequencerEditorPrefs _instance;
        public static SequencerEditorPrefs Instance
        {
            get
            {
                if (_instance != null) return _instance;
                
                // load if possible
                _instance = AssetDatabase.LoadAssetAtPath<SequencerEditorPrefs>(AFEditorUtils.GetPathRelative("SequencerEditorSettings.asset"));
                if (_instance != null) return _instance;
                
                // create a new one
                _instance = CreateInstance<SequencerEditorPrefs>();
                if (!AssetDatabase.IsValidFolder("Assets/Editor")) // validate folder
                    AssetDatabase.CreateFolder("Assets", "Editor");
                AssetDatabase.CreateAsset(_instance, AFEditorUtils.GetPathRelative("SequencerEditorSettings.asset"));
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
                    UnityEditor.Editor editor = null;
                    UnityEditor.Editor.CreateCachedEditor(Instance, null, ref editor);
                    editor.OnInspectorGUI();
                },
                keywords = new HashSet<string>(new[] {"animflex", "anim", "flex", "sequence" })
            };

            return provider;
        }
    }
}