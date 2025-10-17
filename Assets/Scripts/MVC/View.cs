using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MViewName
{
    StartView,
    SelectView,
}

//视图层什么时候注册到MVC里面
public abstract class View : MonoBehaviour
{
    public abstract MViewName Name { get; }

    //视图层需要关注的事件类型
     [HideInInspector] public List<MEventType> attentionEvents = new List<MEventType>();

    protected void RegisterEvent(MEventType eventType)
    {
        if (ContainEventType(eventType)) return;
        attentionEvents.Add(eventType);
    }

    protected void UnregisterEvent(MEventType eventType)
    {
        if (!ContainEventType(eventType)) return;
        attentionEvents.Remove(eventType);
    }

    protected void UnregisterAll()
    {
        attentionEvents.Clear();
    }
    
    public bool ContainEventType(MEventType eventType)
    {
        return attentionEvents.Contains(eventType);
    }

    protected T GetModel<T>(string name) where T : Model
    {
        //返回模型层
        return MVC.GetModel<T>(name);
    }

    protected void SendEvent(MEventType eventType, MEventArgs eventArgs)
    {
        //MVC发送
        MVC.SendEvent(eventType, eventArgs);
    }

    public abstract void HandleEvent(MEventType eventType, MEventArgs eventArgs);

    protected virtual void Start()
    {
        MVC.RegisterView(this);
        Initialize();
    }

    protected virtual void OnDestroy()
    {
        MVC.UnRegisterView(this);
    }

    protected virtual void Initialize() { }
}
