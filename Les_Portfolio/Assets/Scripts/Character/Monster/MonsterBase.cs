using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : MonoBehaviour, IDamage
{
    protected DungeonController dungeonController;
    public void SetDungeonController(DungeonController control) { dungeonController = control; }
    public MonsterData monsterData { get; protected set; }
    public MonsterState monsterState { get; protected set; }
    protected MonsterAniState monsterAniState;

    protected GameObject player;
    public void SetPlayer(GameObject _player) { player = _player; }

    protected Animator animator;
    protected NavMeshAgent agent;
    public GameObject hpBar;
    private float damage = 0;
    private float atkTime = 0;
    public bool isAttack { get; protected set; }


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

    private void Update()
    {
        switch (dungeonController.gameView.dungeonState)
        {
            case DungeonState.Start:
                if (monsterState == MonsterState.Alive)
                {
                    if (MoveCheck())
                        Move();
                    else
                        Attack();
                }
                break;
            case DungeonState.Stop:
                Stop();
                break;
            case DungeonState.Fail:
            case DungeonState.Victory:
                Die();
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
    public void AnimationEvent_Attack(int value)
    {
        isAttack = value > 0 ? true : false;
    }

    private void Hit(float value)
    {
        if (monsterState == MonsterState.Die) return;

        if (value <= monsterData.def)
            damage = 0;
        else
        {
            damage = (value - monsterData.def) / monsterData.hp;
            // Debug.Log($"Monster에게 {damage}의 데미지");
            hpBar.transform.localScale = new Vector3(hpBar.transform.localScale.x - damage, 1, 1);
        }

        if (hpBar.transform.localScale.x <= 0)
        {
            dungeonController.gameView.KillSet();
            Die();
        }
        else
        {
            AnimationChanger(MonsterAniState.Hit);
        }
    }
    public void OnDamage(float _damage)
    {
        atkTime = 0;
        isAttack = false;
        Hit(_damage);
    }

    private void Stop()
    {
        if (!agent.isStopped)
            agent.isStopped = true;
    }
    private void Die()
    {
        if (monsterState != MonsterState.Die)
        {
            isAttack = false;
            monsterState = MonsterState.Die;
            StartCoroutine(DieCoroutine());
        }
    }
    private IEnumerator DieCoroutine()
    {
        AnimationChanger(MonsterAniState.Die);
        hpBar.transform.localScale = new Vector3(0, 1, 1);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        this.gameObject.SetActive(false);
        dungeonController.MonstartEnqueue(this);
    }
    #endregion
}
