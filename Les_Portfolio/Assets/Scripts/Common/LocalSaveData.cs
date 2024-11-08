using System;


[Serializable]
public class LocalVolumeInfo
{
    public float bgmVolumeInfo;
    public float sfxVolumeInfo;

    public bool isBGMVolume;
    public bool isSFXVolume;

    public LocalVolumeInfo()
    {
        bgmVolumeInfo = 1f;
        sfxVolumeInfo = 1f;

        isBGMVolume = true;
        isSFXVolume = true;
    }
}

[Serializable]
public class LocalSettingInfo
{
    public float bgmVolume;
    public float sfxVolume;
    public bool isBgm;
    public bool isSfx;

    public PlayerViewType playerViewType;
    public LanguageType languageType;

    public LocalSettingInfo()
    {
        bgmVolume = 1f;
        sfxVolume = 1f;
        isBgm = true;
        isSfx = true;

        playerViewType = PlayerViewType.ShoulderView;
        languageType = LanguageType.Korean;
    }
}

