using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    Level level;

    private List<Tile> grid;
    void InitGrid()
    {
        grid = new List<Tile>();

        for(int i = 0;i < RowCount; i++)
        {
            for(int j =0; j < ColumnCount; j++)
            {
                Tile tile = new Tile(i, j);
                grid.Add(tile);
            }
        }
    }

    Tile GetTile(Point p)
    {
        return GetTile(p.X, p.Y);
    }

    Tile GetTile(int x,int y)
    {
        return grid[GetIndex(x, y)];
    }

    int GetIndex(int x,int y)
    {
        return y * ColumnCount + x;
    }

    public void SetBackground(string fileName)
    {
        //动态加载贴图
        GameObject go = GameObject.Find("Background");
        if(go == null)
        {
            Debug.LogError("找不到关卡背景对象");
            return;
        }
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        string path = "file://" + Const.MapPath + fileName;
        StartCoroutine(Utils.LoadImageAsync(path, sr));
    }

    public void SetRoad(string fileName)
    {      
        //动态加载
        GameObject go = GameObject.Find("Road");
        if(go == null)
        {
            Debug.LogError("找不到路径对象");
            return;
        }
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        string path = "file://" + Const.MapPath + fileName;
        StartCoroutine(Utils.LoadImageAsync(path, sr));
    }

    private void Awake()
    {
        CaCulateSize();
        InitGrid();
    }

    public void LoadLevel(Level level)
    {
        Clear();
        this.level = level;
        this.SetBackground(level.Background);
        this.SetRoad(level.Road);

        //填充Holder
        for(int i = 0; i< level.Holder.Count; i++)
        {
            Point p = level.Holder[i];
            Tile tile = GetTile(p);
            tile.CanHold = true;
        }
    }

    void ClearHolder()
    {
        for(int i = 0; i < level.Holder.Count; i++)
        {
            this.grid[i].CanHold = false;
        }
    }

    void Clear()
    {
        ClearHolder();
    }

    void CaCulateSize()
    {
        Vector3 leftBottom = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightTop = Camera.main.ViewportToWorldPoint(Vector3.one);

        MapWidth = rightTop.x - leftBottom.x;
        MapHeight = rightTop.y - leftBottom.y;

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
        for(int i = 0; i <= RowCount; i++)
        {
            Gizmos.DrawLine(new Vector3(-MapWidth/2, i * TileHeight - MapHeight/2), new Vector3(MapWidth/2, i * TileHeight - MapHeight/2));
        }

        for(int i = 0;i <= ColumnCount; i++)
        {
            Gizmos.DrawLine(new Vector3(TileWidth * i - MapWidth / 2, 0 - MapHeight / 2), new Vector3(TileWidth * i - MapWidth / 2, MapHeight - MapHeight / 2));
        }
    }
}
