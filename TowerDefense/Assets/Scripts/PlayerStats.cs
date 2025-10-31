using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] Text healthDisplay;
    int health = 10;
    int money = 300;

    public void LoseHealth(int damage)
    {
        health -= damage;

        healthDisplay.text = "Health: " + health;

        if (health <= 0)
        {
            Debug.Log("Game over");
        }
    }
}
