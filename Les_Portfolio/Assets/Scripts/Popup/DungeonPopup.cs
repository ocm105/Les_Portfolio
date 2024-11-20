using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UISystem;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class DungeonPopup : UIPopup
{
    [SerializeField] GameObject frame;
    [SerializeField] Button exitButton;
    [SerializeField] DungeonButton[] dungeonButtons;

    public PopupState Open()
    {
        ShowLayer();
        return state;
    }

    protected override void OnFirstShow()
    {
        exitButton.onClick.AddListener(OnClick_Exit);
    }
    protected override void OnShow()
    {
        Init();
        ShowTween();
    }

    private void Init()
    {
        for (int i = 0; i < dungeonButtons.Length; i++)
        {
            dungeonButtons[i].SetDungeonData(GameDataManager.Instance.dungeon_Data[i + 1]);
        }
    }

    #region Event
    private void OnClick_Exit()
    {
        CloseTween(() => OnResult(PopupResults.Close));
    }
    #endregion

    #region Tween
    private void ShowTween()
    {
        frame.transform.localScale = Vector3.zero;
        frame.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutCubic);
    }
    private void CloseTween(Action call)
    {
        frame.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutCubic).OnComplete(call.Invoke);
    }
    #endregion
}
