using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UISystem;
using TMPro;
using DG.Tweening;

public class TitleView : UIView
{
    [SerializeField] GameObject title;
    [SerializeField] Button startButton;
    [SerializeField] TextMeshProUGUI loadText;
    [SerializeField] TextMeshProUGUI startText;

    public void Show()
    {
        ShowLayer();
    }
    protected override void OnFirstShow()
    {
        startButton.onClick.AddListener(OnClick_StartBtn);
    }
    protected override void OnShow()
    {
        Init();
    }

    private void Init()
    {
        CheckFPS.Instance.EnableFPS(30, Color.red);
        title.SetActive(true);
        startText.gameObject.SetActive(true);
        startButton.gameObject.SetActive(false);
        LocalizationManager.Instance.ChangeLanguage((int)LocalSave.GetSettingInfo().languageType);

        StartCoroutine(DataLoad());
    }
    private IEnumerator DataLoad()
    {
        // Localization Data Load
        yield return StartCoroutine(LocalizationManager.Instance.LoadData());
        WindowDebug.SuccessLog("LocalizationManager Completed");
        // startText.text = LocalizationManager.Instance.GetLocalizeText("Title_language");

        // Resource Down
        float percent;
        string str = LocalizationManager.Instance.GetLocalizeText("Title_resource");

        AddressableManager.Instance.StartDownload_Addressable("All");
        while (!AddressableManager.Instance.isComplete)
        {
            // Debug.Log("DownPercentValue : " + AddressableManager.Instance.DownPercentValue);
            percent = AddressableManager.Instance.downPercent * 100;
            startText.text = $"{str}{Mathf.RoundToInt(percent)}%";
            yield return null;
        }
        yield return StartCoroutine(AddressableManager.Instance.LoadData());

        // Game Data Load
        startText.text = LocalizationManager.Instance.GetLocalizeText("Title_data");
        yield return StartCoroutine(GameDataManager.Instance.LoadData());
        WindowDebug.SuccessLog("GameDataManager Completed");

        yield return new WaitForSeconds(2f);
        startButton.gameObject.SetActive(true);
        startText.text = LocalizationManager.Instance.GetLocalizeText("Title_startButton");
        Tween_Fadein(startText);
    }

    #region Event
    private void OnClick_StartBtn()
    {
        startButton.gameObject.SetActive(false);
        startText.gameObject.SetActive(false);

        PopupState popupState = Les_UIManager.Instance.Popup<DescriptPopup>().Open(DescriptType.Title);
        popupState.OnClose = p => PortfolioStart();
    }

    private void Tween_Fadein(TextMeshProUGUI text)
    {
        if (!text.gameObject.activeInHierarchy) return;

        startText.DOFade(0, 0.5f).onComplete = () => Tween_Fadeout(startText);

    }
    private void Tween_Fadeout(TextMeshProUGUI text)
    {
        if (!text.gameObject.activeInHierarchy) return;

        startText.DOFade(1, 0.5f).onComplete = () => Tween_Fadein(startText);
    }

    private void PortfolioStart()
    {
        LocalPlayerInfo localplayerInfo = LocalSave.GetLocalPlayerInfo();

        if (localplayerInfo.playerType == PlayerType.none)
            LoadingManager.Instance.SceneLoad(Constants.Scene.Character);
        else
            LoadingManager.Instance.SceneLoad(Constants.Scene.Main);

    }
    #endregion

}