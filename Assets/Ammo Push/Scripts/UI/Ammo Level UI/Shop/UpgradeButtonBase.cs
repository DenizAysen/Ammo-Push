using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UpgradeButtonBase : MonoBehaviour
{
    [SerializeField] protected Text levelText;
    [SerializeField] protected Text costText;
    [SerializeField] protected CostDataSo costData;

    protected abstract void SetInteractibility();
    protected abstract void UpdateTexts();
    public abstract void ControlInteractibility();
}
