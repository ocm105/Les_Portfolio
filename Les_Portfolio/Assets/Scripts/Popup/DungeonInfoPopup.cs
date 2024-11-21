using TMPro;
using UnityEngine;
using UISystem;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class DungeonInfoPopup : UIPopup
{
    [SerializeField] GameObject frame;
    [SerializeField] TextMeshProUGUI dungeonName;
    [SerializeField] TextMeshProUGUI dungeonTime;
    [SerializeField] Button okButton;
    [SerializeField] Button exitButton;
    private DungeonData dungeonData;


    public PopupState Open(DungeonData data)
    {
        ShowLayer();
        dungeonData = data;
        return state;
    }

    protected override void OnFirstShow()
    {
        okButton.onClick.AddListener(OnClick_Ok);
        exitButton.onClick.AddListener(OnClick_Exit);
    }
    protected override void OnShow()
    {
        ShowTween();
    }
    private void Init()
    {
        dungeonName.text = dungeonData.name;
        dungeonTime.text = dungeonData.time.ToString();
    }

    #region Event
    public void OnClick_Ok()
    {
        GameDataManager.Instance.currentDugeonData = dungeonData;
        LoadingManager.Instance.SceneLoad(Constants.Scene.Game);
    }
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
