using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Singleton<T> where T : Singleton<T>
{
    protected Singleton() { }

    private static T instance;
    private static System.Object obj = new object();

    public static T GetInstance()
    {
        lock (obj)
        {
            if(instance == null)
            {
                Type p = typeof(T);
                instance = Activator.CreateInstance(p) as T;
                instance.Initial();
            }
        }
        return instance;
    }

    protected abstract void Initial();
}
