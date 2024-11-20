using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : MonoBehaviour, IDamage
{
    public MonsterData monsterData { get; protected set; }
    public MonsterState monsterState { get; protected set; }
    public MonsterAniState monsterAniState { get; protected set; }
    public Action<MonsterBase> dieCall;

    protected Animator animator;
    protected NavMeshAgent agent;

    protected GameObject player;

    public GameObject hpBar;
    private float damage = 0;
    private float atkTime = 0;


    private void Awake()
    {
        animator = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
    }

    protected virtual void Start()
    {
        SetMonsterData(MonsterType.none);
    }
    public void Init()
    {
        atkTime = 0;
        damage = 0;
        hpBar.transform.localScale = Vector3.one;
        this.transform.localPosition = Vector3.zero;
        monsterState = MonsterState.Alive;
        this.gameObject.SetActive(true);
    }

    protected void SetMonsterData(MonsterType monsterType)
    {
        monsterData = GameDataManager.Instance.monster_Data[(int)monsterType];
        agent.speed = monsterData.speed;
    }
    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }

    private void Update()
    {
        switch (monsterState)
        {
            case MonsterState.Alive:
                if (MoveCheck())
                    Move();
                else
                    Attack();
                break;
            case MonsterState.Stop:
                break;
            case MonsterState.Die:
                break;
        }
    }

    #region Animation
    protected virtual void AnimationChanger(MonsterAniState state)
    {
        switch (state)
        {
            case MonsterAniState.Idle:
            case MonsterAniState.Walk:
                animator.SetBool("Move", state == MonsterAniState.Walk ? true : false);
                agent.isStopped = false;
                break;
            case MonsterAniState.Attack:
            case MonsterAniState.Hit:
            case MonsterAniState.Die:
                animator.CrossFade(state.ToString(), 0.1f, 0);
                agent.isStopped = true;
                break;
        }
        monsterAniState = state;
    }
    #endregion

    #region Move
    private bool MoveCheck()
    {
        agent.SetDestination(player.transform.position);

        if (agent.remainingDistance <= agent.stoppingDistance)
            return false;
        else
            return true;
    }
    private void Move()
    {
        AnimationChanger(MonsterAniState.Walk);
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

    private void Hit(float value)
    {
        if (value <= monsterData.def)
            damage = 0;
        else
        {
            damage = (value - monsterData.def) / monsterData.hp;
            Debug.Log($"Monster에게 {damage}의 데미지");
            hpBar.transform.localScale = new Vector3(Mathf.Clamp(hpBar.transform.localScale.x - damage, 0, 1), 1, 1);
        }

        if (hpBar.transform.localScale.x <= 0)
            Die();
        else
        {
            AnimationChanger(MonsterAniState.Hit);
        }
    }
    public void OnDamage(float _damage)
    {
        atkTime = 0;
        Hit(_damage);
    }

    private void Stop()
    {
        monsterState = MonsterState.Stop;
        agent.isStopped = true;
    }
    private void Die()
    {
        StartCoroutine(DieCoroutine());
    }
    private IEnumerator DieCoroutine()
    {
        monsterState = MonsterState.Die;
        AnimationChanger(MonsterAniState.Die);

        yield return new WaitForSeconds(2f);

        this.gameObject.SetActive(false);
        dieCall?.Invoke(this);
    }
    #endregion
}
