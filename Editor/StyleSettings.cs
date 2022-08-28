using System.IO;
using UnityEditor;
using UnityEngine;

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
                    var path = EditorUtils.GetPathRelative("StyleSettings.asset");
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
        public float height;
        public float verticalSpace;
        public Color buttonDefCol;
        public Color buttonYellowCol;
        public Color labelCol;
        public Color tweeerBoxCol;
        public Color onButtonCol;
        public Color offButtonCol;
        public Color popupCol;

        // refresh
        private void OnValidate() => Styles.Refresh();
    }

}