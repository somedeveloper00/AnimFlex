using AnimFlex.Clipper;
using UnityEngine;

public class MyCustomClip : Clip
{
    [SerializeField] private string yourName = "Someone";

    protected override void OnStart()
    {
        Debug.Log($"Hi {yourName}");
        End();
    }
}