using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    private GameModel gameModel;
    private PopupView view;

    public void Load(GameModel gameModel, PopupView view)
    {
        this.gameModel = gameModel;
        this.view = view;
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
