using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor
{
    public class AFStyles
    {
        public readonly struct EditorLabelWidth : IDisposable
        {
            private readonly float width;

            public EditorLabelWidth(float width)
            {
                this.width = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = width;
            }
            public readonly void Dispose() => EditorGUIUtility.labelWidth = width;
        }

        public readonly struct EditorFieldMinWidth : IDisposable
        {
            private readonly float oldWidth;

            public EditorFieldMinWidth(Rect pos, float width = 10)
            {
                oldWidth = EditorGUIUtility.labelWidth;
                if (pos.width - EditorGUIUtility.labelWidth < width)
                    EditorGUIUtility.labelWidth = pos.width - width;
            }

            public readonly void Dispose()
            {
                EditorGUIUtility.labelWidth = oldWidth;
            }
        }

        public readonly struct GuiColor : IDisposable
        {
            private readonly Color oldCol;

            public GuiColor(Color color)
            {
                oldCol = GUI.color;
                GUI.color = color;
            }

            public readonly void Dispose() => GUI.color = oldCol;
        }

        public readonly struct GuiBackgroundColor : IDisposable
        {
            private readonly Color oldCol;

            public GuiBackgroundColor(Color color)
            {
                oldCol = GUI.backgroundColor;
                GUI.backgroundColor = color;
            }

            public readonly void Dispose() => GUI.backgroundColor = oldCol;
        }

        public readonly struct GuiForceActive : IDisposable
        {
            private readonly bool wasEnabled;

            public GuiForceActive(bool disableInPlaymode)
            {
                wasEnabled = GUI.enabled;
                GUI.enabled = !disableInPlaymode || !Application.isPlaying;
            }

            public void Dispose() => GUI.enabled = wasEnabled;
        }

        public readonly struct StyledGuiScope : IDisposable
        {
            private readonly GUIStyle labelStyle;
            private readonly GUIStyle largeLabelStyle;
            private readonly GUIStyle popupStyle;
            private readonly TextAnchor textFieldAnchor;
            private readonly TextAnchor numberFieldAnchor;

            /// <summary>
            /// if editor is null, it won't automatically repaint
            /// </summary>
            public StyledGuiScope(bool dontRuinEverything) // to be forced call constructor
            {
                labelStyle = new GUIStyle(EditorStyles.label);
                largeLabelStyle = new GUIStyle(EditorStyles.largeLabel);
                popupStyle = new GUIStyle(EditorStyles.popup);
                textFieldAnchor = EditorStyles.textField.alignment;
                numberFieldAnchor = EditorStyles.numberField.alignment;

                EditorStyles.label.font = AFEditorSettings.Instance.font;
                EditorStyles.label.fontSize = AFEditorSettings.Instance.fontSize;
                EditorStyles.label.alignment = AFEditorSettings.Instance.labelAlignment;
                EditorStyles.label.normal.textColor = AFEditorSettings.Instance.labelCol;
                EditorStyles.label.hover.textColor = AFEditorSettings.Instance.labelCol_Hover;
                EditorStyles.label.onHover.textColor = AFEditorSettings.Instance.labelCol_Hover;

                EditorStyles.textField.alignment = AFEditorSettings.Instance.labelAlignment;
                EditorStyles.numberField.alignment = AFEditorSettings.Instance.labelAlignment;

                EditorStyles.largeLabel.font = AFEditorSettings.Instance.font;
                EditorStyles.largeLabel.fontSize = AFEditorSettings.Instance.bigFontSize;
                EditorStyles.largeLabel.alignment = AFEditorSettings.Instance.labelAlignment;
                EditorStyles.largeLabel.normal.textColor = AFEditorSettings.Instance.labelCol;

                EditorStyles.popup.font = AFEditorSettings.Instance.font;
                EditorStyles.popup.fontSize = AFEditorSettings.Instance.fontSize;
                EditorStyles.popup.alignment = AFEditorSettings.Instance.labelAlignment;
                EditorStyles.popup.normal.textColor = AFEditorSettings.Instance.popupCol;
            }

            public readonly void Dispose()
            {
                EditorStyles.label.font = labelStyle.font;
                EditorStyles.label.fontSize = labelStyle.fontSize;
                EditorStyles.label.alignment = labelStyle.alignment;
                EditorStyles.label.normal.textColor = labelStyle.normal.textColor;
                EditorStyles.label.hover.textColor = labelStyle.hover.textColor;
                EditorStyles.label.onHover.textColor = labelStyle.onHover.textColor;

                EditorStyles.textField.alignment = textFieldAnchor;
                EditorStyles.numberField.alignment = numberFieldAnchor;

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
            // set all styles to null for refresh
            var styleFIs = typeof(AFStyles)
                .GetFields(BindingFlags.NonPublic | BindingFlags.Static)
                .Where(t => t.Name.StartsWith("_") && t.FieldType == typeof(GUIStyle));
            foreach (var fieldInfo in styleFIs) { fieldInfo.SetValue(null, null); }
        }

        private static GUIStyle _button;

        public static GUIStyle Button
        {
            get
            {
                if (_button != null) return _button;
                var settings = AFEditorSettings.Instance;
                _button = new GUIStyle(GUI.skin.button);
                _button.normal.textColor = settings.buttonDefCol;
                _button.font = settings.font;
                _button.fontSize = settings.fontSize;
                return _button;
            }
        }

        private static GUIStyle _bigButton;

        public static GUIStyle BigButton
        {
            get
            {
                if (_bigButton != null) return _bigButton;
                var settings = AFEditorSettings.Instance;
                _bigButton = new GUIStyle(Button)
                {
                    fontSize = settings.bigFontSize,
                    richText = true
                };
                return _bigButton;
            }
        }

        private static GUIStyle _clearButton;

        public static GUIStyle ClearButton
        {
            get
            {
                if (_clearButton != null) return _clearButton;
                var settings = AFEditorSettings.Instance;
                _clearButton = new GUIStyle(Button)
                {
                    fontSize = settings.bigFontSize
                };
                _clearButton.normal.textColor = settings.buttonDefCol;
                _clearButton.alignment = TextAnchor.MiddleCenter;

                var tex = new Texture2D(2, 2);
                tex.SetPixels32(new Color32[] {
                    new(), new(),
                    new(), new()
                });
                tex.Apply(false);
                _clearButton.normal.background = _clearButton.hover.background =
                    _clearButton.onHover.background = tex;
                return _clearButton;
            }
        }

        private static GUIStyle _specialLabel;

        public static GUIStyle SpecialLabel
        {
            get
            {
                if (_specialLabel != null) return _specialLabel;
                var settings = AFEditorSettings.Instance;
                _specialLabel = new GUIStyle(GUI.skin.label)
                {
                    font = settings.font,
                    alignment = settings.labelAlignment,
                    fontSize = settings.fontSize
                };
                _specialLabel.normal.textColor = settings.labelCol;
                _specialLabel.hover.textColor = _specialLabel.onHover.textColor = settings.labelCol_Hover;
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
                var settings = AFEditorSettings.Instance;
                _label.font = settings.font;
                _label.alignment = settings.labelAlignment;
                _label.fontSize = settings.fontSize;
                return _label;
            }
        }

        private static GUIStyle _bigTextField;

        public static GUIStyle BigTextField
        {
            get
            {
                if (_bigTextField != null) return _bigTextField;
                var settings = AFEditorSettings.Instance;
                _bigTextField = new GUIStyle(EditorStyles.textField)
                {
                    font = settings.font,
                    alignment = settings.labelAlignment,
                    fontSize = settings.bigFontSize,
                    fixedHeight = 0
                };
                return _bigTextField;
            }
        }

        private static GUIStyle _popup;

        public static GUIStyle Popup
        {
            get
            {
                if (_popup != null) return _popup;
                var settings = AFEditorSettings.Instance;
                _popup = new GUIStyle(EditorStyles.popup)
                {
                    font = settings.font,
                    fontSize = settings.fontSize,
                    stretchHeight = true,
                    stretchWidth = true,
                    alignment = TextAnchor.MiddleCenter,
                    fixedHeight = 0
                };
                _popup.normal.textColor = _popup.hover.textColor = _popup.active.textColor =
                    _popup.focused.textColor = settings.popupCol;
                return _popup;
            }
        }

        private static readonly Dictionary<string, GUIContent[]> _cachePropertyPathToBoleanEnumContents = new();

        public static bool DrawBooleanEnum(Rect position, string optionTrue, string optionFalse, bool value,
            string tooltip, out bool result)
        {
            if (!_cachePropertyPathToBoleanEnumContents.TryGetValue(optionTrue, out var options))
            {
                options = new GUIContent[] {
                    new(optionTrue, tooltip), new(optionFalse, tooltip)
                };
                _cachePropertyPathToBoleanEnumContents[optionTrue] = options;
            }

            using var check = new EditorGUI.ChangeCheckScope();
            using (new EditorLabelWidth(0))
                result = EditorGUI.Popup(position, GUIContent.none, value ? 0 : 1, options, Popup) == 0;
            return check.changed;
        }

        public static void DrawBooleanEnum(Rect position, string optionTrue, string optionFalse, SerializedProperty property)
        {
            if (!_cachePropertyPathToBoleanEnumContents.TryGetValue(property.propertyPath, out var options))
            {
                options = new GUIContent[] {
                    new(optionTrue, property.tooltip), new(optionFalse, property.tooltip)
                };
                _cachePropertyPathToBoleanEnumContents[optionTrue] = options;
            }

            using (new EditorLabelWidth(0))
            {
                property.boolValue = EditorGUI.Popup(position, property.boolValue ? 0 : 1, options, Popup) == 0;
            }

        }


        public static void DrawHelpBox(Rect position, string message, MessageType messageType)
        {
            var GetHelpIcon =
                typeof(EditorGUIUtility).GetMethod("GetHelpIcon",
                    BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Default);
            if (GetHelpIcon != null)
            {
                var texture = (Texture)GetHelpIcon.Invoke(null, new object[] { messageType });
                var guiContent = new GUIContent(message, texture);

                var style = new GUIStyle(EditorStyles.helpBox)
                {
                    font = AFEditorSettings.Instance.font,
                    fontSize = AFEditorSettings.Instance.fontSize,
                    alignment = AFEditorSettings.Instance.labelAlignment,
                    wordWrap = false
                };

                using (new GuiColor(Color.yellow))
                    GUI.Label(position, guiContent, style);
            }
        }

        public static float Height => EditorGUIUtility.singleLineHeight;
        public static float BigHeight => EditorGUIUtility.singleLineHeight * 1.25f;
        public static float VerticalSpace => EditorGUIUtility.standardVerticalSpacing;
        public static Color BoxColor => AFEditorSettings.Instance.BoxCol;
        public static Color BoxColorDarker => AFEditorSettings.Instance.BoxColDarker;
    }
}
