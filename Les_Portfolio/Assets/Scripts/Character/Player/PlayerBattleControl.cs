using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleControl : BattleBase
{
    private PlayerInfo playerInfo;

    private void Awake()
    {
        playerInfo = this.GetComponent<PlayerInfo>();
    }

    public override void Attack()
    {
        playerInfo._playerAniControl.AnimationChanger(PlayerAniState.Attack);
    }
    public override void Hit()
    {
        base.Hit();
    }

    public override void Die()
    {
        base.Die();
    }

    public void Skill(int num)
    {
        playerInfo._playerAniControl.AnimationChanger(PlayerAniState.Skill);
    }
}
