using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] bool isPlayerOne;
    [Range(0f, 20f)]
    public float playerSpeed = 15f;
    [Range(1f, 3f)]
    public float sprintMultiplier = 1.25f;
    bool canSprint = true;
    Rigidbody2D rb;
    Vector2 movement;
    float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOne)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            movement.x = Input.GetAxisRaw("PlayerTwoHorizontal");
            movement.y = Input.GetAxisRaw("PlayerTwoVertical");
        }
    }
    void FixedUpdate()
    {
        //Calculate movement speed
        if (isPlayerOne)
        {
            currentSpeed = playerSpeed;
        }
        else
        {
            currentSpeed = playerSpeed;
        }
        //Move the player
        rb.MovePosition(new Vector2(Mathf.Clamp((rb.position.x + movement.normalized.x * currentSpeed * Time.fixedDeltaTime), -8.388f, 8.388f),
            Mathf.Clamp((rb.position.y + movement.normalized.y * currentSpeed * Time.fixedDeltaTime), -4.487f, 4.487f)));

    }
}
/*public class PlayerMovement : MonoBehaviour
{
    public float currentSpeed = 5f; // Player speed
    private Rigidbody2D rb;
    private float playerWidth, playerHeight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Get the player's size (assuming it's a circle or square collider)
        playerWidth = GetComponent<Collider2D>().bounds.size.x;
        playerHeight = GetComponent<Collider2D>().bounds.size.y;
    }

    void FixedUpdate()
    {
        // Get player input
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        // Get the camera's width and height in world space
        Camera camera = Camera.main;
        float cameraHeight = camera.orthographicSize * 2; // Height in world units
        float cameraWidth = cameraHeight * camera.aspect; // Width in world units

        // Calculate boundaries by subtracting half of the player's size
        float xMin = -cameraWidth / 2 + playerWidth / 2;
        float xMax = cameraWidth / 2 - playerWidth / 2;
        float yMin = -cameraHeight / 2 + playerHeight / 2;
        float yMax = cameraHeight / 2 - playerHeight / 2;

        // Move the player and clamp their position within the boundaries
        Vector2 newPosition = rb.position + movement * currentSpeed * Time.fixedDeltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, xMin, xMax);
        newPosition.y = Mathf.Clamp(newPosition.y, yMin, yMax);

        rb.MovePosition(newPosition);
    }
}
*/