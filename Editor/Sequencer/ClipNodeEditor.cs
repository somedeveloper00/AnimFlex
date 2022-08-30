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
        private SerializedProperty _clipProp;
        
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.width = Mathf.Max(position.width, 350);
            
            using (new AFStyles.CenteredEditorStyles())
            {
                using (new AFStyles.GuiColor(Color.white))
                {
                    using (new AFStyles.GuiBackgroundColor(Color.white))
                    {
                        GetProperties(property);
                        EditorGUI.BeginProperty(position, label, property);

                        DrawHeader(position);

                        if (property.isExpanded)
                        {
                            position.y += AFStyles.BigHeight + AFStyles.VerticalSpace * 2;
                            var height = DrawClip(position);
                        }

                        EditorGUI.EndProperty();
                    }
                }
            }
        }

        private float DrawClip(Rect position)
        {
            var linePos = new Rect(position);

            linePos.height = EditorGUI.GetPropertyHeight(_clipProp, GUIContent.none, true);
            EditorGUI.PropertyField(linePos, _clipProp, GUIContent.none, true);

            return linePos.height;
        }
        
        private void DrawHeader(Rect position)
        {
            var linePos = new Rect(position);
            linePos.y += AFStyles.VerticalSpace;
            // linePos.x += 20;
            linePos.width = 10;
            linePos.height = AFStyles.BigHeight;
            
            property.isExpanded = EditorGUI.Foldout(linePos, property.isExpanded, "");

            linePos.x += linePos.width;
            linePos.width = position.width - 30 - 10 - 40 - 60 - 10 - 170;
            using (new AFStyles.GuiBackgroundColor(Color.clear))
                _nameProp.stringValue = EditorGUI.TextField(linePos, _nameProp.stringValue, AFStyles.BigTextField);
            
            // display clip type
            var type = AFEditorUtils.FindType(_clipProp.GetValue().GetType().FullName);
            linePos.x += linePos.width;
            linePos.width = 185;
            if (GUI.Button(linePos, AFEditorUtils.GetTypeName(type), AFStyles.Popup))
            {
                SerializedProperty prop = property; // to hold on to the property for the callback
                AFEditorUtils.CreateTypeInstanceFromHierarchy<Clip>((clip) =>
                {
                    GetProperties(prop);
                    Undo.RecordObject(prop.serializedObject.targetObject, "clip type modified");
                    property.serializedObject.ApplyModifiedProperties();
                    ((ClipNode)property.GetValue()).clip = clip;
                    property.serializedObject.Update();
                });
            }

            linePos.x += 10 + linePos.width;
            linePos.width = 40;
            GUI.Label(linePos, "Delay :", AFStyles.Label);

            linePos.x += linePos.width;
            linePos.width = 50;
            using (new AFStyles.EditorLabelWidth())
            {
                _delayProp.floatValue = EditorGUI.FloatField(linePos, new GUIContent("   "), _delayProp.floatValue);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            GetProperties(property);
            
            var singleLine = AFStyles.Height + AFStyles.VerticalSpace;
            float height = 0;
            
            height += AFStyles.BigHeight + AFStyles.VerticalSpace * 2;
            if (property.isExpanded)
            {
                height += EditorGUI.GetPropertyHeight(_clipProp, GUIContent.none, true);
                height += AFStyles.VerticalSpace;
            }
            
            return height;
        }
        private void GetProperties(SerializedProperty property)
        {
            this.property = property;
            _nameProp = property.FindPropertyRelative(nameof(ClipNode.name));
            _delayProp = property.FindPropertyRelative(nameof(ClipNode.delay));
            _clipProp = property.FindPropertyRelative(nameof(ClipNode.clip));
            
        }

    }
}