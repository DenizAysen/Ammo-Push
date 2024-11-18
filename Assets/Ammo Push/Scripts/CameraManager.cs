using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera initialCamera;
    [SerializeField] private float duration;
    void Start()
    {
        StartCoroutine(ChangeToPlayCamera());
    }

    private IEnumerator ChangeToPlayCamera()
    {
        yield return new WaitForSeconds(duration);
        initialCamera.Priority = 0;
    }
}
