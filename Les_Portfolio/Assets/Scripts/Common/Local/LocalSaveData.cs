using System;


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

[Serializable]
public class LocalPlayerInfo
{
    public PlayerType playerType;

    public LocalPlayerInfo()
    {
        playerType = PlayerType.none;
    }
}

