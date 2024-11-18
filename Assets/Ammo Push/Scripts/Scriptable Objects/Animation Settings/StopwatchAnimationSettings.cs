using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Animation/UI/Stopwatch", fileName = "New Animation Settings")]
public class StopwatchAnimationSettings : ScriptableObject
{
    public float MoveDuration;
    public Vector3 StopwatchAnimScale;
    public float ScaleDuration;

    public float StartPosReturnDuration;
    public float ShrinkDuration;
}
