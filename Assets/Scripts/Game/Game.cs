using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Game : MonoBehaviour
{
    private static Game instance;

    public static Game GetInstance()
    {
        return instance;
    }

    //组合模式
    [HideInInspector] public PoolManager Pool;
    [HideInInspector] public SoundManager Sound;
    [HideInInspector] public StaticData StaticData;

    private void Awake()
    {
        instance = this;

        //读取后台配置信息(Json 头像资源地址，渠道，平台，资源服务器地址，资源版本号，客户端版本号)   对比资源版本号
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        Pool = PoolManager.GetInstance();
        Sound = SoundManager.GetInstance();
        StaticData = StaticData.GetInstance();

        //启动完，跳转至Start场景    EnterScene  ExitScene
        SceneManager.sceneLoaded += OnSceneLoaded;

        //View:打开的时候才需要注册   关闭的时候需要取消注册
        //MVC Model Controller
        //启动游戏的时候就可以注册Model了

        //1.定义处理StartUp事件的Controller

        //2.注册这个Controller
        MVC.RegisterController(MEventType.StartUp, typeof(StartUpCommand));
        MVC.SendEvent(MEventType.StartUp, null);

        //初始化DoTween
        DOTween.Init(false, true, LogBehaviour.Default);

        //进入到开始游戏界面
        LoadScene(1);
    }

    //当场景加载完毕调用
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        MSceneArgs args = new MSceneArgs(scene.buildIndex, scene.name);
        MVC.SendEvent(MEventType.EnterScene, args);
    }

    public void LoadScene(int level)
    {
        //发送退出场景的事件
        //1.构建事件参数
        Scene activeScene = SceneManager.GetActiveScene();
        MSceneArgs args = new MSceneArgs(activeScene.buildIndex, activeScene.name);
        //2.发送事件
        MVC.SendEvent(MEventType.ExitScene, args);

        SceneManager.LoadScene(level, LoadSceneMode.Single);

        //屏幕适配

    }
    
}
