using System;

namespace AnimFlex.Sequencer {
    
    /// <summary>
    /// Ensures the method is called when sequencer preview starts, if the SequenceAnim is on the same GameObject.
    /// It's Editor-Only, but produces no compilation error if used in a runtime method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    internal class CallMethodOnSequencePreviewAttribute : Attribute { }
}