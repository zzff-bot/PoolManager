using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : View
{
    Map map;
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

                break;
            default:
                break;
        }
    }

    //生成萝卜
    public void OnSpawnLuoBo()
    {
        GameObject objLuoBo = PoolManager.GetInstance().Take("LuoBo");
        objLuoBo.GetComponent<LuoBo>().Position = map.Path[map.Path.Length - 1];
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
