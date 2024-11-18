using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem 
{
    public WeaponType weaponType;
    public int amount;

    public InventoryItem(WeaponType weaponType, int amount)
    {
        this.weaponType = weaponType;
        this.amount = amount;
    }

}
