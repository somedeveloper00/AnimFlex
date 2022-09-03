using System;
using System.ComponentModel;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("If Int")]
    [Category("Branch/If/Int")]
    public class CGotoIfInt : CGotoIf<int> { protected override bool IsEqual(int a, int b) => a == b; }
    [DisplayName("If Float")]
    [Category("Branch/If/Float")]
    public class CGotoIfFloat : CGotoIf<float> { protected override bool IsEqual(float a, float b) => Math.Abs(a - b) < 0.1f; }
    [DisplayName("If Bool")]
    [Category("Branch/If/Bool")]
    public class CGotoIfBool : CGotoIf<bool> { protected override bool IsEqual(bool a, bool b) => a == b; }
    [DisplayName("If String")]
    [Category("Branch/If/String")]
    public class CGotoIfString : CGotoIf<string> { protected override bool IsEqual(string a, string b) => a == b; }
    [DisplayName("If Double")]
    [Category("Branch/If/Double")]
    public class CGotoIfDouble : CGotoIf<double> { protected override bool IsEqual(double a, double b) => Math.Abs(a - b) < 0.1f; }
    [DisplayName("If Vector2")]
    [Category("Branch/If/Vector2")]
    public class CGotoIfVector2 : CGotoIf<Vector2> { protected override bool IsEqual(Vector2 a, Vector2 b) => a == b; }
    [DisplayName("If Vector3")]
    [Category("Branch/If/Vector3")]
    public class CGotoIfVector3 : CGotoIf<Vector3> { protected override bool IsEqual(Vector3 a, Vector3 b) => a == b; }
    [DisplayName("If Vector4")]
    [Category("Branch/If/Vector4")]
    public class CGotoIfVector4 : CGotoIf<Vector4> { protected override bool IsEqual(Vector4 a, Vector4 b) => a == b; }
    [DisplayName("If Quaternion")]
    [Category("Branch/If/Quaternion")]
    public class CGotoIfQuaternion : CGotoIf<Quaternion> { protected override bool IsEqual(Quaternion a, Quaternion b) => a == b; }
    [DisplayName("If Rect")]
    [Category("Branch/If/Rect")]
    public class CGotoIfRect : CGotoIf<Rect> { protected override bool IsEqual(Rect a, Rect b) => a == b; }
    [DisplayName("If Color")]
    [Category("Branch/If/Color")]
    public class CGotoIfColor : CGotoIf<Color> { protected override bool IsEqual(Color a, Color b) => a == b; }
    [DisplayName("If Transform")]
    [Category("Branch/If/Transform")]
    public class CGotoIfTransform : CGotoIf<Transform> { protected override bool IsEqual(Transform a, Transform b) => a == b; }
}