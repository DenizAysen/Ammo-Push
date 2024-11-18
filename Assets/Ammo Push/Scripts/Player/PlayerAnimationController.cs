using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    #region Serialize Fields
    [SerializeField] private float moveSpeedMultiplier;
    [SerializeField] private Transform rendererTransform;
    #endregion

    #region Privates
    private Animator _anim;
    private readonly string _pushState = "Push";
    private readonly string _idleState = "Idle"; 
    #endregion
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    #region Event Subscription
    private void OnEnable()
    {
        MobileJoystick.onJoystickReleased += OnJoystickReleased;
        Stopwatch.onGameOver += OnGameOver;
    }

    private void OnGameOver() => PlayIdleAnimation();
    private void OnJoystickReleased() => PlayIdleAnimation();
    private void OnDisable()
    {
        MobileJoystick.onJoystickReleased -= OnJoystickReleased;
        Stopwatch.onGameOver -= OnGameOver;
    } 
    #endregion
    public void ManageAnimations(Vector3 movement)
    {
        if (movement.magnitude > 0)
        {
            _anim.SetFloat("moveSpeed", movement.magnitude * moveSpeedMultiplier);
            PlayPushAnimation();

            rendererTransform.forward = movement.normalized;
        }

        else
            PlayIdleAnimation();
    }
    private void PlayPushAnimation()
    {
        _anim.Play(_pushState);
    }
    private void PlayIdleAnimation()
    {
        _anim.Play(_idleState);
    }
}
