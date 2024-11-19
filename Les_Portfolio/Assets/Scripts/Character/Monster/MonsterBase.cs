using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : MonoBehaviour, IDamage
{
    const float HP_NOMALIZE = 0.01f;

    protected MonsterData monsterData;
    protected MonsterState monsterState;
    protected MonsterAniState monsterAniState;

    protected Animator animator;
    protected NavMeshAgent agent;

    protected GameObject target;
    public GameObject hpBar;
    private float damage = 0;
    private float atkTime = 0;
    private bool isAttack = false;

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
    }

    protected virtual void Start()
    {
        Init(MonsterType.none);
    }
    protected void Init(MonsterType monsterType)
    {
        monsterData = GetMonsterData(monsterType);
        agent.speed = monsterData.speed;

        atkTime = 0;
        damage = 0;
        isAttack = false;
        monsterState = MonsterState.Alive;
        AnimationChanger(MonsterAniState.Idle);
    }

    protected MonsterData GetMonsterData(MonsterType monsterType)
    {
        return GameDataManager.Instance.monster_Data[(int)monsterType];
    }

    private void Update()
    {
        switch (monsterState)
        {
            case MonsterState.Alive:
                if (MoveCheck())
                {
                    if (!isAttack) Move(target.transform.position);
                }
                else
                {
                    MoveStop();
                    Attack();
                }
                break;
            case MonsterState.Stop:
                break;
            case MonsterState.Die:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (monsterState != MonsterState.Alive) return;
        if (!isAttack) return;

        IDamage target = other.GetComponent<IDamage>();
        if (target != null)
        {
            target.OnDamage(monsterData.atk);
        }
    }

    #region Animation
    protected virtual void AnimationChanger(MonsterAniState state)
    {
        switch (state)
        {
            case MonsterAniState.Idle:
                animator.SetBool("Move", false);
                break;
            case MonsterAniState.Walk:
                animator.SetBool("Move", true);
                break;
            case MonsterAniState.Attack:
            case MonsterAniState.Hit:
                animator.CrossFade(state.ToString(), 0, 0);
                break;
            case MonsterAniState.Die:
                animator.Play(state.ToString());
                break;
        }
        monsterAniState = state;
    }
    #endregion

    #region Move
    private bool MoveCheck()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
            return false;
        else
            return true;
    }
    private void Move(Vector3 position)
    {
        agent.isStopped = false;
        agent.SetDestination(position);
        AnimationChanger(MonsterAniState.Walk);
    }
    private void MoveStop()
    {
        if (!agent.isStopped)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }
    }
    #endregion

    #region Event
    public virtual void Attack()
    {
        atkTime += Time.deltaTime;

        if (atkTime >= monsterData.atkspeed)
        {
            AnimationChanger(MonsterAniState.Attack);
            atkTime = 0;
        }
    }
    public void AnimationEvent_Attack(int value)
    {
        isAttack = value == 1 ? true : false;
    }

    private void Hit(float value)
    {
        if (value <= monsterData.def)
            damage = 0;
        else
        {
            damage = (value - monsterData.def) * HP_NOMALIZE;
            hpBar.transform.localScale = Vector2.right * Mathf.Clamp(hpBar.transform.localScale.x - damage, 0, 1);
        }

        if (hpBar.transform.localScale.x <= 0)
            Die();
        else
        {
            if (!isAttack) AnimationChanger(MonsterAniState.Hit);
        }
    }
    public void OnDamage(float damage)
    {
        Hit(damage);
    }

    public virtual void Die()
    {
        monsterState = MonsterState.Die;
        MoveStop();
        AnimationChanger(MonsterAniState.Die);
    }
    public virtual void Stop()
    {
        monsterState = MonsterState.Stop;
        MoveStop();
    }
    #endregion
}
