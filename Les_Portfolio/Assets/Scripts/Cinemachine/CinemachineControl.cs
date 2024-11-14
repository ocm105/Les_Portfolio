using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachineControl : MonoBehaviour
{
    [Tooltip("FPSView, QuarterView, ShoulderView")]
    [SerializeField] GameObject[] views;

    public PlayerViewType playerViewType { get; private set; }

    private void Start()
    {
        SetActiveCinemachine(LocalSave.GetSettingInfo().playerViewType);
    }

    private void SetActiveCinemachine(PlayerViewType type)
    {
        bool isActive = false;

        for (int i = 0; i < views.Length; i++)
        {
            isActive = (int)type == i ? true : false;
            views[i].SetActive(isActive);
        }

        playerViewType = type;
    }

    public void OnChange_Cinemachine(PlayerViewType type)
    {
        LoadingManager.Instance.SetFadeOut(() =>
        {
            SetActiveCinemachine(type);
            LoadingManager.Instance.SetFadeIn();
        });
    }
}
