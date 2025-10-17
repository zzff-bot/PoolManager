using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SelectView : View
{
    private List<Level> levels = new List<Level>();
    private int curIdx = 0;
    private int selectIdx = -1;

    private List<LevelCard> cards = new List<LevelCard>();
    private GameObject leftBtn;
    private GameObject rightBtn;
    private GameObject startLevelBtn;
    private GameObject lockObj;

    public override MViewName Name => MViewName.SelectView;

    public override void HandleEvent(MEventType eventType, MEventArgs eventArgs)
    {

    }

    protected override void Initialize()
    {
        base.Initialize();
        Transform levelCard = transform.Find("LevelCard");
        cards.Add(new LevelCard(levelCard.Find("LevelCard1").gameObject));
        cards.Add(new LevelCard(levelCard.Find("LevelCard2").gameObject));
        cards.Add(new LevelCard(levelCard.Find("LevelCard3").gameObject));

        leftBtn = transform.Find("LeftBtn").gameObject;
        rightBtn = transform.Find("RightBtn").gameObject;
        leftBtn.GetComponent<Button>().onClick.AddListener(OnLeftBtnClick);
        rightBtn.GetComponent<Button>().onClick.AddListener(OnRightBtnClick);

        startLevelBtn = transform.Find("StartLevelBtn").gameObject;
        lockObj = transform.Find("Lock").gameObject;
        startLevelBtn.GetComponent<Button>().onClick.AddListener(OnStartClick);
    }

    protected override void Start()
    {
        base.Start();

        //1.读取关卡列表
        LoadLevels();

        //2.读取存档数据，之前通关到哪里
        curIdx = PlayerPresHelpers.GetCurrentIdx();

        //3.刷新界面
        SetSelectIdx(curIdx);
    }

    private void SetSelectIdx(int selectIdx)
    {
        if(selectIdx != this.selectIdx)
        {
            this.selectIdx = selectIdx;
            RefreshView();
        }
    }

    private void RefreshView()
    {
        //刷新卡
        int leftIdx = this.selectIdx - 1;
        cards[0].SetLevelInfo(this.selectIdx == 0 ? null : levels[leftIdx]);
        cards[0].SetMaskActive(leftIdx > this.curIdx);

        cards[1].SetLevelInfo(levels[this.selectIdx]);
        cards[1].SetMaskActive(this.selectIdx > this.curIdx);

        int rightIdx = this.selectIdx + 1;
        cards[2].SetLevelInfo(this.selectIdx == (levels.Count - 1) ? null : levels[rightIdx]);
        cards[2].SetMaskActive(rightIdx > this.curIdx);

        //刷新按钮
        leftBtn.SetActive(this.selectIdx < (this.levels.Count - 1));
        rightBtn.SetActive(this.selectIdx > 0);

        startLevelBtn.SetActive(this.selectIdx <= this.curIdx);
        lockObj.SetActive(this.selectIdx > this.curIdx);
    }

    private void LoadLevels()
    {
        List<FileInfo> levelList = Utils.GetAllLevelFiles();

        for (int i = 0; i < levelList.Count; i++)
        {
            Level level = new Level();
            Utils.LoadLevel(levelList[i].Name, ref level);
            levels.Add(level);
        }
    }

    private void OnLeftBtnClick()
    {
        int selectIdx = Mathf.Clamp((this.selectIdx + 1), 0, this.levels.Count - 1);
        SetSelectIdx(selectIdx);
    }

    private void OnRightBtnClick()
    {
        int selectIdx = Mathf.Clamp((this.selectIdx - 1), 0, this.levels.Count - 1);
        SetSelectIdx(selectIdx);
    }

    private void OnStartClick()
    {
        //点击开始游戏，   当前关卡下标
        //发送事件
        MLevelArgs args = new MLevelArgs(this.selectIdx, false);
        SendEvent(MEventType.StartLevel, args);        
    }

}
