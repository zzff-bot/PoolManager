using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellTowerCommand : Controller
{
    public override void Excute(MEventArgs eventArgs)
    {
        MSellTowerArgs e = eventArgs as MSellTowerArgs;

        e.Tower.Tile.data = null;

        //修改模型层数据
        GameModel gm = GetModel<GameModel>(MModelName.GameModel);
        gm.Gold += e.Tower.SellPrice;

        Game.GetInstance().Pool.Back(e.Tower.gameObject);
    }
}
