using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Vida máxima
    public int currentHealth; // Vida atual
    public SpriteRenderer spriteRenderer; // Referência ao SpriteRenderer
    public Sprite[] damageSprites; // Lista de sprites para diferentes estágios de dano
    public float invincibilityDuration = 1f; // Duração da invencibilidade

    private bool isInvincible = false; // Se o player está invencível
    private Sprite defaultSprite; // Sprite original

    void Start()
    {
        currentHealth = maxHealth; // Inicializa a vida
        defaultSprite = spriteRenderer.sprite; // Guarda o sprite original
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return; // Se está invencível, ignora o dano

        // Reduz a vida
        currentHealth -= damage;
        Debug.Log("Player took damage: " + damage + ". Current health: " + currentHealth);

        // Atualiza o sprite conforme o dano recebido
        UpdateSprite();

        // Verifica se o jogador morreu
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Torna o jogador temporariamente invencível
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    private void UpdateSprite()
    {
        if (damageSprites.Length == 0 || spriteRenderer == null) return;

        float healthPercentage = (float)currentHealth / maxHealth;
        int spriteIndex = Mathf.Clamp(Mathf.FloorToInt((1 - healthPercentage) * damageSprites.Length), 0, damageSprites.Length - 1);

        spriteRenderer.sprite = damageSprites[spriteIndex];
        Debug.Log("Updated sprite to: " + spriteIndex);
    }

    private System.Collections.IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    private void Die()
    {
        Debug.Log("Player died!");
        Destroy(gameObject);
    }
}
