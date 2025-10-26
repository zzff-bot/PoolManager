using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Role : MonoBehaviour, IReusable
{
    public event Action<int, int> HpEvent;
    public event Action<Role> DeadEvent;
    int curHp;
    int maxHp;

    public int CurHp
    {
        get{ return this.curHp;}
        set
        {
            value = Mathf.Clamp(value, 0, maxHp);
            //刷新
            if (value == this.curHp) return;    //防止无用的执行
            this.curHp = value;
            if (HpEvent != null) HpEvent(this.curHp, this.maxHp);

            if(this.curHp <= 0)
            {
                if (DeadEvent != null) DeadEvent(this);
            }
        }
    }

    public int MaxHp
    {
        get { return this.maxHp; }
        set
        {
            this.maxHp = Mathf.Clamp(value, 0, int.MaxValue);

            if (value == this.maxHp) return;    //防止无用的执行
            this.maxHp = value;
            if (HpEvent != null) HpEvent(this.curHp, this.maxHp);
        }
    }

    public bool IsDead { get { return this.curHp <= 0; } }

    public Vector3 Position { get => this.transform.position; set => this.transform.position = value; }

    public virtual void TakeDamage(int hit)
    {
        if (IsDead) return;
        this.CurHp -= hit;
    }

    public virtual void OnDead(Role role)
    {
        
    }

    public virtual void Back()
    {
        DeadEvent = null;
        HpEvent = null;

        curHp = 0;
        maxHp = 0;
    }

    public virtual void Take()
    {
        DeadEvent += this.OnDead;    
    }
}
