using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UISystem;
using TMPro;
using System;

public class GameView : UIView
{
    [SerializeField] Joystick joystick;
    [SerializeField] TextMeshProUGUI missionText1;
    [SerializeField] TextMeshProUGUI missionText2;
    [SerializeField] TextMeshProUGUI missionText3;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] Button exitButton;
    [SerializeField] Button attackButton;
    [SerializeField] Slider hpBar;
    [SerializeField] Slider mpBar;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI mpText;
    private float maxHp, maxMp;
    private float nowHp, nowMp;

    private float time = 0f;
    private int timeInt = 0;
    private int count = 3;
    private int mission1, mission2, mission3;

    private PlayerInfo playerInfo;
    private DungeonScore dungeonScore;
    public DungeonState dungeonState { get; private set; }
    private int kill;


    public void Show()
    {
        ShowLayer();
    }
    protected override void OnFirstShow()
    {
        playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();

        exitButton.onClick.AddListener(OnClick_ExitBtn);
        attackButton.onClick.AddListener(OnClick_AttackBtn);
    }
    protected override void OnShow()
    {
        Init();
        playerInfo._playerMoveControl.SetJoystick(joystick);
    }
    private void Init()
    {
        SetDungeonState(DungeonState.Stop);
        kill = 0;
        count = 3;
        countText.gameObject.SetActive(false);
        timeInt = (int)GameDataManager.Instance.currentDugeonData.time;
        TimeSet(timeInt);
        mission1 = GameDataManager.Instance.currentDugeonData.mission1;
        mission2 = GameDataManager.Instance.currentDugeonData.mission2;
        mission3 = GameDataManager.Instance.currentDugeonData.mission3;

        PlayerType type = LocalSave.GetLocalPlayerInfo().playerType;
        hpBar.maxValue = maxHp = nowHp = GameDataManager.Instance.player_Data[(int)type].hp;
        mpBar.maxValue = maxMp = nowMp = GameDataManager.Instance.player_Data[(int)type].mp;
        hpText.text = $"{nowHp}/{maxHp}";
        mpText.text = $"{nowMp}/{maxMp}";

        MissionSet();
        GameStart();
    }

    private void Update()
    {
        switch (dungeonState)
        {
            case DungeonState.Start:
                if (timeInt <= 0f)
                {
                    TimeSet(0);
                    SetDungeonState(DungeonState.Victory);
                }
                else
                {
                    time += Time.deltaTime;
                    if (time >= 1)
                    {
                        TimeSet(timeInt--);
                        time = 0;
                    }
                }
                break;
            case DungeonState.Stop:
                break;
            case DungeonState.Fail:
                break;
            case DungeonState.Victory:
                break;
        }
    }

    #region Function
    public void SetDungeonState(DungeonState state)
    {
        switch (state)
        {
            case DungeonState.Start:
                break;
            case DungeonState.Stop:
                break;
            case DungeonState.Fail:
                DungeonFail();
                break;
            case DungeonState.Victory:
                DungeonClear();
                break;
        }
        dungeonState = state;
    }
    private void GameStart()
    {
        StartCoroutine(GameStartCoroutine());
    }
    private IEnumerator GameStartCoroutine()
    {
        countText.gameObject.SetActive(true);
        countText.text = count.ToString();
        // SoundManager.Instance.PlaySFXSound("countDown");
        for (int i = count; i > 0; i--)
        {
            count--;
            countText.text = count.ToString();
            yield return new WaitForSeconds(1f);
        }
        countText.gameObject.SetActive(false);
        SetDungeonState(DungeonState.Start);
    }
    private void GameEnd()
    {
        StartCoroutine(GameEndCoroutine());
    }
    private IEnumerator GameEndCoroutine()
    {
        yield return new WaitForSeconds(1f);

        // 결과창
        string key;
        int resultTime = (int)GameDataManager.Instance.currentDugeonData.time - timeInt;

        if (dungeonState == DungeonState.Fail)
            key = LocalizationManager.Instance.GetLocalizeText("DungeonResultPopup_Fail");
        else
            key = LocalizationManager.Instance.GetLocalizeText("DungeonResultPopup_Victory");

        PopupState popupState = Les_UIManager.Instance.Popup<DungeonResultPopup>().Open(key, kill, resultTime, dungeonScore);
        popupState.OnClose = p => LoadingManager.Instance.SceneLoad(Constants.Scene.Main);
    }

    public void KillSet()
    {
        kill++;
        MissionSet();
    }
    private void MissionSet()
    {
        if (kill < mission1)
            missionText1.text = $"{kill} / {mission1}";
        else
        {
            missionText1.text = $"{mission1} / {mission1}";
            missionText1.fontStyle = FontStyles.Strikethrough;
        }
        if (kill < mission2)
            missionText2.text = $"{kill} / {mission2}";
        else
        {
            missionText2.text = $"{mission2} / {mission2}";
            missionText2.fontStyle = FontStyles.Strikethrough;
        }
        if (kill < mission3)
            missionText3.text = $"{kill} / {mission3}";
        else
        {
            missionText3.text = $"{mission3} / {mission3}";
            missionText3.fontStyle = FontStyles.Strikethrough;
        }
    }
    public void TimeSet(int _time)
    {
        timeText.text = TimeSpan.FromSeconds(_time).ToString(@"mm\:ss");
    }
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
        playerInfo._playerBattleControl.Victory();
        // 현재 던전 정보 갱신
        LocalDungeonInfo Info = LocalSave.GetLocalDungeonInfo(GameDataManager.Instance.currentDugeonData.name);
        Info.clear = true;

        dungeonScore = DungeonScore.none;
        if (kill >= mission1)
            dungeonScore = DungeonScore.bad;
        if (kill >= mission2)
            dungeonScore = DungeonScore.good;
        if (kill >= mission3)
            dungeonScore = DungeonScore.perfect;

        if (dungeonScore > Info.dungeonScore)
            Info.dungeonScore = dungeonScore;

        LocalSave.SetLocalDungeonInfo(Info);

        // 다음 던전 오픈
        int nextIndex = GameDataManager.Instance.currentDugeonData.index + 1;
        LocalDungeonInfo nextInfo = new LocalDungeonInfo(GameDataManager.Instance.dungeon_Data[nextIndex].name);
        nextInfo.open = true;
        LocalSave.SetLocalDungeonInfo(nextInfo);

        GameEnd();
    }
    public void DungeonFail()
    {
        GameEnd();
    }
    #endregion

    #region Event
    private void OnClick_ExitBtn()
    {
        SetDungeonState(DungeonState.Stop);

        PopupState popupState = Les_UIManager.Instance.Popup<BasePopup_TwoBtn>().Open("Common_DungeonExit");
        popupState.OnYes = p => LoadingManager.Instance.SceneLoad(Constants.Scene.Main);
        popupState.OnNo = p => SetDungeonState(DungeonState.Start);
    }

    private void OnClick_AttackBtn()
    {
        playerInfo._playerBattleControl.Attack();
    }
    #endregion
}
