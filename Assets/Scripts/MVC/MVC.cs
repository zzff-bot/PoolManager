using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class MVC
{
    public static Dictionary<MModelName, Model> Models = new Dictionary<MModelName, Model>();
    public static Dictionary<MViewName, View> Views = new Dictionary<MViewName, View>();
    public static Dictionary<MEventType, Type> CommandMap = new Dictionary<MEventType, Type>();

    //注册
    public static void RegisterModel(Model model)
    {
        if (Models.ContainsKey(model.Name))
        {
            Debug.LogError("模型层重复注册:" + model.Name);
            return;
        }
        Models.Add(model.Name, model);
    }

    public static void RegisterView(View view)
    {
        if (Views.ContainsKey(view.Name))
        {
            Debug.LogError("视图层重复注册:" + view.Name);
            return;
        }
        Views.Add(view.Name, view);
    }

    public static void UnRegisterView(View view)
    {
        if (!Views.ContainsKey(view.Name))
        {
            Debug.Log("视图层不存在，不能移除");
            return;
        }
        Views.Remove(view.Name);
    }

    public static void RegisterController(MEventType eventType ,Type controllerType)
    {
        if (CommandMap.ContainsKey(eventType))
        {
            Debug.LogError("控制器重复注册：" + eventType);
            return;
        }
        CommandMap.Add(eventType, controllerType);
    }

    //获取
    public static T GetModel<T>(MModelName name) where T : Model
    {
        Model model = null;
        Models.TryGetValue(name, out model);
        return model as T;
    }

    public static T GetView<T>(MViewName name) where T : View
    {
        View view = null;
        Views.TryGetValue(name, out view);
        return view as T;
    }

    //发送执行函数
    public static void SendEvent(MEventType eventType,MEventArgs eventArgs)
    {
        if (CommandMap.ContainsKey(eventType))
        {
            Type t = CommandMap[eventType];
            Controller c = Activator.CreateInstance(t) as Controller;
            //控制器执行
            c.Excute(eventArgs);
        }

        foreach (View v in Views.Values)
        {
            if (v.attentionEvents.Contains(eventType)){
                v.HandleEvent(eventType,eventArgs);
            }
        }
    }
}