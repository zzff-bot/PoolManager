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
        Monsters.Add(0, new MonsterInfo() { ID = 0, Hp = 2, MoveSpeed = 1f, Price = 50,PrefabName = "Monster0" });
        Monsters.Add(1, new MonsterInfo() { ID = 1, Hp = 3, MoveSpeed = 1f, Price = 60,PrefabName = "Monster1" });
        Monsters.Add(2, new MonsterInfo() { ID = 2, Hp = 3, MoveSpeed = 1f, Price = 80,PrefabName = "Monster2" });
        Monsters.Add(3, new MonsterInfo() { ID = 3, Hp = 3, MoveSpeed = 1f, Price = 90,PrefabName = "Monster3" });
        Monsters.Add(4, new MonsterInfo() { ID = 4, Hp = 3, MoveSpeed = 1f, Price = 100,PrefabName = "Monster4" });
    }

    void InitLuoBo()
    {
        LuoBo.Add(0, new LuoBoInfo() { ID = 0, Hp = 7 });
    }

    void InitTowers()
    {
        Towers.Add(0, new TowerInfo() { ID = 0, PrefabName = "Bottle", NormalIcon = "1/CanClick1", DisabledIcon = "1/CanClick0", MaxLevel = 3, BasePrice = 100, ShotRate = 3, GuardRange = 3f, UseBulletID = 0 }); 
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

    public List<TowerInfo> GetAllTowerInfo()
    {
        //返回一个List
        return Towers.Values.ToList<TowerInfo>();
    }
}
