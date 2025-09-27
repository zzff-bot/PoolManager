using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    //格子的横向索引
    public int X;
    //格子的纵向索引
    public int Y;
    //是否可放置炮塔
    public bool CanHold;
    //炮塔实例
    public object data;

    public Tile(int x,int y)
    {
        this.X = x;
        this.Y = y;
    }

    public override string ToString()
    {
        return string.Format("{x={0}; Y={1}, CanHold={2}}", this.X, this.Y,this.CanHold);
    }
}