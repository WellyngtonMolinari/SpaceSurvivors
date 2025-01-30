using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 3f;
    private Vector2 direction;

    void Start()
    {
        direction = Random.insideUnitCircle.normalized; // Random direction
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}