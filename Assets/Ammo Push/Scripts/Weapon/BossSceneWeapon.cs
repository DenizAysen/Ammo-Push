using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSceneWeapon : MonoBehaviour
{
    [SerializeField] private WeaponDataSo weaponData;
    [SerializeField] private BossLevelWeaponAnimationSettings animationSettings;

    private float _damage;
    private TrailRenderer _trailRenderer;
    private readonly string _bossTag = "Boss";

    public static Action<float> onTakeDamage;
    private void Awake()
    {
        Init();
        _trailRenderer = GetComponent<TrailRenderer>();
    }
    private void Init()
    {
        _damage = weaponData.Damage;
        StartCoroutine(ReturnToNormalSize());
    }

    private IEnumerator ReturnToNormalSize()
    {
        yield return new WaitForSeconds(1.5f);
        transform.DOScale(animationSettings.ScaleAnimationSettings.TargetScale, 
            animationSettings.ScaleAnimationSettings.TargetScaleDuration);
        _trailRenderer.enabled = false;
    }

    public void Shoot(Transform target)
    {
        transform.rotation = Quaternion.identity;
        transform.DOJump(target.position,animationSettings.ShootAnimationSettings.JumpForce,
            animationSettings.ShootAnimationSettings.NumJumps,animationSettings.ShootAnimationSettings.JumpDuration);
        //transform.DOLocalRotate(animationSettings.ShootAnimationSettings.TargetRotation, animationSettings.ShootAnimationSettings.RotateDuration);
        transform.DOScale(animationSettings.ShootAnimationSettings.TargetScale, animationSettings.ShootAnimationSettings.TargetScaleDuration);
        _trailRenderer.enabled=true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == _bossTag)
        {
            onTakeDamage?.Invoke(_damage);
            gameObject.SetActive(false);
        }
    }
}
