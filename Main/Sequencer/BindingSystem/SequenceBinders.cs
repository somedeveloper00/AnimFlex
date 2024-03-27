using System;
using System.ComponentModel;

namespace AnimFlex.Sequencer.BindingSystem {
    [Category("Primitive Types/int")]
    [Serializable] public sealed class SequenceBinder_Int : SequenceBinder<int> { }
    [Category("Primitive Types/float")]
    [Serializable] public sealed class SequenceBinder_Float : SequenceBinder<float> { }
    [Category("Primitive Types/long")]
    [Serializable] public sealed class SequenceBinder_Long : SequenceBinder<long> { }
    [Category("Primitive Types/double")]
    [Serializable] public sealed class SequenceBinder_Double : SequenceBinder<double> { }
    [Category("Primitive Types/bool")]
    [Serializable] public sealed class SequenceBinder_Bool : SequenceBinder<bool> { }
    [Category("Primitive Types/string")]
    [Serializable] public sealed class SequenceBinder_String : SequenceBinder<string> { }
    
    [Category("Unity Basics/Vector2")]
    [Serializable] public sealed class SequenceBinder_Vector2 : SequenceBinder<UnityEngine.Vector2> { }
    [Category("Unity Basics/Vector3")]
    [Serializable] public sealed class SequenceBinder_Vector3 : SequenceBinder<UnityEngine.Vector3> { }
    [Category("Unity Basics/Color")]
    [Serializable] public sealed class SequenceBinder_Color : SequenceBinder<UnityEngine.Color> { }
    [Category("Unity Basics/Animation Curve")]
    [Serializable] public sealed class SequenceBinder_AnimationCurve : SequenceBinder<UnityEngine.AnimationCurve> { }
    [Category("Unity Basics/Gradient")]
    [Serializable] public sealed class SequenceBinder_Gradient : SequenceBinder<UnityEngine.Gradient> { }
    
    [Category("Reference/Object")]
    [Serializable] public sealed class SequenceBinder_Object : SequenceBinder<UnityEngine.Object> { }
    [Category("Reference/Game Object")]
    [Serializable] public sealed class SequenceBinder_GameObject : SequenceBinder<UnityEngine.GameObject> { }
    [Category("Reference/Component")]
    [Serializable] public sealed class SequenceBinder_Component : SequenceBinder<UnityEngine.Component> { }
    [Category("Reference/Transform")]
    [Serializable] public sealed class SequenceBinder_Transform : SequenceBinder<UnityEngine.Transform> { }
    [Category("Reference/Material")]
    [Serializable] public sealed class SequenceBinder_Material : SequenceBinder<UnityEngine.Material> { }
    [Category("Reference/Scriptable Object")]
    [Serializable] public sealed class SequenceBinder_ScriptableObject : SequenceBinder<UnityEngine.ScriptableObject> { }
    [Category("Reference/Sprite")]
    [Serializable] public sealed class SequenceBinder_Sprite : SequenceBinder<UnityEngine.Sprite> { }
}