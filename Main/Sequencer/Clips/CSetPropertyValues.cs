using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace AnimFlex.Sequencer.Clips {
    [DisplayName( "Set Property Value Int" )]
    [Category( "Set Property Value/Int" )]
    public class CSetPropertyValueInt : CSetPropertyValue<int> { }

    [DisplayName( "Set Property Value Float" )]
    [Category( "Set Property Value/Float" )]
    public class CSetPropertyValueFloat : CSetPropertyValue<float> { }

    [DisplayName( "Set Property Value Bool" )]
    [Category( "Set Property Value/Bool" )]
    public class CSetPropertyValueBool : CSetPropertyValue<bool> { }

    [DisplayName( "Set Property Value String" )]
    [Category( "Set Property Value/String" )]
    public class CSetPropertyValueString : CSetPropertyValue<string> { }

    [DisplayName( "Set Property Value Double" )]
    [Category( "Set Property Value/Double" )]
    public class CSetPropertyValueDouble : CSetPropertyValue<double> { }

    [DisplayName( "Set Property Value Vector2" )]
    [Category( "Set Property Value/Vector2" )]
    public class CSetPropertyValueVector2 : CSetPropertyValue<Vector2> { }

    [DisplayName( "Set Property Value Vector3" )]
    [Category( "Set Property Value/Vector3" )]
    public class CSetPropertyValueVector3 : CSetPropertyValue<Vector3> { }

    [DisplayName( "Set Property Value Vector4" )]
    [Category( "Set Property Value/Vector4" )]
    public class CSetPropertyValueVector4 : CSetPropertyValue<Vector4> { }

    [DisplayName( "Set Property Value Quaternion" )]
    [Category( "Set Property Value/Quaternion" )]
    public class CSetPropertyValueQuaternion : CSetPropertyValue<Quaternion> { }

    [DisplayName( "Set Property Value Rect" )]
    [Category( "Set Property Value/Rect" )]
    public class CSetPropertyValueRect : CSetPropertyValue<Rect> { }

    [DisplayName( "Set Property Value Color" )]
    [Category( "Set Property Value/Color" )]
    public class CSetPropertyValueColor : CSetPropertyValue<Color> { }

    [DisplayName( "Set Property Value Transform" )]
    [Category( "Set Property Value/Transform" )]
    public class CSetPropertyValueTransform : CSetPropertyValue<Transform> { }
    
    [DisplayName( "Set Property Value Sprite" )]
    [Category( "Set Property Value/Sprite" )]
    public class CSetPropertyValueSprite : CSetPropertyValue<Sprite> { }

    [DisplayName( "Set Property Value List<Int>" )]
    [Category( "Set Property Value/List/Int" )]
    public class CSetPropertyValueIntList : CSetPropertyValue<List<int>> { }

    [DisplayName( "Set Property Value List<Float>" )]
    [Category( "Set Property Value/List/Float" )]
    public class CSetPropertyValueFloatList : CSetPropertyValue<List<float>> { }

    [DisplayName( "Set Property Value List<Bool>" )]
    [Category( "Set Property Value/List/Bool" )]
    public class CSetPropertyValueBoolList : CSetPropertyValue<List<bool>> { }

    [DisplayName( "Set Property Value List<String>" )]
    [Category( "Set Property Value/List/String" )]
    public class CSetPropertyValueStringList : CSetPropertyValue<List<string>> { }

    [DisplayName( "Set Property Value List<Double>" )]
    [Category( "Set Property Value/List/Double" )]
    public class CSetPropertyValueDoubleList : CSetPropertyValue<List<double>> { }

    [DisplayName( "Set Property Value List<Vector2>" )]
    [Category( "Set Property Value/List/Vector2" )]
    public class CSetPropertyValueVector2List : CSetPropertyValue<List<Vector2>> { }

    [DisplayName( "Set Property Value List<Vector3>" )]
    [Category( "Set Property Value/List/Vector3" )]
    public class CSetPropertyValueVector3List : CSetPropertyValue<List<Vector3>> { }

    [DisplayName( "Set Property Value List<Vector4>" )]
    [Category( "Set Property Value/List/Vector4" )]
    public class CSetPropertyValueVector4List : CSetPropertyValue<List<Vector4>> { }

    [DisplayName( "Set Property Value List<Quaternion>" )]
    [Category( "Set Property Value/List/Quaternion" )]
    public class CSetPropertyValueQuaternionList : CSetPropertyValue<List<Quaternion>> { }

    [DisplayName( "Set Property Value List<Rect>" )]
    [Category( "Set Property Value/List/Rect" )]
    public class CSetPropertyValueRectList : CSetPropertyValue<List<Rect>> { }

    [DisplayName( "Set Property Value List<Color>" )]
    [Category( "Set Property Value/List/Color" )]
    public class CSetPropertyValueColorList : CSetPropertyValue<List<Color>> { }

    [DisplayName( "Set Property Value List<Transform>" )]
    [Category( "Set Property Value/List/Transform" )]
    public class CSetPropertyValueTransformList : CSetPropertyValue<List<Transform>> { }
    
    [DisplayName( "Set Property Value List<Sprite>" )]
    [Category( "Set Property Value/List/Sprite" )]
    public class CSetPropertyValueSpriteList : CSetPropertyValue<List<Sprite>> { }
}