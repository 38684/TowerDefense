
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridDirection
{
    public readonly Vector2Int vector;

    private GridDirection(int x, int y)
    {
        vector = new Vector2Int(x, y);
    }

    public static implicit operator Vector2Int(GridDirection direction)
    {
        return direction.vector;
    }

    public static GridDirection GetDirectionFromVector(Vector2Int vector)
    {
        return CardinalDirections.DefaultIfEmpty(None).FirstOrDefault(direction => direction == vector);
    }

    public static readonly GridDirection
        None = new GridDirection(0, 0),
        North = new GridDirection(0, 1),
        East = new GridDirection(1, 0),
        South = new GridDirection(0, -1),
        West = new GridDirection(-1, 0);

    public static readonly List<GridDirection> CardinalDirections = new List<GridDirection>
    {
        North,
        East,
        South,
        West
    };

    public static readonly List<GridDirection> AllDirections = new List<GridDirection>
    {
        None,
        North,
        East,
        South,
        West,
    };
}