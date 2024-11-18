using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowButtonPanel : MonoBehaviour
{
    private Inventory _inventory;

    [SerializeField] private GameObject throwButtonPrefab;

    private WeaponDataSo[] _weapons;

    private List<ThrowButton> _throwButtonList = new List<ThrowButton>();
    private void OnEnable()
    {
        InventoryController.onInit += OnInit;
        Boss.onPlayerDead += OnPlayerDead;
    }

    private void OnPlayerDead()
    {
        foreach(ThrowButton throwButton in _throwButtonList)
        {
            throwButton.Shrink();
        }
    }

    private void OnInit(Inventory inventory)
    {
        _inventory = inventory;

        //_inventory.DebugInventory();
        //_inventory.DebugInventorySize();

        _weapons = BossLevelDataHolder.Instance.GetCurrentLevelData().WeaponsInLevel;

        InventoryItem[] inventoryItems = _inventory.GetInventoryItems();

        for(int i  = 0; i < _weapons.Length; i++)
        {
            ThrowButton throwButton = Instantiate(throwButtonPrefab, transform).GetComponent<ThrowButton>();
            throwButton.Init(_weapons[i]);
            _throwButtonList.Add(throwButton);
        }

        for(int i = 0; i < _throwButtonList.Count; i++)
        {
            for(int j = 0;  j< inventoryItems.Length; j++)
            {
                if (_throwButtonList[i].WeaponData.WeaponType == inventoryItems[j].weaponType)
                {
                    _throwButtonList[i].SetAmmoAmount(inventoryItems[j].amount);
                }
            }
        }
    }
    private void OnDisable()
    {
        InventoryController.onInit -= OnInit;
        Boss.onPlayerDead -= OnPlayerDead;
    }
}
