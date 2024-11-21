using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Tutle : MonsterBase
{

    protected override void Start()
    {
        SetMonsterData(MonsterType.Turtle);
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
