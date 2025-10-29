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

    private void Start()
    {
        image = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void Load(TowerInfo info,GameModel gameModel)
    {
        this.info = info;
        isEnough = gameModel.Gold >= info.BasePrice;

        //路径    Pictures/NormalMordel/Game/
        string path = "Pictures/NormalMordel/Game/" + (isEnough ? info.NormalIcon : info.DisabledIcon);     //判断哪个成立拿哪个
        image.sprite = Resources.Load<Sprite>(path);
    }

    void OnClick()
    {
        if (!isEnough)
        {
            Debug.Log("生成炮塔");
        }
    }
}
