using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAniControl : MonoBehaviour
{
    private PlayerInfo playerInfo;
    private Animator animator;

    private PlayerAttackLevel attackLevel = 0;
    public PlayerAniState playerAniState { get; private set; }
    private void Start()
    {
        playerInfo = this.GetComponent<PlayerInfo>();
        animator = playerInfo._player.GetComponent<Animator>();
    }

    public void AnimationChanger(PlayerAniState state)
    {
        switch (state)
        {
            case PlayerAniState.Default:
                animator.Play("Move_Blend");
                break;
            case PlayerAniState.Attack:
                StartCoroutine(AttackCoroutine());
                break;
            case PlayerAniState.Skill:
                StartCoroutine(SkillCoroutine());
                break;
            default:
                animator.Play(state.ToString(), 0, 0);
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

            animator.Play(key);

            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName(key));
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

            AnimationChanger(PlayerAniState.Default);
        }
        yield break;
    }
    private IEnumerator SkillCoroutine()
    {
        if (playerAniState == PlayerAniState.Default)
        {
            string key = "Skill01";
            animator.Play(key);

            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName(key));
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

            AnimationChanger(PlayerAniState.Default);
        }
        yield break;
    }
}
