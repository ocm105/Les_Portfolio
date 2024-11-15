using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleControl : MonoBehaviour
{
    private PlayerInfo playerInfo;

    private void Awake()
    {
        playerInfo = this.GetComponent<PlayerInfo>();
    }

    public void Attack()
    {
        playerInfo._playerAniControl.AnimationChanger(PlayerAniState.Attack);
    }
    public void Hit()
    {
    }

    public void Die()
    {
    }

    public void Skill(int num)
    {
        playerInfo._playerAniControl.AnimationChanger(PlayerAniState.Skill);
    }
}
