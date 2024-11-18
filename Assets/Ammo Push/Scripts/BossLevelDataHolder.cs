using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevelDataHolder : MonoBehaviour
{
    [SerializeField] private LevelManagerSo managerSo;

    private int _levelIndex;
    private readonly string _level = "Level";

    public static BossLevelDataHolder Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    //private void OnEnable()
    //{
    //    Boss.onBossDead += OnBossDead;
    //}

    //private void OnBossDead()
    //{
    //    _levelIndex = PlayerPrefs.GetInt("Level");
    //    _levelIndex++;
    //    PlayerPrefs.SetInt("Level", _levelIndex);
    //}
    //private void OnDisable()
    //{
    //    Boss.onBossDead -= OnBossDead;
    //}
    public LevelDataSo GetCurrentLevelData()
    {
        _levelIndex = PlayerPrefs.GetInt(_level);
        return managerSo.Levels[_levelIndex];
    }
}
