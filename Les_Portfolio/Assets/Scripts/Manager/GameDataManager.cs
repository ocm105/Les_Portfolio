using System.Collections;
using System.Collections.Generic;
using UISystem;
using UnityEngine;

public class GameDataManager : SingletonMonoBehaviour<GameDataManager>
{
    public Dictionary<int, DescriptData> discription_Data = new Dictionary<int, DescriptData>();
    public Dictionary<int, PlayerData> player_Data = new Dictionary<int, PlayerData>();
    public Dictionary<int, MonsterData> monster_Data = new Dictionary<int, MonsterData>();
    public Dictionary<int, DungeonData> dungeon_Data = new Dictionary<int, DungeonData>();
    private bool isNetworkData_Completed = false;

    public DungeonData currentDugeonData { get; set; }


    protected override void OnAwakeSingleton()
    {
        base.OnAwakeSingleton();
        DontDestroyOnLoad(this);
    }

    public IEnumerator LoadData()
    {
        if (isNetworkData_Completed == false)
        {
            yield return StartCoroutine(NetworkManager.Instance.GetDescriptRequest((resData) => discription_Data = resData));
            yield return StartCoroutine(NetworkManager.Instance.GetPlayerDataRequest((resData) => player_Data = resData));
            yield return StartCoroutine(NetworkManager.Instance.GetMonsterDataRequest((resData) => monster_Data = resData));
            yield return StartCoroutine(NetworkManager.Instance.GetDungeonDataRequest((resData) => dungeon_Data = resData));
            Init();
        }

        isNetworkData_Completed = true;
    }
    private void Init()
    {
        LocalDungeonInfo info = LocalSave.GetLocalDungeonInfo(dungeon_Data[(int)DungeonType.SlimeDungeon].name);
        info.open = true;
        LocalSave.SetLocalDungeonInfo(info);
    }
}
