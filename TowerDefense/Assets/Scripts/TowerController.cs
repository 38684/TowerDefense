
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerController : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    PlayerStats playerStats;
    List<GameObject> targetList = new List<GameObject>();
    GameObject bullet;
    int level = 1;
    int damage = 1;
    float delay = 0.5f;
    bool isAttacking = false;

    private void Start()
    {
        playerStats = GameObject.Find("Canvas").GetComponent<PlayerStats>();
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
        if (level > 2)
            return;

        if (playerStats.money <= level * 150)
            return;

        playerStats.ChangeMoney(level * -150);
        level++;
        damage *= 2;
    }
}
