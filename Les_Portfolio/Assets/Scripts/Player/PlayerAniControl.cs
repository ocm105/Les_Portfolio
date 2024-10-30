using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAniControl : MonoBehaviour
{
    private PlayerInfo playerInfo;
    private Animator animator;

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
            default:
                animator.Play(state.ToString(), 0, 0);
                break;
        }
    }
    public void SetMoveValue(float value)
    {
        animator.SetFloat("Move", value);
    }
}
