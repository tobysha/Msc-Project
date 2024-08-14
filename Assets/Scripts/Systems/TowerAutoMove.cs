using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerAutoMove : MonoBehaviour
{
    private Tilemap roadTilemap;          // 引用你的道路Tilemap
    public TileBase roadTile;            // 定义道路Tile类型
    public float moveSpeed = 2f;         // 移动速度

    private Vector3Int currentTilePos;   // 当前Tile的位置
    private Vector3 targetPosition;      // 目标位置
    private Vector3Int lastDirection;   // 上次移动的方向

    private bool isSet = false;
    int count;
    void Start()
    {
        // 获取塔当前所在的Tile位置
        //InvokeRepeating("Movement", 0f, 1f);
    }
    public void InitializePath(Tilemap tilemap)
    {
        roadTilemap = tilemap;
        //currentTilePos = roadTilemap.WorldToCell(transform.position);
        //targetPosition = transform.position;
        isSet = true;
    }

    void Update()
    {
        Movement();
        
    }

    void CheckAndMoveToRoadTile()
    {
        
        // 获取防御塔当前Tile的位置
        Vector3Int newTilePos = roadTilemap.WorldToCell(transform.position);

        // 如果已经进入了新的Tile
        if (newTilePos != currentTilePos)
        {
            count++;
            Debug.Log(count);
            currentTilePos = newTilePos;

            // 检测四个方向的邻居Tile
            Vector3Int[] directions = {
                new Vector3Int(1, 0, 0),    // 右
                new Vector3Int(-1, 0, 0),   // 左
                new Vector3Int(0, 1, 0),    // 上
                new Vector3Int(0, -1, 0)    // 下
            };
            Vector3Int preferredDirection = lastDirection;

            // 如果上次方向不在四个方向中，重置
            if (System.Array.IndexOf(directions, preferredDirection) < 0)
            {
                preferredDirection = Vector3Int.zero;
            }
            ShuffleArray(directions, preferredDirection);
            foreach (Vector3Int dir in directions)
            {
                Vector3Int neighborTilePos = currentTilePos + dir;
                TileBase neighborTile = roadTilemap.GetTile(neighborTilePos);

                // 如果相邻Tile是RoadTile，就设置为目标位置
                if (neighborTile == roadTile)
                {
                    Debug.Log(neighborTilePos);
                    //targetPosition = roadTilemap.CellToWorld(neighborTilePos) - roadTilemap.tileAnchor;
                    targetPosition = roadTilemap.CellToWorld(neighborTilePos) + new Vector3(-0,0.5f,0);
                    Debug.Log(targetPosition);
                    return;
                }
            }
        }
    }

    void MoveToTarget()
    {
        // 平滑移动防御塔到目标位置
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
    void ShuffleArray(Vector3Int[] array, Vector3Int preferredDirection)
    {
        if (preferredDirection != Vector3Int.zero)
        {
            int index = System.Array.IndexOf(array, preferredDirection);
            if (index >= 0)
            {
                // 将优先方向移到数组的前面
                Vector3Int temp = array[0];
                array[0] = array[index];
                array[index] = temp;
            }
        }

        // 随机打乱数组的顺序
        for (int i = array.Length - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i + 1);
            Vector3Int temp = array[i];
            array[i] = array[rnd];
            array[rnd] = temp;
        }
    }
    void Movement()
    {
        if (isSet)
        {
            // 每次进入一个Tile时进行检测
            CheckAndMoveToRoadTile();
            // 移动防御塔
            MoveToTarget();
            //Debug.Log(targetPosition);
        }
    }
}
