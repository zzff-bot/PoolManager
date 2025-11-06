using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AccountSuccessView : View
{   
    public GameObject objBtnRestart;
    public GameObject objBtnContinue;
    public Text txtCurRound;
    public Text txtTotalRound;

    int selectIdx = 0;

    public override MViewName Name => MViewName.AccountSuccessView;

    protected override void Initialize()
    {
        base.Initialize();

        Transform tfBackground = transform.Find("BackgoundS");

        objBtnRestart = tfBackground.Find("btnRestart").gameObject;
        objBtnRestart.GetComponent<Button>().onClick.AddListener(BtnRestart);

        objBtnContinue = tfBackground.Find("btnContinue").gameObject;
        objBtnContinue.GetComponent<Button>().onClick.AddListener(BtnSelectContinue);

        txtCurRound = tfBackground.Find("txtCurRound").GetComponent<Text>();
        txtTotalRound = tfBackground.Find("txtTotalRound").GetComponent<Text>();
    }

    public void BtnRestart()
    {
        Game.GetInstance().Pool.Clear();

        GameModel model = GetModel<GameModel>(MModelName.GameModel);
        int curIdx = model.CurLevelIdx;
        this.selectIdx = curIdx;
        MLevelArgs args = new MLevelArgs(this.selectIdx, false);
        SendEvent(MEventType.StartLevel, args);
    }

    public void BtnSelectContinue()
    {
        Game.GetInstance().Pool.Clear();

        Game.GetInstance().LoadScene(2);
    }

    public override void HandleEvent(MEventType eventType, MEventArgs eventArgs)
    {

    }

    protected override void Start()
    {
        base.Start();
        SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);

        GameModel totalRoundModel = GetModel<GameModel>(MModelName.GameModel);
        RoundModel roundModel = GetModel<RoundModel>(MModelName.RoundModel);

        //总共波数
        int total = totalRoundModel.CurLevel.Rounds.Count();
        txtTotalRound.text = total.ToString("D2");

        //当前波数
        int cur = roundModel.CurRoundIdx;
        txtCurRound.text = (cur + 1).ToString("D2");
    }
}
