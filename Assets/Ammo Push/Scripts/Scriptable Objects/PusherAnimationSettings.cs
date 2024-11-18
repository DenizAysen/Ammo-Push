using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Animation/Pusher", fileName = "New Animation Settings")]
public class PusherAnimationSettings : ScriptableObject
{
    public float PushTime;
    public float ScaleTime;
    public float PullTime;
    public float ShrinkTime;
    public Vector3 CyclinderLocalPushMovePoint;
    public float PlaneLocalZScaleAmount;
    public float PlaneDefaultLocalZScale;
    public Vector3 CyclinderLocalPulPoint;
}
