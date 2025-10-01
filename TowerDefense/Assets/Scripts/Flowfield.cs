using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Flowfield
{
    public Tilemap tilemap { get; private set; }
    public Dictionary<string, TileBase> tileDictionary { get; private set; }
    public Cell[,] grid { get; private set; }
    public Vector2Int gridSize { get; private set; }
    public float cellRadius { get; private set; }
    public Cell destinationCell;

    private float cellDiameter;

    public Flowfield(float _cellRadius, Vector2Int _gridSize, Tilemap _tilemap, Dictionary<string, TileBase> _tileDictionary)
    {
        tilemap = _tilemap;
        tileDictionary = _tileDictionary;
        cellRadius = _cellRadius;
        cellDiameter = cellRadius * 2f;
        gridSize = _gridSize;
    }

    public void CreateGrid()
    {
        grid = new Cell[gridSize.x, gridSize.y];

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3 worldPosition = new Vector3(
                    cellDiameter * x - gridSize.x / 2 - cellRadius,
                    cellDiameter * y - gridSize.y / 2 + cellRadius, 
                    0);
                grid[x, y] = new Cell(worldPosition, new Vector2Int(x, y));
            }
        }
    }

    public void CreateCostField()
    {
        Vector3 cellHalfExtents = Vector3.one * cellRadius;
        TileBase tileBase;

        foreach (Cell currentCell in grid)
        {
            tileBase = tilemap.GetTile(tilemap.WorldToCell(currentCell.worldPosition));
            bool hasIncreasedCost = false;

            if (hasIncreasedCost)
                continue;

            if (tileBase == tileDictionary["Rock"])
                currentCell.IncreaseCost(255);

            else if (tileBase == tileDictionary["Mud"])
                currentCell.IncreaseCost(3);

            hasIncreasedCost = true;
        }
    }

    public void CreateIntegrationField(Cell _destinationCell)
    {
        destinationCell = _destinationCell;
        destinationCell.cost = 0;
        destinationCell.bestCost = 0;

        Queue<Cell> cellsToCheck = new Queue<Cell>();

        cellsToCheck.Enqueue(destinationCell);

        while (cellsToCheck.Count > 0)
        {
            Cell currentCell = cellsToCheck.Dequeue();
            List<Cell> currentNeighbors = GetCardinalCells(currentCell.gridIndex, GridDirection.CardinalDirections);

            foreach (Cell currentNeighbor in currentNeighbors)
            {
                if (currentNeighbor.cost == byte.MaxValue) continue;
                if (currentNeighbor.cost + currentCell.bestCost < currentNeighbor.bestCost)
                {
                    currentNeighbor.bestCost = (ushort)(currentNeighbor.cost + currentCell.bestCost);
                    cellsToCheck.Enqueue(currentNeighbor);
                }
            }
        }
    }

    public void CreateFlowfield()
    {
        foreach (Cell currentCell in grid)
        {
            List<Cell> currentNeighbors = GetCardinalCells(currentCell.gridIndex, GridDirection.AllDirections);

            int bestCost = currentCell.bestCost;


            foreach (Cell currentNeighbor in currentNeighbors)
            {
                if (currentNeighbor.bestCost < bestCost)
                {
                    bestCost = currentNeighbor.bestCost;
                    currentCell.bestDirection = GridDirection.GetDirectionFromVector(currentNeighbor.gridIndex - currentCell.gridIndex);
                }
            }
        }
    }

    private List<Cell> GetCardinalCells(Vector2Int nodeIndex, List<GridDirection> directions)
    {
        List<Cell> neighbouringCells = new List<Cell>();

        foreach (Vector2Int currentDirection in directions)
        {
            Cell newNeighbor = GetCellAtRelativePosition(nodeIndex, currentDirection);
            
            if (newNeighbor != null)
                neighbouringCells.Add(newNeighbor);
        }

        return neighbouringCells;
    }

    private Cell GetCellAtRelativePosition(Vector2Int originPosition, Vector2Int relativePosition)
    {
        Vector2Int finalPosition = originPosition + relativePosition;

        // The origin position here counts from bottom left instead of center

        if (finalPosition.x < 0 || 
            finalPosition.x >=  gridSize.x || 
            finalPosition.y < 0 || 
            finalPosition.y >= gridSize.y)
            return null;

        else
            return grid[finalPosition.x, finalPosition.y];
    }

    public Cell WorldToCell(Vector3 worldPosition)
    {
        float percentX = worldPosition.x / (gridSize.x * cellDiameter);
        float percentY = worldPosition.y / (gridSize.y * cellDiameter);

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY); 

        int x = Mathf.Clamp(Mathf.FloorToInt((gridSize.x) * percentX), gridSize.x - 1, 0);
        int y = Mathf.Clamp(Mathf.FloorToInt((gridSize.y) * percentY), gridSize.y - 1, 0);
        return grid[x, y];
    }
}
