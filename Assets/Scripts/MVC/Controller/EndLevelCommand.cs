using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelCommand : Controller
{
    public override void Excute(MEventArgs eventArgs)
    {
        //处理游戏结束的逻辑
        MLevelArgs e = eventArgs as MLevelArgs;

        //停止刷怪
        RoundModel rm = GetModel<RoundModel>(MModelName.RoundModel);
        rm.StopRound();

        GameModel gm = GetModel<GameModel>(MModelName.GameModel);
        gm.EndLevel(e.IsSuccess);

        //展示对应的UI
        if (e.IsSuccess)
        {
            GetView<AccountSuccessView>(MViewName.AccountSuccessView).Show();
        }
        else
        {
            GetView<AccountFailureView>(MViewName.AccountFailureView).Show();
        }
    }
}
