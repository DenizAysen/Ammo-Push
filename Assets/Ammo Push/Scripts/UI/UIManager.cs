using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private GameObject joystickZone;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject timeOutParent;
    [SerializeField] private Text bossLevelText;
    [SerializeField] private Text moneyText;
    [SerializeField] private GameObject tapToStartButton;
    [SerializeField] private float duration;
    #endregion

    #region Privates
    private LevelDataSo _levelData;
    private readonly string _level = "Lv ";
    private readonly string _currency = "$";
    #endregion
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    #region Event Subscription
    private void OnEnable()
    {
        TapToStartButton.onGameStarted += OnGameStarted;
        TimeUpgradeButton.onSetMoney += OnSetMoney;
        SizeUpgradeButton.onSetMoney += OnSetMoney;
        PowerUpgradeButton.onSetMoney += OnSetMoney;
        Stopwatch.onGameOver += OnGameOver;
    }

    private void OnSetMoney(float remainingMoney)
    {
        Debug.Log("kalan para : " + remainingMoney);
        moneyText.text = remainingMoney.ToString("F1") + _currency;
    }
    private void OnGameOver()
    {
        joystickZone.SetActive(false);
        timeOutParent.SetActive(true);
        timeOutParent.GetComponent<TimeOutImage>().PlayTimeOutAnimation();
    }
    private void OnGameStarted()
    {
        joystickZone.SetActive(true);
        shopPanel.SetActive(false);
    }
    private void OnDisable()
    {
        TapToStartButton.onGameStarted -= OnGameStarted;
        TimeUpgradeButton.onSetMoney -= OnSetMoney;
        SizeUpgradeButton.onSetMoney -= OnSetMoney;
        PowerUpgradeButton.onSetMoney -= OnSetMoney;
        Stopwatch.onGameOver -= OnGameOver;
    }
    #endregion
    private void Start()
    {
        _levelData = LevelManager.Instance.GetLevelData();
        bossLevelText.text = _level + _levelData.BossData.BossLevel;
        moneyText.text = PlayerPrefs.GetFloat("Money").ToString("F1") + _currency;
        StartCoroutine(ActivateStartButtonAfterCameraChangeDuration());
    }
    private IEnumerator ActivateStartButtonAfterCameraChangeDuration()
    {
        yield return new WaitForSeconds(duration);
        tapToStartButton.SetActive(true);
    }
}
