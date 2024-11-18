using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpgradeButton : UpgradeButtonBase
{
    private float _money;
    private int _powerLevel;
    private float _upgradeCost;
    private Button _powerUpgradeButton;

    private readonly string _moneyString = "Money";
    private readonly string _level = "Lv ";
    private readonly string _powerString = "Power";

    public static Action<float> onSetMoney;
    public static Action onPowerValueChanged;
    private void Awake()
    {
        _powerUpgradeButton = GetComponent<Button>();
    }
    private void OnEnable()
    {
        _powerUpgradeButton.onClick.AddListener(UpgradePowerLevel);
    }

    private void UpgradePowerLevel()
    {
        _upgradeCost = costData.Costs[_powerLevel];
        if (_money >= _upgradeCost)
        {
            _money -= _upgradeCost;
            _powerLevel++;

            if (_powerLevel > 8) _powerLevel = 8;

            PlayerPrefs.SetFloat(_moneyString, _money);
            PlayerPrefs.SetInt(_powerString, _powerLevel);

            onSetMoney?.Invoke(_money);
            onPowerValueChanged?.Invoke();

            UpdateTexts();

            SetInteractibility();
        }
    }
    private void OnDisable()
    {
        _powerUpgradeButton?.onClick.RemoveListener(UpgradePowerLevel);
    }
    void Start()
    {
        if (!PlayerPrefs.HasKey(_powerString))
        {
            _powerLevel = 0;
            PlayerPrefs.SetInt(_powerString, _powerLevel);
        }
        else
        {
            _powerLevel = PlayerPrefs.GetInt(_powerString);
        }

        _money = PlayerPrefs.GetFloat(_moneyString);

        UpdateTexts();

        SetInteractibility();
    }
    private bool CanUpgradePowerLevel()
    {
        _money = PlayerPrefs.GetFloat(_moneyString);
        return _money >= costData.Costs[_powerLevel];
    }
    public override void ControlInteractibility()
    {
        SetInteractibility();
    }

    protected override void SetInteractibility()
    {
        if (!CanUpgradePowerLevel() || _powerLevel >= 8)
        {
            _powerUpgradeButton.interactable = false;
        }
    }

    protected override void UpdateTexts()
    {
        levelText.text = _level + (_powerLevel + 1);
        costText.text = costData.Costs[_powerLevel].ToString();
    }

}
