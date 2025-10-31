
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] GridController gridController;
    [SerializeField] GameObject[] enemyList;
    public int enemiesAlive = 0;
    int waveCount = 0;
    float waveBudget = 3;
    float delay = 1;
    Vector3 spawnPosition;

    void Start()
    {
        spawnPosition = new Vector3(-gridController.gridSize.x / 2 - gridController.cellradius, 0, 0);
    }

    public void OnSpacebar(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (enemiesAlive > 0)
                return;

            waveCount++;
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        spawnPosition.y = (float)Random.Range(-7, 7);

        switch (waveCount % 10) { 
            case 0:
                for (int i = 0; i < waveBudget; i += 10)
                {
                    Instantiate(enemyList[3], spawnPosition, new Quaternion(0, 0, 0, 0));
                    enemiesAlive++;
                    yield return new WaitForSeconds(delay);
                }
                break;

            case 3:
                for (int i = 0; i < waveBudget; i += 2)
                {
                    Instantiate(enemyList[1], spawnPosition, new Quaternion(0, 0, 0, 0));
                    enemiesAlive++;
                    yield return new WaitForSeconds(delay);
                }
                break;

            case 7:
                for (int i = 0; i < waveBudget; i += 2)
                {
                    Instantiate(enemyList[1], spawnPosition, new Quaternion(0, 0, 0, 0));
                    enemiesAlive++;
                    yield return new WaitForSeconds(delay);
                }
                break;

            case 5:
                for (int i = 0; i < waveBudget; i += 2)
                {
                    Instantiate(enemyList[2], spawnPosition, new Quaternion(0, 0, 0, 0));
                    enemiesAlive++;
                    yield return new WaitForSeconds(delay);
                }
                break;

            default:
                for (int i = 0; i < waveBudget; i++)
                {
                    Instantiate(enemyList[0], spawnPosition, new Quaternion(0, 0, 0, 0));
                    enemiesAlive++;
                    yield return new WaitForSeconds(delay);
                }
                break;
        }

        waveBudget = Mathf.Floor(waveBudget * 1.5f);

        delay /= 0.9f;
        
        if (delay <= 0.1f)
            delay = 0.1f;
        
        yield break;
    }
}
