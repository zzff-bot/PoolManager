using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PopoupMenuType
{
    Create,
    Upgrade,
}

public class PopupView : View
{
    private CreatePanel createPanel;
    private UpgradePanel upgradePanel;
    private Transform tfRoot;
    private bool isShow = false;
    private Vector3 point;

    public Vector3 Point { get => this.point; }

    public bool IsShow
    {
        get;
    }

    public override MViewName Name => MViewName.PopupView;

    public override void HandleEvent(MEventType eventType, MEventArgs eventArgs)
    {

    }

    protected override void Awake()
    {
        base.Awake();
        GameModel gm = GetModel<GameModel>(MModelName.GameModel);

        createPanel = GetComponentInChildren<CreatePanel>();
        createPanel.Load(gm,this);

        upgradePanel = GetComponentInChildren<UpgradePanel>();

        tfRoot = transform.Find("Root");

    }

    protected override void Start()
    {
        base.Start();

        HideAllPanel();
    }

    public void Hide()
    {
        HideAllPanel();
    }

    public void Show(PopoupMenuType type,Vector3 wolrdPoint, Tower tower = null)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(wolrdPoint);
        tfRoot.position = screenPosition;
        this.point = wolrdPoint;
        switch (type)
        {
            case PopoupMenuType.Create:
                ShowCreatePanel();
                break;
            case PopoupMenuType.Upgrade:
                upgradePanel.Load(GetModel<GameModel>(MModelName.GameModel), this, tower);
                ShowUpgradePanel();
                break;
            default:
                break;
        }
    }

    public void HideAllPanel()
    {
        this.createPanel.Hide();
        this.upgradePanel.Hide();
    }

    public void ShowCreatePanel()
    {
        this.createPanel.Show();
    }

    public void ShowUpgradePanel()
    {
        this.upgradePanel.Show();
    }

    public void MySendEvent(MEventType type,MEventArgs args)
    {
        SendEvent(type, args);
    }
}
