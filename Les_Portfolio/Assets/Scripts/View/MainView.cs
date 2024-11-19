using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UISystem;

public class MainView : UIView
{
    [SerializeField] Joystick joystick;
    [SerializeField] Button settingButton;
    [SerializeField] Button attackButton;
    [SerializeField] Button gameStartButton;

    private PlayerInfo playerInfo;
    private CinemachineControl cinemachineControl;

    public void Show()
    {
        ShowLayer();
    }
    protected override void OnFirstShow()
    {
        playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
        cinemachineControl = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<CinemachineControl>();
        settingButton.onClick.AddListener(OnClick_SettingBtn);
        attackButton.onClick.AddListener(OnClick_AttackBtn);
        gameStartButton.onClick.AddListener(OnClick_GameStartBtn);
    }
    protected override void OnShow()
    {
        playerInfo._playerMoveControl.SetJoystick(joystick);
        Les_UIManager.Instance.Popup<DescriptPopup>().Open(DescriptType.Main);
    }

    #region Event
    private void OnClick_SettingBtn()
    {
        PopupState popupState = Les_UIManager.Instance.Popup<SettingPopup>().Open();
        popupState.OnClose = p =>
        {
            LocalSettingInfo settingInfo = (LocalSettingInfo)popupState.ResultParam;
            LocalSettingInfo localSettingInfo = LocalSave.GetSettingInfo();

            localSettingInfo.bgmVolume = settingInfo.bgmVolume;
            localSettingInfo.sfxVolume = settingInfo.sfxVolume;
            localSettingInfo.isBgm = settingInfo.isBgm;
            localSettingInfo.isSfx = settingInfo.isSfx;


            if (localSettingInfo.playerViewType != settingInfo.playerViewType)
            {
                cinemachineControl.OnChange_Cinemachine(settingInfo.playerViewType);
                localSettingInfo.playerViewType = settingInfo.playerViewType;
            }

            if (localSettingInfo.languageType != settingInfo.languageType)
            {
                LocalizationManager.Instance.ChangeLanguage((int)settingInfo.languageType);
                localSettingInfo.languageType = settingInfo.languageType;
            }
            LocalSave.SetSettingInfo(localSettingInfo);
        };
    }

    private void OnClick_GameStartBtn()
    {
        LoadingManager.Instance.SceneLoad(Constants.Scene.Game);
    }

    private void OnClick_AttackBtn()
    {
        playerInfo._playerBattleControl.Attack();
    }
    #endregion
}