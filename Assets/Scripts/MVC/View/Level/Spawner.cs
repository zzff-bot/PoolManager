using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : View
{
    Map map;
    LuoBo luoBo;

    public override MViewName Name => MViewName.Spawner;

    public override void HandleEvent(MEventType eventType, MEventArgs eventArgs)
    {
        Debug.Log(eventType);
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
        GameObject objMonster = PoolManager.GetInstance().Take("Monster" + monsterId);
        Monster monster = objMonster.GetComponent<Monster>();
        monster.Load(map.Path);

        monster.ReachedEvent += OnMonsterReached;
    }

    void OnLuoBoHpEvent(int curHp,int maxHp)
    {

    }

    void LuoBoDeadEvent(Role role)
    {

    }

    protected void OnMonsterReached(Monster monsster)
    {
        Debug.Log("怪物碰到萝卜");
    }

    

    protected void OnMouseDead(Role role)
    {
        //就知道了哪个怪物死了

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
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        UnregisterAll();
    }
}
