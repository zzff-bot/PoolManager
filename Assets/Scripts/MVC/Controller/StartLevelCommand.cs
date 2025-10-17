using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelCommand : Controller
{
    public override void Excute(MEventArgs eventArgs)
    {
        Game.GetInstance().LoadScene(3);
    }
}
