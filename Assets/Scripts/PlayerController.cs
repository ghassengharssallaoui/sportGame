using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] bool isPlayerOne;
    [Range(0f, 20f)]
    public float playerSpeed = 15f;
    [Range(1f, 3f)]
    Rigidbody2D rb;
    Vector2 movement;
    GameManager gameManager;
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

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {

            gameManager.decreaseBallVelocity = false;

        }
    }
}