using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeUpgradeButton : UpgradeButtonBase
{
    #region Privates
    private float _money;
    private int _sizeLevel;
    private float _upgradeCost;
    private Button _sizeUpgradeButton;

    private readonly string _moneyString = "Money";
    private readonly string _level = "Lv ";
    private readonly string _sizeString = "Size";
    #endregion

    #region Actions
    public static Action onSizeValueChanged;
    public static Action<float> onSetMoney; 
    #endregion
    private void Awake()
    {
        _sizeUpgradeButton = GetComponent<Button>();
    }   
    private void OnEnable()
    {
        _sizeUpgradeButton.onClick.AddListener(UpgradeSizeLevel);
    }
    private void UpgradeSizeLevel()
    {
        _upgradeCost = costData.Costs[_sizeLevel];
        if (_money >= _upgradeCost)
        {
            _money -= _upgradeCost;
            _sizeLevel++;

            if (_sizeLevel > 8) _sizeLevel = 8;

            PlayerPrefs.SetFloat(_moneyString, _money);
            PlayerPrefs.SetInt(_sizeString, _sizeLevel);

            onSetMoney?.Invoke(_money);
            onSizeValueChanged?.Invoke();

            UpdateTexts();

            SetInteractibility();
        }
    }
    private void OnDisable()
    {
        _sizeUpgradeButton.onClick.RemoveListener(UpgradeSizeLevel);
    }
    void Start()
    {
        if (!PlayerPrefs.HasKey(_sizeString))
        {
            _sizeLevel = 0;
            PlayerPrefs.SetInt(_sizeString, _sizeLevel);
        }
        else
        {
            _sizeLevel = PlayerPrefs.GetInt(_sizeString);
        }
       //PlayerPrefs.SetFloat(_moneyString, 150f);

        _money = PlayerPrefs.GetFloat(_moneyString);

        UpdateTexts();

        SetInteractibility();
    }
    private bool CanUpgradeSizeLevel()
    {
        _money = PlayerPrefs.GetFloat( _moneyString);
        return _money >= costData.Costs[_sizeLevel];
    }
    protected override void UpdateTexts()
    {
        levelText.text = _level + (_sizeLevel + 1);
        costText.text = costData.Costs[_sizeLevel].ToString();
    }

    public override void ControlInteractibility()
    {
        SetInteractibility();
    }
    protected override void SetInteractibility()
    {
        if (!CanUpgradeSizeLevel() || _sizeLevel >= 8)
        {
            _sizeUpgradeButton.interactable = false;
        }
    }
}
