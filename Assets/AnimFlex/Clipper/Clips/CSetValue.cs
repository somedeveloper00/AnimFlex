using System;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;
using Component = UnityEngine.Component;

namespace AnimFlex.Clipper.Clips
{
    public abstract class CSetValue : Clip
    {
        public Component component;
        public string valueName;
    }
    public abstract class CSetValue<T> : CSetValue
    {
        public T newValue;

        private FieldInfo _cachedFieldInfo;

        public FieldInfo GetFieldInfo()
        {
            if (_cachedFieldInfo != null) return _cachedFieldInfo;
            
            // else, find and cache the field info
            if(component is null)
                throw new Exception("Component is null");
            
            _cachedFieldInfo = component.GetType().GetField(valueName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            
            if (_cachedFieldInfo is null)
                throw new Exception($"{valueName} was not found on {component.name} of {component.gameObject} game object");
            if (_cachedFieldInfo.FieldType != typeof(T))
                throw new System.Exception($"Field type mismatch. {valueName} is {_cachedFieldInfo.FieldType}, but {typeof(T)} was expected.");
            
            return _cachedFieldInfo;
        }

        protected override void OnStart()
        {
            GetFieldInfo().SetValue(component, newValue);
            End();
        }
    }
}