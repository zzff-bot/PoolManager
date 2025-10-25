using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType { 
    Monster0,
}

public class Monster : Role
{
    public const float CLOSE_DISTANCE = 0.1F;

    public event Action<Monster> ReachedEvent;

    public MonsterType monsterType = MonsterType.Monster0;

    float moveSpeed;    //移动速度
    Vector3[] path;     //寻路路径
    int pointIdx = -1;  //当前寻路点下标
    bool isReached = false;     //是否到达

    public float MoveSpeed
    {
        get { return this.moveSpeed; }
        set
        {
            this.moveSpeed = value;
        }
    }

    public bool IsReached { get { return this.isReached; } }

    public void Load(Vector3[] path)
    {
        this.path = path;
        MoveNext();
    }

    public bool HasNext()
    {
        return pointIdx <= path.Length - 2;
    }

    void MoveNext()
    {
        if (!HasNext()) return;

        //刚出来
        if(pointIdx == -1)
        {
            pointIdx = 0;
            MovePosition(path[0]);
        }
        else
        {
            pointIdx++;
        }
    }

    void MovePosition(Vector3 position)
    {
        this.transform.position = position;
    }

    private void Update()
    {
        //寻路
        if (IsReached) return;

        //计算怪物与萝卜的距离
        Vector3 pos = transform.position;
        Vector3 dest = path[pointIdx];
        float dist = Vector3.Distance(pos, dest);
        if(dist <= CLOSE_DISTANCE)  //到达了
        {
            MovePosition(path[pointIdx]);
            if (HasNext())
            {
                MoveNext();
            }
            else
            {
                isReached = true;
                if(ReachedEvent != null)
                {
                    ReachedEvent(this);
                }
            }
        }
        else
        {
            Vector3 direction = dest - pos;
            transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
        }
    }

    public override void Take()
    {
        base.Take();

        //初始化数据
        MaxHp = 10;
        CurHp = MaxHp;
        moveSpeed = 3;
    }

    public override void Back()
    {
        base.Back();
        path = null;
        moveSpeed = 0;
        MaxHp = 0;
        CurHp = 0;
        this.ReachedEvent = null;
        isReached = false;
    }
}
