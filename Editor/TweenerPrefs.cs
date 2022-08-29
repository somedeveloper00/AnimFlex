using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor
{
    public class TweenerPrefs : ScriptableObject
    {
        private static TweenerPrefs m_instance;

        public static TweenerPrefs Instance
        {
            get
            {
                if (m_instance != null) return m_instance;
                var path = AFEditorUtils.GetPathRelative("preferences.asset");
                m_instance = AssetDatabase.LoadAssetAtPath<TweenerPrefs>(path);
                if (m_instance == null)
                {
                    // create
                    m_instance = CreateInstance<TweenerPrefs>();
                    AssetDatabase.CreateAsset(m_instance, path);
                    AssetDatabase.Refresh();
                }

                return m_instance;
            }
        }
        public bool showQuaternionWarnings = true;
        
        [SettingsProvider]
        private static SettingsProvider CreateSettingsProvider()
        {
            var provider = new SettingsProvider("AnimFlex/Tweener", SettingsScope.Project)
            {
                label = "Tweener",
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