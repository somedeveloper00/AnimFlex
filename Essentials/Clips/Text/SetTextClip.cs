using System;
using System.ComponentModel;
using TMPro;
using UnityEngine.UI;

namespace AnimFlex.Sequencer.Clips
{
    [Category("Text/Set Text (TMP)")]
    [DisplayName("Set Text (TMP)")]
    [Serializable]
    public sealed class SetTextMeshClip : Clip
    {
        public TMP_Text text;
        public VariableFetch<string> format = new() { value = "{0}" };
        public VariableFetch<float> arg0;
        public VariableFetch<float> arg1;
        public VariableFetch<float> arg2;
        public VariableFetch<float> arg3;

        protected override void OnStart()
        {
            InjectVariable(ref format);
            InjectVariable(ref arg0);
            InjectVariable(ref arg1);
            InjectVariable(ref arg2);
            InjectVariable(ref arg3);
            text.SetText(format.value, arg0.value, arg1.value, arg2.value, arg3.value);
            PlayNext();
        }

        public override void OnEnd() { }
    }

    [Category("Text/Set Text")]
    [DisplayName("Set Text")]
    [Serializable]
    public sealed class SetTextClip : Clip
    {
        public Text text;
        public VariableFetch<string> format = new() { value = "{0}" };
        public VariableFetch<float> arg0;
        public VariableFetch<float> arg1;
        public VariableFetch<float> arg2;
        public VariableFetch<float> arg3;

        protected override void OnStart()
        {
            InjectVariable(ref format);
            InjectVariable(ref arg0);
            InjectVariable(ref arg1);
            InjectVariable(ref arg2);
            InjectVariable(ref arg3);
            text.text = string.Format(format.value, new float[] { arg0.value, arg1.value, arg2.value, arg3.value });
            PlayNext();
        }

        public override void OnEnd() { }
    }

    [Category("Text/Set Text Int (TMP)")]
    [DisplayName("Set Text Int (TMP)")]
    [Serializable]
    public sealed class SetTextIntMeshClip : Clip
    {
        public TMP_Text text;
        public VariableFetch<string> format = new() { value = "{0}" };
        public VariableFetch<int> arg0;
        public VariableFetch<int> arg1;
        public VariableFetch<int> arg2;
        public VariableFetch<int> arg3;

        protected override void OnStart()
        {
            InjectVariable(ref format);
            InjectVariable(ref arg0);
            InjectVariable(ref arg1);
            InjectVariable(ref arg2);
            InjectVariable(ref arg3);
            text.SetText(format.value, arg0.value, arg1.value, arg2.value, arg3.value);
            PlayNext();
        }

        public override void OnEnd() { }
    }

    [Category("Text/Set Text Int")]
    [DisplayName("Set Text Int")]
    [Serializable]
    public sealed class SetTextIntClip : Clip
    {
        public Text text;
        public VariableFetch<string> format = new() { value = "{0}" };
        public VariableFetch<int> arg0;
        public VariableFetch<int> arg1;
        public VariableFetch<int> arg2;
        public VariableFetch<int> arg3;

        protected override void OnStart()
        {
            InjectVariable(ref format);
            InjectVariable(ref arg0);
            InjectVariable(ref arg1);
            InjectVariable(ref arg2);
            InjectVariable(ref arg3);
            text.text = string.Format(format.value, new float[] { arg0.value, arg1.value, arg2.value, arg3.value });
            PlayNext();
        }

        public override void OnEnd() { }
    }
}