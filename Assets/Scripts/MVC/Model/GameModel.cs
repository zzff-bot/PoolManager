using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameModel : Model
{
    public override MModelName Name => MModelName.GameModel;
    
    private List<Level> levels = new List<Level>();

    private int curLevelIdx;    // 这个是当前解锁的关卡
    private int curSelectIdx;   //  当前选择的关卡
    private int gold;           //  关卡金币数量
    private bool isPlaying;     // 游戏是否进行中

    public List<Level> Levels { get => levels; private set => levels = value; }
    public int CurLevelIdx { get => this.curLevelIdx; }
    public int CurSelectIdx { get => this.curSelectIdx; }
    public int Gold { get => this.gold; }
    public bool IsPlaying { get => this.isPlaying; }
    public bool IsGamePass { get => curLevelIdx >= levels.Count; }
    public Level CurLevel {
        get
        {
            if(curSelectIdx >= levels.Count || curSelectIdx < 0)
            {
                Debug.Log("不存在的关卡下标:" + curSelectIdx);
                return null;
            }
            return levels[curSelectIdx];
        }
    }

    public void Initialize()
    {
        //加载配置表
        List<FileInfo> levelList = Utils.GetAllLevelFiles();

        for (int i = 0; i < levelList.Count; i++)
        {
            //内存换速度
            Level level = new Level();
            Utils.LoadLevel(levelList[i].Name, ref level);
            Levels.Add(level);
        }

        curLevelIdx = PlayerPresHelpers.GetCurrentIdx();
    }
}
        