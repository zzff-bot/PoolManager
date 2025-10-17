using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bird : MonoBehaviour
{
    private float OffsetY = 50f;
    private float duration = 2f;

    private Tweener tweener;
    private void Start()
    {
        //Pingpong Yoyo
        tweener = this.transform.DOLocalMoveY(this.transform.localPosition.y + OffsetY, duration);
        tweener.SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        if (tweener != null) tweener.Kill();
    }
}
