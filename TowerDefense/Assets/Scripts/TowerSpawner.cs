using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] GridController gridController;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] GameObject towerPrefab;
    Cell cellBelow;

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector3 screenClickPosition = Mouse.current.position.ReadValue();
            screenClickPosition.z = 0;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(screenClickPosition);

            if (mousePosition.x < -gridController.gridSize.x / 2 - gridController.cellradius ||
                mousePosition.x > gridController.gridSize.x / 2 - gridController.cellradius ||
                mousePosition.y < -gridController.gridSize.y / 2 ||
                mousePosition.y > gridController.gridSize.y / 2)
                return;
            
            cellBelow =  gridController.currentFlowfield.WorldToCell(new Vector3(Mathf.Round(mousePosition.x - 0.5f) + 0.5f, Mathf.Round(mousePosition.y), 0));

            if (cellBelow.hasTower)
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(mousePosition.x, mousePosition.y), Vector2.zero);
               
                if (hit.collider != null)
                    hit.collider.gameObject.GetComponent<TowerController>().UpgradeTower();

                return;
            }

            if (playerStats.money < 100)
                return;

            Instantiate(towerPrefab, new Vector3(Mathf.Round(mousePosition.x - 0.5f) + 0.5f, Mathf.Round(mousePosition.y), 0), new Quaternion(0, 0, 0, 0));
            playerStats.ChangeMoney(-100);
            cellBelow.hasTower = true;
        }
    }
}
