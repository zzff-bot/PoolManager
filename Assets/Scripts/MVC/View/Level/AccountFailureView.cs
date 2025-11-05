using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountFailureView : View
{
    public GameObject objBtnRestart;
    public GameObject objBtnContinue;
    int selectIdx = 0;

    public override MViewName Name => MViewName.AccountFailureView;

    protected override void Initialize()
    {
        base.Initialize();

        Transform tfBackground = transform.Find("BackgoundF");

        objBtnRestart = tfBackground.Find("btnRestart").gameObject;
        objBtnRestart.GetComponent<Button>().onClick.AddListener(BtnRestart);

        objBtnContinue = tfBackground.Find("btnContinue").gameObject;
        objBtnContinue.GetComponent<Button>().onClick.AddListener(BtnContinue);
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

    public void BtnContinue()
    {
        Game.GetInstance().LoadScene(2);
    }
}
