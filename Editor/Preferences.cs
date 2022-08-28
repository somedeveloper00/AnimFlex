using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor
{
    public class Preferences : ScriptableObject
    {
        private static Preferences m_instance;

        public static Preferences Instance
        {
            get
            {
                if (m_instance != null) return m_instance;
                var path = EditorUtils.GetPathRelative("preferences.asset");
                m_instance = AssetDatabase.LoadAssetAtPath<Preferences>(path);
                if (m_instance == null)
                {
                    // create
                    m_instance = CreateInstance<Preferences>();
                    AssetDatabase.CreateAsset(m_instance, path);
                    AssetDatabase.Refresh();
                }

                return m_instance;
            }
        }
        public bool showQuaternionWarnings = true;
    }
}