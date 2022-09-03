using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips
{
    [DisplayName("Wait Until Int")]
    [Category("Wait Until/Int")]
    public class CWaitUntilInt : CWaitUntil<int>
    {
        protected override bool IsEqual(int a, int b) => a == b;
    }
    [DisplayName("Wait Until Float")] 
    [Category("Wait Until/Float")] 
    public class CWaitUntilFloat : CWaitUntil<float>
    {
        protected override bool IsEqual(float a, float b) => Math.Abs(a - b) < 0.01f;
    }
    [DisplayName("Wait Until Bool")] 
    [Category("Wait Until/Bool")] 
    public class CWaitUntilBool : CWaitUntil<bool>
    {
        protected override bool IsEqual(bool a, bool b) => a == b;
    }
    [DisplayName("Wait Until String")] 
    [Category("Wait Until/String")] 
    public class CWaitUntilString : CWaitUntil<string>
    {
        protected override bool IsEqual(string a, string b) => a == b;
    }
    [DisplayName("Wait Until Double")] 
    [Category("Wait Until/Double")] 
    public class CWaitUntilDouble : CWaitUntil<double>
    {
        protected override bool IsEqual(double a, double b) => Math.Abs(a - b) < 0.001f;
    }
    [DisplayName("Wait Until Vector2")] 
    [Category("Wait Until/Vector2")] 
    public class CWaitUntilVector2 : CWaitUntil<Vector2>
    {
        protected override bool IsEqual(Vector2 a, Vector2 b) => a == b;
    }
    [DisplayName("Wait Until Vector3")] 
    [Category("Wait Until/Vector3")] 
    public class CWaitUntilVector3 : CWaitUntil<Vector3>
    {
        protected override bool IsEqual(Vector3 a, Vector3 b) => a == b;
    }
    [DisplayName("Wait Until Vector4")] 
    [Category("Wait Until/Vector4")] 
    public class CWaitUntilVector4 : CWaitUntil<Vector4>
    {
        protected override bool IsEqual(Vector4 a, Vector4 b) => a == b;
    }
    [DisplayName("Wait Until Quaternion")] 
    [Category("Wait Until/Quaternion")] 
    public class CWaitUntilQuaternion : CWaitUntil<Quaternion>
    {
        protected override bool IsEqual(Quaternion a, Quaternion b) => a == b;
    }
    [DisplayName("Wait Until Rect")] 
    [Category("Wait Until/Rect")] 
    public class CWaitUntilRect : CWaitUntil<Rect>
    {
        protected override bool IsEqual(Rect a, Rect b) => a == b;
    }
    [DisplayName("Wait Until Color")] 
    [Category("Wait Until/Color")] 
    public class CWaitUntilColor : CWaitUntil<Color>
    {
        protected override bool IsEqual(Color a, Color b) => a == b;
    }
    [DisplayName("Wait Until Transform")] 
    [Category("Wait Until/Transform")] 
    public class CWaitUntilTransform : CWaitUntil<Transform>
    {
        protected override bool IsEqual(Transform a, Transform b) => a == b;
    }

    public abstract class CWaitUntilList<T> : CWaitUntil<List<T>>
    {
        protected override bool IsEqual(List<T> a, List<T> b) => a.SequenceEqual(b);
    }
}