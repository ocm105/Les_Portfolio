
// 언어 타입
public enum LanguageType
{
    Korean = 0,
    English,
}

// 설명 타입
public enum DescriptType
{
    NULL = -1,
    Title = 1001,

}

public enum CharacterSceneState
{
    Unclick = 0,
    Click,
    Select
}

public enum CharacterType
{
    Male = 0,
    Female
}

public enum PlayerAniState
{
    Default,
    Victory
}

public enum PlayerCamaraType
{
    FPView,
    QuarterView,
    ShoulderView

}

public enum GAMEDATA_STATE
{
    CONNECTDATAERROR,
    PROTOCOLERROR,
    NODATA,
    REQUESTSUCCESS
}
