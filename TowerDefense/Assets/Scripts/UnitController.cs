
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int health;
    GridController gridController;
    WaveSpawner waveSpawner;
    PlayerStats playerStats;

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

        Cell cellBelow = gridController.currentFlowfield.WorldToCell(transform.position);
        Vector3 moveDirection = new Vector3(cellBelow.bestDirection.vector.x, cellBelow.bestDirection.vector.y, 0);
        GetComponent<Rigidbody2D>().linearVelocity = moveDirection * speed;
    }

    private void Update()
    {
        if (health <= 0)
            Destroy(gameObject);
    }
}
