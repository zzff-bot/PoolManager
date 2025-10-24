using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyWaitForSeconds : CustomYieldInstruction
{
    private float curTime;
    private float waitTime;

    public MyWaitForSeconds(float waitTime)
    {
        this.waitTime = waitTime;
        curTime = Time.realtimeSinceStartup;
    }

    public override bool keepWaiting => (Time.realtimeSinceStartup - curTime) < waitTime;
}
