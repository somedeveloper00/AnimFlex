﻿using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor
{
    public static class AFStyles
    {
        public class CenteredEditorStyles : IDisposable
        {
            public CenteredEditorStyles()
            {
                EditorStyles.textField.alignment = TextAnchor.MiddleCenter;
                EditorStyles.numberField.alignment = TextAnchor.MiddleCenter;
            }
            public void Dispose()
            {
                EditorStyles.textField.alignment = TextAnchor.UpperLeft;
                EditorStyles.numberField.alignment = TextAnchor.UpperLeft;
            }
        }

        public class EditorLabelWidth : IDisposable
        {
            private float width;
            public EditorLabelWidth(float width = 10)
            {
                this.width = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = width;
            }

            public void Dispose()
            { 
                EditorGUIUtility.labelWidth = width;
            }
        }
        public class EditorFieldMinWidth : IDisposable
        {
            private float oldWidth;
            public EditorFieldMinWidth(Rect pos, float width = 10)
            {
                this.oldWidth = EditorGUIUtility.labelWidth;
                if (pos.width - EditorGUIUtility.labelWidth < width)
                    EditorGUIUtility.labelWidth = pos.width - width;
            }

            public void Dispose()
            {
                EditorGUIUtility.labelWidth = oldWidth;
            }
        }

        public class GuiColor : IDisposable
        {
            private Color oldCol;
            public GuiColor(Color color)
            {
                oldCol = GUI.color;
                GUI.color = color;
            }
            public void Dispose() => GUI.color = oldCol;
        }
        
        public class GuiBackgroundColor : IDisposable
        {
            private Color oldCol;
            public GuiBackgroundColor(Color color)
            {
                oldCol = GUI.backgroundColor;
                GUI.backgroundColor = color;
            }
            public void Dispose() => GUI.backgroundColor = oldCol;
        }

        public class GuiForceActive : IDisposable
        {
            private bool wasEnabled;
            public GuiForceActive()
            {
                wasEnabled = GUI.enabled;
                GUI.enabled = true;
            }
            public void Dispose() => GUI.enabled = wasEnabled;
        }

        public class StyledGuiScope : IDisposable
        {
            private GUIStyle labelStyle;
            private GUIStyle largeLabelStyle;
            private GUIStyle popupStyle;
            
            public StyledGuiScope()
            {
                labelStyle = new GUIStyle(EditorStyles.label);
                largeLabelStyle = new GUIStyle(EditorStyles.largeLabel);
                popupStyle = new GUIStyle(EditorStyles.popup);

                EditorStyles.label.font = StyleSettings.Instance.font;
                EditorStyles.label.fontSize = StyleSettings.Instance.fontSize;
                EditorStyles.label.alignment = TextAnchor.MiddleCenter;
                EditorStyles.label.normal.textColor = StyleSettings.Instance.labelCol;
                EditorStyles.label.hover.textColor = Color.white;
                EditorStyles.label.onHover.textColor = Color.white;
                
                EditorStyles.largeLabel.font = StyleSettings.Instance.font;
                EditorStyles.largeLabel.fontSize = StyleSettings.Instance.bigFontSize;
                EditorStyles.largeLabel.alignment = TextAnchor.MiddleCenter;
                EditorStyles.largeLabel.normal.textColor = StyleSettings.Instance.labelCol;
                
                EditorStyles.popup.font = StyleSettings.Instance.font;
                EditorStyles.popup.fontSize = StyleSettings.Instance.fontSize;
                EditorStyles.popup.alignment = TextAnchor.MiddleCenter;
                EditorStyles.popup.normal.textColor = StyleSettings.Instance.popupCol;
            }

            public void Dispose()
            {
                EditorStyles.label.font = labelStyle.font;
                EditorStyles.label.fontSize = labelStyle.fontSize;
                EditorStyles.label.alignment = labelStyle.alignment;
                EditorStyles.label.normal.textColor = labelStyle.normal.textColor;
                EditorStyles.label.hover.textColor = labelStyle.hover.textColor;
                EditorStyles.label.onHover.textColor = labelStyle.onHover.textColor;

                EditorStyles.largeLabel.font = largeLabelStyle.font;
                EditorStyles.largeLabel.fontSize = largeLabelStyle.fontSize;
                EditorStyles.largeLabel.alignment = largeLabelStyle.alignment;
                EditorStyles.largeLabel.normal.textColor = largeLabelStyle.normal.textColor;
                
                EditorStyles.popup.font = popupStyle.font; 
                EditorStyles.popup.fontSize = popupStyle.fontSize; 
                EditorStyles.popup.alignment = popupStyle.alignment;
                EditorStyles.popup.normal.textColor = popupStyle.normal.textColor;

            }
            
        }

        public static void Refresh()
        {
            _button = _yellowButton = _specialLabel = _bigButton = _textField = _bigTextField = _popup = null;
        }

        private static GUIStyle _button;
        public static GUIStyle Button
        {
            get
            {
                if (_button != null) return _button;
                _button = new GUIStyle(GUI.skin.button);
                _button.normal.textColor = StyleSettings.Instance.buttonDefCol;
                _button.font = StyleSettings.Instance.font;
                _button.fontSize = StyleSettings.Instance.fontSize;
                return _button;
            }
        }

        private static GUIStyle _bigButton;
        public static GUIStyle BigButton
        {
            get
            {
                if (_bigButton != null) return _bigButton;
                _bigButton = new GUIStyle(Button);
                _bigButton.fontSize = 18;
                return _bigButton;
            }
        }

        private static GUIStyle _clearButton;
        public static GUIStyle ClearButton
        {
            get
            {
                if (_clearButton != null) return _clearButton;
                _clearButton = new GUIStyle(Button);
                _clearButton.fontSize = 18;
                _clearButton.alignment = TextAnchor.MiddleCenter;
                
                var tex = new Texture2D(2, 2);
                tex.SetPixels(new[]
                {
                    Color.clear, Color.clear, 
                    Color.clear, Color.clear 
                });
                tex.Apply(false);
                _clearButton.normal.background = _clearButton.hover.background = 
                    _clearButton.onHover.background = tex;
                return _clearButton;
            }
        }
        
        private static GUIStyle _yellowButton;
        public static GUIStyle YellowButton
        {
            get
            {
                if (_yellowButton != null) return _yellowButton;
                _yellowButton = new GUIStyle(GUI.skin.button);
                _yellowButton.normal.textColor = StyleSettings.Instance.buttonYellowCol;
                _yellowButton.font = StyleSettings.Instance.font;
                _yellowButton.fontSize = StyleSettings.Instance.fontSize;
                return _yellowButton;
            }
        }

        private static GUIStyle _specialLabel;
        public static GUIStyle SpecialLabel
        {
            get
            {
                if (_specialLabel != null) return _specialLabel;
                _specialLabel = new GUIStyle(GUI.skin.label);
                _specialLabel.font = StyleSettings.Instance.font;
                _specialLabel.alignment = TextAnchor.MiddleCenter;
                _specialLabel.fontSize = StyleSettings.Instance.fontSize;
                _specialLabel.normal.textColor = StyleSettings.Instance.labelCol;
                _specialLabel.hover.textColor = _specialLabel.onHover.textColor = StyleSettings.Instance.labelCol_Hover;
                return _specialLabel;
            }
        }

        private static GUIStyle _label;
        public static GUIStyle Label
        {
            get
            {
                if (_label != null) return _label;
                _label = new GUIStyle(GUI.skin.label);
                _label.font = StyleSettings.Instance.font;
                _label.alignment = TextAnchor.MiddleCenter;
                _label.fontSize = StyleSettings.Instance.fontSize;
                return _label;
            }
        }

        private static GUIStyle _bigTextField;
        public static GUIStyle BigTextField
        {
            get
            {
                if (_bigTextField != null) return _bigTextField;
                _bigTextField = new GUIStyle(EditorStyles.textField);
                _bigTextField.font = StyleSettings.Instance.font;
                // _bigTextField.fontStyle = FontStyle.Bold;
                _bigTextField.alignment = TextAnchor.MiddleCenter;
                _bigTextField.fontSize = StyleSettings.Instance.bigFontSize;
                _bigTextField.fixedHeight = 0;
                return _bigTextField;
            }
        }

        private static GUIStyle _textField;
        public static GUIStyle TextField
        {
            get
            {
                if (_textField != null) return _textField;
                _textField = new GUIStyle(EditorStyles.textField);
                _textField.font = StyleSettings.Instance.font;
                _textField.alignment = TextAnchor.MiddleCenter;
                _textField.fontSize = StyleSettings.Instance.fontSize;
                _textField.fixedHeight = 0;
                return _textField;
            }
        }
        
        private static GUIStyle _popup;
        public static GUIStyle Popup
        {
            get
            {
                if (_popup != null) return _popup;
                _popup = new GUIStyle(EditorStyles.popup);
                _popup.font = StyleSettings.Instance.font;
                _popup.fontSize = StyleSettings.Instance.fontSize;
                _popup.stretchHeight = true;
                _popup.stretchWidth = true;
                _popup.alignment = TextAnchor.MiddleCenter;
                _popup.fixedHeight = 0;
                _popup.normal.textColor = _popup.hover.textColor = _popup.active.textColor =
                    _popup.focused.textColor = StyleSettings.Instance.popupCol;
                return _popup;
            }        
        }

        public static Rect PadX(this Rect rect, float pad) =>
            new Rect(rect.x + pad, rect.y, rect.width - pad * 2, rect.height);

        public static bool DrawBooleanEnum(Rect position, string optionTrue, string optionFalse, bool value, string tooltip, out bool result)
        {
            var options = new GUIContent[]
            {
                new(optionTrue, tooltip), new(optionFalse, tooltip)
            };
            using var check = new EditorGUI.ChangeCheckScope();
            using (new AFStyles.EditorLabelWidth(0))
                result = EditorGUI.Popup(position, GUIContent.none, value ? 0 : 1, options, AFStyles.Popup) == 0;
            return check.changed;
        }

        public static void DrawBooleanEnum(Rect position, string optionTrue, string optionFalse, SerializedProperty property)
        {
            // var options = new GUIContent[]
            // {
            //     new(optionTrue, property.tooltip), new(optionFalse, property.tooltip)
            // };
            var options = new []
            {
                optionTrue, optionFalse
            };

            using (new AFStyles.EditorLabelWidth(0))
            {
                property.boolValue = EditorGUI.Popup(position, property.boolValue ? 0 : 1, options, AFStyles.Popup) == 0;
            }
            
        }

        
        public static void DrawHelpBox(Rect position, string message, MessageType messageType)
        {
            var GetHelpIcon = 
                typeof(EditorGUIUtility).GetMethod("GetHelpIcon", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Default);
            if (GetHelpIcon != null)
            {
                var texture = (Texture)GetHelpIcon.Invoke(null, new object[] { messageType });
                var guiContent = new GUIContent(message, texture);

                var style = new GUIStyle(EditorStyles.helpBox);
                style.font = StyleSettings.Instance.font;
                style.fontSize = StyleSettings.Instance.fontSize;
                style.alignment = TextAnchor.MiddleCenter;
                style.wordWrap = false;

                using(new GuiColor(Color.yellow))
                    GUI.Label(position, guiContent, style);
            }
        }
        public static void DrawBigTextField(SerializedProperty stringProperty, params GUILayoutOption[] options)
        {
            // changing editor styles and keeping their old states
            var style = new GUIStyle(EditorStyles.textField);
            EditorStyles.textField.fontSize = StyleSettings.Instance.bigFontSize;
            EditorStyles.textField.font = StyleSettings.Instance.font;
            EditorStyles.textField.alignment = TextAnchor.MiddleCenter;
            EditorStyles.textField.fixedHeight = AFStyles.BigHeight;

            var list = options.ToList();
            list.Add(GUILayout.Height(AFStyles.BigHeight));
            options = list.ToArray();
            
            GUI.backgroundColor = Color.clear;

            using (new GuiBackgroundColor(Color.clear))
            {
                using (new EditorLabelWidth(0))
                {
                    GUILayout.BeginVertical();
                    stringProperty.stringValue = EditorGUILayout.TextField(stringProperty.stringValue, options);
                    GUILayout.Space(5);
                    GUILayout.EndVertical();
                }
            }

            // reverting the editor styles old states
            EditorStyles.textField.font  = style.font;
            EditorStyles.textField.fontSize = style.fontSize; 
            EditorStyles.textField.fontStyle = style.fontStyle;
            EditorStyles.textField.alignment = style.alignment;
            EditorStyles.textField.fixedHeight = style.fixedHeight;
        }
        
        public static float Height => StyleSettings.Instance.height;
        public static float BigHeight => StyleSettings.Instance.bigHeight;
        public static float VerticalSpace => StyleSettings.Instance.verticalSpace;
        public static Color BoxColor => StyleSettings.Instance.BoxCol;
        public static Color BoxColorDarker => StyleSettings.Instance.BoxColDarker;
    }
}