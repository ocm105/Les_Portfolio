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

    private PlayerInfo playerInfo;

    public void Show()
    {
        ShowLayer();
    }
    protected override void OnFirstShow()
    {
        playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
        playerInfo.SetPlayer(LocalSave.GetLocalCharacterInfo().characterType);
    }
    protected override void OnShow()
    {
        playerInfo._playerMoveControl.SetJoystick(joystick);
        settingButton.onClick.AddListener(OnClick_SettingBtn);
        attackButton.onClick.AddListener(OnClick_AttackBtn);
    }

    #region Event
    private void OnClick_SettingBtn()
    {
        PopupState popupState = Les_UIManager.Instance.Popup<SettingPopup>().Open();
        popupState.OnClose = p => Debug.Log("닫음");
    }
    private void OnClick_AttackBtn()
    {
        playerInfo._playerBattleControl.Attack();
    }
    #endregion
}