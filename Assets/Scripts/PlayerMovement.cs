using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float acceleration = 3f; // Speed buildup
    public float deceleration = 2f; // Slows down smoothly
    public float rotationSpeed = 10f; // Smooth rotation
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    public Sprite idleSprite;
    public Sprite turnLeftSprite;
    public Sprite turnRightSprite;

    private Vector2 targetVelocity;
    private Vector2 mousePosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get movement input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Calculate target velocity
        Vector2 inputDirection = new Vector2(moveX, moveY).normalized;
        if (inputDirection.magnitude > 0)
        {
            targetVelocity += inputDirection * acceleration * Time.deltaTime;
            targetVelocity = Vector2.ClampMagnitude(targetVelocity, moveSpeed); // Limit max speed
        }
        else
        {
            targetVelocity = Vector2.Lerp(targetVelocity, Vector2.zero, deceleration * Time.deltaTime); // Smooth stop
        }

        // Get mouse position for rotation
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Update sprite
        UpdateSprite(moveX);
    }

    void FixedUpdate()
    {
        // Apply smooth movement
        rb.linearVelocity = targetVelocity;

        // Rotate towards the mouse smoothly
        Vector2 lookDir = (mousePosition - rb.position).normalized;
        float targetAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        float angle = Mathf.LerpAngle(rb.rotation, targetAngle, rotationSpeed * Time.fixedDeltaTime);
        rb.rotation = angle;
    }

    void UpdateSprite(float moveX)
    {
        if (moveX < 0) spriteRenderer.sprite = turnLeftSprite;
        else if (moveX > 0) spriteRenderer.sprite = turnRightSprite;
        else spriteRenderer.sprite = idleSprite;
    }
}