using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Weapon : MonoBehaviour
{
    #region Variables
    [SerializeField] private WeaponDataSo weaponData;
    [SerializeField] private WeaponAnimationSettings weaponAnimationSettings;
    public WeaponDataSo WeaponData { get => weaponData; }

    private Rigidbody _rb;
    private float _duration = .2f;
    private readonly string _detectorTag = "Detector";
    private readonly string _conveyorTag = "Conveyor";
    #endregion

    #region Action
    public static Action<Weapon> onAmmoAdded; 
    #endregion
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void Init()
    {
        _rb.mass = weaponData.Mass;
        _rb.drag = _rb.mass - 5f;
        if (_rb.drag <= 0f)
            _rb.drag = 1f;
        //StartCoroutine(ActivateYConstrain());
    }
   
    private void AddAmmo()
    {
        onAmmoAdded?.Invoke(this);
        transform.DOScale(weaponAnimationSettings.EndScaleValue, weaponAnimationSettings.Duration);
        _rb.constraints = RigidbodyConstraints.None;
        _rb.drag = 0;
    }
    private IEnumerator ActivateYConstrain()
    {
        yield return new WaitForSeconds(_duration);
        _rb.constraints = RigidbodyConstraints.FreezePositionY;
    }
    #region Physics
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == _detectorTag)
        {
            AddAmmo();
        }
        if(other.gameObject.tag == _conveyorTag)
        {
            AddAmmo();
        }
    } 
    #endregion
}
