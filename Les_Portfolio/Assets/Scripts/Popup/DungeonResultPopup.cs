using TMPro;
using UnityEngine;
using UISystem;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class DungeonResultPopup : UIPopup
{
    [SerializeField] GameObject frame;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] GameObject[] stars;
    [SerializeField] TextMeshProUGUI dungeonKills;
    [SerializeField] TextMeshProUGUI dungeonTime;
    [SerializeField] Button exitButton;

    private string title;
    private int kill;
    private int time;
    private DungeonScore score;

    public PopupState Open(string _title, int _kill, int _time, DungeonScore _score)
    {
        title = _title;
        kill = _kill;
        time = _time;
        score = _score;
        ShowLayer();
        return state;
    }

    protected override void OnFirstShow()
    {
        exitButton.onClick.AddListener(OnClick_Exit);
    }
    protected override void OnShow()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }
        SetInfo();
        ShowTween();
    }
    private void SetInfo()
    {
        titleText.text = title;
        dungeonKills.text = kill.ToString();
        dungeonTime.text = TimeSpan.FromSeconds(time).ToString(@"mm\:ss");

        for (int i = 0; i < (int)score; i++)
        {
            stars[i].SetActive(true);
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
