using System.Collections;
using System.Collections.Generic;
using UISystem;
using UnityEngine;

public class GameDataManager : SingletonMonoBehaviour<GameDataManager>
{
    public Dictionary<int, DescriptData> discription_Data = new Dictionary<int, DescriptData>();
    private bool isDiscription_Completed = false;

    protected override void OnAwakeSingleton()
    {
        base.OnAwakeSingleton();
        DontDestroyOnLoad(this);
    }

    public IEnumerator LoadData()
    {
        if (isDiscription_Completed == false)
            yield return StartCoroutine(NetworkManager.Instance.GetDescriptRequest((resData) => discription_Data = resData));

        isDiscription_Completed = true;
    }
}
