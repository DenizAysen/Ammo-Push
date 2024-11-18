using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class InventoryController : MonoBehaviour
{
    private Inventory _inventory;
    private string _dataPath;

    public static Action<Inventory> onInit;
    private void Start()
    {
        _dataPath = Application.dataPath + "/inventoryData.txt";
        LoadInventory();
        onInit?.Invoke(_inventory);
    }
    private void LoadInventory()
    {
        string data = "";

        if (File.Exists(_dataPath))
        {
            data = File.ReadAllText(_dataPath);

            _inventory = JsonUtility.FromJson<Inventory>(data);

            if (_inventory == null)
                _inventory = new Inventory();
        }

        else
        {
            File.Create(_dataPath);
            _inventory = new Inventory();
        }
    }
}
