using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Animation/UI/Tap To Start Text", fileName = "New Animation Settings")]
public class TapToStartTextAnimationSettings : ScriptableObject
{
    public Vector3 EndValue;
    public Vector3 StartValue;
    public float ScaleTime;
    public float ShrinkTime;
}
