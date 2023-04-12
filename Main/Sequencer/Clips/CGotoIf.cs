using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Serialization;

namespace AnimFlex.Sequencer.Clips
{
    [Serializable]
    public abstract class CGotoIf : Clip
    {
        public Component component;
        
        [FormerlySerializedAs("valueName")]
        public string fieldName;
        public int indexIf;
        public int indexElse;

        internal abstract Type GetValueType();
        public override void OnEnd() { }
    }

    public abstract class CGotoIf<T> : CGotoIf
    {
        public T value;

        protected override void OnStart()
        {
            GetFieldInfo();

            if (IsEqual((T)cachedFieldInfo.GetValue(component), value))
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
    }
}
