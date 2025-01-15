using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UISystem;
using TMPro;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class TitleView : UIView
{
    [SerializeField] GameObject[] mainObjects;
    [SerializeField] Image loadBar;
    [SerializeField] TextMeshProUGUI loadText;
    [SerializeField] Button gpgLoginButton;
    [SerializeField] Button googleAdsButton;

    private MainState mainState;

    public void Show()
    {
        ShowLayer();
    }
    protected override void OnFirstShow()
    {
        // startButton.onClick.AddListener(OnClick_StartBtn);
        // gpgLoginButton.onClick.AddListener(GPGLogin);
        googleAdsButton.onClick.AddListener(OnClick_GoogleAds);
    }
    protected override void OnShow()
    {
        Init();
    }

    private void Init()
    {
        CheckFPS.Instance.EnableFPS(30, Color.red);
        LocalizationManager.Instance.ChangeLanguage((int)LocalSave.GetSettingInfo().languageType);

        StartCoroutine(DataLoad());
    }
    #region Function
    // 필요한 데이터 다운
    private IEnumerator DataLoad()
    {
        OnChange_MainObject(MainState.Loading);
        // Localization Data Load
        yield return StartCoroutine(LocalizationManager.Instance.LoadData());
        WindowDebug.SuccessLog("LocalizationManager Completed");

        // Resource Down
        float percent;
        string str = LocalizationManager.Instance.GetLocalizeText("Title_resource");

        AddressableManager.Instance.StartDownload_Addressable("All");
        while (!AddressableManager.Instance.isComplete)
        {
            // Debug.Log("DownPercentValue : " + AddressableManager.Instance.DownPercentValue);
            percent = AddressableManager.Instance.downPercent * 100;
            loadText.text = $"{str}{Mathf.RoundToInt(percent)}%";
            yield return null;
        }
        yield return StartCoroutine(AddressableManager.Instance.LoadData());

        // Game Data Load
        yield return StartCoroutine(GameDataManager.Instance.LoadData());
        WindowDebug.SuccessLog("GameDataManager Completed");

        OnChange_MainObject(MainState.Start);
    }
    #endregion

    #region Event
    // 메인 화면 스테이트에 따라 오브젝트 변경
    private void OnChange_MainObject(MainState state)
    {
        mainState = state;
        bool isActive = false;

        for (int i = 0; i < mainObjects.Length; i++)
        {
            isActive = i == (int)state ? true : false;
            mainObjects[i].SetActive(isActive);
        }
    }
    public void GPGLogin()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }
    internal void ProcessAuthentication(SignInStatus status)
    {
        switch (status)
        {
            case SignInStatus.Success:
                string name = PlayGamesPlatform.Instance.GetUserDisplayName();
                string userID = PlayGamesPlatform.Instance.GetUserId();

                Debug.Log($"<color=green>로그인 성공 Name : {name} UserID :{userID}</color>");
                break;
            case SignInStatus.InternalError:
                Debug.Log($"<color=red>로그인 실패 InternalError</color>");
                break;
            case SignInStatus.Canceled:
                Debug.Log($"<color=red>로그인 실패 Canceled</color>");
                break;
        }
    }
    private void OnClick_GoogleAds()
    {
        LoadingManager.Instance.SceneLoad(Constants.Scene.GoogleAds);
    }

    // private void OnClick_StartBtn()
    // {
    //     startButton.gameObject.SetActive(false);
    //     startText.gameObject.SetActive(false);

    //     PopupState popupState = Les_UIManager.Instance.Popup<DescriptPopup>().Open(DescriptType.Title);
    //     popupState.OnClose = p => PortfolioStart();
    // }

    // private void Tween_Fadein(TextMeshProUGUI text)
    // {
    //     if (!text.gameObject.activeInHierarchy) return;

    //     startText.DOFade(0, 0.5f).onComplete = () => Tween_Fadeout(startText);

    // }
    // private void Tween_Fadeout(TextMeshProUGUI text)
    // {
    //     if (!text.gameObject.activeInHierarchy) return;

    //     startText.DOFade(1, 0.5f).onComplete = () => Tween_Fadein(startText);
    // }

    // private void PortfolioStart()
    // {
    //     LocalPlayerInfo localplayerInfo = LocalSave.GetLocalPlayerInfo();

    //     if (localplayerInfo.playerType == PlayerType.none)
    //         LoadingManager.Instance.SceneLoad(Constants.Scene.Character);
    //     else
    //         LoadingManager.Instance.SceneLoad(Constants.Scene.Main);

    // }



    #endregion
}