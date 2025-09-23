using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    protected MonoSingleton() { }

    public static T instance;

    public static T GetInstance()
    {
        GameObject go = GameObject.Find(typeof(T).ToString());
        if (go == null)
            go = new GameObject(typeof(T).ToString());
        else
            return instance;

        instance = go.AddComponent<T>();
        DontDestroyOnLoad(go);
        return instance;
    }
}
