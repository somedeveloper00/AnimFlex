using System;
using System.Reflection;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    public abstract class CGotoIfProperty : Clip
    {
        public Component component;
        public string propertyName;
        public int indexIf;
        public int indexElse;

        internal abstract Type GetValueType();
        public override void OnEnd() { }
    }

    public abstract class CGotoIfProperty<T> : CGotoIfProperty
    {
        public T value;

        protected override void OnStart()
        {
            GetPropertyInfo();

            if (IsEqual((T)cachedPropertyInfo.GetValue(component), value))
            {
                PlayIndex(indexIf);
            }
            else
            {
                PlayIndex(indexElse);
            }
        }

        internal override Type GetValueType() => typeof(T);


        protected abstract bool IsEqual(T a, T b);

        protected PropertyInfo cachedPropertyInfo;
        public PropertyInfo GetPropertyInfo()
        {
            if (cachedPropertyInfo != null) return cachedPropertyInfo;

            // else, find and cache the property info
            if(component is null)
                throw new Exception("Component is null");

            cachedPropertyInfo = component.GetType().GetProperty( propertyName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static |
                BindingFlags.GetProperty );

            if (cachedPropertyInfo is null)
                throw new Exception($"{propertyName} was not found on {component.name} of {component.gameObject} game object");
            if (cachedPropertyInfo.PropertyType != typeof(T))
                throw new System.Exception($"Property type mismatch. {propertyName} is {cachedPropertyInfo.PropertyType}, but {typeof(T)} was expected.");

            return cachedPropertyInfo;
        }
    }
}
