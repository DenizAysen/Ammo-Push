using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Inventory _inventory;

    private LevelDataSo _levelData;

    private string _dataPath;

    private List<WeaponType> _weaponTypes = new List<WeaponType>();

    private List<WeaponDataSo> _weaponDatas = new List<WeaponDataSo>();

    private List<int> _weaponMapValues = new List<int>();
    private void OnEnable()
    {
        AmmoBar.onSaveInventory += OnSaveInventory;
    }

    private void OnSaveInventory(Dictionary<WeaponDataSo, int> weaponMap)
    {
        _weaponDatas = weaponMap.Keys.ToList();
        _weaponMapValues = weaponMap.Values.ToList();

        for(int i = 0; i < _weaponDatas.Count; i++)
        {
            _inventory.SetInventoryValues(_weaponDatas[i].WeaponType, _weaponMapValues[i]);
        }

        SaveInventory();
    }
    private void OnDisable()
    {
        AmmoBar.onSaveInventory -= OnSaveInventory;
    }
    private void Start()
    {
        _dataPath = Application.dataPath + "/inventoryData.txt";

        _levelData = LevelManager.Instance.GetLevelData();

        for(int i = 0; i< _levelData.WeaponsInLevel.Length; i++)
        {
            _weaponTypes.Add(_levelData.WeaponsInLevel[i].WeaponType);
        }
        //Debug.Log(_levelData.WeaponsInLevel.Length);

        _inventory = new Inventory();

        _inventory.CreateDefaultItemList(_weaponTypes);

        SaveInventory();
    }

    //private void LoadInventory()
    //{
    //    string data = "";

    //    if(File.Exists(_dataPath))
    //    {
    //        data = File.ReadAllText(_dataPath);

    //        _inventory = JsonUtility.FromJson<Inventory>(data);

    //        if(_inventory == null)
    //            _inventory = new Inventory();
    //    }

    //    else
    //    {
    //        File.Create(_dataPath);
    //        _inventory = new Inventory();
    //    }
    //}
    private void SaveInventory()
    {
        string data = JsonUtility.ToJson(_inventory, true);
        File.WriteAllText(_dataPath, data);
    }
    
    //private void ClearInventory()
    //{
    //    _inventory.Clear();
    //    SaveInventory();
    //}
}
