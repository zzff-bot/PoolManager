using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitBullet : Bullet
{
    public Monster Target { get; private set; }

    public Vector3 Direction { get; private set; }

    public void Load(int level, Monster monster)
    {
        //父类重载
        Load(level);
        Target = monster;

        Direction = (Target.Position - transform.position).normalized;
    }

    protected override void Update()
    {
        base.Update();
        if (IsExploded) return;

        if (Target != null)
        {
            if (Target.IsDead)
                Direction = (Target.Position - transform.position).normalized;

            transform.Translate(Direction * MoveSpeed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, Target.Position) <= Monster.CLOSE_DISTANCE)
            {
                Target.TakeDamage((int)this.Attack);
                Explode();
            }
        }
        else
        {
            //敌人死亡后，继续向前移动
            transform.Translate(Target.Position - transform.position);
        }
    }

    public override void Back()
    {
        base.Back();
        Target = null;
    }

    public override void Take()
    {
        base.Take();
    }
}
