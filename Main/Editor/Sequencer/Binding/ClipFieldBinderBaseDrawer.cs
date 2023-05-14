using System;
using System.Collections.Generic;
using AnimFlex.Sequencer;
using AnimFlex.Sequencer.BindingSystem;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace AnimFlex.Editor {
    [CustomPropertyDrawer( typeof(SequenceBinder), true )]
    public class SequenceBinderBaseDrawer : PropertyDrawer {

        Dictionary<string, ReorderableList> _selectionsListDict = new Dictionary<string, ReorderableList>();
        static GUIContent _selectionsGuiContent;
        static GUIContent _valueGuiContent;
        static GUIContent _sequenceAnimGuiContent;
        static readonly GUIContent _selectionElementClipIndexGuiContent = new GUIContent("Clip", "The clip to bind to");
        static readonly GUIContent _selectionElementFieldGuiContent = new GUIContent("Field", "The field to bind to");

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var selectionsProp = property.FindPropertyRelative( nameof(SequenceBinder.selections) );
            var valueProp = property.FindPropertyRelative( nameof(SequenceBinder_Int.value) );
            var sequenceAnimProp = property.FindPropertyRelative( nameof(SequenceBinder.sequenceAnim) );
            ensureGuiContentsAreSet( selectionsProp, valueProp, sequenceAnimProp );

            using (new EditorGUI.PropertyScope( position, label, property )) {
                // draw containing box
                EditorGUI.DrawRect( position, AFEditorSettings.Instance.BoxColOutline );
                position.x += AFStyles.VerticalSpace / 2; position.width -= AFStyles.VerticalSpace;
                position.y += AFStyles.VerticalSpace / 2; position.height -= AFStyles.VerticalSpace;
                EditorGUI.DrawRect( position, AFEditorSettings.Instance.BoxColDarker );
                position.x -= AFStyles.VerticalSpace / 2; position.width += AFStyles.VerticalSpace;
                position.y -= AFStyles.VerticalSpace / 2; position.height += AFStyles.VerticalSpace;
                
                position.x += 15; position.width -= 30;
                position.y += AFStyles.VerticalSpace;
                position.height = AFStyles.Height;
                
                drawHeader();
                if ( property.isExpanded ) {
                    using (new EditorGUI.IndentLevelScope()) {
                        drawBodyTop();
                        drawValueList();
                    }
                }
            }

            void drawHeader() {
                var rect = new Rect( position );
                using (new GUILayout.HorizontalScope()) {
                    rect.width = Mathf.Min( rect.width, EditorGUIUtility.labelWidth );
                    property.isExpanded = EditorGUI.Foldout( rect, property.isExpanded, label, true );
                    rect.x += rect.width;
                    rect.width = position.width - rect.width;
                    if (!property.isExpanded) {
                        rect.width /= 2f; rect.width -= 10;
                        EditorGUI.PropertyField( rect, sequenceAnimProp, GUIContent.none );
                        float w = rect.width;
                        rect.x += rect.width; rect.width = 20;
                        GUI.Label( rect, "->" );
                        rect.x += rect.width; rect.width = w;
                        EditorGUI.PropertyField( rect, valueProp, GUIContent.none );
                    }
                }
                position.y += Mathf.Max( EditorGUI.GetPropertyHeight( valueProp ), AFStyles.Height ) + AFStyles.VerticalSpace;
            }

            void drawBodyTop() {
                position.width /= 2f;
                using (new AFStyles.EditorLabelWidth( 80 ))
                    EditorGUI.PropertyField( position, sequenceAnimProp, _sequenceAnimGuiContent );
                position.x += position.width;
                using (new AFStyles.EditorLabelWidth( 80 ))
                    EditorGUI.PropertyField( position, valueProp, _valueGuiContent );
                position.y += AFStyles.Height + AFStyles.VerticalSpace;
                position.x -= position.width; position.width *= 2f;
            }
            
            void drawValueList() {
                if (!_selectionsListDict.TryGetValue( property.propertyPath, out var _selectionList ) || _selectionList == null) {
                    // create list
                    _selectionList = new ReorderableList( selectionsProp.serializedObject, selectionsProp, true, true, true, true );
                    _selectionList.multiSelect = false;
                    _selectionList.drawElementCallback += (rect, index, _, _) => drawSelectionListElement( rect, index );
                    _selectionList.elementHeight = AFStyles.Height + AFStyles.VerticalSpace * 2;
                    _selectionList.drawHeaderCallback += rect => EditorGUI.LabelField( rect, _selectionsGuiContent, AFStyles.SpecialLabel );
                    _selectionList.headerHeight = AFStyles.Height + AFStyles.VerticalSpace;
                    _selectionsListDict[property.propertyPath] = _selectionList;
                }
                
                _selectionList.DoList( position );
            }

            void drawSelectionListElement(Rect rect, int index) {
                var elementProp = selectionsProp.GetArrayElementAtIndex( index );
                var indexProp = elementProp.FindPropertyRelative( nameof(SequenceBinder.FieldSelection.clipIndex) );
                var fieldNameProp = elementProp.FindPropertyRelative( nameof(SequenceBinder.FieldSelection.fieldName) );

                using (new EditorGUI.PropertyScope( rect, GUIContent.none, elementProp )) {
                    rect.height = AFStyles.Height;
                    rect.width /= 2f;
                    rect.width -= 4;
                    drawClipSelection();
                    rect.x += rect.width + 4 * 2;
                    drawFieldName();
                }

                void drawClipSelection() {
                    var sequenceAnim = sequenceAnimProp.objectReferenceValue as SequenceAnim;
                    GUIContent[] options = Array.Empty<GUIContent>();
                    if (sequenceAnim) {
                        options = new GUIContent[sequenceAnim.sequence.nodes.Length];
                        for (int i = 0; i < options.Length; i++)
                            options[i] = new GUIContent( $"({i}) {sequenceAnim.sequence.nodes[i].name}" );
                    }
                    else {
                        options = new GUIContent[] { new GUIContent( indexProp.intValue.ToString() ) };
                    }
                    
                    using (new AFStyles.EditorLabelWidth( 30 ))
                    using (var check = new EditorGUI.ChangeCheckScope()) {
                        var r = EditorGUI.Popup( rect, _selectionElementClipIndexGuiContent, indexProp.intValue, options );
                        if (check.changed) indexProp.intValue = r;
                    }
                }

                void drawFieldName() {
                    var sequenceAnim = sequenceAnimProp.objectReferenceValue as SequenceAnim;

                    if (!sequenceAnim) {
                        AFStyles.DrawHelpBox( rect, "SequenceAnim field is empty", MessageType.Warning );
                        return;
                    }
                    if (sequenceAnim.sequence.nodes == null || sequenceAnim.sequence.nodes.Length == 0) {
                        AFStyles.DrawHelpBox( rect, "No clips found", MessageType.Warning );
                        return;
                    }
            
                    if (sequenceAnim.sequence.nodes.Length <= indexProp.intValue) {
                        AFStyles.DrawHelpBox( rect, "Clip index out of range", MessageType.Error );
                        return;
                    }
            
                    var valueType = ( (SequenceBinder)property.GetValue() ).GetselectionValueType();
                    var fieldNames = BindingUtils.GetAllBindableFieldsOnClipGuiContent( sequenceAnim.sequence.nodes[indexProp.intValue].clip, valueType );

                    if (fieldNames.Length == 0) {
                        AFStyles.DrawHelpBox( rect, $"No fields found. Type: \"{valueType.FullName}\"", MessageType.Warning );
                    }
                    else {
                        int selectedIndex = -1;
                        for (int i = 0; i < fieldNames.Length; i++) {
                            if (fieldNames[i].text == fieldNameProp.stringValue) {
                                selectedIndex = i;
                                break;
                            }
                        }
                
                        // prevent invalid selections
                        if (selectedIndex == -1) {
                            fieldNameProp.stringValue = fieldNames[0].text;
                            selectedIndex = 0;
                        }
                
                        using (new AFStyles.EditorLabelWidth( 30 ))
                        using (var check = new EditorGUI.ChangeCheckScope()) {
                            var r = EditorGUI.Popup( rect, _selectionElementFieldGuiContent, selectedIndex, fieldNames );
                            if (check.changed) fieldNameProp.stringValue = fieldNames[r].text;
                        }
                    }
                }
            }
        }
        


        void ensureGuiContentsAreSet(SerializedProperty selectionsProp, SerializedProperty valueProp, SerializedProperty sequenceAnimProp) {
            _valueGuiContent ??= new GUIContent( "Value", valueProp.tooltip );
            _selectionsGuiContent ??= new GUIContent( "Selections", selectionsProp.tooltip );
            _sequenceAnimGuiContent ??= new GUIContent( "Sequence", sequenceAnimProp.tooltip );
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            var valueProp = property.FindPropertyRelative( nameof(SequenceBinder_Int.value) );
            var h = Mathf.Max( AFStyles.Height, EditorGUI.GetPropertyHeight( valueProp ) ) + AFStyles.VerticalSpace * 2;

            if (property.isExpanded) {
                // list
                if (_selectionsListDict.TryGetValue( property.propertyPath, out var _selectionList ) && _selectionList != null)
                    h += _selectionList.GetHeight() + AFStyles.Height + AFStyles.VerticalSpace * 2;
                else 
                    h += 30;
            }

            return h;
        }
    }
}