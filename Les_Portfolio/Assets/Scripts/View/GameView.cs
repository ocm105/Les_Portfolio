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
        hpBar.value = nowHp = Mathf.Round(value * 10) * 0.1f;
        hpText.text = $"{nowHp}/{maxHp}";
    }
    public void SetMP(float value)
    {
        mpBar.value = nowMp = value;
        mpText.text = $"{nowMp}/{maxMp}";
    }

    public void DungeonClear()
    {
        LocalDungeonInfo Info = LocalSave.GetLocalDungeonInfo(GameDataManager.Instance.currentDugeonData.name);
        Info.clear = true;
        Info.dungeonScore = DungeonScore.good;
        LocalSave.SetLocalDungeonInfo(Info);

        int nextIndex = GameDataManager.Instance.currentDugeonData.index + 1;
        LocalDungeonInfo nextInfo = new LocalDungeonInfo(GameDataManager.Instance.dungeon_Data[nextIndex].name);
        nextInfo.open = true;
        LocalSave.SetLocalDungeonInfo(nextInfo);

        //메인씬 이동
    }
    public void DungeonFail()
    {
        // 실패 팝업
        // 메인씬 이동
    }

    private void OnClick_SettingBtn()
    {
        // PopupState popupState = Les_UIManager.Instance.Popup<SettingPopup>().Open();
    }

    private void OnClick_AttackBtn()
    {
        playerInfo._playerBattleControl.Attack();
    }
    #endregion
}
