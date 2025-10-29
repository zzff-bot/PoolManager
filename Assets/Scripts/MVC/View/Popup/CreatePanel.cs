using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePanel : MonoBehaviour
{
    private GameObject objPrefab;

    private bool inited;

    private void Start()
    {
        objPrefab = transform.Find("Prefab").gameObject;
        objPrefab.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Init()
    {

    }
}