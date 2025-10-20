using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountSuccessView : View
{
    public override MViewName Name => MViewName.AccountSuccessView;

    public override void HandleEvent(MEventType eventType, MEventArgs eventArgs)
    {
        throw new System.NotImplementedException();
    }

    protected override void Start()
    {
        base.Start();
        SetActive(false);
    }
}
