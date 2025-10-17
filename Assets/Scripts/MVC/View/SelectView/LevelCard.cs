using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCard
{
    private GameObject gameObject;
    private Image image;

    public LevelCard(GameObject gameObject)
    {
        this.gameObject = gameObject;
        image = gameObject.GetComponent<Image>();
    }

    public void SetMaskActive(bool active)
    {
        image.color = active ? new Color(0.5f, .5f, .5f, 1f) : Color.white;
    }

    public void SetLevelInfo(Level level)
    {
        if(level == null)
        {
            this.gameObject.SetActive(false);
            return;
        }
        this.gameObject.SetActive(true);
        Game.GetInstance().StartCoroutine(Utils.LoadSpriteAsync(Const.CardPath + level.CardImage, image));
    }
}
