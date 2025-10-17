using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartView : View
{
    public override MViewName Name => MViewName.StartView;
    public override void HandleEvent(MEventType eventType, MEventArgs eventArgs)
    {

    }

    public void OnAdventureBtnClick()
    {
        Game.GetInstance().LoadScene(2);
    }
}
