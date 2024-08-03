using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Tilemaps;
using WaveFunctionCollapse;

public class Test : MonoBehaviour
{
    public Tilemap inputTilemap;
    public Tilemap outputTilemap;
    public int patternSize;
    public int maxIteration = 500;
    public int outputWidth = 5;
    public int outputHeight = 5;
    public bool equlaWeightts = false;
    ValuesManager<TileBase> valuesManager;
    WFCCore core;
    PatternManager manager;
    TileMapOutput output;

    public TileBase RoadTile;
    public TileBase groundTile;
    public TileBase stoneTile;
    public GameObject monsterPrefab;
    void Start()
    {
        CreateWFC();
    }
    public void CreateWFC()
    {
        InputReader reader = new InputReader(inputTilemap);
        var grid = reader.ReadInputToGrid();
        //for (int row = 0; row < grid.Length; row++)
        //{
        //    for (int col = 0; col < grid[0].Length; col++)
        //    {
        //        Debug.Log("Row: " + row + " Col: " + col + " tile name " + grid[row][col].Value.name);
        //    }
        //}
        valuesManager = new ValuesManager<TileBase>(grid);
        manager = new PatternManager(patternSize);
        manager.ProcessGrid(valuesManager, equlaWeightts);
        core = new WFCCore(outputWidth, outputHeight, maxIteration, manager);
        

    }
    public void CreateTilemap()
    {
        output = new TileMapOutput(valuesManager, outputTilemap);
        var result = core.CreateOputputGrid();
        output.CreateOutput(manager, result, outputWidth, outputHeight);


    }
    public void SaveTilemap()
    {
        if (output.OutputImage != null)
        {
            outputTilemap = output.OutputImage;
            GameObject objectToSave = outputTilemap.gameObject;
            PrefabUtility.SaveAsPrefabAsset(objectToSave, "Assets/Saved/output.prefab");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void cleanRoads()
    {
        for (int x = 0; x < outputTilemap.size.x; x++)
        {
            for (int y = 0; y < outputTilemap.size.y; y++)
            {
                if (outputTilemap.GetTile(new Vector3Int(x, y, 0)) == RoadTile)
                {
                    outputTilemap.SetTile(new Vector3Int(x, y, 0), groundTile);
                }
            }
        }
    }

    public void GenerateMonsters()
    {
        for (int x = 0; x < outputTilemap.size.x; x++)
        {
            for (int y = 0; y < outputTilemap.size.y; y++)
            {
                if (outputTilemap.GetTile(new Vector3Int(x, y, 0)) == stoneTile)
                {
                    float halfWidth = outputTilemap.cellSize.x / 2;
                    float halfHeight = outputTilemap.cellSize.y / 2;
                    Vector3 cellCenter = outputTilemap.CellToWorld(new Vector3Int(x, y, 0)) + new Vector3(0, halfHeight, 0);
                    Instantiate(monsterPrefab, cellCenter, Quaternion.identity);
                }
            }
        }
    }
}
