using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownCompleteCommand : Controller
{
    public override void Excute(MEventArgs args)
    {
        //开始刷新怪物
        GetModel<RoundModel>(MModelName.RoundModel).StartRound();
    }
}
