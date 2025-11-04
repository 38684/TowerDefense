
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TowerController : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Text towerLevelDisplay;
    List<GameObject> targetList = new List<GameObject>();
    PlayerStats playerStats;
    CircleCollider2D towerCollider;
    GameObject bullet;
    int level = 1;
    int damage = 1;
    float delay = 0.5f;
    bool isAttacking = false;

    private void Start()
    {
        playerStats = GameObject.Find("Canvas").GetComponent<PlayerStats>();
        towerCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        targetList.Add(other.gameObject);

        if (!isAttacking)
            StartCoroutine(TowerAttack());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        targetList.Remove(other.gameObject);
    }

    IEnumerator TowerAttack() 
    {
        isAttacking = true;

        while (targetList.Count > 0)
        {
            bullet = Instantiate(bulletPrefab, transform.position, new Quaternion(0,0,0,0));
            bullet.GetComponent<BulletDamage>().moveDirection = (targetList[0].transform.position - transform.position).normalized;
            bullet.GetComponent<BulletDamage>().damage = damage;

            yield return new WaitForSeconds(delay);
        }

        isAttacking = false;

        yield break;
    }

    public void UpgradeTower()
    {
        if (level > 3)
            return;

        if (playerStats.money <= level * 150)
            return;

        playerStats.ChangeMoney(-100 + level * -100);
        level++;
        towerLevelDisplay.text += "X";
        damage += 2;
        towerCollider.radius += 2f;
    }
}
