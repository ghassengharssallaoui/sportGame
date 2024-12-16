using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public float playerSpeed = 15f;
    Rigidbody2D rb;
    Vector2 movement;
    [SerializeField] Vector2 playerOneStartPos = new Vector2(7, 0);
    [SerializeField] Vector2 playerTwoStartPos = new Vector2(-7, 0);
    private void OnEnable()
    {
        GameManager.Instance.OnGoalHit += ResetPosition;
        GameManager.Instance.OnOverHit += ResetPosition;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGoalHit -= ResetPosition;
        GameManager.Instance.OnOverHit += ResetPosition;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (gameObject.tag == "PlayerOne")
        {
            movement.x = Input.GetAxisRaw("PlayerOneHorizontal");
            movement.y = Input.GetAxisRaw("PlayerOneVertical");
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(new Vector2(Mathf.Clamp((rb.position.x + movement.normalized.x * playerSpeed * Time.fixedDeltaTime), -10.17f, 10.17f),
            Mathf.Clamp((rb.position.y + movement.normalized.y * playerSpeed * Time.fixedDeltaTime), -4.487f, 4.487f)));

    }

    void ResetPosition(int playerScored)
    {
        if (gameObject.tag == "PlayerOne")
            transform.position = playerOneStartPos;
        if (gameObject.tag == "PlayerTwo")
            transform.position = playerTwoStartPos;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            int player = gameObject.CompareTag("PlayerOne") ? 1 : 2;
            // Notify the GameManager
            GameManager.Instance.NotifyPlayerHit(player);
        }
    }
}