using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] private WeaponSpawnArea[] spawnAreas;
    private GameObject bullet;
    void Start()
    {
        for (int i = 0; i < spawnAreas.Length; i++)
        {
            foreach(Transform spawnPoint in spawnAreas[i].SpawnPoints)
            {
                bullet = Instantiate(spawnAreas[i].WeaponData.WeaponPrefab, spawnPoint.position, 
                    spawnAreas[i].WeaponData.WeaponPrefab.transform.localRotation, spawnPoint);
                bullet.GetComponent<Weapon>().Init();
            }
        }
    }
}

[Serializable]
public class WeaponSpawnArea
{
    public Transform[] SpawnPoints;
    public WeaponDataSo WeaponData;
}
