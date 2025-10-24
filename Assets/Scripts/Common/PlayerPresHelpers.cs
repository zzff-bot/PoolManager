using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresHelpers
{
    #region 当前关卡

    //这种存档，是存在手机本地，换设备就会消失
    public static string CurrentLevelKey = "CurrentLevel";
    public static void SetCurrentLevelIdx(int idx)
    {
        PlayerPrefs.SetInt(CurrentLevelKey, idx);
    }

    public static int GetCurrentIdx()
    {
        return PlayerPrefs.GetInt(CurrentLevelKey);
    }
    #endregion
}
