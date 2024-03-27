using System;
using System.ComponentModel;
using AnimFlex.Sequencer.BindingSystem;
using TMPro;
using UnityEngine.UI;

namespace AnimFlex
{
    [Category("Text/TMP_Text")]
    [Serializable] public class SequenceBinder_TMP_Text : SequenceBinder<TMP_Text> { }
    [Category("Text/Text")]
    [Serializable] public class SequenceBinder_Text : SequenceBinder<Text> { }
}