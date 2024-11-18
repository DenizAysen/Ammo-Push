using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUITapToStart : MonoBehaviour
{
    [SerializeField] private RectTransform tapToStartText;
    [SerializeField] private TapToStartTextAnimationSettings animationSettings;

    public static Action onGameStarted;

    private Button _button;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClicked);
        PlayTapToStartTextAnimation();
    }
    private void OnClicked()
    {
        onGameStarted?.Invoke();
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClicked);
    }
    private void PlayTapToStartTextAnimation()
    {
        var sequance = DOTween.Sequence();
        sequance.Append(tapToStartText.DOScale(animationSettings.EndValue, animationSettings.ScaleTime));
        sequance.Append(tapToStartText.DOScale(animationSettings.StartValue, animationSettings.ShrinkTime));
        sequance.Play();
        sequance.OnComplete(PlayTapToStartTextAnimation);
        if (!gameObject.activeSelf)
        {
            sequance.Kill();
        }
    }
}
