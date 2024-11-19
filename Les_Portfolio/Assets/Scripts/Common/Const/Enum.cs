

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
    Character = 2001,
    Main = 3001,

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

#region Player
public enum PlayerType
{
    none = -1,
    Male = 0,
    Female,
    Max
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
#endregion

#region Monster
public enum MonsterType
{
    none = -1,
    Slime = 1,
    Turtle = 2,
    Max
}
public enum MonsterAniState
{
    Idle,
    Walk,
    Attack,
    Hit,
    Die
}

public enum MonsterState
{
    Alive,
    Stop,
    Die
}
#endregion


