using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationController : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private GameObject[] bossMeshes;
    [SerializeField] private GameObject healthbar;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void PlayTakeDamageAnimation()
    {
        if (!_animator.IsInTransition(0))
        {
            _animator.SetBool("TakeDamage", true);
            StartCoroutine(ContiunetoPlayWalkAnimation());
        }     
    }
    public void PlayIdleAnimation()
    {
        _animator.SetBool("IsWalking", false);
    }
    private IEnumerator ContiunetoPlayWalkAnimation()
    {
        yield return new WaitForSeconds(.1f);
        _animator.SetBool("TakeDamage", false);
    }
    public void PlayDeadAnimation()
    {
        _animator.SetBool("IsDead", true);
        //healthbar.SetActive(false);
    }
    public void PlayJumpAnimationEvent()
    {
        healthbar.SetActive(true);
    }
    public void PlayAttackAnimation()
    {
        _animator.SetBool("Attack", true);
        _animator.SetBool("IsWalking", false);
        healthbar.SetActive(false);
    }
    public void PlayWalkAnimation() => _animator.SetBool("IsWalking", true);
}
