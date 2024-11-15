using System.Collections;
using System.Collections.Generic;
using UISystem;
using UnityEngine;

public class GameDataManager : SingletonMonoBehaviour<GameDataManager>
{
    public Dictionary<int, DescriptData> discription_Data = new Dictionary<int, DescriptData>();
    public Dictionary<int, MonsterData> monster_Data = new Dictionary<int, MonsterData>();

    private bool isNetworkData_Completed = false;

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
            yield return StartCoroutine(NetworkManager.Instance.GetMonsterDataRequest((resData) => monster_Data = resData));
        }

        isNetworkData_Completed = true;
    }
}
