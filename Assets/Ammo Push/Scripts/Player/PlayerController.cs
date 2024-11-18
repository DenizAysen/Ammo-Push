using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    #region Serialize Fields
    [SerializeField] private MobileJoystick joystick;
    [SerializeField] private PlayerAnimationController playerAnimator;
    [SerializeField] private PlayerPusherController playerPusherController;
    [SerializeField] private PowerDataSo powerData;
    [SerializeField] private float moveSpeed;
    #endregion

    #region Privates
    private Rigidbody _rb;
    private bool _canMove;

    private Vector3 _movementVector;
    #endregion
    //public static Action onPlayerMoving;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    #region Event Subscription
    private void OnEnable()
    {
        MobileJoystick.onJoystickReleased += OnJoystickReleased;
        playerPusherController.onPusherAnimationsEnded += OnPusherAnimationsEnded;
        TapToStartButton.onGameStarted += OnGameStarted;
        Stopwatch.onGameOver += OnGameOver;
        PowerUpgradeButton.onPowerValueChanged += OnPowerValueChanged;
    }

    private void OnPowerValueChanged()
    {
        SetPlayerMass();
    }
    private void SetPlayerMass()
    {
        int powerLevel = PlayerPrefs.GetInt("Power");
        _rb.mass = powerData.PlayerMassArray[powerLevel];
    }
    private void OnGameOver()
    {
        StopMovement();
    }
    private void OnGameStarted() => _canMove = true;
    private void OnPusherAnimationsEnded() => _canMove = true;
    private void OnJoystickReleased()
    {
        StopMovement();
    }
    private void OnDisable()
    {
        MobileJoystick.onJoystickReleased -= OnJoystickReleased;
        playerPusherController.onPusherAnimationsEnded -= OnPusherAnimationsEnded;
        TapToStartButton.onGameStarted -= OnGameStarted;
        Stopwatch.onGameOver -= OnGameOver;
        PowerUpgradeButton.onPowerValueChanged -= OnPowerValueChanged;
    }
    #endregion
    private void Start()
    {
        SetPlayerMass();
    }
    private void FixedUpdate()
    {
        ManageMovement();
    }
    private void ManageMovement()
    {
        _movementVector = joystick.MovementVector * moveSpeed * Time.fixedDeltaTime / Screen.width;

        _movementVector.z = _movementVector.y;
        _movementVector.y = 0;

        if(_canMove)
        {
            _rb.velocity = _movementVector;
            playerAnimator.ManageAnimations(_movementVector);
            playerPusherController.ManageRotate(_movementVector);
            //if(movementVector.magnitude > 0)
            //    onPlayerMoving?.Invoke();
        }
    }
    private void StopMovement()
    {
        _canMove = false;
        _rb.velocity = Vector3.zero;
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
