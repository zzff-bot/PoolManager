using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IReusable
{
    private int Level { get; set; }

    public float BaseSpeed { get;private set; }

    public float BaseAttack { get; private set; }

    public float MoveSpeed { get { return BaseSpeed * Level; } }

    public float Attack { get { return BaseAttack * Level; } }

    public float DelayTime = 0.1f;

    protected bool IsExploded = false;

    protected virtual void Awake()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    public void Load(int level)
    {
        this.Level = level;

        this.BaseSpeed = 10;
        this.BaseAttack = 1;
    }

    public void Explode()
    {
        if (IsExploded) return;
        IsExploded = true;

        //Ïú»Ù×Óµ¯
        Game.GetInstance().Pool.Back(this.gameObject);
        StopCoroutine("DelayDestroy");
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(DelayTime);
        Explode();
    }

    public virtual void Back()
    {
        IsExploded = true;
        Level = 0;
        BaseSpeed = 0;
        BaseAttack = 0;
    }

    public virtual void Take()
    {
        IsExploded = false;
        StartCoroutine(DelayDestroy());
    }
}
