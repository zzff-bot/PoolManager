using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitTower : Tower
{
    Transform shotPoint;

    private void Awake()
    {
        shotPoint = transform.Find("ShotPoint");
    }

    public override void LookAt(Monster target)
    {
        
    }

    public override void Shot(Monster target)
    {
        base.Shot(target);
        //Éú³É×Óµ¯
        GameObject go = Game.GetInstance().Pool.Take("BottleBullet");
        BottleBullet bullet = go.GetComponent<Bullet>() as BottleBullet;
        go.transform.position = shotPoint.position;
        bullet.Load(1, target);
    }
}
