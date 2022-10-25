using System;
using System.Reflection;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    public abstract class CWaitUntil : Clip
    {
        public string valueName;
        public Component component;
        [Tooltip("In Seconds")]
        public float checkEvery = 0.1f;

#if UNITY_EDITOR
        internal abstract Type GetValueType();
#endif

	    public override void OnEnd() { }
    }
    public abstract class CWaitUntil<T> : CWaitUntil
    {
#if UNITY_EDITOR
        internal override Type GetValueType() => typeof(T);
#endif

        public T value;

        private float passedTime = 0;

        protected FieldInfo cachedFieldInfo;
        public FieldInfo GetFieldInfo()
        {
            if (cachedFieldInfo != null) return cachedFieldInfo;

            // else, find and cache the field info
            if(component is null)
                throw new Exception("Component is null");

            cachedFieldInfo = component.GetType().GetField(valueName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            if (cachedFieldInfo is null)
                throw new Exception($"{valueName} was not found on {component.name} of {component.gameObject} game object");
            if (cachedFieldInfo.FieldType != typeof(T))
                throw new System.Exception($"Field type mismatch. {valueName} is {cachedFieldInfo.FieldType}, but {typeof(T)} was expected.");

            return cachedFieldInfo;
        }

        protected override void OnStart()
        {
            GetFieldInfo();
            passedTime = 0;
        }

        public override bool hasTick() => true;

        public override void Tick(float deltaTime)
        {
	        passedTime += deltaTime;
            if(passedTime > checkEvery)
            {
	            passedTime = 0;
                if(IsEqual((T)cachedFieldInfo.GetValue(component), value))
                    PlayNext();
            }
        }

        protected abstract bool IsEqual(T a, T b);
    }
}
