using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MEventArgs
{
    
}

public class MIntArgs : MEventArgs
{
    public int value;

    public MIntArgs(int mValue)
    {
        value = mValue;
    }
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

public class MMonsterDeadArgs : MEventArgs
{
    public Monster Monster;
    public MMonsterDeadArgs(Monster monster)
    {
        Monster = monster;
    }
}

public class MSpawnTowerArgs : MEventArgs
{
    public int TowerID;
    public Vector3 Position = Vector3.zero;

    public MSpawnTowerArgs() { }

    public MSpawnTowerArgs(int id) { this.TowerID = id; }

    public MSpawnTowerArgs(int id,Vector3 position) { this.TowerID = id;Position = position; }
}

public class MSellTowerArgs : MEventArgs
{
    public Tower Tower;

    public MSellTowerArgs(Tower tower)
    {
        this.Tower = tower;
    }
}