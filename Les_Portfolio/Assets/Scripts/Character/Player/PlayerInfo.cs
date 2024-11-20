using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    public Camera _mainCamera { get { return mainCamera; } }
    [SerializeField] PlayerMoveControl playerMoveControl;
    public PlayerMoveControl _playerMoveControl { get { return playerMoveControl; } }

    [SerializeField] PlayerAniControl playerAniControl;
    public PlayerAniControl _playerAniControl { get { return playerAniControl; } }

    [SerializeField] PlayerBattleControl playerBattleControl;
    public PlayerBattleControl _playerBattleControl { get { return playerBattleControl; } }


    private GameObject player;
    public GameObject _player { get { return player; } }
    public PlayerData playerData { get; private set; }
    public CinemachineControl cinemachineControl { get; private set; }

    private void Awake()
    {
        cinemachineControl = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<CinemachineControl>();
        SetPlayer(LocalSave.GetLocalPlayerInfo().playerType);
    }

    private void SetPlayer(PlayerType type)
    {
        player = Instantiate(AddressableManager.Instance.GetFBX(type.ToString()), Vector3.zero, Quaternion.identity, this.transform);
        playerData = GameDataManager.Instance.player_Data[(int)type];

        playerMoveControl.SetPlayerSpeed(GameDataManager.Instance.player_Data[(int)type].speed);

        playerAniControl.SetAnimator(player.GetComponent<Animator>());
        playerAniControl.AnimationChanger(PlayerAniState.Default);
        playerAniControl.SetMoveValue(0f);

        playerBattleControl?.SetPlayerData(playerData);
    }
}
