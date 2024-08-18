using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementSystem : MonoBehaviour
{
    public Tilemap tilemap; // Tilemap reference

    public float moveSpeed = 2f; // 移动速度
    //public Tilemap tilemap; // 引用Tilemap
    public TileBase roadTile; // 可移动的Tile类型
    public LayerMask towerLayer; // 防御塔的层，用于检测目标位置是否已被占据

    private Vector3Int currentCellPosition; // 当前所在的Tile格子位置
    private Vector3 targetPosition; // 目标位置
    private bool isMoving = false; // 标识是否正在移动

    void Start()
    {

    }
    public void InitializePath(Tilemap map, List<Vector3Int> path)
    {
        tilemap = map;
        currentCellPosition = tilemap.WorldToCell(transform.position);
        targetPosition = tilemap.GetCellCenterWorld(currentCellPosition);
        transform.position = targetPosition ;
    }

    // Update is called once per frame
    void Update()
    {

        OnKeyboardMovement();
    }
    private void OnKeyboardMovement()
    {
        if (isMoving)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
            return;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if ((horizontal != 0 || vertical != 0) && !isMoving)
        {
            Vector3Int direction = new Vector3Int(
                horizontal != 0 ? (int)Mathf.Sign(horizontal) : 0,
                vertical != 0 ? (int)Mathf.Sign(vertical) : 0,
                0
            );

            if (direction.x != 0 && direction.y != 0)
            {
                direction = new Vector3Int(direction.x - direction.y, direction.x + direction.y, 0);
            }

            Vector3Int newCellPosition = currentCellPosition + direction;

            TileBase tileAtTarget = tilemap.GetTile(newCellPosition);

            Vector3 targetWorldPosition = tilemap.GetCellCenterWorld(newCellPosition);
            Collider2D towerAtTarget = Physics2D.OverlapPoint(targetWorldPosition, towerLayer);


            if (tileAtTarget == roadTile && towerAtTarget == null)
            {
                currentCellPosition = newCellPosition;
                targetPosition = tilemap.GetCellCenterWorld(currentCellPosition);//offset
                isMoving = true;
            }
        }

    }
}

