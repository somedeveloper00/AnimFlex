using AnimFlex.Sequencer.Binding;
using AnimFlex.Sequencer.BindingSystem;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor {
    [CustomPropertyDrawer(typeof(ClipFieldBinder.FieldSelection), true)]
    public class FieldSelectionDrawer : PropertyDrawer {
        
        static readonly GUIContent clipSelectionGuiContent = new GUIContent("Clip", "The clip to bind to");
        static readonly GUIContent fieldSelectionGuiContent = new GUIContent("Field", "The field to bind to");
            
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var indexProp = property.FindPropertyRelative( nameof(ClipFieldBinder.FieldSelection.clipIndex) );
            var fieldNameProp = property.FindPropertyRelative( nameof(ClipFieldBinder.FieldSelection.fieldName) );
            using (new EditorGUI.PropertyScope( position, label, property )) {
                using (new AFStyles.EditorLabelWidth( 70 )) {
                    property.isExpanded = true;
                    position.height = AFStyles.Height;
                    position.width /= 2f;
                    position.width -= 4;
                    position.y += AFStyles.VerticalSpace; // space before
                    drawClipSelection( position, indexProp );
                    position.x += position.width + 4 * 2;
                    drawFieldName( position, indexProp.intValue, property, fieldNameProp );
                    position.y += AFStyles.VerticalSpace; // space after
                }
            }
        }

        void drawClipSelection(Rect pos, SerializedProperty indexProp) {
            var sequenceAnim = SequencerBindingEditor.CurrentTargetingSequenceAnim;
            var options = new GUIContent[sequenceAnim.sequence.nodes.Length];
            for (int i = 0; i < options.Length; i++) options[i] = new GUIContent( sequenceAnim.sequence.nodes[i].name );
            
            using (new AFStyles.EditorLabelWidth( 45 ))
            using (var check = new EditorGUI.ChangeCheckScope()) {
                var r = EditorGUI.Popup( pos, clipSelectionGuiContent, indexProp.intValue, options );
                if (check.changed) indexProp.intValue = r;
            }
        }
        
        void drawFieldName(Rect pos, int clipIndex, SerializedProperty property, SerializedProperty fieldNameProp) {
            var sequenceAnim = SequencerBindingEditor.CurrentTargetingSequenceAnim;
            
            if (sequenceAnim.sequence.nodes == null || sequenceAnim.sequence.nodes.Length == 0) {
                AFStyles.DrawHelpBox( pos, "No clips found", MessageType.Warning );
                return;
            }
            
            if (sequenceAnim.sequence.nodes.Length <= clipIndex) {
                AFStyles.DrawHelpBox( pos, "Clip index out of range", MessageType.Error );
                return;
            }
            
            var valueType = ( (ClipFieldBinder)ClipFieldBinderBaseDrawer.Current.GetValue() ).GetselectionValueType();
            var fieldNames = BindingUtils.GetAllBindableFieldsOnClipGuiContent( sequenceAnim.sequence.nodes[clipIndex].clip, valueType);

            if (fieldNames.Length == 0) {
                AFStyles.DrawHelpBox( pos, $"No fields found of type \"{valueType.Name}\"", MessageType.Warning );
            }
            else {
                int selectedIndex = -1;
                for (int i = 0; i < fieldNames.Length; i++) {
                    if (fieldNames[i].text == fieldNameProp.stringValue) {
                        selectedIndex = i;
                        break;
                    }
                }
                
                if (selectedIndex == -1) {
                    fieldNameProp.stringValue = fieldNames[0].text;
                }
                
                using (new AFStyles.EditorLabelWidth( 45 ))
                using (var check = new EditorGUI.ChangeCheckScope()) {
                    var r = EditorGUI.Popup( pos, fieldSelectionGuiContent, selectedIndex, fieldNames );
                    if (check.changed) fieldNameProp.stringValue = fieldNames[r].text;
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return AFStyles.Height + AFStyles.VerticalSpace * 2;
        }
    }
}