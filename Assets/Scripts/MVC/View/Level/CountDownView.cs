using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownView : View
{
    public override MViewName Name => MViewName.CountDownView;

    private GameObject[] numbers;

    private int time = 3;
    private int remainTime = 0;

    public override void HandleEvent(MEventType eventType, MEventArgs eventArgs)
    {
        throw new System.NotImplementedException();
    }

    protected override void Initialize()
    {
        base.Initialize();
        numbers = new GameObject[3];
        Transform countDown = transform.Find("CountDown");
        numbers[0] = countDown.Find("1").gameObject;
        numbers[1] = countDown.Find("2").gameObject;
        numbers[2] = countDown.Find("3").gameObject;
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(DisplayCount());
    }

    IEnumerator DisplayCount()
    {
        remainTime = time;
        while (true)
        {
            RefreshNumbers(remainTime);
            yield return new WaitForSeconds(1f);
            remainTime--;

            if (remainTime <= 0)
                break;
        }

        //倒计时结束
        SetActive(false);
        //派发倒计时结束的事件

    }

    private void RefreshNumbers(int remainTime)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i].SetActive(i == (remainTime - 1));
        }
    }
}
