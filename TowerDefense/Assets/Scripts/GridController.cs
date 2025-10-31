
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour
{
    public Tilemap roughTerrainTilemap;
    public Tilemap impassibleTerrainTilemap;
    public Vector2Int gridSize;
    public float cellradius = 0.5f;
    public Flowfield currentFlowfield;

    private void InitializeFlowfield()
    {
        currentFlowfield = new Flowfield(cellradius, gridSize, roughTerrainTilemap, impassibleTerrainTilemap);
        currentFlowfield.CreateGrid();
    }

    private void Start()
    {
        InitializeFlowfield();

        currentFlowfield.CreateCostField();

        Cell cellDestination = currentFlowfield.grid[gridSize.x - 1, gridSize.y / 2];
        currentFlowfield.CreateIntegrationField(cellDestination);
        currentFlowfield.CreateFlowfield();
    }
}
