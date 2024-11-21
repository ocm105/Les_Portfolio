using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UISystem;
using System;

[Serializable]
public class DungeonData
{
    public int index;
    public string name;     // 이름
    public MonsterType monster;        // 몬스터 타입
    public float time;      // 시간
    public int mission1;
    public int mission2;
    public int mission3;
}

public partial class NetworkManager : SingletonMonoBehaviour<NetworkManager>
{
    public const string Dungeon_DATA_PATH = "https://docs.google.com/spreadsheets/d/1f3pklhv_iEpukXS6HCqoPPomcdSBPq9hVEsxbhUijU4/export?format=csv";

    public IEnumerator GetDungeonDataRequest(Action<Dictionary<int, DungeonData>> callback = null)
    {
        yield return StartCoroutine(Request_Get(Dungeon_DATA_PATH, (dataState, resData) =>
        {
            switch (dataState)
            {
                case GAMEDATA_STATE.CONNECTDATAERROR:
                    break;
                case GAMEDATA_STATE.PROTOCOLERROR:
                    break;
                case GAMEDATA_STATE.REQUESTSUCCESS:
                    callback?.Invoke(CSVReader.ReadFromResource<DungeonData>(resData));
                    break;
            }
        }));
    }
}
