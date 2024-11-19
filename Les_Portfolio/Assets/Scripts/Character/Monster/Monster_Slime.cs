using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Slime : MonsterBase
{

    protected override void Start()
    {
        Init(MonsterType.Slime);
    }

    protected override void AnimationChanger(MonsterAniState state)
    {
        base.AnimationChanger(state);
    }

    public override void Attack()
    {
        base.Attack();
    }
    public override void Die()
    {
        base.Die();
    }
    public override void Stop()
    {
        base.Stop();
    }
}
