

#region Setting
// 언어 타입
public enum LanguageType
{
    Korean = 0,
    English,
    Max,
}

public enum PlayerViewType
{
    FPSView = 0,
    QuarterView,
    ShoulderView
}
#endregion

#region System
// 설명 타입
public enum DescriptType
{
    NULL = -1,
    Title = 1001,

}
public enum GAMEDATA_STATE
{
    CONNECTDATAERROR,
    PROTOCOLERROR,
    NODATA,
    REQUESTSUCCESS
}
#endregion
public enum CharacterSceneState
{
    Unclick = 0,
    Click,
    Select
}

public enum CharacterType
{
    none = -1,
    Male = 0,
    Female
}

public enum PlayerAniState
{
    Default,
    Attack,
    Skill,
    Victory
}

public enum PlayerAttackLevel
{
    Attack01 = 0,
    Attack02,
    Max
}



