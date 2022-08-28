using System;
using UnityEditor;
using UnityEngine;

namespace AnimFlex.Editor
{
    public static partial class Styles
    {
        public static void Refresh()
        {
            _button = _yellowButton = _label = _boolButtonON = _boolButtonOFF = _popup = null;
        }

        private static GUIStyle _button;
        public static GUIStyle Button
        {
            get
            {
                if (_button != null) return _button;
                _button = GUI.skin.button;
                _button.normal.textColor = StyleSettings.Instance.buttonDefCol;
                _button.font = StyleSettings.Instance.font;
                _button.fontSize = StyleSettings.Instance.fontSize;
                return _button;
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
                _label.normal.textColor = _label.hover.textColor = _label.active.textColor =
                    _label.focused.textColor = StyleSettings.Instance.labelCol;
                return _label;
            }
        }

        private static GUIStyle _boolButtonON;
        public static GUIStyle BoolButtonON
        {
            get
            {
                if (_boolButtonON != null) return _boolButtonON;
                _boolButtonON = new GUIStyle(GUI.skin.button);
                _boolButtonON.fontSize = StyleSettings.Instance.fontSize;
                _boolButtonON.normal.textColor = _boolButtonON.hover.textColor = _boolButtonON.active.textColor =
                    _boolButtonON.focused.textColor = StyleSettings.Instance.onButtonCol;
                return _boolButtonON;
            }
        }

        private static GUIStyle _boolButtonOFF;
        public static GUIStyle BoolButtonOFF
        {
            get
            {
                if (_boolButtonOFF != null) return _boolButtonOFF;
                _boolButtonOFF = new GUIStyle(GUI.skin.button);
                _boolButtonOFF.fontSize = StyleSettings.Instance.fontSize;
                _boolButtonOFF.normal.textColor = _boolButtonOFF.hover.textColor = _boolButtonOFF.active.textColor =
                    _boolButtonOFF.focused.textColor = StyleSettings.Instance.offButtonCol;
                return _boolButtonOFF;
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

        public static float Height => StyleSettings.Instance.height;
        public static float VerticalSpace => StyleSettings.Instance.verticalSpace;
        public static Color TweenerBoxColor => StyleSettings.Instance.tweeerBoxCol;
    }
}