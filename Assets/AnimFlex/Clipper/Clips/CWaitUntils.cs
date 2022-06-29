using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

namespace AnimFlex.Clipper.Clips
{
    [DisplayName("Wait Until/Int")]
    public class CWaitUntilInt : CWaitUntil<int>
    {
        protected override bool Equals(int a, int b) => a == b;
    }
    [DisplayName("Wait Until/Float")] public class CWaitUntilFloat : CWaitUntil<float>
    {
        protected override bool Equals(float a, float b) => Math.Abs(a - b) < 0.01f;
    }
    [DisplayName("Wait Until/Bool")] public class CWaitUntilBool : CWaitUntil<bool>
    {
        protected override bool Equals(bool a, bool b) => a == b;
    }
    [DisplayName("Wait Until/String")] public class CWaitUntilString : CWaitUntil<string>
    {
        protected override bool Equals(string a, string b) => a == b;
    }
    [DisplayName("Wait Until/Double")] public class CWaitUntilDouble : CWaitUntil<double>
    {
        protected override bool Equals(double a, double b) => Math.Abs(a - b) < 0.001f;
    }
    [DisplayName("Wait Until/Vector2")] public class CWaitUntilVector2 : CWaitUntil<Vector2>
    {
        protected override bool Equals(Vector2 a, Vector2 b) => a == b;
    }
    [DisplayName("Wait Until/Vector3")] public class CWaitUntilVector3 : CWaitUntil<Vector3>
    {
        protected override bool Equals(Vector3 a, Vector3 b) => a == b;
    }
    [DisplayName("Wait Until/Vector4")] public class CWaitUntilVector4 : CWaitUntil<Vector4>
    {
        protected override bool Equals(Vector4 a, Vector4 b) => a == b;
    }
    [DisplayName("Wait Until/Quaternion")] public class CWaitUntilQuaternion : CWaitUntil<Quaternion>
    {
        protected override bool Equals(Quaternion a, Quaternion b) => a == b;
    }
    [DisplayName("Wait Until/Rect")] public class CWaitUntilRect : CWaitUntil<Rect>
    {
        protected override bool Equals(Rect a, Rect b) => a == b;
    }
    [DisplayName("Wait Until/Color")] public class CWaitUntilColor : CWaitUntil<Color>
    {
        protected override bool Equals(Color a, Color b) => a == b;
    }
    [DisplayName("Wait Until/int list")] public class CWaitUntilListInt : CWaitUntil<List<int>>
    {
        protected override bool Equals(List<int> a, List<int> b) => a.SequenceEqual(b);
    }
}