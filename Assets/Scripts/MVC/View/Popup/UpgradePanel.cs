using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    private GameModel gameModel;
    private PopupView view;

    private UpgradeIcon upgradeIcon;
    private SellIcon sellIcon;

    private Tower tower;

    private void Awake()
    {
        upgradeIcon = this.GetComponentInChildren<UpgradeIcon>();
        sellIcon = this.GetComponentInChildren<SellIcon>();
    }

    public void Load(GameModel gameModel, PopupView view, Tower tower)
    {
        this.gameModel = gameModel;
        this.view = view;
        this.tower = tower;

        upgradeIcon.Load(tower, gameModel, view);
        sellIcon.Load(tower, view);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
