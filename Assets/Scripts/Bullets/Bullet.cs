using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected Vector2 direction;
    protected float speed;
    protected float duration;
    protected GameObject target;

    public void Initialize(Vector2 direction, float speed, GameObject target, float duration)
    {
        this.direction = direction;
        this.speed = speed;
        this.target = target;
        this.duration = duration;
    }

    protected virtual void Update()
    {
        // Move the bullet
        transform.Translate(direction * speed * Time.deltaTime);

        // Optional: Destroy the bullet if it goes out of bounds
        if (transform.position.magnitude > 50) // Adjust based on your game area
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet hits the target
        if (collision.gameObject == target)
        {
            OnHitTarget(collision);
            Destroy(gameObject);
        }
    }

    // Abstract method for specific behavior when hitting the target
    protected abstract void OnHitTarget(Collider2D collision);
}
