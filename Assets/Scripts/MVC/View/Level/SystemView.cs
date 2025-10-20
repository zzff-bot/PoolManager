using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemView : View
{
    public override MViewName Name => MViewName.SystemView;

    public override void HandleEvent(MEventType eventType, MEventArgs eventArgs)
    {
        throw new System.NotImplementedException();
    }

    protected override void Initialize()
    {
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
    }

    private void OnRestartClick()
    {

    }

    private void OnSelectLevelClick()
    {
        Game.GetInstance().LoadScene(2);
    }
}
