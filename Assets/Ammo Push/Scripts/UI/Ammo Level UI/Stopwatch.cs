using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private Image timeBar;
    [SerializeField] private Text timeText;

    [SerializeField] private RectTransform movePoint;
    [SerializeField] private Vector3 animSize;

    [SerializeField] private StopwatchAnimationSettings stopwatchAnimationSettings;
    [SerializeField] private TimeDataSo timeData;
    #endregion

    #region Privates
    private float _time;
    private float _currentTime;
    private int _timeLevel;
    #endregion

    #region Actions
    public static Action onGameOver;
    #endregion

    #region Event Subscription
    private void OnEnable()
    {
        TapToStartButton.onGameStarted += OnGameStarted;
        TimeUpgradeButton.onTimeValueChanged += OnTimeValueChanged;
    }

    private void OnTimeValueChanged()
    {
        UpdateTimeText();
    }

    private void Start()
    {
        UpdateTimeText();
    }
    private void OnGameStarted()
    {
        StartCoroutine(ReduceTime());
        PlayGameStartAnimation();
    }
    private void OnDisable()
    {
        TapToStartButton.onGameStarted -= OnGameStarted;
        TimeUpgradeButton.onTimeValueChanged -= OnTimeValueChanged;
    }
    #endregion
    private void UpdateTimeText()
    {
        _timeLevel = PlayerPrefs.GetInt("Time");
        _time = timeData.StopwatchValues[_timeLevel];
        timeText.text = _time.ToString();
    }
    #region Stopwatch Animation
    private IEnumerator ReduceTime()
    {
        _currentTime = _time;
        while (_currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            _currentTime--;
            timeBar.fillAmount = (_currentTime) / _time;
            timeText.text = _currentTime.ToString();
        }
        onGameOver?.Invoke();
    }
    private void PlayGameStartAnimation()
    {
        var sequance = DOTween.Sequence();
        Vector3 startPos = gameObject.GetComponent<RectTransform>().position;
        Vector3 startScale = transform.localScale;

        sequance.Append(transform.DOMove(movePoint.position, stopwatchAnimationSettings.MoveDuration));
        sequance.Join(transform.DOScale(stopwatchAnimationSettings.StopwatchAnimScale, stopwatchAnimationSettings.ScaleDuration));
        sequance.Append(transform.DOMove(startPos, stopwatchAnimationSettings.StartPosReturnDuration));
        sequance.Join(transform.DOScale(startScale, stopwatchAnimationSettings.ShrinkDuration));

        sequance.Play();
    } 
    #endregion
}
