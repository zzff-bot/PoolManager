using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MModelName
{
    GameModel,
    RoundModel,
}

public abstract class Model
{
    public abstract MModelName Name { get; }

    //发送消息
    public void SendEvent(MEventType eventType,MEventArgs eventArgs)
    {
        //MVC调度中心
        MVC.SendEvent(eventType, eventArgs);
    }
}
