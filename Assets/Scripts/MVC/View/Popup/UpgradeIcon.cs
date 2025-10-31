using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeIcon : MonoBehaviour
{
    private Tower tower;
    private GameModel gm;
    private GameObject objImg;
    private Text txtPrice;
    private PopupView popupView;

    private bool canLevelUp = false;

    private void Awake()
    {
        objImg = transform.Find("Img").gameObject;
        txtPrice = transform.Find("txtPrice").GetComponent<Text>();

        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void Load(Tower tower,GameModel gm,PopupView popupView)
    {
        this.tower = tower;
        this.gm = gm;
        this.popupView = popupView;

        //刷新界面
        if (tower.IsTopLevel)
        {
            this.objImg.SetActive(false);
            this.txtPrice.text = "已满级";
        }
        else
        {
            this.txtPrice.text = tower.Price.ToString();
            this.objImg.SetActive(gm.Gold >= tower.Price);
        }
    }

    private void OnClick()
    {
        this.popupView.HideAllPanel();
        if (tower.IsTopLevel || gm.Gold < tower.Price)
        {
            return;
        }

        //如果Tower可以升级，就扣钱并且升级
        gm.Gold -= tower.Price;
        tower.Level += 1;
    }
}
