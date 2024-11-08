using System.Collections;
using System.Collections.Generic;
using UISystem;
using UnityEngine;
using UnityEngine.UI;

public class CharacterView : UIView
{
    [SerializeField] Button characterSelectMale;
    [SerializeField] Button characterSelectFemale;
    [SerializeField] Button selectButton;
    [SerializeField] GameObject[] characterSelectFrames;
    [SerializeField] Animator[] animators;

    private Animator selectAnimator;

    private LocalCharacterInfo localCharacterInfo;

    public void Show()
    {
        ShowLayer();
    }
    protected override void OnFirstShow()
    {
        characterSelectMale.onClick.AddListener(() => OnClick_CharacterSelect(CharacterType.Male));
        characterSelectFemale.onClick.AddListener(() => OnClick_CharacterSelect(CharacterType.Female));
        selectButton.onClick.AddListener(OnClick_Select);
    }
    protected override void OnShow()
    {
        Init();
        localCharacterInfo = LocalSave.GetLocalCharacterInfo();
    }

    private void Init()
    {
        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].SetFloat("Select", (int)CharacterSceneState.Unclick);
        }

    }

    #region Event
    private void OnClick_CharacterSelect(CharacterType type)
    {
        bool isActive = false;
        float animValue = 0;
        for (int i = 0; i < characterSelectFrames.Length; i++)
        {
            isActive = (int)type == i ? true : false;
            characterSelectFrames[i].SetActive(isActive);

            animValue = isActive == true ? (int)CharacterSceneState.Click : (int)CharacterSceneState.Unclick;
            animators[i].Rebind();
            animators[i].SetFloat("Select", animValue);

            if (isActive) selectAnimator = animators[i];
        }

        localCharacterInfo.characterType = type;
    }
    private void OnClick_Select()
    {
        selectAnimator.Rebind();
        selectAnimator.SetFloat("Select", (int)CharacterSceneState.Select);
        LocalSave.SetLocalCharacterInfo(localCharacterInfo);
        LoadingManager.Instance.SceneLoad(Constants.Scene.Main);
    }
    #endregion
}
