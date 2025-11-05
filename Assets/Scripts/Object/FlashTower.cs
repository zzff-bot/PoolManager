using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashTower : Tower
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

        if (target == null || target.IsDead) return;

        //Éú³É×Óµ¯
        GameObject go = Game.GetInstance().Pool.Take("FlashBullet");
        FlashBullet bullet = go.GetComponent<Bullet>() as FlashBullet;
        go.transform.position = shotPoint.position;
        bullet.Load(1, target);
    }
}
