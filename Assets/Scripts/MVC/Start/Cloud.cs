using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cloud : MonoBehaviour
{
    public float OffsetX = 1400f;
    public float duration = 10f;

    private Tweener tweener;
    private void Start()
    {
        tweener = this.transform.DOLocalMoveX(this.transform.localPosition.x + OffsetX, duration);
        tweener.SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Restart);
    }

    private void OnDestroy()
    {
        if (tweener != null) tweener.Kill();
    }
}
