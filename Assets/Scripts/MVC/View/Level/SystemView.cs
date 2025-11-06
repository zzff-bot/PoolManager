using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemView : View
{
    MenuVIew view;
    int selectIdx = 0;

    public override MViewName Name => MViewName.SystemView;

    public override void HandleEvent(MEventType eventType, MEventArgs eventArgs)
    {
        throw new System.NotImplementedException();
    }

    protected override void Initialize()
    {
        view = GetView<MenuVIew>(MViewName.MenuVIew);

        base.Initialize();
        Transform tfBg = transform.Find("Background");
        tfBg.Find("btnContinue").GetComponent<Button>().onClick.AddListener(OnContinueClick);
        tfBg.Find("btnRestart").GetComponent<Button>().onClick.AddListener(OnRestartClick);
        tfBg.Find("btnSelectLevel").GetComponent<Button>().onClick.AddListener(OnSelectLevelClick);
        tfBg.Find("btnClose").GetComponent<Button>().onClick.AddListener(OnContinueClick);
    }

    protected override void Start()
    {
        base.Start();
        SetActive(false);
    }

    public override void SetActive(bool isActive)
    {
        base.SetActive(isActive);
        MenuVIew view = GetView<MenuVIew>(MViewName.MenuVIew);
        if(view != null)
        {
            GetView<MenuVIew>(MViewName.MenuVIew).IsPlaying = !isActive;       
        }        
    }

    private void OnContinueClick()
    {
        SetActive(false);
        view.IsPlaying = true;
        Time.timeScale = 1;
    }

    private void OnRestartClick()
    {
        GetModel<RoundModel>(MModelName.RoundModel).StopRound();
        Time.timeScale = 1;

        GameModel model = GetModel<GameModel>(MModelName.GameModel);
        int curIdx = model.CurLevelIdx;
        this.selectIdx = curIdx;
        MLevelArgs args = new MLevelArgs(this.selectIdx, false);
        SendEvent(MEventType.StartLevel, args);
        Game.GetInstance().Pool.Clear();
    }

    private void OnSelectLevelClick()
    {
        GetModel<RoundModel>(MModelName.RoundModel).StopRound();
        Time.timeScale = 1;
        Game.GetInstance().Pool.Clear();

        Game.GetInstance().LoadScene(2);
    }
}
