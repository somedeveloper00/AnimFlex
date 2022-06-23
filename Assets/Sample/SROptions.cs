using UnityEngine;

public partial class SROptions
{
    public void TestSinIn()
    {
        Debug.Log(CallNativeCode.in_sin(0));
    }

    public void TestSinOut()
    {
        Debug.Log(CallNativeCode.out_sin(0));
    }
}