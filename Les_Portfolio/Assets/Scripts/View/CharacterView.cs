using System.Collections;
using System.Collections.Generic;
using UISystem;
using UnityEngine;
using UnityEngine.UI;

public class CharacterView : UIView
{
    [SerializeField] Button characterSelectMale;
    [SerializeField] Button characterSelectFemale;
    [SerializeField] GameObject[] characterSelectFrames;

    public void Show()
    {
        ShowLayer();
    }
    protected override void OnFirstShow()
    {
        characterSelectMale.onClick.AddListener(() => OnClick_CharacterSelect(CharacterType.Male));
        characterSelectFemale.onClick.AddListener(() => OnClick_CharacterSelect(CharacterType.Female));
    }
    protected override void OnShow()
    {
        Init();
    }

    private void Init()
    {

    }

    #region Event
    private void OnClick_CharacterSelect(CharacterType type)
    {
        bool isActive = false;
        for (int i = 0; i < characterSelectFrames.Length; i++)
        {
            isActive = (int)type == i ? true : false;
            characterSelectFrames[i].SetActive(isActive);
        }
    }
    #endregion
}
