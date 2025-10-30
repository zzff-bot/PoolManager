using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//建造者模式
public class CreateIcon : MonoBehaviour
{
    Image image;
    TowerInfo info;
    bool isEnough;
    GameModel gameModel;
    PopupView view;

    private void Awake()
    {
        image = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void Load(TowerInfo info,GameModel gameModel, PopupView view)
    {
        this.info = info;
        this.gameModel = gameModel;
        this.view = view;

        CheckIsEnough();
    }

    public void CheckIsEnough()
    {
        isEnough = gameModel.Gold >= info.BasePrice;
        //路径    Pictures/NormalMordel/Game/
        string path = "Pictures/NormalMordel/Game/Tower/" + (isEnough ? info.NormalIcon : info.DisabledIcon);     //判断哪个成立拿哪个
        image.sprite = Resources.Load<Sprite>(path);
    }

    void OnClick()
    {
        if (isEnough)
        {
            MSpawnTowerArgs e = new MSpawnTowerArgs();
            e.TowerID = info.ID;
            e.Position = this.view.Point;
            this.view.MySendEvent(MEventType.SpawnTower, e);
        }

        this.view.HideAllPanel();
    }
}
