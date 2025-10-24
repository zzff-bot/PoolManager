using UnityEngine;
using UnityEngine.UI;

public enum GameSpeed
{
    One = 1,
    Two = 2,
}

public class MenuVIew : View
{
    public override MViewName Name => MViewName.MenuVIew;

    private int score;
    private int curRound;
    private int totalRound;
    private GameSpeed playspeed;
    private bool isPlaying = false;

    private Text txtScore;
    private Text txtCurRound;
    private Text txtTotalRound;
    private GameObject objBtnOne;
    private GameObject objBtnTwo;
    private GameObject objBtnResume;
    private GameObject objBtnPause;

    public int Score
    {
        get { return this.score; }
        set
        {
            score = Mathf.Clamp(value, 0, int.MaxValue);
            txtScore.text = score.ToString();
        }
    }

    public int CurRound
    {
        get { return this.curRound; }
        set
        {
            this.curRound = Mathf.Clamp(value, 0, int.MaxValue);
            this.txtCurRound.text = this.curRound.ToString("D2");
        }
    }

    protected override void Start()
    {
        base.Start();

        RegisterEvent(MEventType.StartRound);

        this.Score = 0;
        this.IsPlaying = true;
        this.PlaySpeed = GameSpeed.One;
        this.CurRound = 0;
        this.TotalRound = 0;
    }

    public int TotalRound
    {
        get { return this.totalRound; }
        set
        {
            this.totalRound = Mathf.Clamp(value, 0, int.MaxValue);
            this.txtTotalRound.text = this.totalRound.ToString("D2");
        }
    }

    public GameSpeed PlaySpeed
    {
        get { return this.playspeed; }
        set
        {
            this.playspeed = value;
            this.objBtnOne.SetActive(this.playspeed == GameSpeed.One);
            this.objBtnTwo.SetActive(this.playspeed == GameSpeed.Two);
        }
    }

    public bool IsPlaying
    {
        get { return this.isPlaying; }
        set
        {
            this.isPlaying = value;
            this.objBtnResume.SetActive(!this.isPlaying);
            this.objBtnPause.SetActive(this.isPlaying);
        }
    }

    protected override void Initialize()
    {
        base.Initialize();
        //先锁定父物体再确认
        Transform tfBackground = transform.Find("Background");
        txtScore = tfBackground.Find("txtScore").GetComponent<Text>();

        Transform tfRoundInfo = tfBackground.Find("RoundInfo");
        txtCurRound = tfRoundInfo.Find("txtCurRound").GetComponent<Text>();
        txtTotalRound = tfRoundInfo.Find("txtTotalRound").GetComponent<Text>();

        

        objBtnOne = tfBackground.Find("btnOne").gameObject;
        objBtnOne.GetComponent<Button>().onClick.AddListener(OnOneClick);

        objBtnTwo = tfBackground.Find("btnTwo").gameObject;
        objBtnTwo.GetComponent<Button>().onClick.AddListener(OnTwoClick);

        objBtnResume = tfBackground.Find("btnResume").gameObject;
        objBtnResume.GetComponent<Button>().onClick.AddListener(OnResumeClick);

        objBtnPause = tfBackground.Find("btnPause").gameObject;
        objBtnPause.GetComponent<Button>().onClick.AddListener(OnPauseClick);

        tfBackground.Find("btnSystem").GetComponent<Button>().onClick.AddListener(OnSystemClick);
    }

    public override void HandleEvent(MEventType eventType, MEventArgs eventArgs)
    {
        
        switch (eventType)
        {
            case MEventType.EnterScene:
                Debug.Log("MenuView中的EnterScence");
                break;
            case MEventType.ExitScene:
                break;
            case MEventType.StartUp:
                break;
            case MEventType.StartLevel:
                break;
            case MEventType.EndLevel:
                break;
            case MEventType.StartRound:
                MRoundArgs e = eventArgs as MRoundArgs;
                OnRoundInfoUpdate(e.CurRoundIdx, e.TotalRound);
                break;
            case MEventType.SpawnMonster:
                break;
            case MEventType.CountDownComplete:
                break;
            default:
                break;
        }
    }

    void OnRoundInfoUpdate(int curRound,int totalRound)
    {
        this.CurRound = curRound + 1;
        this.TotalRound = totalRound;
    }

    private void OnOneClick()
    {
        this.PlaySpeed = GameSpeed.Two;
    }

    private void OnTwoClick()
    {
        this.PlaySpeed = GameSpeed.One;
    }

    private void OnResumeClick()
    {
        this.IsPlaying = true;
    }

    private void OnPauseClick()
    {
        this.IsPlaying = false;
    }

    private void OnSystemClick()
    {
        //GetView()
        View systemView = GetView<SystemView>(MViewName.SystemView);
        systemView.SetActive(true);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        UnregisterAll();
    }
}
