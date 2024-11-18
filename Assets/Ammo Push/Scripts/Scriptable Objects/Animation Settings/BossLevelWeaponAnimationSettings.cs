using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Animation/Boss Scene/Weapon", fileName = "New Animation Settings")]
public class BossLevelWeaponAnimationSettings : ScriptableObject
{
    public WeaponScaleAnimationSettings ScaleAnimationSettings;

    public BossSceneShootAnimationSettings ShootAnimationSettings;
}

[Serializable]
public class WeaponScaleAnimationSettings
{
    public float TargetScale;
    public float TargetScaleDuration;
}
[Serializable]
public class BossSceneShootAnimationSettings
{
    public float JumpForce;
    public int NumJumps;
    public float JumpDuration;

    public Vector3 TargetRotation;
    public float RotateDuration;

    public float TargetScale;
    public float TargetScaleDuration;
}
