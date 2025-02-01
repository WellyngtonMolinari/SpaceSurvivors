using UnityEngine;

public class HomingLaser : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 200f; // How fast the laser rotates toward the target
    private Transform target;

    void Start()
    {
        // Find the nearest enemy when the laser is spawned
        FindNearestEnemy();
    }

    void Update()
    {
        if (target == null)
        {
            // If no target is found, move straight up (or in a default direction)
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            return;
        }

        // Rotate toward the target
        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        transform.Rotate(0, 0, -rotateAmount * rotationSpeed * Time.deltaTime);

        // Move forward
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    public float homingRange = 10f; // Maximum distance to find enemies

    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance && distance <= homingRange)
            {
                closestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); // Destroy the enemy
            Destroy(gameObject); // Destroy the laser
        }
    }
}