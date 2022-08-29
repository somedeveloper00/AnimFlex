using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace AnimFlex.Editor
{
    public class StyleSettings : ScriptableObject
    {
        private static StyleSettings m_instance; 
        public static StyleSettings Instance
        {
            get
            {
                if (m_instance == null)
                {
                    var path = AFEditorUtils.GetPathRelative("StyleSettings.asset");
                    if (!File.Exists(path))
                    {
                        m_instance = CreateInstance<StyleSettings>();
                        m_instance.name = "StyleSettings";
                        AssetDatabase.CreateAsset(m_instance, path);
                        AssetDatabase.Refresh();
                    }
                    m_instance = AssetDatabase.LoadAssetAtPath<StyleSettings>(path);
                }
                return m_instance;
            }
        }

        public Font font;
        public int fontSize;
        public int bigFontSize;
        public float bigHeight;
        public float height;
        public float verticalSpace;
        public Color buttonDefCol;
        public Color buttonYellowCol;
        public Color labelCol;
        [FormerlySerializedAs("tweeerBoxCol")] public Color BoxCol;
        [FormerlySerializedAs("tweeerBoxCol")] public Color BoxColDarker;
        public Color backgroundBoxCol;
        public Color popupCol;

        // refresh
        private void OnValidate() => AFStyles.Refresh();
        
        [SettingsProvider]
        private static SettingsProvider CreateSettingsProvider() 
        {
            var provider = new SettingsProvider("AnimFlex/General", SettingsScope.Project)
            {
                label = "General",
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