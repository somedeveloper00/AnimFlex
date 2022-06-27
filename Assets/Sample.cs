using System;
using System.Collections.Generic;
using AnimFlex.Clipper;
using UnityEngine;
using UnityEngine.Serialization;

public class Sample : MonoBehaviour
{
    [SerializeField] private ClipSequence sequence;
    public List<int> myInts;
    public int myInt;
}