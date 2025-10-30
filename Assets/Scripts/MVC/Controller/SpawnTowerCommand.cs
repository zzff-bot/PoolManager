using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTowerCommand : Controller
{
    public override void Excute(MEventArgs eventArgs)
    {
        MSpawnTowerArgs e = eventArgs as MSpawnTowerArgs;
        TowerInfo towerInfo = Game.GetInstance().StaticData.GetTowerInfo(e.TowerID);

        GameModel gm = GetModel<GameModel>(MModelName.GameModel);
        gm.Gold -= towerInfo.BasePrice; 

        GetView<Spawner>(MViewName.Spawner).SpawnerTower(towerInfo,e.Position);

        GetView<MenuVIew>(MViewName.MenuVIew).Score = gm.Gold;
    }
    
}
