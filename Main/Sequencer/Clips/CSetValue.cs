using System;
using System.Reflection;
using UnityEngine.Serialization;
using Component = UnityEngine.Component;

namespace AnimFlex.Sequencer.Clips
{
    [Serializable]
    public abstract class CSetValue : Clip
    {
        public Component component;
        
        [FormerlySerializedAs( "valueName" )] 
        public string fieldName;

	    public override void OnEnd() { }

#if UNITY_EDITOR
        internal abstract Type GetValueType();
#endif
    }
    public abstract class CSetValue<T> : CSetValue
    {
        public T value;

        protected FieldInfo cachedFieldInfo;
        public FieldInfo GetFieldInfo()
        {
            if (cachedFieldInfo != null) return cachedFieldInfo;

            // else, find and cache the field info
            if(component is null)
                throw new Exception("Component is null");

            cachedFieldInfo = component.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            if (cachedFieldInfo is null)
                throw new Exception($"{fieldName} was not found on {component.name} of {component.gameObject} game object");
            if (cachedFieldInfo.FieldType != typeof(T))
                throw new System.Exception($"Field type mismatch. {fieldName} is {cachedFieldInfo.FieldType}, but {typeof(T)} was expected.");

            return cachedFieldInfo;
        }
#if UNITY_EDITOR
        internal override Type GetValueType() => typeof(T);
#endif

        protected override void OnStart()
        {
            GetFieldInfo().SetValue(component, value);
            PlayNext();
        }
    }
}
