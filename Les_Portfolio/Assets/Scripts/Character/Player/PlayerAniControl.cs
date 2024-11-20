using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAniControl : MonoBehaviour
{
    private PlayerInfo playerInfo;
    private Animator animator;

    private PlayerAttackLevel attackLevel = 0;
    public PlayerAniState playerAniState { get; private set; }

    private void Awake()
    {
        playerInfo = this.GetComponent<PlayerInfo>();
    }

    public void SetAnimator(Animator _animator)
    {
        animator = _animator;
    }

    public void AnimationChanger(PlayerAniState state)
    {
        switch (state)
        {
            case PlayerAniState.Default:
                animator.CrossFade("Move_Blend", 0.1f, 0);
                break;
            case PlayerAniState.Attack:
                StartCoroutine(AttackCoroutine());
                break;
            case PlayerAniState.Skill:
                StartCoroutine(AnimationCoroutine("Skill01"));
                break;
            case PlayerAniState.Hit:
                StartCoroutine(AnimationCoroutine(state.ToString()));
                break;
            case PlayerAniState.Victory:
            case PlayerAniState.Die:
                animator.CrossFade(state.ToString(), 0.1f, 0);
                break;
        }

        playerAniState = state;
    }

    public void SetMoveValue(float value)
    {
        animator.SetFloat("Move", value);
    }
    private IEnumerator AttackCoroutine()
    {
        if (playerAniState == PlayerAniState.Default)
        {
            string key = "Attack_Blend";

            animator.SetFloat("Attack", (float)attackLevel);
            attackLevel++;

            if (attackLevel == PlayerAttackLevel.Max)
                attackLevel = PlayerAttackLevel.Attack01;

            animator.CrossFade(key.ToString(), 0.1f, 0);

            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName(key));
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f);

            AnimationChanger(PlayerAniState.Default);
        }
        yield break;
    }
    private IEnumerator AnimationCoroutine(string key)
    {
        if (playerAniState == PlayerAniState.Default)
        {
            animator.CrossFade(key.ToString(), 0.1f, 0);

            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName(key));
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f);

            AnimationChanger(PlayerAniState.Default);
        }
        yield break;
    }
}
