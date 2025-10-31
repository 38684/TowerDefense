
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] TMP_Text healthDisplay;
    [SerializeField] TMP_Text moneyDisplay;
    public int money = 100;
    int health = 10;

    public void LoseHealth(int damage)
    {
        if (damage == 0)
            damage = 1;

        health -= damage;

        healthDisplay.text = "Health: " + health;

        if (health <= 0)
        {
            Debug.Log("Game over");
        }
    }

    public void ChangeMoney(int loss)
    {
        money += loss;

        moneyDisplay.text = "Money: " + money;
    }
}
