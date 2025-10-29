using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : View
{
    public const int MonsterDamege = 1;
    Map map;
    LuoBo luoBo;

    public override MViewName Name => MViewName.Spawner;

    public override void HandleEvent(MEventType eventType, MEventArgs eventArgs)
    {
        switch (eventType)
        {
            case MEventType.EnterScene:
                // 1.获取地图组件               
                GameObject objMap = GameObject.Find("Map").gameObject;
                if(objMap == null)
                {
                    Debug.LogError("找不到Map对象，请检查");
                }
                map = objMap.GetComponent<Map>();

                // 2.加载地图关卡数据
                map.LoadLevel(GetModel<GameModel>(MModelName.GameModel).CurLevel);

                // 3.生成萝卜
                //怎么封装Luobo与 Monster
                OnSpawnLuoBo();
                break;
            case MEventType.StartRound:
                break;
            case MEventType.SpawnMonster:
                // 4.刷怪
                MSpawnMonsterArgs e = eventArgs as MSpawnMonsterArgs;
                OnSpawnMonster(e.MonsterID);
                break;
            default:
                break;
        }
    }

    //生成萝卜
    public void OnSpawnLuoBo()
    {
        GameObject objLuoBo = PoolManager.GetInstance().Take("LuoBo");
        luoBo = objLuoBo.GetComponent<LuoBo>();
        luoBo.Position = map.Path[map.Path.Length - 1];

        luoBo.HpEvent += OnLuoBoHpEvent;
        luoBo.DeadEvent += LuoBoDeadEvent;
    }

    //生成怪物
    void OnSpawnMonster(int monsterId)
    {
        MonsterInfo info = Game.GetInstance().StaticData.GetMonsterInfo(monsterId);
        GameObject objMonster = Game.GetInstance().Pool.Take(info.PrefabName);
        Monster monster = objMonster.GetComponent<Monster>();
        monster.Load(map.Path,info);

        monster.ReachedEvent += OnMonsterReached;
        monster.DeadEvent += OnMonsterDead;
    }

    void OnLuoBoHpEvent(int curHp,int maxHp)
    {

    }

    void LuoBoDeadEvent(Role role)
    {
        //回收
        Game.GetInstance().Pool.Back(role.gameObject);

        //游戏结束
        GameModel gm = GetModel<GameModel>(MModelName.GameModel);
        SendEvent(MEventType.EndLevel, new MLevelArgs(gm.CurSelectIdx, false));
    }

    protected void OnMonsterReached(Monster monster)
    {
        //观察者
        //委托
        luoBo.TakeDamage(MonsterDamege);
        monster.CurHp = 0;
    }

    void OnMonsterDead(Role monster)
    {
        //怪物要回收
        Game.GetInstance().Pool.Back(monster.gameObject);

        //加分
        MMonsterDeadArgs args = new MMonsterDeadArgs(monster as Monster);
        SendEvent(MEventType.MonsterDead, args);

        RoundModel rm = GetModel<RoundModel>(MModelName.RoundModel);
        GameModel gm = GetModel<GameModel>(MModelName.GameModel);       
        if (!luoBo.IsDead && rm.IsComplete)
        {   
            Monster[] monsters = GameObject.FindObjectsOfType<Monster>();
            //游戏胜利
            if (monsters.Length == 0)
                SendEvent(MEventType.EndLevel, new MLevelArgs(gm.CurSelectIdx, true));
        }
    }


    protected override void Awake()
    {
        base.Awake();
        RegisterEvent(MEventType.StartRound);
        RegisterEvent(MEventType.SpawnMonster);
        RegisterEvent(MEventType.EnterScene);
    }

    protected override void Start()
    {
        base.Start();

        GameObject go = Game.GetInstance().Pool.Take("Bottle");
        Tile tile = map.GetTile(3, 2);
        go.GetComponent<Tower>().Load(tile);
        go.transform.position = map.GetPosition(tile);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        UnregisterAll();
    }
}
