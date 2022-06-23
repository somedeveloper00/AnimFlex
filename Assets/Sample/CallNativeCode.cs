using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class CallNativeCode : MonoBehaviour
{

    [DllImport("native")]
    public static extern float add(float x, float y);
    [DllImport("libtween")]
    public static extern float in_sin(float t);
    [DllImport("libtween")]
    public static extern float out_sin(float t);

    public Transform trans;
    public Transform fromTrans, toTrans;

    public UnityEngine.UI.Text logTxt;
    
    public void InSineMove()
    {
        StartCoroutine(MoveFromTo(in_sin));
    }
    public void OutSineMove()
    {
        StartCoroutine(MoveFromTo(out_sin));
    }
    public IEnumerator MoveFromTo(Func<float, float> ease)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            trans.position = Vector3.Lerp(fromTrans.position, toTrans.position, ease(t));
            yield return null;
        }
    }
}
