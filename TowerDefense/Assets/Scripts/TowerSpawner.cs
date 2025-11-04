
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] Tilemap impassibleTerrainTilemap;
    [SerializeField] GridController gridController;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] GameObject towerPrefab;
    [SerializeField] GameObject towerPreview;
    Cell cellBelow;
    Vector3 mousePosition;
    Vector3 roundedMousePosition;
    bool isPlacing;

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        roundedMousePosition = new Vector3(Mathf.Floor(mousePosition.x) + 0.5f, Mathf.Floor(mousePosition.y + 0.5f), 0);
        
        if (towerPreview.activeSelf)
            towerPreview.transform.position = new Vector3(roundedMousePosition.x, roundedMousePosition.y - 0.1f, 0);
    }

    public void PlaceTower()
    {
        towerPreview.SetActive(true);
        isPlacing = true;
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        towerPreview.SetActive(false);
        isPlacing = false;
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            cellBelow =  gridController.currentFlowfield.WorldToCell(roundedMousePosition);

            if (impassibleTerrainTilemap.GetTile(impassibleTerrainTilemap.WorldToCell(cellBelow.worldPosition)) == null)
                return;

            if (cellBelow.hasTower)
            {
                RaycastHit2D hit = Physics2D.Raycast(roundedMousePosition, Vector2.zero);

                if (hit.collider != null)
                    hit.collider.gameObject.transform.GetChild(0).GetComponent<TowerController>().UpgradeTower();

                return;
            }

            if (!isPlacing)
                return;

            if (playerStats.money < 100)
                return;

            Instantiate(towerPrefab, new Vector3(roundedMousePosition.x, roundedMousePosition.y - 0.1f, 0), new Quaternion(0, 0, 0, 0));
            playerStats.ChangeMoney(-100);
            cellBelow.hasTower = true;
        }
    }
}
