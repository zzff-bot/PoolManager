using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MEventArgs
{
    
}

public class MSceneArgs : MEventArgs
{
    public int sceneIdx;
    public string sceneName;

    public MSceneArgs() { }

    public MSceneArgs(int sceneIdx,string sceneName)
    {
        this.sceneIdx = sceneIdx;
        this.sceneName = sceneName;
    }
}

public class MLevelArgs : MEventArgs
{
    public int LevelIdx;
    public bool IsSuccess;

    public MLevelArgs(int levelIdx,bool isSuccess)
    {
        LevelIdx = levelIdx;
        IsSuccess = isSuccess;
    }
}

public class MRoundArgs : MEventArgs
{
    public int CurRoundIdx;
    public int TotalRound;

    public MRoundArgs(int curRoundIdx,int totalRound)
    {
        CurRoundIdx = curRoundIdx;
        TotalRound = totalRound;
    }
}

public class MSpawnMonsterArgs : MEventArgs
{
    public int MonsterID;
    public MSpawnMonsterArgs() { }
    public MSpawnMonsterArgs(int monsterId) { this.MonsterID = monsterId; }
}