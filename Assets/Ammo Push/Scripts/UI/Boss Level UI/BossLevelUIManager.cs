using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLevelUIManager : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private GameObject tapToStartButton;
    [SerializeField] private float tapToStartActivationDuration;
    [SerializeField] private float rewardPanelOpenDuration;

    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject rewardPanel;

    [SerializeField] private RectTransform rewardPanelMovePos; 
    #endregion

    private float _duration = 1f;
    #region Event Subscription
    private void OnEnable()
    {
        Boss.onBossDead += OnBossDead;
        Boss.onPlayerDead += OnPlayerDead;
    }

    private void OnBossDead()
    {
        StartCoroutine(OpenRewardPanel());
    }

    private void OnPlayerDead()
    {
        StartCoroutine(OpenRewardPanel());
    }
    private void OnDisable()
    {
        Boss.onPlayerDead -= OnPlayerDead;
        Boss.onBossDead -= OnBossDead;
    } 
    #endregion
    private IEnumerator OpenRewardPanel()
    {
        yield return new WaitForSeconds(rewardPanelOpenDuration);
        gamePanel.SetActive(false);
        rewardPanel.SetActive(true);

        rewardPanel.GetComponent<RectTransform>().DOMove(rewardPanelMovePos.position, _duration);
    }
    void Start()
    {
        StartCoroutine(ActivateTapToStartButtonWithDelay());
    }

    private IEnumerator ActivateTapToStartButtonWithDelay()
    {
        yield return new WaitForSeconds(tapToStartActivationDuration);
        tapToStartButton.SetActive(true);
    }
}
