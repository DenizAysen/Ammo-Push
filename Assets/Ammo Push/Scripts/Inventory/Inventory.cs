using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
public class Inventory 
{
    [SerializeField] private List<InventoryItem> _items = new List<InventoryItem>();
    private InventoryItem _item;

    public void CreateDefaultItemList(List<WeaponType> weaponTypes)
    {
        for (int i = 0; i < weaponTypes.Count; i++)
        {
            _items.Add(new InventoryItem(weaponTypes[i], 0));
        }
    }
    public void SetInventoryValues(WeaponType weaponType, int collectedWeaponAmount)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            _item = _items[i];

            if(_item.weaponType == weaponType)
            {
                _item.amount = collectedWeaponAmount;
            }
        }

        //DebugInventory();
    }

    public void DebugInventory()
    {
        foreach (InventoryItem item in _items)
        {
            Debug.Log("We have " + item.amount + " items in out " + item.weaponType + " list.");
        }
    }
    public void DebugInventorySize() => Debug.Log("Inventoryde " +_items.Count + " eleman var");
    public InventoryItem[] GetInventoryItems()
    {
        return _items.ToArray();
    }
    public void Clear() => _items.Clear();
}
