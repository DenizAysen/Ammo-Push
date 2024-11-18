using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private GameObject ammoUIPrefab; 
    #endregion

    #region Privates
    private WeaponDataSo _weaponData;
    private AmmoUIContainer _uiContainer;
    private readonly string _x = "x";
    private bool _canAddToWeaponMap = true;
    #endregion

    #region Collections
    private Dictionary<WeaponDataSo, int> _weaponMap = new Dictionary<WeaponDataSo, int>();
    private Dictionary<WeaponDataSo, AmmoUIContainer> _uiContainerMap = new Dictionary<WeaponDataSo, AmmoUIContainer>();
    #endregion

    public static Action<Dictionary<WeaponDataSo, int>> onSaveInventory;

    #region Event Subscription
    private void OnEnable()
    {
        Weapon.onAmmoAdded += OnAmmoAdded;
        Stopwatch.onGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        _canAddToWeaponMap = false;
        onSaveInventory?.Invoke(_weaponMap);
    }
    private void OnAmmoAdded(Weapon weapon)
    {
        if (_canAddToWeaponMap)
        {
            _weaponData = weapon.WeaponData;
            if (_weaponMap.ContainsKey(_weaponData))
            {
                _weaponMap[_weaponData] += 1;
                if (_uiContainerMap.ContainsKey(_weaponData))
                {
                    _uiContainerMap[_weaponData].WeaponText.text = _x + _weaponMap[_weaponData];
                    _uiContainerMap[_weaponData].PlayTextAnimation();
                }
            }
            else
            {
                _weaponMap.Add(_weaponData, 1);
                _uiContainer = Instantiate(ammoUIPrefab, transform).GetComponent<AmmoUIContainer>();
                _uiContainer.WeaponImage.sprite = _weaponData.WeaponIcon;
                _uiContainer.WeaponText.text = _x + 1;
                _uiContainerMap.Add(_weaponData, _uiContainer);
            }
        }       
    }
    private void OnDisable()
    {
        Weapon.onAmmoAdded -= OnAmmoAdded;
        Stopwatch.onGameOver -= OnGameOver;
    } 
    #endregion

}
