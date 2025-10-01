
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitController : MonoBehaviour
{
    public GridController gridController;
    public GameObject unitPrefab;
    public int amountUnitsPerSpawn;
    public float moveSpeed;
    public InputActionReference leftClick;
    public InputActionReference rightClick;

    private List<GameObject> unitsInGame;

    private void LeftClick(InputAction.CallbackContext Object)
    {
        SpawnUnits();
    }

    private void RightClick(InputAction.CallbackContext Object)
    {
        DestroyUnits();
    }

    private void FixedUpdate()
    {
        if (gridController.currentFlowfield == null) { return; }
        foreach (GameObject unit in unitsInGame)
        {
            Cell nodeBelow = gridController.currentFlowfield.WorldToCell(unit.transform.position);
            Vector3 moveDirection = new Vector3(nodeBelow.bestDirection.vector.x, nodeBelow.bestDirection.vector.y, 0);
            Rigidbody2D unitRigidbody = unit.GetComponent<Rigidbody2D>();
            unitRigidbody.linearVelocity = moveDirection * moveSpeed;
        }
    }

    private void SpawnUnits()
    {
        Vector2Int gridSize = gridController.gridSize;
        float nodeRadius = gridController.cellradius;
        Vector2 maxSpawnPosition = new Vector2(gridSize.x * nodeRadius * 2, gridSize.y * nodeRadius * 2);
        Vector3 newPosition;

        for (int i = 0; i < amountUnitsPerSpawn; i++)
        {
            GameObject newUnit = Instantiate(unitPrefab);
            newUnit.transform.parent = transform;
            unitsInGame.Add(newUnit);
        }
    }

    private void DestroyUnits()
    {
        foreach (GameObject gameObject in unitsInGame)
        {
            Destroy(gameObject);
        }

        unitsInGame.Clear();
    }

    private void Awake()
    {
        unitsInGame = new List<GameObject>();
    }

    private void OnEnable()
    {
        leftClick.action.started += LeftClick;
        rightClick.action.started += RightClick;
    }
}
