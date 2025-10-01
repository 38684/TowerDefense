using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector2Int gridSize;
    public float cellradius = 0.5f;
    public Flowfield currentFlowfield;

    [System.Serializable]
    public struct TileList
    { 
        public string tileName;
        public TileBase tileBase;
    }

    public TileList[] tileList;
    public Dictionary<string, TileBase> tileDictionary;

    private void Awake()
    {
        tileDictionary = new Dictionary<string, TileBase>(tileList.Length);
        foreach (var entry in tileList)
            tileDictionary.Add(entry.tileName, entry.tileBase);
    }

    private void InitializeFlowfield()
    {
        currentFlowfield = new Flowfield(cellradius, gridSize, tilemap, tileDictionary);
        currentFlowfield.CreateGrid();
    }

    private void Start()
    {
        InitializeFlowfield();

        currentFlowfield.CreateCostField();

        Cell cellDestination = currentFlowfield.grid[gridSize.x / 2 + 8, gridSize.y / 2];
        currentFlowfield.CreateIntegrationField(cellDestination);
        currentFlowfield.CreateFlowfield();
    }
}
