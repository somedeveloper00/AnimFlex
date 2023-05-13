using System.Collections.Generic;
using AnimFlex.Sequencer.Binding;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace AnimFlex.Editor {
    [CustomPropertyDrawer( typeof(ClipFieldBinder), true )]
    public class ClipFieldBinderBaseDrawer : PropertyDrawer {

        Dictionary<string, ReorderableList> _selectionsListDict = new Dictionary<string, ReorderableList>();
        GUIContent _selectionsLabel = new GUIContent( "Selections To Bind", "The selected fields will be binded with the given Value" );
        GUIContent _nameGuiContent = new GUIContent("Name", "The name is used to reference this binder through script");
        GUIContent _valueGuiContent = new GUIContent( "Value", "The value to bind to the selected fields to" );

        public static SerializedProperty Current;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var nameProp = property.FindPropertyRelative( nameof(ClipFieldBinder.name) );
            var selectionsProp = property.FindPropertyRelative( nameof(ClipFieldBinder.selections) );
            var valueProp = property.FindPropertyRelative( nameof(ClipFieldBinderInt.value) );

            using (new EditorGUI.PropertyScope( position, label, property )) {
                position.height = AFStyles.Height;
                drawHeader();
                if ( property.isExpanded ) {
                    using (new EditorGUI.IndentLevelScope()) {
                        drawValueList();
                    }
                }
            }

            void drawHeader() {
                var rect = new Rect( position );
                rect.x += 10; rect.width = 20;
                property.isExpanded = EditorGUI.Foldout( rect, property.isExpanded, "   ", true, AFStyles.Foldout );
                rect.x += rect.width;
                rect.width = position.width - 30;
                using (new GUILayout.HorizontalScope()) {
                    rect.width /= 2f;
                    rect.width -= 4;
                    using (new AFStyles.EditorLabelWidth( 50 )) {
                        using (new AFStyles.GuiBackgroundColor( new Color( 0, 0, 0, 0 ) ))
                            EditorGUI.PropertyField( rect, nameProp, _nameGuiContent );
                        rect.x += rect.width + 4 * 2;
                        EditorGUI.PropertyField( rect, valueProp, _valueGuiContent );
                    }
                }
                position.y += Mathf.Max(EditorGUI.GetPropertyHeight( valueProp ), AFStyles.Height) + AFStyles.VerticalSpace;
            }

            void drawValueList() {
                if (!_selectionsListDict.TryGetValue( property.propertyPath, out var _selectionList ) || _selectionList == null) {
                    // create list
                    _selectionList = new ReorderableList( selectionsProp.serializedObject, selectionsProp, true, true, true, true );
                    _selectionList.multiSelect = false;
                    _selectionList.drawElementCallback += (rect, index, _, _) => {
                        Current = property;
                        EditorGUI.PropertyField( rect, selectionsProp.GetArrayElementAtIndex( index ), true );
                    };
                    _selectionList.elementHeightCallback += index => EditorGUI.GetPropertyHeight( selectionsProp.GetArrayElementAtIndex( index ), true );
                    _selectionList.headerHeight = AFStyles.Height + AFStyles.VerticalSpace;
                    _selectionList.drawHeaderCallback += rect => EditorGUI.LabelField( rect, _selectionsLabel, AFStyles.SpecialLabel );
                    _selectionsListDict[property.propertyPath] = _selectionList;
                }
                
                _selectionList.DoList( position );
            }
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            var valueProp = property.FindPropertyRelative( nameof(ClipFieldBinderInt.value) );
            var h = EditorGUI.GetPropertyHeight( valueProp ) + AFStyles.VerticalSpace;

            if (property.isExpanded) {
                // list
                if (_selectionsListDict.TryGetValue( property.propertyPath, out var _selectionList ) && _selectionList != null)
                    h += _selectionList.GetHeight() + AFStyles.VerticalSpace;
                else 
                    h += 30;
            }
            
            

            return h;
        }
    }
}