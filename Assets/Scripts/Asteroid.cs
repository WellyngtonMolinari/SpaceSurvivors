using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 3f; // Velocidade de movimento
    public float rotationSpeed = 50f; // Velocidade de rotação base
    public float rotationSpeedIncrease = 100f; // Aumento da rotação após colisão
    public int damage = 1; // Dano causado ao jogador

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        rb.linearVelocity = randomDirection * speed; // Usa linearVelocity para movimento
        rb.angularVelocity = rotationSpeed; // Define a rotação inicial
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Meteor collided with: " + collision.collider.name);

        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Collided with player!");
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Aplica dano ao jogador
            }

            // Calcula a nova direção de movimento usando a normal da colisão
            Vector2 newDirection = Vector2.Reflect(rb.linearVelocity.normalized, collision.contacts[0].normal);
            rb.linearVelocity = newDirection * speed; // Aplica a nova velocidade

            // Aumenta a rotação
            rb.angularVelocity += rotationSpeedIncrease;
        }
        else if (collision.collider.CompareTag("Laser"))
        {
            Debug.Log("Collided with laser!");
            Destroy(collision.gameObject); // Destroi o laser
            rb.angularVelocity += rotationSpeedIncrease; // Aumenta a rotação
        }
        else
        {
            Debug.Log("Collided with something else!");
            // Rebater ao colidir com qualquer outro objeto
            Vector2 newDirection = Vector2.Reflect(rb.linearVelocity.normalized, collision.contacts[0].normal);
            rb.linearVelocity = newDirection * speed;
        }
    }
}
