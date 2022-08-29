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

      
    }
    public abstract class CWaitUntil<T> : CWaitUntil
    {
        public T value;
        
        private long startTicks;
        
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
            startTicks = 0; // so it checks in the first frame
        }

        public override bool hasTick() => true;

        public override void Tick()
        {
            var secondsPassed = (DateTime.UtcNow.Ticks - startTicks) * 0.000_000_1f;
            if(secondsPassed > checkEvery)
            {
                startTicks = DateTime.UtcNow.Ticks;
                if(IsEqual((T)cachedFieldInfo.GetValue(component), value))
                    PlayNext();
            }
        }
        
        protected abstract bool IsEqual(T a, T b);
    }
}