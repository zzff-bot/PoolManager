using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    protected override void Initial()
    {
        //生成播放音频的对象
        GameObject go = new GameObject("SoundManager");

        Object.DontDestroyOnLoad(go);
    }
}
