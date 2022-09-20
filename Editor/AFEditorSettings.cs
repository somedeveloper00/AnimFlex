using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace AnimFlex.Editor
{
    public class AFEditorSettings : ScriptableObject
    {
	    private static string m_path;
        private static AFEditorSettings m_instance;
        public static AFEditorSettings Instance
        {
            get
            {
                if (!File.Exists(m_path))
                {
                    m_instance = CreateInstance<AFEditorSettings>();
                    m_instance.name = "StyleSettings";
                    AssetDatabase.CreateAsset(m_instance, m_path);
                    AssetDatabase.Refresh();

	                Debug.LogWarning($"AnimFlex settings file not found! Created a new one at {m_path}.\n" +
	                                 $"You should make sure not to delete the .ind file in Editor/Resource subfolder of AnimFlex root.");
                }

                if (m_instance == null)
                {
                    m_instance = AssetDatabase.LoadAssetAtPath<AFEditorSettings>(m_path);
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
        public Color labelCol_Hover;
        [FormerlySerializedAs("tweeerBoxCol")] public Color BoxCol;
        [FormerlySerializedAs("tweeerBoxCol")] public Color BoxColDarker;
        public Color backgroundBoxCol;
        public Color backgroundBoxColDarker;
        public Color popupCol;
        public bool repaintEveryFrame = true;


        private void OnEnable()
        {
	        m_path = AFEditorUtils.GetPathRelative("StyleSettings.asset");
        }

        // refresh
        private void OnValidate() => AFStyles.Refresh();

        [SettingsProvider]
        private static SettingsProvider CreateSettingsProvider()
        {
            var provider = new SettingsProvider("AnimFlex/Editor", SettingsScope.Project)
            {
                label = "AnimFlex Editor Settings",
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
