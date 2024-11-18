using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileJoystick : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private RectTransform joystickOutline;
    [SerializeField] private RectTransform joystickKnob;
    [SerializeField] private float moveFactor;
    #endregion

    #region Privates
    private bool _canControlJoyStick;
    private Vector3 _clickedPos;
    #endregion

    public static Action onJoystickReleased;

    #region Properties
    public Vector3 MovementVector { get; private set; }
    #endregion

    #region Unity Methods
    void Start()
    {
        HideJoystick();
    }

    // Update is called once per frame
    void Update()
    {
        if (_canControlJoyStick)
            ControlJoystick();
    }
    #endregion

    #region Joystick Methods
    public void PressedOnJoystickZone()
    {
        _clickedPos = Input.mousePosition;
        joystickOutline.position = _clickedPos;

        ShowJoystick();
    }
    private void ShowJoystick()
    {
        joystickOutline.gameObject.SetActive(true);
        _canControlJoyStick = true;
    }
    private void HideJoystick()
    {
        joystickOutline.gameObject.SetActive(false);
        _canControlJoyStick = false;

        MovementVector = Vector3.zero;
    }
    private void ControlJoystick()
    {
        Vector3 currentPos = Input.mousePosition;
        Vector3 direction = currentPos - _clickedPos;

        float moveMagnitude = direction.magnitude * moveFactor / Screen.width;

        moveMagnitude = Mathf.Min(moveMagnitude, joystickOutline.rect.width / 2);

        MovementVector = direction.normalized * moveMagnitude;

        Vector3 targetPos = _clickedPos + MovementVector;

        joystickKnob.position = targetPos;

        if (Input.GetMouseButtonUp(0))
        {
            HideJoystick();
            onJoystickReleased?.Invoke();
        }
    } 
    #endregion
}
