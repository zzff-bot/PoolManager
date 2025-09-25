using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    public abstract string Name { get; }

    //视图层需要关注的事件类型
    public List<EventType> attentionEvents = new List<EventType>();

    protected void RegisterEvent(EventType eventType)
    {
        if (ContainEventType(eventType)) return;
        attentionEvents.Add(eventType);
    }

    protected void UnregisterEvent(EventType eventType)
    {
        if (!ContainEventType(eventType)) return;
        attentionEvents.Remove(eventType);
    }

    protected void UnregisterAll()
    {
        attentionEvents.Clear();
    }
    
    public bool ContainEventType(EventType eventType)
    {
        return attentionEvents.Contains(eventType);
    }

    protected T GetModel<T>(string name) where T : Model
    {
        //返回模型层
        return MVC.GetModel<T>(name);
    }

    protected void SendEvent(EventType eventType, MEventArgs eventArgs)
    {
        //MVC发送
        MVC.SendEvent(eventType, eventArgs);
    }

    public abstract void HandleEvent(EventType eventType, MEventArgs eventArgs);
}
