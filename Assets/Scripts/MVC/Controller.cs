using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller
{
    public T GetModel<T>(MModelName name) where T : Model
    {
        return MVC.GetModel<T>(name);
    }

    public T GetView<T>(MViewName name) where T: View
    {
        return MVC.GetView<T>(name);
    }

    public void RegisterModel(Model model)
    {
        MVC.RegisterModel(model);
    }

    public void RegisterView(View view)
    {
        MVC.RegisterView(view);
    }

    public void RegisterController(MEventType eventType,Type controllerType)
    {
        MVC.RegisterController(eventType, controllerType);
    }

    public abstract void Excute(MEventArgs eventArgs);
}