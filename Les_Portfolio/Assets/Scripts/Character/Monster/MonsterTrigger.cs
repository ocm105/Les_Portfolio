using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTrigger : MonoBehaviour
{
    [SerializeField] MonsterBase monsterBase;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && monsterBase.isAttack)
        {
            IDamage target = other.GetComponent<IDamage>();
            if (target != null)
                target.OnDamage(monsterBase.monsterData.atk);
        }
    }
}
