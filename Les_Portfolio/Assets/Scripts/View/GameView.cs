using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UISystem;
using TMPro;

public class GameView : UIView
{
    [SerializeField] Joystick joystick;
    [SerializeField] Button settingButton;
    [SerializeField] Button attackButton;
    [SerializeField] Slider hpBar;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] Slider mpBar;
    [SerializeField] TextMeshProUGUI mpText;
    private float maxHp, maxMp;
    private float nowHp, nowMp;

    private PlayerInfo playerInfo;
    private MonsterPool monsterPool;

    public void Show()
    {
        ShowLayer();
    }
    protected override void OnFirstShow()
    {
        playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
        monsterPool = GameObject.FindObjectOfType<MonsterPool>();
        settingButton.onClick.AddListener(OnClick_SettingBtn);
        attackButton.onClick.AddListener(OnClick_AttackBtn);
    }
    protected override void OnShow()
    {
        playerInfo._playerMoveControl.SetJoystick(joystick);
        Init();
        monsterPool.StartMonsterWave();
    }

    private void Init()
    {
        PlayerType type = LocalSave.GetLocalPlayerInfo().playerType;
        hpBar.maxValue = maxHp = nowHp = GameDataManager.Instance.player_Data[(int)type].hp;
        mpBar.maxValue = maxMp = nowMp = GameDataManager.Instance.player_Data[(int)type].mp;
        hpText.text = $"{nowHp}/{maxHp}";
        mpText.text = $"{nowMp}/{maxMp}";

    }

    #region Event
    public void SetHP(float value)
    {
        hpBar.value = nowHp = value;
        hpText.text = $"{nowHp}/{maxHp}";
    }
    public void SetMP(float value)
    {
        mpBar.value = nowMp = value;
        mpText.text = $"{nowMp}/{maxMp}";
    }

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
