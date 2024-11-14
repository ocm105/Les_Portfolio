using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] GameObject fpsViewTarget;

    private float winX, winZ;
    private float mobX, mobZ;
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

                mobX = Mathf.Abs(joystick.Horizontal) >= 0.05 ? joystick.Horizontal : 0;
                mobZ = Mathf.Abs(joystick.Vertical) >= 0.05 ? joystick.Vertical : 0;

                winX = Input.GetAxis("Horizontal");
                winZ = Input.GetAxis("Vertical");


                switch (playerInfo.cinemachineControl.playerViewType)
                {
                    case PlayerViewType.FPSView:
                        movePostion = this.transform.right * mobX + this.transform.forward * mobZ;
#if UNITY_EDITOR_WIN
                        movePostion.x += winX;
                        movePostion.z += winZ;
#endif
                        moveSpeed = Mathf.Clamp01(Mathf.Abs(movePostion.x) + Mathf.Abs(movePostion.z));

                        this.transform.rotation = Quaternion.Euler(0, fpsViewTarget.transform.eulerAngles.y, 0);
                        characterController.Move(movePostion * speed * Time.fixedDeltaTime);
                        break;

                    case PlayerViewType.QuarterView:
                    case PlayerViewType.ShoulderView:
                        movePostion.x = mobX;
                        movePostion.z = mobZ;
#if UNITY_EDITOR_WIN
                        movePostion.x += winX;
                        movePostion.z += winZ;
#endif
                        moveSpeed = Mathf.Clamp01(Mathf.Abs(movePostion.x) + Mathf.Abs(movePostion.z));

                        if (moveSpeed > 0)
                        {
                            lookPosition = Quaternion.LookRotation(movePostion).eulerAngles;
                            this.transform.rotation = Quaternion.Euler(Vector3.up * (lookPosition.y + mainCam.transform.eulerAngles.y)).normalized;
                        }
                        characterController.Move(this.transform.forward * speed * moveSpeed * Time.fixedDeltaTime);
                        break;
                }

                playerInfo._playerAniControl.SetMoveValue(moveSpeed);
                break;
            default:
                break;
        }
    }
}
