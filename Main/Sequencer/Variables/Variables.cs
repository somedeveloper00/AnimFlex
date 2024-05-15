using System;
using System.ComponentModel;
using AnimFlex.Sequencer.Clips;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AnimFlex.Sequencer.Variables
{
    [Category("Sequencer"), DisplayName("Node Selection"), Serializable] public sealed class Variable_NodeSelection : Variable<NodeSelection> { }
    [Category("Primitives"), DisplayName("Int"), Serializable] public sealed class Variable_Int : Variable<int> { }
    [Category("Primitives"), DisplayName("Float"), Serializable] public sealed class Variable_Float : Variable<float> { }
    [Category("Primitives"), DisplayName("Bool"), Serializable] public sealed class Variable_Bool : Variable<bool> { }
    [Category("Primitives"), DisplayName("Double"), Serializable] public sealed class Variable_Double : Variable<double> { }
    [Category("Primitives"), DisplayName("String"), Serializable] public sealed class Variable_String : Variable<string> { }
    [Category("Primitives"), DisplayName("Vector2"), Serializable] public sealed class Variable_Vector2 : Variable<Vector2> { }
    [Category("Primitives"), DisplayName("Vector3"), Serializable] public sealed class Variable_Vector3 : Variable<Vector3> { }
    [Category("Primitives"), DisplayName("Vector4"), Serializable] public sealed class Variable_Vector4 : Variable<Vector4> { }
    [Category("Primitives"), DisplayName("Quaternion"), Serializable] public sealed class Variable_Quaternion : Variable<Quaternion> { }
    [Category("Primitives"), DisplayName("Rect"), Serializable] public sealed class Variable_Rect : Variable<Rect> { }
    [Category("Primitives"), DisplayName("Color"), Serializable] public sealed class Variable_Color : Variable<Color> { }
    [Category("Primitives"), DisplayName("Object"), Serializable] public sealed class Variable_Object : Variable<Object> { }
    [Category("Primitives"), DisplayName("Transform"), Serializable] public sealed class Variable_Transform : Variable<Transform> { }
    [Category("Primitives"), DisplayName("GameObject"), Serializable] public sealed class Variable_GameObject : Variable<GameObject> { }
}