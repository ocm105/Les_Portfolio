using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UISystem;
using UnityEngine.UI;

public class SettingPopup : UIPopup
{
    #region Sound
    [Header("Sound")]
    [SerializeField] Sprite soundOnSprite;
    [SerializeField] Sprite soundOffSprite;
    [SerializeField] Button bgmButton;
    [SerializeField] Button sfxButton;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;
    private float bgmVolume, sfxVolume;
    private bool isBgm, isSfx;

    #endregion

    #region View
    [Header("View")]
    [SerializeField] Sprite viewOnSprite;
    [SerializeField] Sprite viewOffSprite;
    [Tooltip("FPSView, QuarterView, ShoulderView")]
    [SerializeField] Button[] viewButtons;
    private PlayerViewType playerViewType;
    #endregion

    #region Language
    [Header("Language")]
    [SerializeField] Sprite lggOnSprite;
    [SerializeField] Sprite lggOffSprite;
    [Tooltip("Korean, English")]
    [SerializeField] Button[] lggButtons;
    private LanguageType languageType;
    #endregion

    public PopupState Open()
    {
        ShowLayer();
        return state;
    }

    protected override void OnFirstShow()
    {
        bgmSlider.onValueChanged.AddListener((value) => OnChange_BGM(value));
        sfxSlider.onValueChanged.AddListener((value) => OnChange_SFX(value));
        bgmButton.onClick.AddListener(() => OnClick_BGM(!isBgm));
        sfxButton.onClick.AddListener(() => OnClick_SFX(!isSfx));

        for (int i = 0; i < viewButtons.Length; i++)
        {
            PlayerViewType type = (PlayerViewType)i;
            viewButtons[i].onClick.AddListener(() => OnClick_ViewBtn(type));
        }
        for (int i = 0; i < lggButtons.Length; i++)
        {
            LanguageType type = (LanguageType)i;
            lggButtons[i].onClick.AddListener(() => OnClick_languageBtn(type));
        }
    }
    protected override void OnShow() { }
    private void Init() { }

    #region Event
    private void OnChange_BGM(float value)
    {
        bgmVolume = value;

        if (bgmVolume <= 0f)
            bgmButton.image.sprite = soundOffSprite;
        else
            bgmButton.image.sprite = soundOnSprite;
    }
    private void OnChange_SFX(float value)
    {
        sfxVolume = value;

        if (sfxVolume <= 0f)
            sfxButton.image.sprite = soundOffSprite;
        else
            sfxButton.image.sprite = soundOnSprite;
    }
    private void OnClick_BGM(bool isOn)
    {
        isBgm = isOn;
        bgmSlider.value = isSfx ? 1f : 0f;
    }
    private void OnClick_SFX(bool isOn)
    {
        isSfx = isOn;
        sfxSlider.value = isSfx ? 1f : 0f;
    }

    private void OnClick_ViewBtn(PlayerViewType type)
    {
        for (int i = 0; i < viewButtons.Length; i++)
        {
            viewButtons[i].image.sprite = i == (int)type ? viewOnSprite : viewOffSprite;
        }

        playerViewType = type;
    }

    private void OnClick_languageBtn(LanguageType type)
    {
        for (int i = 0; i < lggButtons.Length; i++)
        {
            lggButtons[i].image.sprite = i == (int)type ? lggOnSprite : lggOffSprite;
        }

        languageType = type;
    }
    #endregion
}
