using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerAutoMove : MonoBehaviour
{
    private Tilemap roadTilemap;          // ������ĵ�·Tilemap
    public TileBase roadTile;            // �����·Tile����
    public float moveSpeed = 2f;         // �ƶ��ٶ�

    private Vector3Int currentTilePos;   // ��ǰTile��λ��
    private Vector3 targetPosition;      // Ŀ��λ��
    private Vector3Int lastDirection;   // �ϴ��ƶ��ķ���

    private bool isSet = false;
    int count;
    void Start()
    {
        // ��ȡ����ǰ���ڵ�Tileλ��
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
        
        // ��ȡ��������ǰTile��λ��
        Vector3Int newTilePos = roadTilemap.WorldToCell(transform.position);

        // ����Ѿ��������µ�Tile
        if (newTilePos != currentTilePos)
        {
            count++;
            Debug.Log(count);
            currentTilePos = newTilePos;

            // ����ĸ�������ھ�Tile
            Vector3Int[] directions = {
                new Vector3Int(1, 0, 0),    // ��
                new Vector3Int(-1, 0, 0),   // ��
                new Vector3Int(0, 1, 0),    // ��
                new Vector3Int(0, -1, 0)    // ��
            };
            Vector3Int preferredDirection = lastDirection;

            // ����ϴη������ĸ������У�����
            if (System.Array.IndexOf(directions, preferredDirection) < 0)
            {
                preferredDirection = Vector3Int.zero;
            }
            ShuffleArray(directions, preferredDirection);
            foreach (Vector3Int dir in directions)
            {
                Vector3Int neighborTilePos = currentTilePos + dir;
                TileBase neighborTile = roadTilemap.GetTile(neighborTilePos);

                // �������Tile��RoadTile��������ΪĿ��λ��
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
        // ƽ���ƶ���������Ŀ��λ��
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
    void ShuffleArray(Vector3Int[] array, Vector3Int preferredDirection)
    {
        if (preferredDirection != Vector3Int.zero)
        {
            int index = System.Array.IndexOf(array, preferredDirection);
            if (index >= 0)
            {
                // �����ȷ����Ƶ������ǰ��
                Vector3Int temp = array[0];
                array[0] = array[index];
                array[index] = temp;
            }
        }

        // ������������˳��
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
            // ÿ�ν���һ��Tileʱ���м��
            CheckAndMoveToRoadTile();
            // �ƶ�������
            MoveToTarget();
            //Debug.Log(targetPosition);
        }
    }
}
