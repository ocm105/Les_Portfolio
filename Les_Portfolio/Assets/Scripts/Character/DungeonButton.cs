using System.Collections;
using System.Collections.Generic;
using UISystem;
using UnityEngine;
using UnityEngine.UI;

public class DungeonButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Image icon;
    [SerializeField] GameObject[] stars;

    private DungeonData dungeonData;

    private void Awake()
    {
        button.onClick.AddListener(OnClick_Button);
    }
    public void SetDungeonData(DungeonData data)
    {
        dungeonData = data;
        SetInfo(LocalSave.GetLocalDungeonInfo(data.name));
    }
    private void SetInfo(LocalDungeonInfo info)
    {
        if (info.open && dungeonData.monster != MonsterType.none)
        {
            button.interactable = true;
            button.image.color = Color.white;
            icon.color = Color.white;
        }
        else
        {
            button.interactable = false;
            button.image.color = Color.gray;
            icon.color = Color.gray;
        }

        if (info.clear)
        {
            for (int i = 0; i < (int)info.dungeonScore; i++)
            {
                stars[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].SetActive(false);
            }
        }
    }

    public void OnClick_Button()
    {
        if (dungeonData.monster != MonsterType.none)
            Les_UIManager.Instance.Popup<DungeonInfoPopup>().Open(dungeonData);
        else
            Les_UIManager.Instance.Popup<BasePopup_Toast>().Open("Common_Develop");
    }
}
