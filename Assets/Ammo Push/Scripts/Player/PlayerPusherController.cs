using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerPusherController : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private Transform pusherPlane;
    [SerializeField] private Transform secondCyclinder;
    [SerializeField] private Transform forcePos;

    [SerializeField] private SizeDataSo sizeData;
    [SerializeField] private PowerDataSo powerData;
    [SerializeField] private PusherAnimationSettings pusherAnimationSettings;
    [SerializeField] private LayerMask weaponLayerMask;
    #endregion

    #region Privates
    Rigidbody _pusherRb;
    private bool _canRotate;
    private bool _canPush;
    private Transform _puhserParent;
    private Rigidbody _secondCyclinderRb;
    private float _timer;
    private float _forceMultiplier;
    #endregion

    public Action onPusherAnimationsEnded;

    private void Awake()
    {
        //Force data yap
        _timer = 0f;
        _canRotate = true;
        _pusherRb = GetComponent<Rigidbody>();
        _puhserParent = transform.parent;
        _secondCyclinderRb = secondCyclinder.GetComponent<Rigidbody>();
    }

    #region Event Subscription
    private void OnEnable()
    {
        MobileJoystick.onJoystickReleased += OnJoystickReleased;
        SizeUpgradeButton.onSizeValueChanged += OnSizeValueChanged;
        PowerUpgradeButton.onPowerValueChanged += OnPowerValueChanged;
    }

    private void OnPowerValueChanged()
    {
        SetCylindersMasses();
    }
    private void SetCylindersMasses()
    {
        int powerLevel = PlayerPrefs.GetInt("Power");
        _pusherRb.mass = powerData.PlayerMassArray[powerLevel];
        _secondCyclinderRb.mass = _pusherRb.mass;
        _forceMultiplier = _pusherRb.mass * 5f;
    }
    private void OnSizeValueChanged()
    {
        SetCylendersSize();
    }
    private void SetCylendersSize()
    {
        int sizeLevel = PlayerPrefs.GetInt("Size");
        Vector3 newSize = new Vector3(sizeData.PusherSizeValues[sizeLevel], transform.localScale.y, transform.localScale.z);
        transform.localScale = newSize;
        _secondCyclinderRb.transform.localScale = newSize;
    }
    private void OnJoystickReleased()
    {
        secondCyclinder.gameObject.SetActive(true);
        pusherPlane.gameObject.SetActive(true);
        _canPush = true;
    }
   
    private void OnDisable()
    {
        MobileJoystick.onJoystickReleased -= OnJoystickReleased;
        SizeUpgradeButton.onSizeValueChanged -= OnSizeValueChanged;
        PowerUpgradeButton.onPowerValueChanged -= OnPowerValueChanged;
    }
    #endregion
    private void Start()
    {
        SetCylendersSize();
        SetCylindersMasses();
    }
    #region Cylinder Animations
    public void ManageRotate(Vector3 movement)
    {
        _canRotate = movement.magnitude > 0 ? true : false;

        if (_canRotate)
        {
            transform.Rotate(new Vector3(100f, 0, 0) * Time.deltaTime);
        }
        else
            return;
    } 
    private void PlayPullAnimation()
    {
        var sequance = DOTween.Sequence();

        sequance.Append(pusherPlane.DOScaleZ(pusherAnimationSettings.PlaneDefaultLocalZScale, pusherAnimationSettings.ShrinkTime));
        sequance.Join(secondCyclinder.DOLocalMove(pusherAnimationSettings.CyclinderLocalPulPoint, pusherAnimationSettings.PullTime));
        sequance.Play();
        sequance.OnComplete(() =>
        {
            secondCyclinder.gameObject.SetActive(false);
            pusherPlane.gameObject.SetActive(false);
            onPusherAnimationsEnded?.Invoke();
        });
    }
    #endregion
    #region Physics
    private void Push()
    {
        //_secondCyclinderRb.AddForce(_puhserParent.forward * 800f, ForceMode.Force);
        _secondCyclinderRb.AddForce(_puhserParent.forward * _forceMultiplier, ForceMode.Impulse);
        _timer += Time.fixedDeltaTime;
        SetPlaneScale();
        if (Vector3.Distance(transform.position, secondCyclinder.position) >= 7f || _timer >= 1.5f)
        {
            _canPush = false;
            _secondCyclinderRb.velocity = Vector3.zero;
           PlayPullAnimation();
            _timer = 0f;
        }
    }
    private void SetPlaneScale()
    {
        float distance_percantage = Mathf.Abs((transform.localPosition.z - secondCyclinder.localPosition.z) / 7);
        if (distance_percantage > 1f) distance_percantage = 1f;
        pusherPlane.localScale = new Vector3(pusherPlane.localScale.x, pusherPlane.localScale.y, pusherAnimationSettings.PlaneDefaultLocalZScale
            + (1.49f * distance_percantage));

    }
    private void FixedUpdate()
    {
        if (_canPush)
        {
            Push();
        }
        FixAngularVelocity();
    }
    
    private void FixAngularVelocity()
    {
        if (_pusherRb.angularVelocity.magnitude > 0f)
        {
            _pusherRb.angularVelocity = Vector3.zero;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            return;
        }
    }
    #endregion
}
