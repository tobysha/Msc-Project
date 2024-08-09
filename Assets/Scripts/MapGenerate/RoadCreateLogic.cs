using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class RoadCreateLogic : MonoBehaviour
{
    public Tilemap tilemap;
    [SerializeField] private Tilemap[] Environments;
    public TileBase[] environmentTiles;
    public TileBase[] tiles; // 需要拖动修改的Tile
    public TileBase roadTile;
    public GameObject TowerPrefab;
    public int RoadCost = 10;

    private Dictionary<Vector3Int, TileBase> originalTiles = new Dictionary<Vector3Int, TileBase>();
    private bool debugMode = false;
    private bool isDragging;

    private GameObject TowerShadow;
    private bool isPlacingTower = false;

    private GameManager gameData;
    private int originMoney;

    private void Start()
    {
        GameObject data = GameObject.Find("GameManager");
        gameData = data.GetComponent<GameManager>();
        //AdjustTileZPosition();
    }
    private void Update()
    {
        if (debugMode)
        {
            ChangeRoad();
        }
        TowerSpawner();
    }
    void AdjustTileZPosition()
    {
        /*  始终让上面的图层压着下面的图层  */
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                TileBase tile = tilemap.GetTile(pos);
                Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                tilemap.SetTransformMatrix(localPlace, Matrix4x4.TRS(new Vector3(0, 0, -pos.y * 0.1f), Quaternion.identity, Vector3.one));
            }
        }
    }
    private bool IsGroundTile(Vector3Int position)
    {
        bool isground = true;
        if(tilemap.GetTile(position) == roadTile)
        {
            return false;
        }
        foreach (Tilemap tm in Environments)
        {
            TileBase currentTile = tm.GetTile(position);
            foreach(TileBase tb in environmentTiles)
            {
                if (currentTile == tb)
                {
                    isground = false;
                    //Debug.Log("dizhuan");
                    break;
                }
            }
        }
        return isground;
    }
    private void ChangeRoad()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject()) // 当鼠标左键按下时开始拖动
        {
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0) || IsPointerOverUIObject()) // 当鼠标左键释放时停止拖动
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3Int cellPosition = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)); // 获取鼠标当前位置对应的Tilemap坐标
            cellPosition.z = 0;
            TileBase currentTile = tilemap.GetTile(cellPosition);
            //Debug.Log(cellPosition);
            if (currentTile != null && IsGroundTile(cellPosition) && gameData.getMoney() > RoadCost) // 确保该位置在Tilemap范围内
            {
                gameData.setMoney(gameData.getMoney()-RoadCost);
                // 根据鼠标拖动，修改Tilemap中的Tile内容
                tilemap.SetTile(cellPosition, tiles[Random.Range(0, tiles.Length)]);
            }
        }
    }
    public void SaveOriginalState()
    {
        originMoney = gameData.getMoney();
        originalTiles.Clear();
        BoundsInt bounds = tilemap.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(pos);
            if (tile != null)
            {
                originalTiles[pos] = tile;
            }
        }
    }
    public void setDebugMode(bool state)
    {
        debugMode = state;
    }
    public void ResetTilemap()
    {
        gameData.setMoney(originMoney);
        tilemap.ClearAllTiles();
        foreach (var kvp in originalTiles)
        {
            tilemap.SetTile(kvp.Key, kvp.Value);
        }
    }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    public void setPlacingTowerState(bool b, GameObject currentTower)
    {
        TowerPrefab = currentTower;
        TowerShadow = null;
        isPlacingTower = b;
    }

    private void TowerSpawner()
    {
        int towervalue = TowerPrefab.GetComponent<ObjectsData>().value;
        if (isPlacingTower && gameData.getMoney() > towervalue)
        {
            if (TowerShadow == null)
            {
                GameObject towerShadow = Instantiate(TowerPrefab);
                SpriteRenderer sr = towerShadow.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    Color c = sr.color;
                    c.a = 0.5f; // Semi-transparent
                    sr.color = c;
                }
                TowerShadow = towerShadow;
            }
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = tilemap.WorldToCell(mouseWorldPos);
            cellPos.z = 0;
            Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cellPos);

            if (TowerShadow != null)
            {
                TowerShadow.transform.position = cellCenterPos;
                TowerShadow.GetComponent<FireSystem>().SetisShadow(true);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (IsRoadTile(cellPos))
                {
                    List<Vector3Int> path = FindPath(cellPos);
                    isPlacingTower = false;
                    GameObject tower = Instantiate(TowerPrefab, cellCenterPos, Quaternion.identity);
                    MovementSystem towermovement = tower.GetComponent<MovementSystem>();
                    if (towermovement != null)
                    {
                        towermovement.InitializePath(tilemap, path);
                    }
                    gameData.setMoney(gameData.getMoney() - towervalue);
                    Destroy(TowerShadow);
                }
            }
        }
        else
        {
            isPlacingTower = false;
        }
    }
    bool IsRoadTile(Vector3Int cellPos)
    {
        TileBase tile = tilemap.GetTile(cellPos);
        return tile == roadTile;
    }
    List<Vector3Int> FindPath(Vector3Int startCell)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        Stack<Vector3Int> stack = new Stack<Vector3Int>();
        HashSet<Vector3Int> visited = new HashSet<Vector3Int>();

        stack.Push(startCell);
        visited.Add(startCell);

        while (stack.Count > 0)
        {
            Vector3Int current = stack.Pop();
            path.Add(current);

            foreach (Vector3Int direction in new Vector3Int[] { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right })
            {
                Vector3Int neighbor = current + direction;
                if (!visited.Contains(neighbor) && IsRoadTile(neighbor))
                {
                    stack.Push(neighbor);
                    visited.Add(neighbor);
                }
            }
        }

        return path;
    }
}
