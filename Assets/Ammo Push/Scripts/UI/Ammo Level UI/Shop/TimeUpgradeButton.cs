using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUpgradeButton : UpgradeButtonBase
{
    #region Serialized Fields
    //[SerializeField] private TimeDataSo timeData;
    [Header("Sprites")]
    [SerializeField] private Sprite UnupgradeableSprite;
    #endregion

    #region Privates
    private int _timeLevel;
    private float _money;

    private readonly string _moneyString = "Money";
    private readonly string _timeString = "Time";
    private readonly string _level = "Lv ";

    private Button _timeUpgradeButton;
    #endregion
    public static Action<float> onSetMoney;
    public static Action onTimeValueChanged;
    private void Awake()
    {
        _timeUpgradeButton = GetComponent<Button>();
    }
    private void OnEnable()
    {
        _timeUpgradeButton.onClick.AddListener(UpgradeTimeLevel);
    }
    private void UpgradeTimeLevel()
    {
        float _upgradeCost = costData.Costs[_timeLevel];
        if (_money >= _upgradeCost)
        {
            _money -= _upgradeCost;
            _timeLevel++;

            PlayerPrefs.SetFloat(_moneyString, _money);
            PlayerPrefs.SetInt(_timeString, _timeLevel);

            onSetMoney?.Invoke(_money);
            onTimeValueChanged?.Invoke();

            UpdateTexts();

            SetInteractibility();
        }
    }
    private void OnDisable()
    {
        _timeUpgradeButton.onClick.RemoveListener(UpgradeTimeLevel);
    }
    void Start()
    {
        if (!PlayerPrefs.HasKey(_timeString))
        {
            _timeLevel = 0;
            PlayerPrefs.SetInt(_timeString, _timeLevel);
        }
        else
        {
            _timeLevel = PlayerPrefs.GetInt(_timeString);
        }
        //PlayerPrefs.SetFloat(_moneyString, 150f);
        _money = PlayerPrefs.GetFloat(_moneyString);

        UpdateTexts();

        SetInteractibility();
    }
    private bool CanUpgradeTimeLevel()
    {
        _money = PlayerPrefs.GetFloat( _moneyString);
        return _money >= costData.Costs[_timeLevel];
    }
    protected override void SetInteractibility()
    {
        if(!CanUpgradeTimeLevel() || _timeLevel >= 8)
        {
            _timeUpgradeButton.interactable = false;
        }
    }
    protected override void UpdateTexts()
    {
        levelText.text = _level + (_timeLevel + 1);
        costText.text = costData.Costs[_timeLevel].ToString();
    }

    public override void ControlInteractibility()
    {
        SetInteractibility();
    }
}
