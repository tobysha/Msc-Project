using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestingGrid : MonoBehaviour
{
    public Tilemap tilemap;

    void Start()
    {
        AdjustTileZPosition();
    }

    void AdjustTileZPosition()
    {
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
}
