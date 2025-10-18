using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 鼠标左键：设置寻路点
// 鼠标右键：设置放置点
//修改C#脚本，先停止游戏运行

// 参数:订阅者   左键/右键  Tile点击到的格子

//点击屏幕格子事件参数

public class TileClickEventArags : EventArgs
{
    public int MouseButton; // 鼠标左键：0，鼠标右键：1
    public Tile Tile;       // 点击到的格子

    public TileClickEventArags(int mouseButton, Tile tile)
    {
        this.MouseButton = mouseButton;
        this.Tile = tile;
    }
}

public class Map : MonoBehaviour
{
    //常量：只有被定义的时候才能赋值，定义之后不再修改它的值
    public const int RowCount = 8;
    public const int ColumnCount = 12;

    float MapWidth;
    float MapHeight;

    float TileWidth;
    float TileHeight;

    public bool ShowGizmos = true;

#if UNITY_EDITOR
    [HideInInspector]
    public Texture2D CardImage; //只在编辑器阶段存在
    [HideInInspector]
    public Texture2D Background;
    [HideInInspector]
    public Texture2D TempRoad;
#endif

    //定义点击屏幕格子事件    功能前瞻性
    public event EventHandler<TileClickEventArags> OnTileClickEvent;

    Level level;

    private List<Tile> grid;
    private List<Tile> road = new List<Tile>();

