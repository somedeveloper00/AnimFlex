using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips {
    [DisplayName( "Wait Until Property Int" )]
    [Category( "Wait Until Property/Int" )]
    public class CWaitUntilPropertyInt : CWaitUntilProperty<int> {
        protected override bool IsEqual(int a, int b) => a == b;
    }

    [DisplayName( "Wait Until Property Float" )]
    [Category( "Wait Until Property/Float" )]
    public class CWaitUntilPropertyFloat : CWaitUntilProperty<float> {
        protected override bool IsEqual(float a, float b) => Math.Abs( a - b ) < 0.01f;
    }

    [DisplayName( "Wait Until Property Bool" )]
    [Category( "Wait Until Property/Bool" )]
    public class CWaitUntilPropertyBool : CWaitUntilProperty<bool> {
        protected override bool IsEqual(bool a, bool b) => a == b;
    }

    [DisplayName( "Wait Until Property String" )]
    [Category( "Wait Until Property/String" )]
    public class CWaitUntilPropertyString : CWaitUntilProperty<string> {
        protected override bool IsEqual(string a, string b) => a == b;
    }

    [DisplayName( "Wait Until Property Double" )]
    [Category( "Wait Until Property/Double" )]
    public class CWaitUntilPropertyDouble : CWaitUntilProperty<double> {
        protected override bool IsEqual(double a, double b) => Math.Abs( a - b ) < 0.001f;
    }

    [DisplayName( "Wait Until Property Vector2" )]
    [Category( "Wait Until Property/Vector2" )]
    public class CWaitUntilPropertyVector2 : CWaitUntilProperty<Vector2> {
        protected override bool IsEqual(Vector2 a, Vector2 b) => a == b;
    }

    [DisplayName( "Wait Until Property Vector3" )]
    [Category( "Wait Until Property/Vector3" )]
    public class CWaitUntilPropertyVector3 : CWaitUntilProperty<Vector3> {
        protected override bool IsEqual(Vector3 a, Vector3 b) => a == b;
    }

    [DisplayName( "Wait Until Property Vector4" )]
    [Category( "Wait Until Property/Vector4" )]
    public class CWaitUntilPropertyVector4 : CWaitUntilProperty<Vector4> {
        protected override bool IsEqual(Vector4 a, Vector4 b) => a == b;
    }

    [DisplayName( "Wait Until Property Quaternion" )]
    [Category( "Wait Until Property/Quaternion" )]
    public class CWaitUntilPropertyQuaternion : CWaitUntilProperty<Quaternion> {
        protected override bool IsEqual(Quaternion a, Quaternion b) => a == b;
    }

    [DisplayName( "Wait Until Property Rect" )]
    [Category( "Wait Until Property/Rect" )]
    public class CWaitUntilPropertyRect : CWaitUntilProperty<Rect> {
        protected override bool IsEqual(Rect a, Rect b) => a == b;
    }

    [DisplayName( "Wait Until Property Color" )]
    [Category( "Wait Until Property/Color" )]
    public class CWaitUntilPropertyColor : CWaitUntilProperty<Color> {
        protected override bool IsEqual(Color a, Color b) => a == b;
    }

    [DisplayName( "Wait Until Property Transform" )]
    [Category( "Wait Until Property/Transform" )]
    public class CWaitUntilPropertyTransform : CWaitUntilProperty<Transform> {
        protected override bool IsEqual(Transform a, Transform b) => a == b;
    }

    public abstract class CWaitUntilPropertyList<T> : CWaitUntilProperty<List<T>> {
        protected override bool IsEqual(List<T> a, List<T> b) => a.SequenceEqual( b );
    }
}