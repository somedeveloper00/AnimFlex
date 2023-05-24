using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor {
    public class AFStyles {
        
        public class EditorLabelWidth : IDisposable {
            float width;

            public EditorLabelWidth(float width = 10) {
                this.width = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = width;
            }
            public void Dispose() => EditorGUIUtility.labelWidth = width;
        }

        public class EditorFieldMinWidth : IDisposable {
            float oldWidth;

            public EditorFieldMinWidth(Rect pos, float width = 10) {
                oldWidth = EditorGUIUtility.labelWidth;
                if (pos.width - EditorGUIUtility.labelWidth < width)
                    EditorGUIUtility.labelWidth = pos.width - width;
            }

            public void Dispose() {
                EditorGUIUtility.labelWidth = oldWidth;
            }
        }

        public class GuiColor : IDisposable {
            Color oldCol;

            public GuiColor(Color color) {
                oldCol = GUI.color;
                GUI.color = color;
            }

            public void Dispose() => GUI.color = oldCol;
        }

        public class GuiBackgroundColor : IDisposable {
            Color oldCol;

            public GuiBackgroundColor(Color color) {
                oldCol = GUI.backgroundColor;
                GUI.backgroundColor = color;
            }

            public void Dispose() => GUI.backgroundColor = oldCol;
        }

        public class GuiForceActive : IDisposable {
            bool wasEnabled;

            public GuiForceActive(bool disableInPlaymode = true) {
                wasEnabled = GUI.enabled;
                GUI.enabled = !disableInPlaymode || !Application.isPlaying;
            }

            public void Dispose() => GUI.enabled = wasEnabled;
        }

        public class StyledGuiScope : IDisposable {
            GUIStyle labelStyle;
            GUIStyle largeLabelStyle;
            GUIStyle popupStyle;
            UnityEditor.Editor _editor;
            TextAnchor textFieldAnchor;
            TextAnchor numberFieldAnchor;

            /// <summary>
            /// if editor is null, it won't automatically repaint
            /// </summary>
            public StyledGuiScope(UnityEditor.Editor editor = null) {
                _editor = editor;
                labelStyle = new GUIStyle( EditorStyles.label );
                largeLabelStyle = new GUIStyle( EditorStyles.largeLabel );
                popupStyle = new GUIStyle( EditorStyles.popup );
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

            public void Dispose() {
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

                if (_editor != null && AFEditorSettings.Instance.repaintEveryFrame) { _editor.Repaint(); }
            }

        }

        public static void Refresh() {
            // set all styles to null for refresh
            var styleFIs = typeof(AFStyles)
                .GetFields( BindingFlags.NonPublic | BindingFlags.Static )
                .Where( t => t.Name.StartsWith( "_" ) && t.FieldType == typeof(GUIStyle) );
            foreach (var fieldInfo in styleFIs) { fieldInfo.SetValue( null, null ); }
        }

        static GUIStyle _button;

        public static GUIStyle Button {
            get {
                if (_button != null) return _button;
                var settings = AFEditorSettings.Instance;
                _button = new GUIStyle( GUI.skin.button );
                _button.normal.textColor = settings.buttonDefCol;
                _button.font = settings.font;
                _button.fontSize = settings.fontSize;
                return _button;
            }
        }

        static GUIStyle _bigButton;

        public static GUIStyle BigButton {
            get {
                if (_bigButton != null) return _bigButton;
                var settings = AFEditorSettings.Instance;
                _bigButton = new GUIStyle( Button );
                _bigButton.fontSize = settings.bigFontSize;
                _bigButton.richText = true;
                return _bigButton;
            }
        }

        static GUIStyle _clearButton;

        public static GUIStyle ClearButton {
            get {
                if (_clearButton != null) return _clearButton;
                var settings = AFEditorSettings.Instance;
                _clearButton = new GUIStyle( EditorStyles.miniButtonRight );
                _clearButton.fontSize = settings.bigFontSize;
                _clearButton.normal.textColor = settings.buttonDefCol;
                _clearButton.alignment = TextAnchor.MiddleCenter;
                return _clearButton;
            }
        }

        static GUIStyle _specialLabel;

        public static GUIStyle SpecialLabel {
            get {
                if (_specialLabel != null) return _specialLabel;
                var settings = AFEditorSettings.Instance;
                _specialLabel = new GUIStyle( GUI.skin.label );
                _specialLabel.font = settings.font;
                _specialLabel.alignment = settings.labelAlignment;
                _specialLabel.fontSize = settings.fontSize;
                _specialLabel.normal.textColor = settings.labelCol;
                _specialLabel.hover.textColor = _specialLabel.onHover.textColor = settings.labelCol_Hover;
                return _specialLabel;
            }
        }

        static GUIStyle _label;

        public static GUIStyle Label {
            get {
                if (_label != null) return _label;
                _label = new GUIStyle( GUI.skin.label );
                var settings = AFEditorSettings.Instance;
                _label.font = settings.font;
                _label.alignment = settings.labelAlignment;
                _label.fontSize = settings.fontSize;
                return _label;
            }
        }

        static GUIStyle _bigTextField;

        public static GUIStyle BigTextField {
            get {
                if (_bigTextField != null) return _bigTextField;
                var settings = AFEditorSettings.Instance;
                _bigTextField = new GUIStyle( EditorStyles.textField );
                _bigTextField.font = settings.font;
                _bigTextField.alignment = settings.labelAlignment;
                _bigTextField.fontSize = settings.bigFontSize;
                _bigTextField.fixedHeight = 0;
                return _bigTextField;
            }
        }

        static GUIStyle _popup;

        public static GUIStyle Popup {
            get {
                if (_popup != null) return _popup;
                var settings = AFEditorSettings.Instance;
                _popup = new GUIStyle( EditorStyles.popup );
                _popup.font = settings.font;
                _popup.fontSize = settings.fontSize;
                _popup.stretchHeight = true;
                _popup.stretchWidth = true;
                _popup.alignment = TextAnchor.MiddleCenter;
                _popup.fixedHeight = 0;
                _popup.normal.textColor = _popup.hover.textColor = _popup.active.textColor =
                    _popup.focused.textColor = settings.popupCol;
                return _popup;
            }
        }

        public static bool DrawBooleanEnum(Rect position, string optionTrue, string optionFalse, bool value,
            string tooltip, out bool result) {
            var options = new GUIContent[] {
                new GUIContent( optionTrue, tooltip ), new GUIContent( optionFalse, tooltip )
            };
            using var check = new EditorGUI.ChangeCheckScope();
            using (new EditorLabelWidth( 0 ))
                result = EditorGUI.Popup( position, GUIContent.none, value ? 0 : 1, options, Popup ) == 0;
            return check.changed;
        }

        public static void DrawBooleanEnum(Rect position, string optionTrue, string optionFalse,
            SerializedProperty property) {
            var options = new GUIContent[] {
                new GUIContent( optionTrue, property.tooltip ), new GUIContent( optionFalse, property.tooltip )
            };

            using (new EditorLabelWidth( 0 )) {
                property.boolValue = EditorGUI.Popup( position, property.boolValue ? 0 : 1, options, Popup ) == 0;
            }

        }


        public static void DrawHelpBox(Rect position, string message, MessageType messageType) {
            var GetHelpIcon =
                typeof(EditorGUIUtility).GetMethod( "GetHelpIcon",
                    BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Default );
            if (GetHelpIcon != null) {
                var texture = (Texture)GetHelpIcon.Invoke( null, new object[] { messageType } );
                var guiContent = new GUIContent( message, texture );

                var style = new GUIStyle( EditorStyles.helpBox );
                style.font = AFEditorSettings.Instance.font;
                style.fontSize = AFEditorSettings.Instance.fontSize;
                style.alignment = AFEditorSettings.Instance.labelAlignment;
                style.wordWrap = false;

                using (new GuiColor( Color.yellow ))
                    GUI.Label( position, guiContent, style );
            }
        }

        public static float Height => AFEditorSettings.Instance.height;
        public static float BigHeight => AFEditorSettings.Instance.bigHeight;
        public static float VerticalSpace => AFEditorSettings.Instance.verticalSpace;
        public static Color BoxColor => AFEditorSettings.Instance.BoxCol;
        public static Color BoxColorDarker => AFEditorSettings.Instance.BoxColDarker;
    }
}
