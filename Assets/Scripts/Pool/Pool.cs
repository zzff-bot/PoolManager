using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private string path;
    private Object prefab;
    private List<GameObject> list = new List<GameObject>();
    private List<GameObject> activelist = new List<GameObject>();

    public Pool(string path)
    {
        this.path = path;
        Load();
    }

    void Load()
    {
        prefab = Resources.Load(this.path);
    }

    public GameObject Take()
    {
        GameObject go;
        if(list.Count <= 0)
        {
            go = GameObject.Instantiate(prefab) as GameObject;
        }
        else
        {
            go = list[0];
            list.RemoveAt(0);
        }

        activelist.Add(go);
        go.SetActive(true);
        IReusable reusable = go.GetComponent<IReusable>();
        reusable.Take();
        return go;
    }

    public bool Contain(GameObject go)
    {
        return activelist.Contains(go);
    }

    public void Back(GameObject go)
    {
        if (!activelist.Contains(go)) return;
        activelist.Remove(go);
        list.Add(go);
        go.SetActive(false);
        IReusable reusable = go.GetComponent<IReusable>();
        reusable.Back();


    }

    public void Clear()
    {
        if(list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                GameObject.Destroy(list[i]);
            }
            list.Clear();
        }

        if(activelist.Count > 0)
        {
            for(int i = 0;i < activelist.Count; i++)
            {
                GameObject.Destroy(activelist[i]);
            }
            activelist.Clear();
        }
    }
}
