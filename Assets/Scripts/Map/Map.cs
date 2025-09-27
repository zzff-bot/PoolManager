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

    private void Awake()
    {
        CaCulateSize();
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

    private void OnDrawGizmos()
    {
        
    }
}
