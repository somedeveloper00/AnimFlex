using System;
using System.ComponentModel;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("If Property Int")]
    [Category("Branch/If Property/Int")]
    public class CGotoIfPropertyInt : CGotoIfProperty<int> { protected override bool IsEqual(int a, int b) => a == b; }
    [DisplayName("If Property Float")]
    [Category("Branch/If Property/Float")]
    public class CGotoIfPropertyFloat : CGotoIfProperty<float> { protected override bool IsEqual(float a, float b) => Math.Abs(a - b) < 0.1f; }
    [DisplayName("If Property Bool")]
    [Category("Branch/If Property/Bool")]
    public class CGotoIfPropertyBool : CGotoIfProperty<bool> { protected override bool IsEqual(bool a, bool b) => a == b; }
    [DisplayName("If Property String")]
    [Category("Branch/If Property/String")]
    public class CGotoIfPropertyString : CGotoIfProperty<string> { protected override bool IsEqual(string a, string b) => a == b; }
    [DisplayName("If Property Double")]
    [Category("Branch/If Property/Double")]
    public class CGotoIfPropertyDouble : CGotoIfProperty<double> { protected override bool IsEqual(double a, double b) => Math.Abs(a - b) < 0.1f; }
    [DisplayName("If Property Vector2")]
    [Category("Branch/If Property/Vector2")]
    public class CGotoIfPropertyVector2 : CGotoIfProperty<Vector2> { protected override bool IsEqual(Vector2 a, Vector2 b) => a == b; }
    [DisplayName("If Property Vector3")]
    [Category("Branch/If Property/Vector3")]
    public class CGotoIfPropertyVector3 : CGotoIfProperty<Vector3> { protected override bool IsEqual(Vector3 a, Vector3 b) => a == b; }
    [DisplayName("If Property Vector4")]
    [Category("Branch/If Property/Vector4")]
    public class CGotoIfPropertyVector4 : CGotoIfProperty<Vector4> { protected override bool IsEqual(Vector4 a, Vector4 b) => a == b; }
    [DisplayName("If Property Quaternion")]
    [Category("Branch/If Property/Quaternion")]
    public class CGotoIfPropertyQuaternion : CGotoIfProperty<Quaternion> { protected override bool IsEqual(Quaternion a, Quaternion b) => a == b; }
    [DisplayName("If Property Rect")]
    [Category("Branch/If Property/Rect")]
    public class CGotoIfPropertyRect : CGotoIfProperty<Rect> { protected override bool IsEqual(Rect a, Rect b) => a == b; }
    [DisplayName("If Property Color")]
    [Category("Branch/If Property/Color")]
    public class CGotoIfPropertyColor : CGotoIfProperty<Color> { protected override bool IsEqual(Color a, Color b) => a == b; }
    [DisplayName("If Property Transform")]
    [Category("Branch/If Property/Transform")]
    public class CGotoIfPropertyTransform : CGotoIfProperty<Transform> { protected override bool IsEqual(Transform a, Transform b) => a == b; }
}