using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StaticData : Singleton<StaticData>
{
    //存静态数据 提供获取静态数据的接口
    Dictionary<int, BulletInfo> Bullets = new Dictionary<int, BulletInfo>();
    Dictionary<int, MonsterInfo> Monsters = new Dictionary<int, MonsterInfo>();
    Dictionary<int, LuoBoInfo> LuoBo = new Dictionary<int, LuoBoInfo>();
    Dictionary<int, TowerInfo> Towers = new Dictionary<int, TowerInfo>();
    protected override void Initial()
    {
        InitBullets();
        InitMonster();
        InitLuoBo(); 
        InitTowers();
    }

    void InitBullets()
    {
        Bullets.Add(0, new BulletInfo() { ID = 0, PrefabName = "BottleBullet", BaseSpeed = 5f, BaseAttack = 1 });
    }

    void InitMonster()
    {
        Monsters.Add(0, new MonsterInfo() { ID = 0, Hp = 5, MoveSpeed = 1f, Price = 1,PrefabName = "Monster0" });
        Monsters.Add(1, new MonsterInfo() { ID = 1, Hp = 5, MoveSpeed = 1f, Price = 2});
    }

    void InitLuoBo()
    {
        LuoBo.Add(0, new LuoBoInfo() { ID = 0, Hp = 7 });
    }

    void InitTowers()
    {
        Towers.Add(0, new TowerInfo() { ID = 0, PrefabName = "Bottle", NormalIcon = "1/CanClick1", DisabledIcon = "1/CanClick0", MaxLevel = 3, BasePrice = 100, ShotRate = 2, GuardRange = 3f, UseBulletID = 0 }); 
    }

    public LuoBoInfo GetLuoboInfo()
    {
        return LuoBo[0];
    }

    public MonsterInfo GetMonsterInfo(int monsterId)
    {
        return Monsters[monsterId];
    }

    public BulletInfo GetBulletInfo(int bulletID)
    {
        return Bullets[bulletID];
    }

    public TowerInfo GetTowerInfo(int towerID)
    {
        return Towers[towerID];
    }

    public List<TowerInfo> GetTowerInfo()
    {
        //返回一个List
        return Towers.Values.ToList<TowerInfo>();
    }
}
