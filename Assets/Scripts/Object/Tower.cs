using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour, IReusable
{
    private int level;
    private int maxLevel;
    private float shotRate; //每秒攻击多少次
    private float shotInterval;
    private float timer;
    private float guardRange;   //寻敌范围
    Monster target;
    Animator animator;

    public int Level
    {
        get { return this.level; }
        set
        {
            value = Mathf.Clamp(value, 0, maxLevel);
            this.level = value;
            transform.localScale = Vector3.one * (1 + this.level * 0.3f);
        }
    }

    public int MaxLevel { get; }

    public bool IsTopLevel { get { return level >= maxLevel; } }

    public float ShotRate { get { return this.shotRate; } }

    public float GuardRange { get { return this.guardRange; } }

    //购买价格
    public int BasePrice { get; private set; }   //自动属性:自动生成一个字段存储

    //升级价格
    public int Price { get { return BasePrice * level; } }

    public Tile Tile { get; private set; }

    public void Back()
    {
        target = null;
        Tile = null;
        shotRate = 0;
        shotInterval = 0;
        Level = 0;
        maxLevel = 0;
        BasePrice = 0;
    }

    private void Update()
    {
        if(target == null)
        {
            //锁敌：并没有选取最近的一个(后续自己加)
            Monster[] monsters = GameObject.FindObjectsOfType<Monster>();
            foreach (var monster in monsters)
            {
                if (!monster.IsDead && Vector3.Distance(transform.position, monster.Position) <= GuardRange)
                {
                    target = monster;
                    break;
                }
            }
        }
        else
        {
            if (target.IsDead || Vector3.Distance(transform.position, target.Position) > GuardRange)
            {
                target = null;
                LookAt(null);
                return;
            }

            LookAt(target);

            timer += Time.deltaTime;
            if(timer >= this.shotInterval)
            {
                Shot(target);
                timer = 0;
            }
        }
    }

    void LookAt(Monster target)
    {
        if (target == null)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            //弧度，弧度没有360度的问题
            Vector3 direction = (target.Position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x);
            float euler = angle * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, euler - 90);
        }
    }

    public virtual void Shot(Monster target)
    {
        
    }

    public void Load(Tile tile,TowerInfo info)
    {
        this.Tile = tile;

        //加载数据
        this.shotRate = info.ShotRate;
        this.shotInterval = 1 / this.shotRate;
        Level = 1;
        maxLevel = info.MaxLevel;
        BasePrice = info.BasePrice;
        guardRange = info.GuardRange;
    }

    public void Take()
    {
        //先找自己身上的，再往子对象身上找
        animator = GetComponentInChildren<Animator>();
    }
}
