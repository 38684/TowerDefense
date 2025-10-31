
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public Vector3 moveDirection;
    public int damage;
    float speed = 10000f;
    float timer = 0;

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer > 10f)
            Destroy(gameObject);

        transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection, Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<EnemyController>().LoseHealth(damage);
        Destroy(gameObject);
    }
}
