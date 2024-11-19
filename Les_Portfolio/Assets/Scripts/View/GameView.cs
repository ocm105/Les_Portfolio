using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UISystem;

public class GameView : UIView
{
    [SerializeField] Joystick joystick;
    [SerializeField] Button settingButton;
    [SerializeField] Button attackButton;

    private PlayerInfo playerInfo;
    private MonsterPool monsterPool;

    public void Show()
    {
        ShowLayer();
    }
    protected override void OnFirstShow()
    {
        playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
        monsterPool = GameObject.FindGameObjectWithTag("Monster").GetComponent<MonsterPool>();
        settingButton.onClick.AddListener(OnClick_SettingBtn);
        attackButton.onClick.AddListener(OnClick_AttackBtn);
    }
    protected override void OnShow()
    {
        playerInfo._playerMoveControl.SetJoystick(joystick);
        monsterPool.StartMonsterWave();
    }

    #region Event
    private void OnClick_SettingBtn()
    {
        // PopupState popupState = Les_UIManager.Instance.Popup<SettingPopup>().Open();
        // popupState.OnClose = p =>
        // {
        //     LocalSettingInfo settingInfo = (LocalSettingInfo)popupState.ResultParam;
        //     LocalSettingInfo localSettingInfo = LocalSave.GetSettingInfo();

        //     localSettingInfo.bgmVolume = settingInfo.bgmVolume;
        //     localSettingInfo.sfxVolume = settingInfo.sfxVolume;
        //     localSettingInfo.isBgm = settingInfo.isBgm;
        //     localSettingInfo.isSfx = settingInfo.isSfx;


        //     if (localSettingInfo.playerViewType != settingInfo.playerViewType)
        //     {
        //         localSettingInfo.playerViewType = settingInfo.playerViewType;
        //     }

        //     if (localSettingInfo.languageType != settingInfo.languageType)
        //     {
        //         LocalizationManager.Instance.ChangeLanguage((int)settingInfo.languageType);
        //         localSettingInfo.languageType = settingInfo.languageType;
        //     }
        //     LocalSave.SetSettingInfo(localSettingInfo);
        // };
    }

    private void OnClick_AttackBtn()
    {
        playerInfo._playerBattleControl.Attack();
    }
    #endregion
}
