using System;

namespace AnimFlex.Tweening
{
    public class EasingIdentifierAttribute : Attribute
    {
        public int ID;

        public EasingIdentifierAttribute(int id)
        {
            ID = id;
        }
    }
}