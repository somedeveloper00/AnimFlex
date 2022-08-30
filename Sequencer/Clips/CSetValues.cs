using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Set Value/Int")] public class CSetValuesInt : CSetValue<int> { }
    [DisplayName("Set Value/Float")] public class CSetValuesFloat : CSetValue<float> { }
    [DisplayName("Set Value/Bool")] public class CSetValuesBool : CSetValue<bool> { }
    [DisplayName("Set Value/String")] public class CSetValuesString : CSetValue<string> { }
    [DisplayName("Set Value/Double")] public class CSetValuesDouble : CSetValue<double> { }
    [DisplayName("Set Value/Vector2")] public class CSetValuesVector2 : CSetValue<Vector2> { }
    [DisplayName("Set Value/Vector3")] public class CSetValuesVector3 : CSetValue<Vector3> { }
    [DisplayName("Set Value/Vector4")] public class CSetValuesVector4 : CSetValue<Vector4> { }
    [DisplayName("Set Value/Quaternion")] public class CSetValuesQuaternion : CSetValue<Quaternion> { }
    [DisplayName("Set Value/Rect")] public class CSetValuesRect : CSetValue<Rect> { }
    [DisplayName("Set Value/Color")] public class CSetValuesColor : CSetValue<Color> { }
    [DisplayName("Set Value/Transform")] public class CSetValuesTransform : CSetValue<Transform> { }

    [DisplayName("Set Value/List/Int")] public class CSetValuesIntList : CSetValue<List<int>> { }
    [DisplayName("Set Value/List/Float")] public class CSetValuesFloatList : CSetValue<List<float>> { }
    [DisplayName("Set Value/List/Bool")] public class CSetValuesBoolList : CSetValue<List<bool>> { }
    [DisplayName("Set Value/List/String")] public class CSetValuesStringList : CSetValue<List<string>> { }
    [DisplayName("Set Value/List/Double")] public class CSetValuesDoubleList : CSetValue<List<double>> { }
    [DisplayName("Set Value/List/Vector2")] public class CSetValuesVector2List : CSetValue<List<Vector2>> { }
    [DisplayName("Set Value/List/Vector3")] public class CSetValuesVector3List : CSetValue<List<Vector3>> { }
    [DisplayName("Set Value/List/Vector4")] public class CSetValuesVector4List : CSetValue<List<Vector4>> { }
    [DisplayName("Set Value/List/Quaternion")] public class CSetValuesQuaternionList : CSetValue<List<Quaternion>> { }
    [DisplayName("Set Value/List/Rect")] public class CSetValuesRectList : CSetValue<List<Rect>> { }
    [DisplayName("Set Value/List/Color")] public class CSetValuesColorList : CSetValue<List<Color>> { }
    [DisplayName("Set Value/List/Transform")] public class CSetValuesTransformList : CSetValue<List<Transform>> { }
}