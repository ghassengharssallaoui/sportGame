using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static int posOne = 0, posTwo = 0;
    [SerializeField] bool isPlayerOne;
    [Range(0f, 20f)]
    public float playerSpeed = 15f;
    [Range(1f, 3f)]
    Rigidbody2D rb;
    Vector2 movement;
    GameManager gameManager;
    int playerTwoTouches, playerOneTouches = 0;
    public Text playerOnePossition, playerTwoPossition;

    // Timers for position updates
    float playerOneLastPositionUpdateTime = 0f;
    float playerTwoLastPositionUpdateTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (isPlayerOne)
        {
            movement.x = Input.GetAxisRaw("PlayerTwoHorizontal");
            movement.y = Input.GetAxisRaw("PlayerTwoVertical");
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(new Vector2(Mathf.Clamp((rb.position.x + movement.normalized.x * playerSpeed * Time.fixedDeltaTime), -8.388f, 8.388f),
            Mathf.Clamp((rb.position.y + movement.normalized.y * playerSpeed * Time.fixedDeltaTime), -4.487f, 4.487f)));
        playerOnePossition.text = "Possession: " + posOne;
        playerTwoPossition.text = "Possession: " + posTwo;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Player One's position update (with cooldown of 1 second)
            if (isPlayerOne && Time.time - playerOneLastPositionUpdateTime >= 0.1f)
            {
                playerOneLastPositionUpdateTime = Time.time; // Update last position update time

                posOne++;

            }
            // Player Two's position update (with cooldown of 1 second)
            if (!isPlayerOne && Time.time - playerTwoLastPositionUpdateTime >= 0.1f)
            {
                playerTwoLastPositionUpdateTime = Time.time; // Update last position update time
                posTwo++;
            }

            gameManager.decreaseBallVelocity = false;
        }
    }
}
