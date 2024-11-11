using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleControl : MonoBehaviour
{
    private PlayerInfo playerInfo;

    private void Start()
    {
        playerInfo = this.GetComponent<PlayerInfo>();
    }

    public void Attack()
    {
        playerInfo._playerAniControl.AnimationChanger(PlayerAniState.Attack);
    }

    public void Skill(int num)
    {
        playerInfo._playerAniControl.AnimationChanger(PlayerAniState.Skill);
    }
}
