using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Slime : MonsterBase
{

    protected override void Start()
    {
        SetMonsterInfo(MonsterType.Slime);
    }

    protected override void AnimationChanger(MonsterAniState state)
    {
        base.AnimationChanger(state);
    }

    public override void Attack()
    {
        base.Attack();
    }
}
