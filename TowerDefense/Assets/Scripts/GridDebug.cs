
using UnityEditor;
using UnityEngine;

public enum FlowfieldDisplayType { None, AllIcons, DestinationIcon, CostField, IntegrationField };

public class GridDebug : MonoBehaviour
{
    [SerializeField] GridController gridController;
    [SerializeField] bool displayGrid;
    [SerializeField] FlowfieldDisplayType currentDisplayType;
    public Sprite[] flowfieldIcons;

    private void OnValidate()
    {
        DrawFlowfield();
    }

    public void DrawFlowfield()
    {
        ClearCellDisplay();

        if (gridController.currentFlowfield == null)
            return;

        switch (currentDisplayType)
        {
            case FlowfieldDisplayType.AllIcons:
                DisplayAllCells();
                break;
            
            case FlowfieldDisplayType.DestinationIcon:
                DisplayDestinationCell();
                break;
        }
    }

    private void DisplayAllCells()
    {
        foreach (Cell currentCell in gridController.currentFlowfield.grid)
        {
            DisplayCell(currentCell);
        }
    }

    private void DisplayDestinationCell()
    {
        DisplayCell(gridController.currentFlowfield.destinationCell);
    }

    private void DisplayCell(Cell cell)
    {
        GameObject iconGameObject = new GameObject();
        SpriteRenderer iconSpriteRenderer = iconGameObject.AddComponent<SpriteRenderer>();
        iconSpriteRenderer.sortingOrder = 1;
        iconGameObject.transform.parent = transform;
        iconGameObject.transform.position = cell.worldPosition;

        switch (cell.cost) {
            case 0:
                iconSpriteRenderer.sprite = flowfieldIcons[3];
                return;

            case byte.MaxValue:
                iconSpriteRenderer.sprite = flowfieldIcons[2];
                return;
        }

        if (cell.bestDirection == GridDirection.North)
        {
            iconSpriteRenderer.sprite = flowfieldIcons[0];
        }
        else if (cell.bestDirection == GridDirection.East)
        {
            iconSpriteRenderer.sprite = flowfieldIcons[0];
            iconGameObject.transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if (cell.bestDirection == GridDirection.South)
        {
            iconSpriteRenderer.sprite = flowfieldIcons[0];
            iconGameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (cell.bestDirection == GridDirection.West)
        {
            iconSpriteRenderer.sprite = flowfieldIcons[0];
            iconGameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else {
            iconSpriteRenderer.sprite = flowfieldIcons[2];
        }
    }

    public void ClearCellDisplay()
    {
        foreach (Transform transform in transform)
        {
            Destroy(transform.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if (displayGrid)
        {
            if (gridController.currentFlowfield == null)
                DrawGrid(gridController.gridSize, Color.yellow, gridController.cellradius);

            else
                DrawGrid(gridController.gridSize, Color.white, gridController.cellradius);
        }

        if (gridController.currentFlowfield == null)
            return;

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.MiddleCenter;

        switch (currentDisplayType)
        {
            case FlowfieldDisplayType.CostField:

                foreach (Cell currentCell in gridController.currentFlowfield.grid)
                    Handles.Label(currentCell.worldPosition, currentCell.cost.ToString(), style);
                break;

            case FlowfieldDisplayType.IntegrationField:
                
                foreach (Cell currentCell in gridController.currentFlowfield.grid)
                    Handles.Label(currentCell.worldPosition, currentCell.bestCost.ToString(), style);
                break;
        }
    }

    private void DrawGrid(Vector2Int drawGridSize, Color drawColor, float drawCellRadius)
    {
        Gizmos.color = drawColor;

        for (int x = 0; x < drawGridSize.x; x++)
        {
            for (int y = 0; y < drawGridSize.y; y++)
            {
                Vector3 center = new Vector3(
                    drawCellRadius * 2 * x - gridController.gridSize.x / 2 - drawCellRadius,
                    drawCellRadius * 2 * y - gridController.gridSize.y / 2,
                    0);
                Vector3 size = Vector3.one * drawCellRadius * 2;
                Gizmos.DrawWireCube(center, size);
            }
        }
    }
}
