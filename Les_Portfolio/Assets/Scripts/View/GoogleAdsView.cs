using UnityEngine;
using UISystem;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class GoogleAdsView : UIView
{
    private string _adUnitId;
    private BannerView _bannerView;
    [SerializeField] Button bannerBtn;
    [SerializeField] Button bannerBtn2;

    public void Show()
    {
        ShowLayer();
    }
    protected override void OnFirstShow()
    {
        bannerBtn.onClick.AddListener(LoadBannerAds);
        bannerBtn2.onClick.AddListener(DestroyBannerAds);
    }
    protected override void OnShow()
    {
        Init();
    }

    private void Init()
    {
        MobileAds.Initialize(initStatus => { });
        // ListenToAdEvents();
    }

    #region Function
    // 구글 광고 생성 (배너)
    public void CreateBannerAds()
    {
#if UNITY_ANDROID
        _adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
        _adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        _adUnitId = "unused";
#endif
        if (_bannerView != null) DestroyBannerAds();

        AdSize adSize = AdSize.GetLandscapeAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        _bannerView = new BannerView(_adUnitId, adSize, AdPosition.Top);
    }
    // 구글 광고 로드 (배너)
    public void LoadBannerAds()
    {
        CreateBannerAds();

        var adRequest = new AdRequest();
        _bannerView.LoadAd(adRequest);
    }
    // 구글 광고 삭제 (배너)
    public void DestroyBannerAds()
    {
        if (_bannerView != null)
        {
            _bannerView.Destroy();
            _bannerView = null;
        }
    }
    private void ListenToAdEvents()
    {
        // Raised when an ad is loaded into the banner view.
        _bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + _bannerView.GetResponseInfo());
        };
        // Raised when an ad fails to load into the banner view.
        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
        };
        // Raised when the ad is estimated to have earned money.
        _bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log($"Banner view paid {adValue.Value} {adValue.CurrencyCode}.");
        };
        // Raised when an impression is recorded for an ad.
        _bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        _bannerView.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        // Raised when an ad opened full screen content.
        _bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        _bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };
    }
    #endregion
}
