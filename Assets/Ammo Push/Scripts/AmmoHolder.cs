using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AmmoHolder : MonoBehaviour
{
    #region Private
    private Inventory _inventory;
    private GameObject _gmo;
    private int _randomAmmoIndex;
    private int _randomSpawnPointIndex;
    private int _totalAmunationCount;
    private int _randomShootPoint;
    private bool _noAmmo = false;
    private BossSceneWeapon _weapon;
    #endregion

    [SerializeField] private Transform canonPos;

    #region Collections
    private WeaponDataSo[] _weapons;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform[] shootPoints;

    private Dictionary<WeaponDataSo, List<GameObject>> _weaponMap = new Dictionary<WeaponDataSo, List<GameObject>>();
    #endregion

    public static Action onAmmoFinished;
    public static Action<WeaponDataSo> onWeaponFinished;

    #region Event Subscription
    private void OnEnable()
    {
        InventoryController.onInit += OnInit;
        ThrowButton.onShoot += OnShoot;
        BossUITapToStart.onGameStarted += OnGameStarted;
        Boss.onReachedPlayer += OnReachedPlayer;
    }

    private void OnReachedPlayer()
    {
        CheckAmunationCount();
        if (!_noAmmo)
        {
            foreach(WeaponDataSo weapon in _weapons)
            {
                ShootAllAmmo(weapon);
            }
        }
    }

    private void ShootAllAmmo(WeaponDataSo so)
    {
        if (_weaponMap[so].Count > 0)
        {
            onWeaponFinished?.Invoke(so);
            for(int i = _weaponMap[so].Count-1; i>=0 ; i--)
            {
                //_randomAmmoIndex = UnityEngine.Random.Range(0, _weaponMap[so].Count);
                _randomShootPoint = UnityEngine.Random.Range(0, shootPoints.Length);
                _weapon = _weaponMap[so][i].GetComponent<BossSceneWeapon>();
                _weaponMap[so].RemoveAt(i);
                _weapon.transform.position = canonPos.position;
                _weapon.Shoot(shootPoints[_randomShootPoint]);
                CheckAmunationCount();
            }
        }
    }

    private void OnGameStarted()
    {
        CheckAmunationCount();
    }

    private void OnShoot(WeaponDataSo so)
    {
        if (_weaponMap[so].Count > 0)
        {
            _randomAmmoIndex = UnityEngine.Random.Range(0, _weaponMap[so].Count);
            _randomShootPoint = UnityEngine.Random.Range(0, shootPoints.Length);
            _weapon = _weaponMap[so][_randomAmmoIndex].GetComponent<BossSceneWeapon>();
            _weaponMap[so].RemoveAt(_randomAmmoIndex);
            _weapon.transform.position = canonPos.position;
            _weapon.Shoot(shootPoints[_randomShootPoint]);
            CheckAmunationCount();
        }
        else
        {
            return;
        }
    }

    private void OnInit(Inventory inventory)
    {
        InitWeaponMap(inventory);
    }
    private void OnDisable()
    {
        InventoryController.onInit -= OnInit;
        ThrowButton.onShoot -= OnShoot;
        BossUITapToStart.onGameStarted -= OnGameStarted;
        Boss.onReachedPlayer -= OnReachedPlayer;
    } 
    #endregion
    private void InitWeaponMap(Inventory inventory)
    {
        _weapons = BossLevelDataHolder.Instance.GetCurrentLevelData().WeaponsInLevel;
        _inventory = inventory;

        InventoryItem[] inventoryItems = _inventory.GetInventoryItems();      

        for(int i = 0; i< _weapons.Length; i++)
        {

            for(int j = 0 ; j < inventoryItems.Length; j++)
            {
                if (inventoryItems[j].weaponType == _weapons[i].WeaponType)
                {
                    
                    if (inventoryItems[j].amount > 0)
                    {
                        for (int k = 0; k < inventoryItems[j].amount; k++)
                        {
                            _gmo = Instantiate(_weapons[i].BossLevelWeaponPrefab);
                            _randomSpawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
                            _gmo.transform.position = spawnPoints[_randomSpawnPointIndex].position;
                            _gmo.transform.parent = spawnPoints[_randomSpawnPointIndex];
                            if(!_weaponMap.TryAdd(_weapons[i], new List<GameObject> { _gmo }))
                            {
                                _weaponMap[_weapons[i]].Add(_gmo);
                            }

                        }
                    }
                    else
                    {
                        _weaponMap.Add(_weapons[i], new List<GameObject> { });
                    }
                    
                }
            }
        }        
    }
    private void CheckAmunationCount()
    {
        _totalAmunationCount = 0;

        for (int i = 0; i < _weapons.Length; i++)
        {
            _totalAmunationCount += _weaponMap[_weapons[i]].Count;
        }
        //Debug.Log(_totalAmunationCount);
        if(_totalAmunationCount <= 0)
        {
            onAmmoFinished?.Invoke();
            _noAmmo = true;
        }
    }
}
