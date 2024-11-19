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
    [SerializeField] Transform malePos;
    [SerializeField] Transform femalePos;
    private Animator[] animators;

    private Animator selectAnimator;

    private LocalPlayerInfo localPlayerInfo;

    public void Show()
    {
        ShowLayer();
    }
    protected override void OnFirstShow()
    {
        characterSelectMale.onClick.AddListener(() => OnClick_CharacterSelect(PlayerType.Male));
        characterSelectFemale.onClick.AddListener(() => OnClick_CharacterSelect(PlayerType.Female));
        selectButton.onClick.AddListener(OnClick_Select);
    }
    protected override void OnShow()
    {
        Init();
        localPlayerInfo = LocalSave.GetLocalPlayerInfo();
    }

    private void Init()
    {
        animators = new Animator[2];
        GameObject male = Instantiate(AddressableManager.Instance.GetFBX("MaleCharacter"), malePos);
        animators[0] = male.GetComponent<Animator>();
        GameObject female = Instantiate(AddressableManager.Instance.GetFBX("FemaleCharacter"), femalePos);
        animators[1] = female.GetComponent<Animator>();

        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].SetFloat("Select", (int)CharacterSceneState.Unclick);
        }
        Les_UIManager.Instance.Popup<DescriptPopup>().Open(DescriptType.Character);
    }

    #region Event
    private void OnClick_CharacterSelect(PlayerType type)
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

        localPlayerInfo.playerType = type;
    }
    private void OnClick_Select()
    {
        selectAnimator.Rebind();
        selectAnimator.SetFloat("Select", (int)CharacterSceneState.Select);
        LocalSave.SetLocalPlayerInfo(localPlayerInfo);
        LoadingManager.Instance.SceneLoad(Constants.Scene.Main);
    }
    #endregion
}
