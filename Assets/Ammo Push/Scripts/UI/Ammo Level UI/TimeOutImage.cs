using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOutImage : MonoBehaviour
{
    [SerializeField] private RectTransform timeOutImage;
    [SerializeField] private RectTransform timeOutTextImage;

    public void PlayTimeOutAnimation()
    {
        var sequance = DOTween.Sequence();
        sequance.Append(timeOutImage.DOScale(Vector3.one, 1f));
        sequance.Append(timeOutTextImage.DOScale(Vector3.one, 1f));
        sequance.Play();
    }
}
