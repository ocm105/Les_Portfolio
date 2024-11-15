using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MonsterBase : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Animator animator;

    protected MonsterData monsterData;

    protected MonsterAniState monsterAniState;

    private void Start()
    {
        Init();
    }
    protected virtual void Init()
    {
        monsterData = GetMonsterData(MonsterType.none);
        AnimationChanger(MonsterAniState.Default);
    }
    private MonsterData GetMonsterData(MonsterType monsterType)
    {
        return GameDataManager.Instance.monster_Data[(int)monsterType];
    }

    #region Animation
    protected virtual void AnimationChanger(MonsterAniState state)
    {
        switch (state)
        {
            case MonsterAniState.Default:
                animator.Play("Move_Blend");
                break;
            case MonsterAniState.Attack:
                animator.Play("Attack");
                break;
            case MonsterAniState.Hit:
                animator.Play("Hit");
                break;
            case MonsterAniState.Die:
                animator.Play("Die");
                break;
            default:
                animator.Play(state.ToString(), 0, 0);
                break;
        }
        monsterAniState = state;
    }
    #endregion

    #region Event
    public virtual void Attack()
    {

    }
    public virtual void Hit()
    {

    }
    public virtual void Die()
    {

    }

    // public virtual void Skill() { }
    #endregion


}
