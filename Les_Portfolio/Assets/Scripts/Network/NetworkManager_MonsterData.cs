using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UISystem;
using System;

[Serializable]
public class MonsterData
{
    public int index;
    public MonsterType name;// 이름
    public float hp;        // 체력
    public float atk;       // 공격력
    public float atkspeed;  // 공격속도
    public float def;       // 방어력
    public float speed;     // 스피드
}

public partial class NetworkManager : SingletonMonoBehaviour<NetworkManager>
{
    public const string MONSTER_DATA_PATH = "https://docs.google.com/spreadsheets/d/1QaGvz4XmbMwFnUNE6BBvY7X6pPfzUdiaPcD45Em6UlU/export?format=csv";

    public IEnumerator GetMonsterDataRequest(Action<Dictionary<int, MonsterData>> callback = null)
    {
        yield return StartCoroutine(Request_Get(MONSTER_DATA_PATH, (dataState, resData) =>
        {
            switch (dataState)
            {
                case GAMEDATA_STATE.CONNECTDATAERROR:
                    break;
                case GAMEDATA_STATE.PROTOCOLERROR:
                    break;
                case GAMEDATA_STATE.REQUESTSUCCESS:
                    callback?.Invoke(CSVReader.ReadFromResource<MonsterData>(resData));
                    break;
            }
        }));
    }
}
