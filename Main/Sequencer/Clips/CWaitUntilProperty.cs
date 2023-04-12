using System;
using System.Reflection;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    public abstract class CWaitUntilProperty : Clip
    {
        public string propertyName;
        public Component component;
        [Tooltip("In Seconds")]
        public float checkEvery = 0.1f;

#if UNITY_EDITOR
        internal abstract Type GetValueType();
#endif

	    public override void OnEnd() { }
    }
    public abstract class CWaitUntilProperty<T> : CWaitUntilProperty
    {
#if UNITY_EDITOR
        internal override Type GetValueType() => typeof(T);
#endif

        public T value;

        private float passedTime = 0;

        protected PropertyInfo cachedPropertyInfo;

        public PropertyInfo GetPropertyInfo() {
            if (cachedPropertyInfo != null) return cachedPropertyInfo;

            // else, find and cache the property info
            if (component is null)
                throw new Exception( "Component is null" );

            cachedPropertyInfo = component.GetType().GetProperty( propertyName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static |
                BindingFlags.SetProperty );

            if (cachedPropertyInfo is null)
                throw new Exception(
                    $"{propertyName} was not found on {component.name} of {component.gameObject} game object" );
            if (cachedPropertyInfo.PropertyType != typeof(T))
                throw new System.Exception(
                    $"Property type mismatch. {propertyName} is {cachedPropertyInfo.PropertyType}, but {typeof(T)} was expected." );

            return cachedPropertyInfo;
        }

        protected override void OnStart()
        {
            GetPropertyInfo();
            passedTime = 0;
        }

        public override bool hasTick() => true;

        public override void Tick(float deltaTime)
        {
	        passedTime += deltaTime;
            if(passedTime > checkEvery)
            {
	            passedTime = 0;
                if(IsEqual((T)cachedPropertyInfo.GetValue(component), value))
                    PlayNext();
            }
        }

        protected abstract bool IsEqual(T a, T b);
    }
}
