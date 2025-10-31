using UnityEngine;
using UnityEngine.InputSystem;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] GridController gridController;
    [SerializeField] GameObject towerPrefab;

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector3 mousePosition = Input.mousePosition;

            if (mousePosition.x < -gridController.gridSize.x / 2 - gridController.cellradius)
            {
                Instantiate(towerPrefab, new Vector3(mousePosition.x, mousePosition.y, 0), new Quaternion(0, 0, 0, 0));
            }
        }
    }
}
