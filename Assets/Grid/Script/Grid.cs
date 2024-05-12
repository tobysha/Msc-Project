using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private int width;
    private int height;
    private int cellSize;
    private int[,] cells;

    public Grid(int width, int height, int cellSize, int[,] cells)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.cells = new int[width,height];

        for(int i = 0; i<width; i++)
        {
            for(int j = 0; j < height ; j++)
            {
                CreateWorldText(cells[i,j].ToString(), Color.white, null,GetWorldPos(i,j),30);
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

    public static TextMesh CreateWorldText( string text , Color color,Transform parent = null, Vector3 localPos = default(Vector3),int fontSize = 40, TextAnchor textAnchor = TextAnchor.MiddleCenter, TextAlignment alignment = TextAlignment.Left, int sortingOrder = 5000)
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
