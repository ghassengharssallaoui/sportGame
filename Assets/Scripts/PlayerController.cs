using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Sprite[] skins;

    [SerializeField]
    private SpriteRenderer playerOne, playerTwo;


    [HideInInspector]
    public float playerSpeed = 15f;
    Rigidbody2D rb;
    Vector2 movement;
    [SerializeField] Vector2 playerOneStartPos = new Vector2(7, 0);
    [SerializeField] Vector2 playerTwoStartPos = new Vector2(-7, 0);
    private int maxStamina = 1;
    private float currentStamina;
    [SerializeField]
    private GameObject staminaBar;
    [SerializeField]
    [Range(0f, 0.01f)]
    float staminaRecoveryRate = 0.003f, staminaDrainOnBallHit = 0.005f;
    [SerializeField]
    [Range(0f, 0.1f)]
    float staminaDrainOnMovement = 0.01f;
    [SerializeField]
    [Range(0f, 1f)]
    float staminaRecoveryOnGoldenGoal = 0.15f, staminaRecoveryOnHalfTime = 0.3f;
    private float xBoundary = 10f;
    private float yBoundary = 4.35f;
    private void OnEnable()
    {
        GameManager.Instance.OnGoalHit += ResetPosition;
        GameManager.Instance.OnOverHit += ResetPosition;
        GameManager.Instance.OnHalfTimeEnded += HalfTime;
        GameManager.Instance.OnGoldenGoalStart += GoldenGoal;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGoalHit -= ResetPosition;
        GameManager.Instance.OnOverHit += ResetPosition;
        GameManager.Instance.OnHalfTimeEnded -= HalfTime;
        GameManager.Instance.OnGoldenGoalStart += GoldenGoal;

    }
    void Start()
    {
        currentStamina = maxStamina;
        rb = GetComponent<Rigidbody2D>();
        playerOne.sprite = skins[SkinManager.PlayerOneSkinIndex];
        playerTwo.sprite = skins[SkinManager.PlayerTwoSkinIndex];
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
    private void HalfTime()
    {
        AdjustStamina(staminaRecoveryOnHalfTime);
    }
    private void GoldenGoal()
    {
        AdjustStamina(staminaRecoveryOnGoldenGoal);
    }
    private void FixedUpdate()
    {
        if (Time.timeScale == 0) return; // Skip logic if the game is paused

        Vector2 previousPosition = rb.position;
        Vector2 newPosition = new Vector2(
            Mathf.Clamp(rb.position.x + movement.normalized.x * playerSpeed * Time.fixedDeltaTime * currentStamina, -xBoundary, xBoundary),
            Mathf.Clamp(rb.position.y + movement.normalized.y * playerSpeed * Time.fixedDeltaTime * currentStamina, -yBoundary, yBoundary)
        );

        // Check if movement occurs
        if (previousPosition != newPosition)
        {
            AdjustStamina(-staminaDrainOnMovement * movement.magnitude * Time.fixedDeltaTime); // Adjust damage value as needed
        }
        else
        {
            AdjustStamina(staminaRecoveryRate * Time.fixedDeltaTime); // Adjust health restoration rate as needed
        }

        rb.MovePosition(newPosition);
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
            AdjustStamina(-staminaDrainOnBallHit);
            int player = gameObject.CompareTag("PlayerOne") ? 1 : 2;
            // Notify the GameManager
            GameManager.Instance.NotifyPlayerHit(player);
        }
    }
    private void AdjustStamina(float amount)
    {
        currentStamina += amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina); // Ensure health does not exceed maxHealth

        float staminaRatio = (float)currentStamina / maxStamina;
        staminaBar.transform.localScale = new Vector3(staminaRatio, 1f, 1f);
    }
}