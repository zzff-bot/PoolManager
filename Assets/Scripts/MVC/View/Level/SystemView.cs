using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemView : View
{
    public override MViewName Name => throw new System.NotImplementedException();

    public override void HandleEvent(MEventType eventType, MEventArgs eventArgs)
    {
        throw new System.NotImplementedException();
    }

    public override void SetActive(bool isActive)
    {
        base.SetActive(isActive);
    }
}
