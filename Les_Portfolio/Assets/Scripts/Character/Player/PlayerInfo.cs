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

    [SerializeField] Transform profilePlayerPos;
    private GameObject player;
    public GameObject _player { get { return player; } }

    public CinemachineControl cinemachineControl { get; private set; }

    private void Awake()
    {
        cinemachineControl = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<CinemachineControl>();
        SetPlayer(LocalSave.GetLocalPlayerInfo().playerType);
    }

    private void SetPlayer(PlayerType type)
    {
        Instantiate(AddressableManager.Instance.GetFBX(type.ToString()), profilePlayerPos);
        player = Instantiate(AddressableManager.Instance.GetFBX(type.ToString()), Vector3.zero, Quaternion.identity, this.transform);
    }
}
