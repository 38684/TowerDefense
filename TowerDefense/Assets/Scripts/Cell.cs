
using UnityEngine;

public class Cell
{
    public Vector3 worldPosition;
    public Vector2Int gridIndex;
    public byte cost;
    public ushort bestCost;
    public GridDirection bestDirection;
    
    public Cell(Vector3 _worldPosition, Vector2Int _gridIndex)
    {
        worldPosition = _worldPosition;
        gridIndex = _gridIndex;
        cost = 1;
        bestCost = ushort.MaxValue;
        bestDirection = GridDirection.None;
    }

    public void SetCost(int amount)
    {
        if (cost == byte.MaxValue) { return; }
        if (amount >= byte.MaxValue) {  cost = byte.MaxValue; }
        else { cost = (byte)amount; }
    }
}
