using AnimFlex.Sequencer;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;

namespace AnimFlex.Editor.Sequencer
{
    [CustomPropertyDrawer(typeof(ClipNode))]
    public class ClipNodeEditor : PropertyDrawer
    {
        private SerializedProperty property;
        private SerializedProperty _nameProp;
        private SerializedProperty _delayProp;
        
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GetProperties(property);
            EditorGUI.BeginProperty(position, label, property);

            float height = GetPropertyHeight(property, label);

            DrawHeader(position);
            
            EditorGUI.EndProperty();
        }

 
        private void DrawHeader(Rect position)
        {
            var linePos = new Rect(position);
            linePos.x += 20;
            linePos.width = 10;
            linePos.height = StyleSettings.Instance.bigHeight;
            
            property.isExpanded = EditorGUI.Foldout(linePos, property.isExpanded, "");

            linePos.x += linePos.width;
            linePos.width = position.width - 20 - 10 - 80 - 10;
            using (new AFStyles.GuiBackgroundColor(Color.clear))
                EditorGUI.TextField(linePos, _nameProp.stringValue, (GUIStyle)AFStyles.BigTextField);

            linePos.x += 10 + linePos.width;
            linePos.width = 40;
            GUI.Label(linePos, "Delay :", AFStyles.Label);

            linePos.x += linePos.width;
            using (new AFStyles.EditorLabelWidth())
            {
                _delayProp.floatValue = EditorGUI.FloatField(linePos, new GUIContent("   "), _delayProp.floatValue, AFStyles.TextField);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            GetProperties(property);
            
            var singleLine = AFStyles.Height + AFStyles.VerticalSpace;
            float height = 0;
            
            height += StyleSettings.Instance.bigHeight;
            
            return height;
        }
        private void GetProperties(SerializedProperty property)
        {
            this.property = property;
            _nameProp = property.FindPropertyRelative(nameof(ClipNode.name));
            _delayProp = property.FindPropertyRelative(nameof(ClipNode.delay));
            
        }

    }
}