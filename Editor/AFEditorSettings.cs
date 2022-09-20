using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace AnimFlex.Editor
{
    public class AFEditorSettings : ScriptableObject
    {
	    private static string m_path = String.Empty;
        private static AFEditorSettings m_instance;
        public static AFEditorSettings Instance
        {
            get
            {
	            if(m_path == String.Empty)
		            m_path = AFEditorUtils.GetPathRelative("StyleSettings.asset");

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

#region Setting props
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
#endregion

        // refresh
        private void OnValidate() => AFStyles.Refresh();

#region ProjectSettings things

	    private static int _currentSelectedResetFileIndex;
	    private static string[] _resetPaths;
	    private static string[] _resetNames;

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

                    DrawReset();
                },
                keywords = new HashSet<string>(new[] {"animflex", "anim", "flex", "sequence" })
            };

            return provider;
        }

        private static void DrawReset()
        {
	        using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
	        {
		        if (GUILayout.Button("Reset to "))
		        {
			        ResetTo(_resetPaths[_currentSelectedResetFileIndex]);
			        AFStyles.Refresh();
		        }

		        // update list
		        if (_resetPaths == null || _resetPaths.Any(path => File.Exists(path) == false))
		        {
			        _resetPaths = Directory.GetFiles(AFEditorUtils.GetPathRelative("BuiltIn-Styles/"))
				        .Where(str => !str.EndsWith(".meta")).ToArray();
			        _resetNames = _resetPaths
				        .Select(path => ObjectNames.NicifyVariableName(Path.GetFileNameWithoutExtension(path)))
				        .ToArray();
		        }

		        _currentSelectedResetFileIndex = EditorGUILayout.Popup(_currentSelectedResetFileIndex, _resetNames);
	        }
        }

        private static void ResetTo(string filePath)
        {
	        if (!File.Exists(filePath))
	        {
		        Debug.LogError($"File at {filePath} does not exist!");
		        return;
	        }

	        var style = AssetDatabase.LoadAssetAtPath<AFEditorSettings>(filePath);
	        if (style == null)
	        {
		        Debug.LogError($"{nameof(AFEditorSettings)} not found at {filePath}");
	        }

	        foreach (var fieldInfo in Instance.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
	        {
		        fieldInfo.SetValue(Instance, fieldInfo.GetValue(style));
	        }
        }

#endregion
    }

}
