using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Model
{
    public abstract string Name { get; }

    //发送消息
    public void SendEvent(MEventType eventType,MEventArgs eventArgs)
    {
        //MVC调度中心
    }
}
