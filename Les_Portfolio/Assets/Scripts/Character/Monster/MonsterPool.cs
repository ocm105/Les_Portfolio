using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    [SerializeField] GameObject player;

    [Range(1, 300)]
    [SerializeField] int monsterPoolSize = 0;
    [SerializeField] float delay = 0f;
    [SerializeField] Transform[] createPos;

    public Queue<MonsterBase> monsterPool = new Queue<MonsterBase>();

    private void Start()
    {
        MonsterPoolCreate();
    }

    public void StartMonsterWave()
    {
        StartCoroutine(StartMonsterWaveCoroutine());
    }
    private IEnumerator StartMonsterWaveCoroutine()
    {
        // while (true)
        {
            if (monsterPool.Count <= 0)
            {
                MonstartEnqueue(MonsterInstantiate());
            }
            MonsterDequeue();
            yield return new WaitForSeconds(delay);
        }
    }

    private void MonsterPoolCreate()
    {
        for (int i = 0; i < monsterPoolSize; i++)
        {
            monsterPool.Enqueue(MonsterInstantiate());
        }
    }
    public void MonstartEnqueue(MonsterBase monsterBase)
    {
        monsterPool.Enqueue(monsterBase);
    }
    private void MonsterDequeue()
    {
        MonsterBase monsterBase = monsterPool.Dequeue();
        monsterBase.gameObject.SetActive(true);
        monsterBase.Init();
    }
    private MonsterBase MonsterInstantiate()
    {
        GameObject go = Instantiate(AddressableManager.Instance.GetFBX(MonsterType.Slime.ToString()), GetRandomCreatePosition());
        MonsterBase monsterBase = go.GetComponent<MonsterBase>();
        monsterBase.SetTarget(player);
        go.SetActive(false);

        return monsterBase;
    }
    private Transform GetRandomCreatePosition()
    {
        int ran = Random.Range(0, createPos.Length);

        return createPos[ran];
    }
}
