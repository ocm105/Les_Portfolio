using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UISystem;

public class MainView : UIView
{
    [SerializeField] Joystick joystick;

    private PlayerInfo playerInfo;

    public void Show()
    {
        ShowLayer();
    }
    protected override void OnFirstShow()
    {
        playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
        playerInfo.SetPlayer(CharacterType.Male);
    }
    protected override void OnShow()
    {
        playerInfo._playerMoveControl.SetJoystick(joystick);
    }
}
