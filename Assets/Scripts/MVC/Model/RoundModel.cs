using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundModel : Model
{
    public override MModelName Name => MModelName.RoundModel;

    //数据与数据逻辑
    public const float ROUND_INTERVAL = 3F; //回合间隔时间
    public const float SPAWN_INTERVAL = 1F; //出怪时间

    List<Round> rounds = new List<Round>();
    int curRoundIdx = -1;   //当前回合数
    bool isComplete = false;  //是否所有回合都结束了

    Coroutine runner;

    public int CurRoundIdx { get => this.curRoundIdx; }

    public int RoundCount { get => rounds.Count; }

    public bool IsComplete { get => this.isComplete; }

    public Round CurRound { get => rounds[CurRoundIdx]; }

    public void LoadLevel(Level level)
    {
        rounds = level.Rounds;
    }

    //数据逻辑
    public void StartRound()
    {
        runner = Game.GetInstance().StartCoroutine(RunRound());        
    }

    public void StopRound()
    {
        if(runner != null)
        {
            Game.GetInstance().StopCoroutine(runner);
            runner = null;
        }
    }

    IEnumerator RunRound()
    {

        curRoundIdx = -1;
        isComplete = false;
        
        for (int i = 0; i < RoundCount; i++)
        {   
            curRoundIdx = i;
            //发送回合开始事件

            MRoundArgs argsRound = new MRoundArgs(curRoundIdx, RoundCount);
            SendEvent(MEventType.StartRound, argsRound);

            //刷新这一回合的怪物
            for (int j = 0; j < CurRound.Count; j++)
            {
                yield return new MyWaitForSeconds(SPAWN_INTERVAL);
                //刷怪
                MSpawnMonsterArgs argsMonster = new MSpawnMonsterArgs(CurRound.MonsterId);
                SendEvent(MEventType.SpawnMonster, argsMonster);
                //Debug.Log("刷怪");

                //是否刷完了
                if(i == RoundCount - 1 && j == CurRound.Count - 1)
                {
                    isComplete = true;
                }
            }

            if(!isComplete)
                yield return new MyWaitForSeconds(ROUND_INTERVAL);

        }

        //yield：终止函数调用，并且返回函数手柄，当再次调用函数的时候，从手柄处继续执行
    }
}
