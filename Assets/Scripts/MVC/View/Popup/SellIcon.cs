using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellIcon : MonoBehaviour
{
    Tower tower;
    PopupView popupView;
    private Text txtPrice;

    private void Awake()
    {
        txtPrice = transform.Find("txtPrice").GetComponent<Text>();
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void Load(Tower tower,PopupView popupView)
    {
        this.tower = tower;
        this.popupView = popupView;

        this.txtPrice.text = tower.Price.ToString();
    }

    private void OnClick()
    {
        this.popupView.HideAllPanel();

        this.popupView.MySendEvent(MEventType.SellTower,new MSellTowerArgs(this.tower));
    }
}
