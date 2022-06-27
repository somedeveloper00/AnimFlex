using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace AnimFlex.Clipper.Clips
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
    [DisplayName("Set Value/int list")] public class CSetValuesListInt : CSetValue<List<int>> { }
}