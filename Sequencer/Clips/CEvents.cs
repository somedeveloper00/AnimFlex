using System.ComponentModel;
using UnityEngine.Events;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Void Event")]
    [Category("Events/Void")]
    public sealed class CEvent : Clip
    {
        public UnityEvent Event;

        protected override void OnStart()
        {
            Event.Invoke();
            PlayNext();
        }
        public override void OnEnd() { }
    }

    public abstract class CEvent<T> : Clip
    {
        public UnityEvent<T> Event;
        public T Value;

        protected override void OnStart()
        {
            Event.Invoke(Value);
            PlayNext();
        }
        public override void OnEnd() { }
    }

    public abstract class CEvent<T1, T2> : Clip
    {
        public UnityEvent<T1, T2> Event;
        public T1 Value1;
        public T2 Value2;

        protected override void OnStart()
        {
            Event.Invoke(Value1, Value2);
            PlayNext();
        }
        public override void OnEnd() { }
    }

    public abstract class CEvent<T1, T2, T3> : Clip
    {
        public UnityEvent<T1, T2, T3> Event;
        public T1 Value1;
        public T2 Value2;
        public T3 Value3;

        protected override void OnStart()
        {
            Event.Invoke(Value1, Value2, Value3);
            PlayNext();
        }
        public override void OnEnd() { }
    }

    [DisplayName("Event (string)")]
    [Category("Events/1 argument/(string)")]
    public sealed class CEventString : CEvent<string>
    {
    }

    [DisplayName("Event (int)")]
    [Category("Events/1 argument/(int)")]
    public sealed class CEventInt : CEvent<int>
    {
    }

    [DisplayName("Event (float)")]
    [Category("Events/1 argument/(float)")]
    public sealed class CEventFloat : CEvent<float>
    {
    }

    [DisplayName("Event (bool)")]
    [Category("Events/1 argument/(bool)")]
    public sealed class CEventBool : CEvent<bool>
    {
    }

    [DisplayName("Event (string, string)")]
    [Category("Events/2 arguments/(string, string)")]
    public sealed class CEventStringString : CEvent<string, string>
    {
    }

    [DisplayName("Event (string, int)")]
    [Category("Events/2 arguments/(string, int)")]
    public sealed class CEventStringInt : CEvent<string, int>
    {
    }

    [DisplayName("Event (string, float)")]
    [Category("Events/2 arguments/(string, float)")]
    public sealed class CEventStringFloat : CEvent<string, float>
    {
    }

    [DisplayName("Event (string, bool)")]
    [Category("Events/2 arguments/(string, bool)")]
    public sealed class CEventStringBool : CEvent<string, bool>
    {
    }

    [DisplayName("Event (int, int)")]
    [Category("Events/2 arguments/(int, int)")]
    public sealed class CEventIntInt : CEvent<int, int>
    {
    }

    [DisplayName("Event (int, float)")]
    [Category("Events/2 arguments/(int, float)")]
    public sealed class CEventIntFloat : CEvent<int, float>
    {
    }

    [DisplayName("Event (int, bool)")]
    [Category("Events/2 arguments/(int, bool)")]
    public sealed class CEventIntBool : CEvent<int, bool>
    {
    }

    [DisplayName("Event (float, float)")]
    [Category("Events/2 arguments/(float, float)")]
    public sealed class CEventFloatFloat : CEvent<float, float>
    {
    }

    [DisplayName("Event (float, bool)")]
    [Category("Events/2 arguments/(float, bool)")]
    public sealed class CEventFloatBool : CEvent<float, bool>
    {
    }

    [DisplayName("Event (bool, bool)")]
    [Category("Events/2 arguments/(bool, bool)")]
    public sealed class CEventBoolBool : CEvent<bool, bool>
    {
    }

    [DisplayName("Event (string, string, string)")]
    [Category("Events/3 arguments/(string...)/(string, string, string)")]
    public sealed class CEventStringStringString : CEvent<string, string, string>
    {
    }

    [DisplayName("Event (string, string, int)")]
    [Category("Events/3 arguments/(string...)/(string, string, int)")]
    public sealed class CEventStringStringInt : CEvent<string, string, int>
    {
    }

    [DisplayName("Event (string, string, float)")]
    [Category("Events/3 arguments/(string...)/(string, string, float)")]
    public sealed class CEventStringStringFloat : CEvent<string, string, float>
    {
    }

    [DisplayName("Event (string, string, bool)")]
    [Category("Events/3 arguments/(string...)/(string, string, bool)")]
    public sealed class CEventStringStringBool : CEvent<string, string, bool>
    {
    }

    [DisplayName("Event (string, int, int)")]
    [Category("Events/3 arguments/(string...)/(string, int, int)")]
    public sealed class CEventStringIntInt : CEvent<string, int, int>
    {
    }

    [DisplayName("Event (string, int, float)")]
    [Category("Events/3 arguments/(string...)/(string, int, float)")]
    public sealed class CEventStringIntFloat : CEvent<string, int, float>
    {
    }

    [DisplayName("Event (string, int, bool)")]
    [Category("Events/3 arguments/(string...)/(string, int, bool)")]
    public sealed class CEventStringIntBool : CEvent<string, int, bool>
    {
    }

    [DisplayName("Event (string, float, float)")]
    [Category("Events/3 arguments/(string...)/(string, float, float)")]
    public sealed class CEventStringFloatFloat : CEvent<string, float, float>
    {
    }

    [DisplayName("Event (string, float, bool)")]
    [Category("Events/3 arguments/(string...)/(string, float, bool)")]
    public sealed class CEventStringFloatBool : CEvent<string, float, bool>
    {
    }

    [DisplayName("Event (string, bool, bool)")]
    [Category("Events/3 arguments/(string...)/(string, bool, bool)")]
    public sealed class CEventStringBoolBool : CEvent<string, bool, bool>
    {
    }

    [DisplayName("Event (int, string, string)")]
    [Category("Events/3 arguments/(int...)/(int, string, string)")]
    public sealed class CEventIntStringString : CEvent<int, string, string>
    {
    }

    [DisplayName("Event (int, string, int)")]
    [Category("Events/3 arguments/(int...)/(int, string, int)")]
    public sealed class CEventIntStringInt : CEvent<int, string, int>
    {
    }

    [DisplayName("Event (int, string, float)")]
    [Category("Events/3 arguments/(int...)/(int, string, float)")]
    public sealed class CEventIntStringFloat : CEvent<int, string, float>
    {
    }

    [DisplayName("Event (int, string, bool)")]
    [Category("Events/3 arguments/(int...)/(int, string, bool)")]
    public sealed class CEventIntStringBool : CEvent<int, string, bool>
    {
    }

    [DisplayName("Event (int, int, int)")]
    [Category("Events/3 arguments/(int...)/(int, int, int)")]
    public sealed class CEventIntIntInt : CEvent<int, int, int>
    {
    }

    [DisplayName("Event (int, int, float)")]
    [Category("Events/3 arguments/(int...)/(int, int, float)")]
    public sealed class CEventIntIntFloat : CEvent<int, int, float>
    {
    }

    [DisplayName("Event (int, int, bool)")]
    [Category("Events/3 arguments/(int...)/(int, int, bool)")]
    public sealed class CEventIntIntBool : CEvent<int, int, bool>
    {
    }

    [DisplayName("Event (int, float, float)")]
    [Category("Events/3 arguments/(int...)/(int, float, float)")]
    public sealed class CEventIntFloatFloat : CEvent<int, float, float>
    {
    }

    [DisplayName("Event (int, float, bool)")]
    [Category("Events/3 arguments/(int...)/(int, float, bool)")]
    public sealed class CEventIntFloatBool : CEvent<int, float, bool>
    {
    }

    [DisplayName("Event (int, bool, bool)")]
    [Category("Events/3 arguments/(int...)/(int, bool, bool)")]
    public sealed class CEventIntBoolBool : CEvent<int, bool, bool>
    {
    }

    [DisplayName("Event (float, string, string)")]
    [Category("Events/3 arguments/(float...)/(float, string, string)")]
    public sealed class CEventFloatStringString : CEvent<float, string, string>
    {
    }

    [DisplayName("Event (float, string, int)")]
    [Category("Events/3 arguments/(float...)/(float, string, int)")]
    public sealed class CEventFloatStringInt : CEvent<float, string, int>
    {
    }

    [DisplayName("Event (float, string, float)")]
    [Category("Events/3 arguments/(float...)/(float, string, float)")]
    public sealed class CEventFloatStringFloat : CEvent<float, string, float>
    {
    }

    [DisplayName("Event (float, string, bool)")]
    [Category("Events/3 arguments/(float...)/(float, string, bool)")]
    public sealed class CEventFloatStringBool : CEvent<float, string, bool>
    {
    }

    [DisplayName("Event (float, int, int)")]
    [Category("Events/3 arguments/(float...)/(float, int, int)")]
    public sealed class CEventFloatIntInt : CEvent<float, int, int>
    {
    }

    [DisplayName("Event (float, int, float)")]
    [Category("Events/3 arguments/(float...)/(float, int, float)")]
    public sealed class CEventFloatIntFloat : CEvent<float, int, float>
    {
    }

    [DisplayName("Event (float, int, bool)")]
    [Category("Events/3 arguments/(float...)/(float, int, bool)")]
    public sealed class CEventFloatIntBool : CEvent<float, int, bool>
    {
    }

    [DisplayName("Event (float, float, float)")]
    [Category("Events/3 arguments/(float...)/(float, float, float)")]
    public sealed class CEventFloatFloatFloat : CEvent<float, float, float>
    {
    }

    [DisplayName("Event (float, float, bool)")]
    [Category("Events/3 arguments/(float...)/(float, float, bool)")]
    public sealed class CEventFloatFloatBool : CEvent<float, float, bool>
    {
    }

    [DisplayName("Event (float, bool, bool)")]
    [Category("Events/3 arguments/(float...)/(float, bool, bool)")]
    public sealed class CEventFloatBoolBool : CEvent<float, bool, bool>
    {
    }

    [DisplayName("Event (bool, string, string)")]
    [Category("Events/3 arguments/(bool...)/(bool, string, string)")]
    public sealed class CEventBoolStringString : CEvent<bool, string, string>
    {
    }

    [DisplayName("Event (bool, string, int)")]
    [Category("Events/3 arguments/(bool...)/(bool, string, int)")]
    public sealed class CEventBoolStringInt : CEvent<bool, string, int>
    {
    }

    [DisplayName("Event (bool, string, float)")]
    [Category("Events/3 arguments/(bool...)/(bool, string, float)")]
    public sealed class CEventBoolStringFloat : CEvent<bool, string, float>
    {
    }

    [DisplayName("Event (bool, string, bool)")]
    [Category("Events/3 arguments/(bool...)/(bool, string, bool)")]
    public sealed class CEventBoolStringBool : CEvent<bool, string, bool>
    {
    }

    [DisplayName("Event (bool, int, int)")]
    [Category("Events/3 arguments/(bool...)/(bool, int, int)")]
    public sealed class CEventBoolIntInt : CEvent<bool, int, int>
    {
    }

    [DisplayName("Event (bool, int, float)")]
    [Category("Events/3 arguments/(bool...)/(bool, int, float)")]
    public sealed class CEventBoolIntFloat : CEvent<bool, int, float>
    {
    }

    [DisplayName("Event (bool, int, bool)")]
    [Category("Events/3 arguments/(bool...)/(bool, int, bool)")]
    public sealed class CEventBoolIntBool : CEvent<bool, int, bool>
    {
    }

    [DisplayName("Event (bool, float, float)")]
    [Category("Events/3 arguments/(bool...)/(bool, float, float)")]
    public sealed class CEventBoolFloatFloat : CEvent<bool, float, float>
    {
    }

    [DisplayName("Event (bool, float, bool)")]
    [Category("Events/3 arguments/(bool...)/(bool, float, bool)")]
    public sealed class CEventBoolFloatBool : CEvent<bool, float, bool>
    {
    }

    [DisplayName("Event (bool, bool, bool)")]
    [Category("Events/3 arguments/(bool...)/(bool, bool, bool)")]
    public sealed class CEventBoolBoolBool : CEvent<bool, bool, bool>
    {
    }
}
