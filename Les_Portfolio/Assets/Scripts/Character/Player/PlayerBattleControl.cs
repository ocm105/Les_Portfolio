using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleControl : MonoBehaviour, IDamage
{
    private PlayerInfo playerInfo;
    private GameView gameView;

    public float hp { get; private set; }
    public float mp { get; private set; }
    private float def = 0;
    private float damage = 0f;

    private void Awake()
    {
        playerInfo = this.GetComponent<PlayerInfo>();
        gameView = FindObjectOfType<GameView>();
    }

    public void SetPlayerData(PlayerData playerData)
    {
        hp = playerData.hp;
        mp = playerData.mp;
        def = playerData.def;
    }

    public void Attack()
    {
        playerInfo._playerAniControl.AnimationChanger(PlayerAniState.Attack);
    }
    public void Hit(float value)
    {
        if (value <= def)
            damage = 0;
        else
        {
            damage = value - def;
            hp -= damage;
        }

        if (hp <= 0)
        {
            hp = 0;
            Die();
        }
        else
        {
            playerInfo._playerAniControl.AnimationChanger(PlayerAniState.Hit);
        }
        gameView.SetHP(hp);
    }
    public void OnDamage(float damage)
    {
        Debug.Log($"Player에게 {damage}의 데미지");
        Hit(damage);
    }
    private void Die()
    {
        StartCoroutine(DieCoroutine());
    }
    private IEnumerator DieCoroutine()
    {
        playerInfo._playerAniControl.AnimationChanger(PlayerAniState.Die);

        yield return new WaitForSeconds(2f);

        LoadingManager.Instance.SceneLoad(Constants.Scene.Main);
    }

    public void Skill(int num)
    {
        playerInfo._playerAniControl.AnimationChanger(PlayerAniState.Skill);
        gameView.SetHP(mp);
    }
}
