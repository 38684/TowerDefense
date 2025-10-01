using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridDirection
{
    public readonly Vector2Int vector;

    private  GridDirection(int x, int y)
    {
        vector = new Vector2Int(x, y);
    }

    public static implicit operator Vector2Int(GridDirection direction)
    {
        return direction.vector;
    }

    public static GridDirection GetDirectionFromVector(Vector2Int vector)
    {
        return EightWindDirections.DefaultIfEmpty(None).FirstOrDefault(direction => direction == vector);
    }

    public static readonly GridDirection
        None = new GridDirection(0, 0),
        North = new GridDirection(0, 1),
        NorthEast = new GridDirection(1, 1),
        East = new GridDirection(1, 0),
        SouthEast = new GridDirection(1, -1),
        South = new GridDirection(0, -1),
        SouthWest = new GridDirection(-1, -1),
        West = new GridDirection(-1, 0),
        NorthWest = new GridDirection(-1, 1);

    public static readonly List<GridDirection> CardinalDirections = new List<GridDirection>
    {
        North,
        East,
        South,
        West
    };

    public static readonly List<GridDirection> EightWindDirections = new List<GridDirection>
    {
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest
    };

    public static readonly List<GridDirection> AllDirections = new List<GridDirection>
    {
        None,
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest
    };
}