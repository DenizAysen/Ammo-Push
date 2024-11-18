using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Level", fileName = "New Level Data")]
public class LevelDataSo : ScriptableObject
{
    public WeaponDataSo[] WeaponsInLevel;
    public BossData BossData;
    public RewardData RewardData;
}

[Serializable]
public class RewardData
{
    public float MinPrize;
    public float MaxPrize;
}
[Serializable]
public class BossData
{
    public float BossHealth;
    public float BossTargetPosZ;
    public float BossMoveDuration;
    public float BossFastMoveDuration;
    public int BossLevel;
}
