using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanelController : MonoBehaviour
{
    [SerializeField] private List<UpgradeButtonBase> upgradeButtons;
    private void OnEnable()
    {
        SizeUpgradeButton.onSizeValueChanged += OnSizeValueChanged;
        TimeUpgradeButton.onTimeValueChanged += OnTimeValueChanged;
        PowerUpgradeButton.onPowerValueChanged += OnPowerValueChanged;
    }

    private void OnPowerValueChanged()
    {
        ResetUpgradeButtonsInteractibility();
    }

    private void OnTimeValueChanged()
    {
        ResetUpgradeButtonsInteractibility();
    }

    private void OnSizeValueChanged()
    {
        ResetUpgradeButtonsInteractibility();
    }
    private void OnDisable()
    {
        SizeUpgradeButton.onSizeValueChanged -= OnSizeValueChanged;
        TimeUpgradeButton.onTimeValueChanged -= OnTimeValueChanged;
        PowerUpgradeButton.onPowerValueChanged -= OnPowerValueChanged;
    }
    private void ResetUpgradeButtonsInteractibility()
    {
        foreach (UpgradeButtonBase upgradeButton in upgradeButtons)
        {
            upgradeButton.ControlInteractibility();
        }
    }
}
