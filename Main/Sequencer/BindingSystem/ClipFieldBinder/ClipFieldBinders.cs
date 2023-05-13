using System;
using System.ComponentModel;

namespace AnimFlex.Sequencer.Binding {
    [Category("Primitive Types/int")]
    [Serializable] public sealed class ClipFieldBinderInt : ClipFieldBinder<int> { }
    [Category("Primitive Types/uint")]
    [Serializable] public sealed class ClipFieldBinderUInt : ClipFieldBinder<uint> { }
    [Category("Primitive Types/float")]
    [Serializable] public sealed class ClipFieldBinderFloat : ClipFieldBinder<float> { }
    [Category("Primitive Types/long")]
    [Serializable] public sealed class ClipFieldBinderLong : ClipFieldBinder<long> { }
    [Category("Primitive Types/ulong")]
    [Serializable] public sealed class ClipFieldBinderUDouble : ClipFieldBinder<ulong> { }
    [Category("Primitive Types/double")]
    [Serializable] public sealed class ClipFieldBinderDouble : ClipFieldBinder<double> { }
    [Category("Primitive Types/bool")]
    [Serializable] public sealed class ClipFieldBinderBool : ClipFieldBinder<bool> { }
    [Category("Primitive Types/string")]
    [Serializable] public sealed class ClipFieldBinderString : ClipFieldBinder<string> { }
    [Category("Unity Basics/Vector2")]
    [Serializable] public sealed class ClipFieldBinderVector2 : ClipFieldBinder<UnityEngine.Vector2> { }
    [Category("Unity Basics/Vector3")]
    [Serializable] public sealed class ClipFieldBinderVector3 : ClipFieldBinder<UnityEngine.Vector3> { }
    [Category("Unity Basics/Quaternion")]
    [Serializable] public sealed class ClipFieldBinderQuaternion : ClipFieldBinder<UnityEngine.Quaternion> { }
    [Category("Unity Basics/Color")]
    [Serializable] public sealed class ClipFieldBinderColor : ClipFieldBinder<UnityEngine.Color> { }
    [Category("Unity Basics/Color32")]
    [Serializable] public sealed class ClipFieldBinderColor32 : ClipFieldBinder<UnityEngine.Color32> { }
    [Category("Unity Basics/Animation Curve")]
    [Serializable] public sealed class ClipFieldBinderAnimationCurve : ClipFieldBinder<UnityEngine.AnimationCurve> { }
    [Category("Unity Basics/Gradient")]
    [Serializable] public sealed class ClipFieldBinderGradient : ClipFieldBinder<UnityEngine.Gradient> { }
    [Category("Reference/Object")]
    [Serializable] public sealed class ClipFieldBinderObject : ClipFieldBinder<UnityEngine.Object> { }
    [Category("Reference/Game Object")]
    [Serializable] public sealed class ClipFieldBinderGameObject : ClipFieldBinder<UnityEngine.GameObject> { }
    [Category("Reference/Component")]
    [Serializable] public sealed class ClipFieldBinderComponent : ClipFieldBinder<UnityEngine.Component> { }
    [Category("Reference/Transform")]
    [Serializable] public sealed class ClipFieldBinderTransform : ClipFieldBinder<UnityEngine.Transform> { }
    [Category("Reference/Material")]
    [Serializable] public sealed class ClipFieldBinderMaterial : ClipFieldBinder<UnityEngine.Material> { }
}