using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapToStartButton : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private RectTransform tapToStartText;
    [SerializeField] private TapToStartTextAnimationSettings animationSettings;
    #endregion

    #region Privates
    private Button _tapToStartButton; 
    #endregion
    public static Action onGameStarted;
    private void Awake()
    {
        _tapToStartButton = GetComponent<Button>();
        _tapToStartButton.onClick.AddListener(OnClicked);
        PlayTapToStartTextAnimation();
    }
    private void OnClicked()
    {
        onGameStarted?.Invoke();
        gameObject.SetActive(false);
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
            //Debug.Log("Animasyon durdu");
        }
    }
    private void OnDisable()
    {
        _tapToStartButton.onClick.RemoveListener(OnClicked);
    }
}
