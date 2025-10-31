
using JetBrains.Annotations;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] float speed;
    GridController gridController;
    WaveSpawner waveSpawner;
    PlayerStats playerStats;
    Cell cellBelow;

    private void Start()
    {
        gridController = GameObject.Find("GridController").GetComponent<GridController>();
        waveSpawner = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
        playerStats = GameObject.Find("Canvas").GetComponent<PlayerStats>();
    }
    
    private void FixedUpdate()
    {
        if (transform.position.x >= 17.5)
        {
            waveSpawner.enemiesAlive--;
            playerStats.LoseHealth(health / 2);
            Destroy(gameObject);
        }

        if (gridController.currentFlowfield == null) { return; }

        //Debug.Log(transform.position.x % 1);
        //Debug.Log(transform.position.y % 1);

        //if (transform.position.x % 1 + 0.5f < 0.25f ||
        //    transform.position.x % 1 + 0.5f > 0.75f ||
        //    transform.position.y % 1 < 0.25f ||
        //    transform.position.y % 1 > 0.75f) { return; }
        if(cellBelow != null)
        {
            Vector3 difference = cellBelow.worldPosition - transform.position;
            if (difference.magnitude > 0.95f)
            {
                cellBelow = gridController.currentFlowfield.WorldToCell(transform.position);
            }
        }
        else
            cellBelow = gridController.currentFlowfield.WorldToCell(transform.position);

        Vector3 moveDirection = new Vector3(cellBelow.bestDirection.vector.x, cellBelow.bestDirection.vector.y, 0);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection, Time.deltaTime * speed);
    }

    public void LoseHealth(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            waveSpawner.enemiesAlive--;
            playerStats.ChangeMoney(20);
            Destroy(gameObject);
        }
    }
}
