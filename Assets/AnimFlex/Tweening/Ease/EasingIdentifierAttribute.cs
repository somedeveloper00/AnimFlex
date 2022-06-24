using System;
using UnityEditor.iOS.Xcode;

namespace AnimFlex.Tweening
{
    public class EasingIdentifierAttribute : Attribute
    {
        public int ID;
        public string DisplayName;

        public EasingIdentifierAttribute(int id, string displayName)
        {
            this.DisplayName = displayName;
            ID = id;
        }
    }
}