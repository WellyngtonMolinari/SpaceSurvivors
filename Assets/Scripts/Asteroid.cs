using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 3f; // Movement speed
    public float rotationSpeed = 50f; // Base rotation speed
    public float rotationSpeedIncrease = 100f; // Speed increase on collision
    public int damage = 1; // Damage dealt to the player

    private Vector2 direction;
    private Rigidbody2D rb;

    void Start()
    {
        direction = Random.insideUnitCircle.normalized; // Random direction
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Move the asteroid
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Rotate the asteroid
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Collision with the player
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.TakeDamage(damage); // Damage the player
            }

            // Increase rotation speed
            rotationSpeed += rotationSpeedIncrease;
        }
        else if (other.CompareTag("Laser"))
        {
            // Collision with a laser
            Destroy(other.gameObject); // Destroy the laser
            rotationSpeed += rotationSpeedIncrease; // Increase rotation speed
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Collision with the player (physics-based)
            PlayerMovement player = collision.collider.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.TakeDamage(damage); // Damage the player
            }

            // Increase rotation speed
            rotationSpeed += rotationSpeedIncrease;
        }
    }
}