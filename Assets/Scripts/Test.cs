using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    //private List<GameObject> golist = new List<GameObject>();
    //public GameObject go;

    private void Awake()
    {
        SoundManager.GetInstance();
    }

    private void Update()
    {
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        go = PoolManager.GetInstance().Take("Prefabs/Cube");
    //        golist.Add(go);
    //    }

    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        PoolManager.GetInstance().Back(golist[0]);
    //        golist.RemoveAt(0);
    //    }

    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        PoolManager.GetInstance().Clear();
    //        golist.Clear();
    //    }
            
    }
}
