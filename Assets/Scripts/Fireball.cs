using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    private GameObject target;
    [SerializeField] float staminaDecrease = 0.2f;

    public void Initialize(Vector2 direction, float speed, GameObject target)
    {
        this.direction = direction;
        this.speed = speed;
        this.target = target;
    }

    private void Update()
    {
        // Move the fireball
        transform.Translate(direction * speed * Time.deltaTime);

        // Optional: Destroy the fireball if it goes out of bounds
        if (transform.position.magnitude > 50) // Adjust based on your game area
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the fireball hits the target
        if (collision.gameObject == target)
        {

            // Reduce stamina
            PlayerController targetPlayer = collision.GetComponent<PlayerController>();
            if (targetPlayer != null)
            {
                targetPlayer.ChangeStamina(-staminaDecrease);
            }

            // Destroy the fireball
            Destroy(gameObject);
        }
    }
}
