using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    private Vector3 movePostion;
    private Vector3 lookPosition;
    private float moveSpeed = 0f;
    private Camera mainCam;

    private Joystick joystick;
    private CharacterController characterController;
    private PlayerInfo playerInfo;

    private void Awake()
    {
        playerInfo = this.GetComponent<PlayerInfo>();
        mainCam = playerInfo._mainCamera;
        characterController = this.GetComponent<CharacterController>();
    }

    private void Init()
    {
        playerInfo._playerAniControl.AnimationChanger(PlayerAniState.Default);
        playerInfo._playerAniControl.SetMoveValue(0f);
    }

    public void SetJoystick(Joystick joystick)
    {
        this.joystick = joystick;
    }

    private void FixedUpdate()
    {
        if (joystick == null)
            return;

        switch (playerInfo._playerAniControl.playerAniState)
        {
            case PlayerAniState.Default:
                movePostion.x = Mathf.Abs(joystick.Horizontal) >= 0.05 ? joystick.Horizontal : 0;
                movePostion.z = Mathf.Abs(joystick.Vertical) >= 0.05 ? joystick.Vertical : 0;

#if UNITY_EDITOR_WIN
                movePostion.x += Input.GetAxis("Horizontal");
                movePostion.z += Input.GetAxis("Vertical");
#endif
                moveSpeed = Mathf.Clamp01(Mathf.Abs(movePostion.x) + Mathf.Abs(movePostion.z));

                if (moveSpeed > 0)
                {
                    lookPosition = Quaternion.LookRotation(movePostion).eulerAngles;
                    this.transform.rotation = Quaternion.Euler(Vector3.up * (lookPosition.y + mainCam.transform.eulerAngles.y)).normalized;
                }

                characterController.Move(this.transform.forward * speed * moveSpeed * Time.fixedDeltaTime);

                playerInfo._playerAniControl.SetMoveValue(moveSpeed);
                break;
            default:
                break;
        }
    }
}
