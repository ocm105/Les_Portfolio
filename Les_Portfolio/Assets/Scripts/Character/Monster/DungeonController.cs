using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    [SerializeField] GameObject player;
    public GameView gameView { get; private set; }

    [Range(1, 300)]
    [SerializeField] int monsterPoolSize = 0;
    [SerializeField] Transform[] createPos;
    [SerializeField] float delay = 0f;
    private float time = 0f;

    public Queue<MonsterBase> monsterPool = new Queue<MonsterBase>();


    private void Awake()
    {
        gameView = GameObject.FindObjectOfType<GameView>();
    }

    private void Start()
    {
        MonsterPoolCreate();
    }
    private void Update()
    {
        switch (gameView.dungeonState)
        {
            case DungeonState.Start:
                time += Time.deltaTime;
                if (time >= delay)
                {
                    if (monsterPool.Count <= 0)
                    {
                        MonstartEnqueue(MonsterInstantiate());
                    }
                    MonsterDequeue();
                    time = 0;
                }
                break;
            case DungeonState.Stop:
                break;
            case DungeonState.Fail:
            case DungeonState.Victory:
                break;
        }
    }

    public void MonstartEnqueue(MonsterBase monsterBase)
    {
        monsterPool.Enqueue(monsterBase);
    }
    private void MonsterDequeue()
    {
        MonsterBase monsterBase = monsterPool.Dequeue();
        monsterBase.Init();
    }

    private void MonsterPoolCreate()
    {
        for (int i = 0; i < monsterPoolSize; i++)
        {
            MonstartEnqueue(MonsterInstantiate());
        }
    }
    private MonsterBase MonsterInstantiate()
    {
        GameObject go = Instantiate(AddressableManager.Instance.GetFBX(GameDataManager.Instance.currentDugeonData.monster.ToString()), GetRandomPosition());
        MonsterBase monsterBase = go.GetComponent<MonsterBase>();
        monsterBase.SetDungeonController(this);
        monsterBase.SetPlayer(player);
        go.SetActive(false);

        return monsterBase;
    }
    private Transform GetRandomPosition()
    {
        return createPos[Random.Range(0, createPos.Length)];
    }

}
