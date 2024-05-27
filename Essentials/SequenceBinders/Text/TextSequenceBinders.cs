using System;
using System.ComponentModel;
using AnimFlex.Sequencer.BindingSystem;
using TMPro;
using UnityEngine.UI;

namespace AnimFlex.Sequencer.Clips
{
    [Category("Text/TMP_Text")]
    [Serializable] public sealed class SequenceBinder_TMP_Text : SequenceBinder<TMP_Text> { }
    
    [Category("Text/Text")]
    [Serializable] public sealed class SequenceBinder_Text : SequenceBinder<Text> { }
}