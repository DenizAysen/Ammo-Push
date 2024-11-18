using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUIContainer : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private Image weaponImage;
    [SerializeField] private Text weaponText; 
    #endregion

    #region Propesties
    public Image WeaponImage { get => weaponImage; set => weaponImage = value; }
    public Text WeaponText { get => weaponText; set => weaponText = value; } 
    #endregion
    public void PlayTextAnimation()
    {
        weaponText.transform.DOScale(1.2f, .05f).OnComplete(() => weaponText.transform.DOScale(1f, .05f));
    }
}
