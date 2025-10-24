using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpCommand : Controller
{
    public override void Excute(MEventArgs eventArgs)
    {
        //处理StartUp事件的逻辑
        //注册Model层
        GameModel gameModel = new GameModel();
        RegisterModel(gameModel);
        RegisterModel(new RoundModel());

        //注册Controller层
        RegisterController(MEventType.EnterScene, typeof(EnterSceneCommand));
        RegisterController(MEventType.ExitScene, typeof(ExitSceneCommand));
        RegisterController(MEventType.StartLevel, typeof(StartLevelCommand));
        RegisterController(MEventType.EndLevel, typeof(EndLevelCommand));
        RegisterController(MEventType.CountDownComplete, typeof(CountDownCompleteCommand));

        gameModel.Initialize();
    }
}
