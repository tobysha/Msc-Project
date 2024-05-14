using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private int width;
    private int height;
    private float cellSize;
    private int[,] cells;
    public GameObject gameObject1;

    public Grid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        cells = new int[width, height];

        for(int i = 0; i < cells.GetLength(0); i++)
        {
            for(int j = 0; j < cells.GetLength(1); j++)
            {
                CreateWorldText(cells[i, j].ToString(), Color.white, gameObject1.transform, GetWorldPos(i, j)+ new Vector3(cellSize,cellSize)* .5f, 30);
                Debug.DrawLine(GetWorldPos(i, j), GetWorldPos(i, j + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPos(i, j), GetWorldPos(i + 1, j), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPos(0, height), GetWorldPos(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPos(width, 0), GetWorldPos(width, height), Color.white, 100f);
    }

    private Vector3 GetWorldPos(int x, int y)
    {
        return new Vector3(x,y)* cellSize;
    }

    public static TextMesh CreateWorldText( string text , Color color,Transform parent , Vector3 localPos ,int fontSize , TextAnchor textAnchor = TextAnchor.MiddleCenter, TextAlignment alignment = TextAlignment.Left, int sortingOrder = 5000)
    {
        GameObject go = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = go.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPos;
        TextMesh textMesh  = go.AddComponent<TextMesh>();
        textMesh.text = text;
        textMesh.anchor = textAnchor;
        textMesh.alignment = alignment;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }
}
