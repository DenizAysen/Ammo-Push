using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoWorldCanvas : MonoBehaviour
{
    [SerializeField] private Image ammoImage;

    private Vector3 _dirToCamera;

    public void Init(WeaponDataSo weaponData)
    {
        ammoImage.sprite = weaponData.WeaponIcon;
    }
    public void PlayAmmoWorldCanvasAnimation()
    {
        if(DOTween.IsTweening(this))
        {
            return;
        }

        var sequance = DOTween.Sequence();
        sequance.Append(transform.DOScale(1.3f, .5f));
        sequance.Append(transform.DOScale(0f, .5f));
        sequance.Play();

        sequance.OnComplete(() => gameObject.SetActive(false));
    }
    public void SetRotation(Transform target)
    {
        _dirToCamera = (target.position - transform.position).normalized;
        transform.LookAt(transform.position + _dirToCamera * -1);
    }
}
