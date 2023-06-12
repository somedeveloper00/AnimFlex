using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor {
    [CustomPropertyDrawer(typeof(Sequencer.Clips.CComment))]
    public class CCommentEditor : PropertyDrawer {
        float lastWidth = 100;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var msgProp = property.FindPropertyRelative( nameof(Sequencer.Clips.CComment.message) );
            using (new EditorGUI.PropertyScope( position, label, property )) {
                position.y += AFStyles.VerticalSpace;
                position.x += 10; position.width -= 20;
                position.height = Mathf.Max(AFStyles.BigHeight, AFStyles.CenteredTextField.CalcHeight( new(msgProp.stringValue), position.width ));
                using (new AFStyles.GuiBackgroundColor( AFStyles.BoxColor )) {
                    msgProp.stringValue = EditorGUI.TextArea( position, msgProp.stringValue, AFStyles.CenteredTextField );
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            var msgProp = property.FindPropertyRelative( nameof(Sequencer.Clips.CComment.message) );
            return AFStyles.VerticalSpace * 2 + Mathf.Max(AFStyles.BigHeight, AFStyles.CenteredTextField.CalcHeight( new(msgProp.stringValue), lastWidth ));
        }
    }
}