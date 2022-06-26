using AnimFlex.Clipper;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

[DisplayName("Custom/Say Hello To Caller")]
public class MyCustomClip : Clip
{
    [SerializeField] private string yourName = "Someone";

    protected override void OnStart()
    {
        Debug.Log($"Hi {yourName}");
        End();
        
    }
}
