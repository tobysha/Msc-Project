using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Mathematics;
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
    public GameObject[] monsterPrefab;

    public int MoneyCal = 0;
    void Start()
    {
        MoneyCal = 0;
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
    public bool IsMapGenerateSuccess()
    {
        bool IsSucccess;
        IsSucccess = core.Isfinished;
        return IsSucccess;
    }
    public void SaveTilemap()
    {
        if (output.OutputImage != null)
        {
            outputTilemap = output.OutputImage;
            GameObject objectToSave = outputTilemap.gameObject;
            //PrefabUtility.SaveAsPrefabAsset(objectToSave, "Assets/Saved/output.prefab");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void cleanRoads()
    {
        int count = 0;
        for (int x = 0; x < outputTilemap.size.x; x++)
        {
            for (int y = 0; y < outputTilemap.size.y; y++)
            {
                if (outputTilemap.GetTile(new Vector3Int(x, y, 0)) == RoadTile)
                {
                    count++;
                    outputTilemap.SetTile(new Vector3Int(x, y, 0), groundTile);
                }
            }
        }
        MoneyCal += count * 10;
    }

    public void GenerateMonsters()
    {
        for (int x = 0; x < outputTilemap.size.x; x++)
        {
            for (int y = 0; y < outputTilemap.size.y; y++)
            {
                if (outputTilemap.GetTile(new Vector3Int(x, y, 0)) == stoneTile)
                {
                    float halfHeight = outputTilemap.cellSize.y / 2;
                    Vector3 cellCenter = outputTilemap.CellToWorld(new Vector3Int(x, y, 0)) + new Vector3(0, halfHeight, 0);
                    GameObject monster = Instantiate(monsterPrefab[UnityEngine.Random.Range(0, monsterPrefab.Length)], cellCenter, Quaternion.identity);
                    //if(monster.gameObject.TryGetComponent(out ObjectsData component))
                    //{
                    //    ObjectsData data = component;
                    //    MoneyCal += (int)(data.Difficulty * 90);
                    //    Debug.Log(MoneyCal);
                    //}
                }
            }
        }
    }
}
