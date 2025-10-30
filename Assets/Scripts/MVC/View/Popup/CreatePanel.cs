using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePanel : MonoBehaviour
{
    private GameObject objPrefab;

    private bool inited = false;    //是否已经初始化
    private List<TowerInfo> listTowerInfo;
    private List<CreateIcon> listCreateIcons = new List<CreateIcon>();
    private GameModel gameModel;
    private PopupView view;

    public void Load(GameModel gameModel,PopupView view)
    {
        this.gameModel = gameModel;
        this.view = view;
    }

    private void Awake()
    {
        objPrefab = transform.Find("Prefab").gameObject;
        objPrefab.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
        if (!inited) Init();
        for (int i = 0; i < listCreateIcons.Count; i++)
        {
            listCreateIcons[i].CheckIsEnough();
        }
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Init()
    {
        //根据配置表数量生成所有炮塔对应的图标
        inited = true;

        listTowerInfo = Game.GetInstance().StaticData.GetAllTowerInfo();

        if(listTowerInfo.Count > 0)
        {
            for (int i = 0; i < listTowerInfo.Count; i++)
            {
                CreateIcon icon = CreateIcon();
                icon.Load(listTowerInfo[i], gameModel, view);
                listCreateIcons.Add(icon);
            }
        }
    }

    CreateIcon CreateIcon()
    {
        GameObject go = GameObject.Instantiate(this.objPrefab);
        go.transform.parent = this.transform;
        go.SetActive(true);
        return go.GetComponent<CreateIcon>();
    }
}