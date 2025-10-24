using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelCommand : Controller
{
    public override void Excute(MEventArgs args)
    {
        MLevelArgs e = args as MLevelArgs;
        GameModel gameModel = GetModel<GameModel>(MModelName.GameModel);
        gameModel.StartLevel(e.LevelIdx);

        GetModel<RoundModel>(MModelName.RoundModel).LoadLevel(gameModel.CurLevel);
         
        Game.GetInstance().LoadScene(3);
    }
}
