using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] PlayerMoveControl playerMoveControl;
    public PlayerMoveControl _playerMoveControl { get { return playerMoveControl; } }

    [SerializeField] PlayerAniControl playerAniControl;
    public PlayerAniControl _playerAniControl { get { return playerAniControl; } }


    [SerializeField] GameObject[] playerPrefabs;
    private GameObject player;
    public GameObject _player { get { return player; } }
    public void SetPlayer(CharacterType type)
    {
        player = Instantiate(playerPrefabs[(int)type], Vector3.zero, Quaternion.identity, this.transform);
    }
}
