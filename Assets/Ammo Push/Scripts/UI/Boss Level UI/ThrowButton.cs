using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ThrowButton : MonoBehaviour , IPointerDownHandler , IPointerUpHandler
{
    #region Serialized Fields
    [SerializeField] private Image weaponImage;
    [SerializeField] private Text weaponAmountText;
    [SerializeField] private Sprite zeroAmmoWithoutPressedSprite;
    [SerializeField] private Sprite zeroAmmoWithPressedSprite;
    [SerializeField] private float shrinkDuration;
    #endregion
    #region Privates
    private Button _button;
    private Image _buttonImage;
    private int _amount;
    private RectTransform _rectTransform;
    private bool _isShooting;
    private bool _notPressed;
    #endregion
    #region Properties
    public WeaponDataSo WeaponData { get; private set; }
    #endregion

    public static Action<WeaponDataSo> onShoot;

    #region Bullet Initialization
    private void Awake()
    {
        _button = GetComponent<Button>();
        _rectTransform = GetComponent<RectTransform>();
        _buttonImage = GetComponent<Image>();
    }
    private void OnEnable()
    {
        AmmoHolder.onWeaponFinished += OnWeaponFinished;
    }

    private void OnWeaponFinished(WeaponDataSo so)
    {
        if(WeaponData == so)
        {
            CloseButtonInteractibility();
        }
    }
    private void OnDisable()
    {
        AmmoHolder.onWeaponFinished -= OnWeaponFinished;
    }
    public void Init(WeaponDataSo weaponData)
    {
        WeaponData = weaponData;
        weaponImage.sprite = WeaponData.WeaponIcon;
    }

    public void SetAmmoAmount(int amount)
    {
        _amount = amount;
        weaponAmountText.text = _amount.ToString();
        //Debug.Log(WeaponData.WeaponType + " turunden " + _amount + " tane var");
        StartCoroutine(PlayInitAnimation());             
    }

    private IEnumerator PlayInitAnimation()
    {
        yield return new WaitForSeconds(2f);
        _rectTransform.DOScale(1f, .5f).OnComplete(() =>
        {
            if (_amount <= 0)
            {
                _notPressed = true;
                CloseButtonInteractibility(_notPressed);
            }
        });
    }

    private void CloseButtonInteractibility(bool notPressed = false)
    {
        _button.interactable = false;
        weaponAmountText.text = 0.ToString();
        _buttonImage.sprite = zeroAmmoWithoutPressedSprite;
        if (notPressed)
            _buttonImage.sprite = zeroAmmoWithoutPressedSprite;
    }
    #endregion

    public void OnPointerDown(PointerEventData eventData)
    {
        _isShooting = true;
    }
    private IEnumerator ShootAmmo()
    {     
        if(_amount > 0)
        {
            yield return new WaitForSeconds(.2f);
            onShoot?.Invoke(WeaponData);
            _amount--;
            weaponAmountText.text = _amount.ToString();
            if (_amount <= 0)
            {               
                CloseButtonInteractibility();
                _isShooting = false;
            }
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isShooting = false;
    }
    public void Shrink()
    {
        transform.DOScale(Vector3.zero, shrinkDuration);
        _button.interactable = false;
    }
    private void Update()
    {
        if (_isShooting)
        {
            StartCoroutine(ShootAmmo());
        }
        else
        {
            return;
        }
    }
}
