using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuoBo : Role
{
    private Animator animator;

    public override void TakeDamage(int hit)
    {
        base.TakeDamage(hit);
        if (IsDead) return;
        if (animator != null) animator.SetInteger("Hp", this.CurHp);
    }

    public override void OnDead(Role role)
    {
        base.OnDead(role);

    }

    public override void Take()
    {
        base.Take();
        animator = transform.GetComponent<Animator>();

        this.MaxHp = 7;
        this.CurHp = this.MaxHp;
    }

    public override void Back()
    {
        base.Back();

    }
}
