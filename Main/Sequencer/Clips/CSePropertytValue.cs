using System;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;
using Component = UnityEngine.Component;

namespace AnimFlex.Sequencer.Clips
{
    public abstract class CSetPropertyValue : Clip
    {
        public Component component;
        public string propertyName;

	    public override void OnEnd() { }

#if UNITY_EDITOR
        internal abstract Type GetValueType();
#endif
    }
    public abstract class CSetPropertyValue<T> : CSetPropertyValue
    {
        public T value;

        protected PropertyInfo cachedPropertyInfo;
        public PropertyInfo GetPropertyInfo()
        {
            if (cachedPropertyInfo != null) return cachedPropertyInfo;

            // else, find and cache the property info
            if(component is null)
                throw new Exception("Component is null");

            cachedPropertyInfo = component.GetType().GetProperty( propertyName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static |
                BindingFlags.SetProperty );

            if (cachedPropertyInfo is null)
                throw new Exception($"{propertyName} was not found on {component.name} of {component.gameObject} game object");
            if (cachedPropertyInfo.PropertyType != typeof(T))
                throw new System.Exception($"Property type mismatch. {propertyName} is {cachedPropertyInfo.PropertyType}, but {typeof(T)} was expected.");

            return cachedPropertyInfo;
        }
#if UNITY_EDITOR
        internal override Type GetValueType() => typeof(T);
#endif

        protected override void OnStart()
        {
            GetPropertyInfo().SetValue(component, value);
            PlayNext();
        }
    }
}
