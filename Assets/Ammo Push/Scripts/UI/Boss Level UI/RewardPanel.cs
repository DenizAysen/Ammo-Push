using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RewardPanel : MonoBehaviour
{
    #region Privates
    private bool _canMoveToNextLevel;
    private int _levelIndex;
    private float _totalReward;
    private RewardData _rewardData; 
    #endregion

    #region Serialized Fields
    [SerializeField] private Text minRewardText;
    [SerializeField] private Text totalRewardText;
    [SerializeField] private Text damagePercantageText;
    [SerializeField] private Button nextButton; 
    #endregion
    #region Event Subscription
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        Boss.onBossDead += OnBossDead;
        Boss.onPlayerDead += OnPlayerDead;
        Boss.onGiveReward += OnGiveReward;
        nextButton.onClick.AddListener(SaveData);
    }

    private void OnGiveReward(float percantage)
    {
        StartCoroutine(PlayRewardPanelAnimations(percantage));       
    }
    private void OnPlayerDead() => _canMoveToNextLevel = false;
    private void OnBossDead() => _canMoveToNextLevel = true;
    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    private void UnsubscribeEvents()
    {
        Boss.onBossDead -= OnBossDead;
        Boss.onPlayerDead -= OnPlayerDead;
        Boss.onGiveReward -= OnGiveReward;
        nextButton.onClick.RemoveListener(SaveData);
    }
    #endregion
    private void SaveData()
    {
        _levelIndex = PlayerPrefs.GetInt("Level");
        if (_canMoveToNextLevel)
        {
            _levelIndex++;
            PlayerPrefs.SetInt("Level", _levelIndex % 3);
            _levelIndex = PlayerPrefs.GetInt("Level");
        }

        PlayerPrefs.SetFloat("Money", _totalReward);
        SceneManager.LoadScene(_levelIndex);

    }
    private IEnumerator PlayRewardPanelAnimations(float damagePercantage)
    {
        yield return new WaitForSeconds(1f);
        float currentPercantage = 0f;
        while(currentPercantage < damagePercantage)
        {
            currentPercantage+= Time.deltaTime * 2f;
            if (currentPercantage >= damagePercantage)
            {
                currentPercantage = damagePercantage;
                damagePercantageText.text = (damagePercantage * 100f).ToString("F1") + "%";
                ShowTotalRewardText(currentPercantage);
                break;
            }
            damagePercantageText.text = (currentPercantage * 100f).ToString("F1") + "%";
            yield return new WaitForSeconds(0.065f);
        }
        if(currentPercantage == 0f)
        {
            damagePercantageText.text = (currentPercantage * 100f).ToString("F1") + "%";
            ShowTotalRewardText(currentPercantage);
        }        
    }
    private void ShowTotalRewardText(float percantage)
    {
        totalRewardText.gameObject.SetActive(true);
        _totalReward = _rewardData.MinPrize + (percantage * _rewardData.MaxPrize);
        totalRewardText.text = _totalReward.ToString("F1");
        nextButton.gameObject.SetActive(true);
    }
    private void Start()
    {
        _rewardData = BossLevelDataHolder.Instance.GetCurrentLevelData().RewardData;
        minRewardText.text = _rewardData.MinPrize.ToString();
    }
}
