using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FlashBullet : Bullet
{
    public Monster Target { get; private set; }
    public Vector3 Direction { get; private set; }

    public void Load(int level, Monster monster)
    {
        //∏∏¿‡÷ÿ‘ÿ
        Load(level);
        Target = monster;

        Direction = (Target.Position - transform.position).normalized;
    }

    public override void Take()
    {
        base.Take();

        if(Target ==null || Target.IsDead)
        {
            Explode();
            return;
        }

        transform.position = Target.Position;

        Target.TakeDamage((int)Attack);
    }

    protected override void Update()
    {
        
    }

    public override void Back()
    {
        base.Back();
        Target = null;
        StopAllCoroutines();
    }
}
