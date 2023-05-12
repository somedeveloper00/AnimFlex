using System;

namespace AnimFlex.Sequencer.Binding {
    [Serializable] public sealed class ClipFieldBinderInt : ClipFieldBinder<int> { }
    [Serializable] public sealed class ClipFieldBinderUInt : ClipFieldBinder<uint> { }
    [Serializable] public sealed class ClipFieldBinderFloat : ClipFieldBinder<float> { }
    [Serializable] public sealed class ClipFieldBinderLong : ClipFieldBinder<long> { }
    [Serializable] public sealed class ClipFieldBinderUDouble : ClipFieldBinder<ulong> { }
    [Serializable] public sealed class ClipFieldBinderDouble : ClipFieldBinder<double> { }
    [Serializable] public sealed class ClipFieldBinderBool : ClipFieldBinder<bool> { }
    [Serializable] public sealed class ClipFieldBinderString : ClipFieldBinder<string> { }
    [Serializable] public sealed class ClipFieldBinderVector2 : ClipFieldBinder<UnityEngine.Vector2> { }
    [Serializable] public sealed class ClipFieldBinderVector3 : ClipFieldBinder<UnityEngine.Vector3> { }
    [Serializable] public sealed class ClipFieldBinderVector4 : ClipFieldBinder<UnityEngine.Vector4> { }
    [Serializable] public sealed class ClipFieldBinderQuaternion : ClipFieldBinder<UnityEngine.Quaternion> { }
    [Serializable] public sealed class ClipFieldBinderColor : ClipFieldBinder<UnityEngine.Color> { }
    [Serializable] public sealed class ClipFieldBinderColor32 : ClipFieldBinder<UnityEngine.Color32> { }
    [Serializable] public sealed class ClipFieldBinderAnimationCurve : ClipFieldBinder<UnityEngine.AnimationCurve> { }
    [Serializable] public sealed class ClipFieldBinderGradient : ClipFieldBinder<UnityEngine.Gradient> { }
    [Serializable] public sealed class ClipFieldBinderObject : ClipFieldBinder<UnityEngine.Object> { }
    [Serializable] public sealed class ClipFieldBinderGameObject : ClipFieldBinder<UnityEngine.GameObject> { }
    [Serializable] public sealed class ClipFieldBinderComponent : ClipFieldBinder<UnityEngine.Component> { }
    [Serializable] public sealed class ClipFieldBinderTransform : ClipFieldBinder<UnityEngine.Transform> { }
    [Serializable] public sealed class ClipFieldBinderRectTransform : ClipFieldBinder<UnityEngine.RectTransform> { }
    [Serializable] public sealed class ClipFieldBinderTexture : ClipFieldBinder<UnityEngine.Texture> { }
    [Serializable] public sealed class ClipFieldBinderSprite : ClipFieldBinder<UnityEngine.Sprite> { }
    [Serializable] public sealed class ClipFieldBinderMaterial : ClipFieldBinder<UnityEngine.Material> { }
    [Serializable] public sealed class ClipFieldBinderMesh : ClipFieldBinder<UnityEngine.Mesh> { }
    [Serializable] public sealed class ClipFieldBinderAudioClip : ClipFieldBinder<UnityEngine.AudioClip> { }
    [Serializable] public sealed class ClipFieldBinderAnimationClip : ClipFieldBinder<UnityEngine.AnimationClip> { }
    [Serializable] public sealed class ClipFieldBinderFont : ClipFieldBinder<UnityEngine.Font> { }
}