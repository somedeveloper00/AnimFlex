using System.ComponentModel;
using UnityEngine.Events;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Events/Void")]
    public sealed class CEvent : Clip
    {
        public UnityEvent Event;

        protected override void OnStart()
        {
            Event.Invoke();
            End();
        }

    }

    public abstract class CEvent<T> : Clip
    {
        public UnityEvent<T> Event;
        public T Value;

        protected override void OnStart()
        {
            Event.Invoke(Value);
            End();
        }
    }

    public abstract class CEvent<T1, T2> : Clip
    {
        public UnityEvent<T1, T2> Event;
        public T1 Value1;
        public T2 Value2;

        protected override void OnStart()
        {
            Event.Invoke(Value1, Value2);
            End();
        }
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
            End();
        }
    }

    [DisplayName("Events/1 argument/(string)")]
    public sealed class CEventString : CEvent<string>
    {
    }

    [DisplayName("Events/1 argument/(int)")]
    public sealed class CEventInt : CEvent<int>
    {
    }

    [DisplayName("Events/1 argument/(float)")]
    public sealed class CEventFloat : CEvent<float>
    {
    }

    [DisplayName("Events/1 argument/(bool)")]
    public sealed class CEventBool : CEvent<bool>
    {
    }

    [DisplayName("Events/2 arguments/(string, string)")]
    public sealed class CEventStringString : CEvent<string, string>
    {
    }

    [DisplayName("Events/2 arguments/(string, int)")]
    public sealed class CEventStringInt : CEvent<string, int>
    {
    }

    [DisplayName("Events/2 arguments/(string, float)")]
    public sealed class CEventStringFloat : CEvent<string, float>
    {
    }

    [DisplayName("Events/2 arguments/(string, bool)")]
    public sealed class CEventStringBool : CEvent<string, bool>
    {
    }

    [DisplayName("Events/2 arguments/(int, int)")]
    public sealed class CEventIntInt : CEvent<int, int>
    {
    }

    [DisplayName("Events/2 arguments/(int, float)")]
    public sealed class CEventIntFloat : CEvent<int, float>
    {
    }

    [DisplayName("Events/2 arguments/(int, bool)")]
    public sealed class CEventIntBool : CEvent<int, bool>
    {
    }

    [DisplayName("Events/2 arguments/(float, float)")]
    public sealed class CEventFloatFloat : CEvent<float, float>
    {
    }

    [DisplayName("Events/2 arguments/(float, bool)")]
    public sealed class CEventFloatBool : CEvent<float, bool>
    {
    }

    [DisplayName("Events/2 arguments/(bool, bool)")]
    public sealed class CEventBoolBool : CEvent<bool, bool>
    {
    }

    [DisplayName("Events/3 arguments/(string...)/(string, string, string)")]
    public sealed class CEventStringStringString : CEvent<string, string, string>
    {
    }

    [DisplayName("Events/3 arguments/(string...)/(string, string, int)")]
    public sealed class CEventStringStringInt : CEvent<string, string, int>
    {
    }

    [DisplayName("Events/3 arguments/(string...)/(string, string, float)")]
    public sealed class CEventStringStringFloat : CEvent<string, string, float>
    {
    }

    [DisplayName("Events/3 arguments/(string...)/(string, string, bool)")]
    public sealed class CEventStringStringBool : CEvent<string, string, bool>
    {
    }

    [DisplayName("Events/3 arguments/(string...)/(string, int, int)")]
    public sealed class CEventStringIntInt : CEvent<string, int, int>
    {
    }

    [DisplayName("Events/3 arguments/(string...)/(string, int, float)")]
    public sealed class CEventStringIntFloat : CEvent<string, int, float>
    {
    }

    [DisplayName("Events/3 arguments/(string...)/(string, int, bool)")]
    public sealed class CEventStringIntBool : CEvent<string, int, bool>
    {
    }

    [DisplayName("Events/3 arguments/(string...)/(string, float, float)")]
    public sealed class CEventStringFloatFloat : CEvent<string, float, float>
    {
    }

    [DisplayName("Events/3 arguments/(string...)/(string, float, bool)")]
    public sealed class CEventStringFloatBool : CEvent<string, float, bool>
    {
    }

    [DisplayName("Events/3 arguments/(string...)/(string, bool, bool)")]
    public sealed class CEventStringBoolBool : CEvent<string, bool, bool>
    {
    }

    [DisplayName("Events/3 arguments/(int...)/(int, string, string)")]
    public sealed class CEventIntStringString : CEvent<int, string, string>
    {
    }

    [DisplayName("Events/3 arguments/(int...)/(int, string, int)")]
    public sealed class CEventIntStringInt : CEvent<int, string, int>
    {
    }

    [DisplayName("Events/3 arguments/(int...)/(int, string, float)")]
    public sealed class CEventIntStringFloat : CEvent<int, string, float>
    {
    }

    [DisplayName("Events/3 arguments/(int...)/(int, string, bool)")]
    public sealed class CEventIntStringBool : CEvent<int, string, bool>
    {
    }

    [DisplayName("Events/3 arguments/(int...)/(int, int, int)")]
    public sealed class CEventIntIntInt : CEvent<int, int, int>
    {
    }

    [DisplayName("Events/3 arguments/(int...)/(int, int, float)")]
    public sealed class CEventIntIntFloat : CEvent<int, int, float>
    {
    }

    [DisplayName("Events/3 arguments/(int...)/(int, int, bool)")]
    public sealed class CEventIntIntBool : CEvent<int, int, bool>
    {
    }

    [DisplayName("Events/3 arguments/(int...)/(int, float, float)")]
    public sealed class CEventIntFloatFloat : CEvent<int, float, float>
    {
    }

    [DisplayName("Events/3 arguments/(int...)/(int, float, bool)")]
    public sealed class CEventIntFloatBool : CEvent<int, float, bool>
    {
    }

    [DisplayName("Events/3 arguments/(int...)/(int, bool, bool)")]
    public sealed class CEventIntBoolBool : CEvent<int, bool, bool>
    {
    }

    [DisplayName("Events/3 arguments/(float...)/(float, string, string)")]
    public sealed class CEventFloatStringString : CEvent<float, string, string>
    {
    }

    [DisplayName("Events/3 arguments/(float...)/(float, string, int)")]
    public sealed class CEventFloatStringInt : CEvent<float, string, int>
    {
    }

    [DisplayName("Events/3 arguments/(float...)/(float, string, float)")]
    public sealed class CEventFloatStringFloat : CEvent<float, string, float>
    {
    }

    [DisplayName("Events/3 arguments/(float...)/(float, string, bool)")]
    public sealed class CEventFloatStringBool : CEvent<float, string, bool>
    {
    }

    [DisplayName("Events/3 arguments/(float...)/(float, int, int)")]
    public sealed class CEventFloatIntInt : CEvent<float, int, int>
    {
    }

    [DisplayName("Events/3 arguments/(float...)/(float, int, float)")]
    public sealed class CEventFloatIntFloat : CEvent<float, int, float>
    {
    }

    [DisplayName("Events/3 arguments/(float...)/(float, int, bool)")]
    public sealed class CEventFloatIntBool : CEvent<float, int, bool>
    {
    }

    [DisplayName("Events/3 arguments/(float...)/(float, float, float)")]
    public sealed class CEventFloatFloatFloat : CEvent<float, float, float>
    {
    }

    [DisplayName("Events/3 arguments/(float...)/(float, float, bool)")]
    public sealed class CEventFloatFloatBool : CEvent<float, float, bool>
    {
    }

    [DisplayName("Events/3 arguments/(float...)/(float, bool, bool)")]
    public sealed class CEventFloatBoolBool : CEvent<float, bool, bool>
    {
    }

    [DisplayName("Events/3 arguments/(bool...)/(bool, string, string)")]
    public sealed class CEventBoolStringString : CEvent<bool, string, string>
    {
    }

    [DisplayName("Events/3 arguments/(bool...)/(bool, string, int)")]
    public sealed class CEventBoolStringInt : CEvent<bool, string, int>
    {
    }

    [DisplayName("Events/3 arguments/(bool...)/(bool, string, float)")]
    public sealed class CEventBoolStringFloat : CEvent<bool, string, float>
    {
    }

    [DisplayName("Events/3 arguments/(bool...)/(bool, string, bool)")]
    public sealed class CEventBoolStringBool : CEvent<bool, string, bool>
    {
    }

    [DisplayName("Events/3 arguments/(bool...)/(bool, int, int)")]
    public sealed class CEventBoolIntInt : CEvent<bool, int, int>
    {
    }

    [DisplayName("Events/3 arguments/(bool...)/(bool, int, float)")]
    public sealed class CEventBoolIntFloat : CEvent<bool, int, float>
    {
    }

    [DisplayName("Events/3 arguments/(bool...)/(bool, int, bool)")]
    public sealed class CEventBoolIntBool : CEvent<bool, int, bool>
    {
    }

    [DisplayName("Events/3 arguments/(bool...)/(bool, float, float)")]
    public sealed class CEventBoolFloatFloat : CEvent<bool, float, float>
    {
    }

    [DisplayName("Events/3 arguments/(bool...)/(bool, float, bool)")]
    public sealed class CEventBoolFloatBool : CEvent<bool, float, bool>
    {
    }

    [DisplayName("Events/3 arguments/(bool...)/(bool, bool, bool)")]
    public sealed class CEventBoolBoolBool : CEvent<bool, bool, bool>
    {
    }
}