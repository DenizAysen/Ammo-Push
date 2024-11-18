using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Animation/Weapon", fileName = "New Animation Settings")]
public class WeaponAnimationSettings : ScriptableObject
{
    public float Duration;
    public float EndScaleValue;
}
