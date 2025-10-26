using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeadCommand : Controller
{
    public override void Excute(MEventArgs eventArgs)
    {
        MMonsterDeadArgs e = eventArgs as MMonsterDeadArgs;

        GameModel gameModel = GetModel<GameModel>(MModelName.GameModel);
        gameModel.Gold += e.Monster.Score;

        //GetView<MenuVIew>(MViewName.MenuVIew).Score = gameModel.Gold;
    }
}
