using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCanvasPool : MonoBehaviour
{
    #region Collections
    private List<GameObject> ammoWorldCanvasList = new List<GameObject>();
    #endregion

    [SerializeField] private GameObject ammoWorldCanvasPrefab;

    #region Privates
    private WeaponDataSo _weaponData;
    private AmmoWorldCanvas _worldCanvas;
    private Transform _mainCamera;
    private GameObject _worldCanvasGameObject;
    private bool _canAddWeapon;
    #endregion

    private void Awake()
    {
        _canAddWeapon = false;
        _mainCamera = Camera.main.transform;
        for (int i = 0; i< 30; i++)
        {
            _worldCanvasGameObject = Instantiate(ammoWorldCanvasPrefab, transform);
            ammoWorldCanvasList.Add(_worldCanvasGameObject);
        }
    }

    #region Event Subsription
    private void OnEnable()
    {
        TapToStartButton.onGameStarted += OnGameStarted;
        Weapon.onAmmoAdded += OnAmmoAdded;
        Stopwatch.onGameOver += OnGameOver;
    }

    private void OnGameStarted() => _canAddWeapon=true;

    private void OnAmmoAdded(Weapon weapon)
    {
        if (!_canAddWeapon)
            return;

        _weaponData = weapon.WeaponData;
        
        for (int i = 0; i < ammoWorldCanvasList.Count; i++)
        {
            if (ammoWorldCanvasList[i].activeSelf)
                continue;

            ammoWorldCanvasList[i].SetActive(true);
            ammoWorldCanvasList[i].transform.position = weapon.gameObject.transform.position;
            _worldCanvas = ammoWorldCanvasList[i].GetComponent<AmmoWorldCanvas>();
            _worldCanvas.Init(_weaponData);
            _worldCanvas.SetRotation(_mainCamera);
            _worldCanvas.PlayAmmoWorldCanvasAnimation();
            break;
        }
        //Debug.Log("Burasi calisiyor");
    }
    private void OnGameOver() => _canAddWeapon = false;
    private void OnDisable()
    {
        Weapon.onAmmoAdded -= OnAmmoAdded;
        TapToStartButton.onGameStarted -= OnGameStarted;
        Stopwatch.onGameOver -= OnGameOver;
    } 
    #endregion
}
