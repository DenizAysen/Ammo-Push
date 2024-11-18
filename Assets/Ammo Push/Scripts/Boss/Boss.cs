using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    #region Privates
    private float _maxHealth;
    private float _currentHealth;
    private float _targetPosZValue;
    private float _moveDuration, _fastMoveDuration;

    private BossData _bossData;

    private readonly string _targetTag = "Target";
    #endregion

    #region Serialized Fields
    [SerializeField] private BossAnimationController animationController;
    [SerializeField] private Image healthBarFill;
    #endregion

    #region Actions
    public static Action onBossDead;
    public static Action onPlayerDead;
    public static Action onReachedPlayer;
    public static Action<float> onGiveReward;
    #endregion
    #region Boss Initiliazation
    void Start()
    {
        Init();
    }
    private void Init()
    {
        _bossData = BossLevelDataHolder.Instance.GetCurrentLevelData().BossData;
        _maxHealth = _bossData.BossHealth;
        _targetPosZValue = _bossData.BossTargetPosZ;
        _moveDuration = _bossData.BossMoveDuration;
        _fastMoveDuration = _bossData.BossFastMoveDuration;
        _currentHealth = _maxHealth;
    }
    #endregion
    #region Event Subscription
    private void OnEnable()
    {
        BossSceneWeapon.onTakeDamage += OnTakeDamage;
        BossUITapToStart.onGameStarted += OnGameStarted;
        AmmoHolder.onAmmoFinished += OnAmmoFinished;
    }
    private void OnAmmoFinished() => StartCoroutine(MoveFastlytoPlayer());
    private void OnGameStarted() => MoveToPlayer();
    private void OnTakeDamage(float damage)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damage;
            healthBarFill.fillAmount = MathF.Abs(_currentHealth / _maxHealth);
            animationController.PlayTakeDamageAnimation();
            if (_currentHealth <= 0f)
            {
                Debug.Log("Boss oldu");
                _currentHealth = 0f;
                healthBarFill.fillAmount = 0f;
                StopMoving();
                onBossDead?.Invoke();
                animationController.PlayDeadAnimation();
                onGiveReward?.Invoke(((_maxHealth - _currentHealth) / _maxHealth));
            }
        }
        else
            return;
    }
    private void OnDisable()
    {
        BossSceneWeapon.onTakeDamage -= OnTakeDamage;
        BossUITapToStart.onGameStarted -= OnGameStarted;
        AmmoHolder.onAmmoFinished -= OnAmmoFinished;
    }
    #endregion
    #region Animations
    private void MoveToPlayer()
    {
        transform.DOMoveZ(_targetPosZValue, _moveDuration);
        animationController.PlayWalkAnimation();
    }
    private void StopMoving()
    {
        StopActiveTween();
    }

    private void StopActiveTween()
    {
        if (DOTween.IsTweening(transform))
        {
            transform.DOKill();
        }
    }

    private IEnumerator MoveFastlytoPlayer()
    {
        yield return new WaitForSeconds(.5f);
        if(_currentHealth > 0)
        {
            StopMoving();
            //transform.DOMoveZ(_targetPosZValue, _fastMoveDuration).OnComplete(() => 
            //{
                
            //});
            transform.DOMoveZ(_targetPosZValue, _fastMoveDuration);
        }
       
    } 
    #endregion
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StopActiveTween();
            onReachedPlayer?.Invoke();
            //onPlayerDead?.Invoke();
            Debug.Log(_currentHealth);
        }
        else if(other.tag == _targetTag)
        {
            StopActiveTween();
            onPlayerDead?.Invoke();
            onGiveReward?.Invoke(((_maxHealth - _currentHealth) / _maxHealth));
            animationController.PlayAttackAnimation();
        }
    }

}