    void InitGrid()
    {
        grid = new List<Tile>();

        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColumnCount; j++)
            {
                Tile tile = new Tile(i, j);
                grid.Add(tile);
            }
        }
    }

    public List<Tile> Grid
    {
        get
        {
            return grid;
        }
    }

    public List<Tile> Road
    {
        get
        {
            return road;
        }
    }

    public Vector3[] Path
    {
        get
        {
            List<Vector3> temp = new List<Vector3>();
            for (int i = 0; i < road.Count; i++)
            {
                temp.Add(GetPosition(road[i]));
            }
            return temp.ToArray();
        }
    }

    Vector3 GetPosition(Tile tile)
    {
        float x = -MapWidth / 2 + (tile.Y + 0.5f) * TileWidth;
        float y = -MapHeight / 2 + (tile.X + 0.5f) * TileHeight;
        return new Vector3(x, y, 0);
    }

    Tile GetTile(Point p)
    {
        return GetTile(p.X, p.Y);
    }

    public Tile GetTile(int x, int y)
    {
        int index = GetIndex(x, y);
        if (index < grid.Count)
            return grid[index];
        else
            return null;
    }

    int GetIndex(int x, int y)
    {
        return x * ColumnCount + y;
    }

    public void SetBackground(string fileName)
    {
        //动态加载贴图
        GameObject go = transform.Find("Background").gameObject;   
        if (go == null)
        {
            Debug.LogError("找不到关卡背景对象");
            return;
        }
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        string path = Const.MapPath + fileName;
        StartCoroutine(Utils.LoadImageAsync(path, sr));
    }

    public void SetRoad(string fileName)
    {
        //动态加载
        GameObject go = transform.Find("Road").gameObject;
        if (go == null)
        {
            Debug.LogError("找不到路径对象");
            return;
        }
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        string path = Const.MapPath + fileName;
        StartCoroutine(Utils.LoadImageAsync(path, sr));
    }

    private void Awake()
    {
        CaCulateSize();
        InitGrid();

        //测试
        //Level level = new Level();
        //Utils.LoadLevel("level0.xml", ref level);
        //LoadLevel(level);

        OnTileClickEvent += OnTileClick;
    }

    void OnTileClick(object sender, EventArgs args)
    {
        TileClickEventArags eventArags = args as TileClickEventArags;

        // 鼠标点击屏幕左键：寻路点
        if (eventArags.MouseButton == 0 && !eventArags.Tile.CanHold)
        {
            if (road.Contains(eventArags.Tile))
            {
                // 点击的是寻路点，则取消
                road.Remove(eventArags.Tile);
            }
            else
            {
                // 增加寻路点
                road.Add(eventArags.Tile);
            }
        }

        // 鼠标点击屏幕右键：放置点
        if (eventArags.MouseButton == 1 && !road.Contains(eventArags.Tile))
        {
            eventArags.Tile.CanHold = !eventArags.Tile.CanHold;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TriggerClickTileEvent(0);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            TriggerClickTileEvent(1);
        }
    }

    void TriggerClickTileEvent(int mouseButton)
    {
        Tile tile = GetUnderMouseButton();
        // 派发事件 tile(没有点击到格子，还要不要派发事件)
        // 1.构建事件参数
        TileClickEventArags args = new TileClickEventArags(mouseButton, tile);
        // 2.触发事件
        if (OnTileClickEvent != null)
            OnTileClickEvent.Invoke(this, args);
    }

    Tile GetUnderMouseButton()
    {
        // 屏幕坐标
        //Input.mousePosition
        // 转成世界坐标
        //Camera.main.ScreenToViewportPoint

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int row = (int)((worldPoint.y + this.MapHeight / 2) / TileHeight);
        int col = (int)((worldPoint.x + this.MapWidth / 2) / TileWidth);

        //转换成行列数
        return GetTile(row, col);
    }

    public void LoadLevel(Level level)
    {
        Clear();
        this.level = level;
        this.SetBackground(level.Background);
        this.SetRoad(level.Road);

        //填充Holder
        for (int i = 0; i < level.Holder.Count; i++)
        {
            Point p = level.Holder[i];
            Tile tile = GetTile(p);
            tile.CanHold = true;
        }

        //填充路径点
        for (int i = 0; i < level.Path.Count; i++)
        {
            Point p = level.Path[i];
            Tile tile = GetTile(p.X,p.Y);
            road.Add(tile);
        }
    }

    public void ClearHolder()
    {
        for (int i = 0; i < this.grid.Count; i++)
        {
            this.grid[i].CanHold = false;
        }
    }

    public void ClearPath()
    {
        road.Clear();
    }

    void Clear()
    {
        ClearHolder();
        ClearPath();
    }

    void CaCulateSize()
    {
        Vector3 leftDown = new Vector3(0, 0);
        Vector3 rightUp = new Vector3(1, 1);
        Vector3 p1 = Camera.main.ViewportToWorldPoint(leftDown);//把视口坐标转换成世界坐标。

        Vector3 p2 = Camera.main.ViewportToWorldPoint(rightUp);

        MapWidth = (p2.x - p1.x);
        MapHeight = (p2.y - p1.y);

        TileWidth = MapWidth / ColumnCount;
        TileHeight = MapHeight / RowCount;
    }

    //画地图格子辅助线
    private void OnDrawGizmos()
    {
        if (!ShowGizmos) return;
        CaCulateSize();
        //画行
        Gizmos.color = Color.green;
        for (int i = 0; i <= RowCount; i++)
        {
            Gizmos.DrawLine(new Vector3(-MapWidth / 2, i * TileHeight - MapHeight / 2), new Vector3(MapWidth / 2, i * TileHeight - MapHeight / 2));
        }

        for (int i = 0; i <= ColumnCount; i++)
        {
            Gizmos.DrawLine(new Vector3(TileWidth * i - MapWidth / 2, 0 - MapHeight / 2), new Vector3(TileWidth * i - MapWidth / 2, MapHeight - MapHeight / 2));
        }

        //绘制寻路点+可放置点
        //绘制起点+终点
        if (grid == null) return;
        Gizmos.color = Color.red;
        if (road.Count > 0)
        {
            for (int i = 0; i < road.Count; i++)
            {
                if (i == 0)
                {
                    Gizmos.DrawIcon(GetPosition(road[i]), "startPoint");
                }

                if (i >= 1 && i == road.Count - 1)
                {
                    Gizmos.DrawIcon(GetPosition(road[i]), "luobo");
                }

                if (i < road.Count - 1)
                    Gizmos.DrawLine(GetPosition(road[i]), GetPosition(road[i + 1]));
            }
        }

        //绘制可放置点
        for (int i = 0; i < grid.Count; i++)
        {
            if (grid[i].CanHold)
            {
                Gizmos.DrawIcon(GetPosition(grid[i]), "Grid");
            }
        }
    }
}
