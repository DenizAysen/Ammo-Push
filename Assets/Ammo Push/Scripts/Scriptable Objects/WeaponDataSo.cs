using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Weapon/Data", fileName = "New Weapon Data")]
public class WeaponDataSo : ScriptableObject
{
    public GameObject WeaponPrefab;
    public GameObject BossLevelWeaponPrefab;
    public float Mass;
    public float Damage;
    public Sprite WeaponIcon;
    public WeaponType WeaponType;
}
