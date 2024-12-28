using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Vector2 previousPosition;
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

        previousPosition = rb.position;

        // Handle movement based on player tag
        if (tag == "PlayerOne")
        {
            MovePlayer(TeamsManager.Instance.PlayerOneIndex, isPlayerOne: true);
        }
        else if (tag == "PlayerTwo")
        {
            MovePlayer(TeamsManager.Instance.PlayerTwoIndex, isPlayerOne: false);
        }
    }

    // Local method to handle player movement
    void MovePlayer(int teamIndex, bool isPlayerOne)
    {
        float adjustedSpeed = playerSpeed * TeamsManager.Instance.Teams[teamIndex].speed / 5 * Time.fixedDeltaTime * currentStamina;

        // Determine the strengthMultiplier based on the player and direction
        float strengthMultiplier = 1;

        if (isPlayerOne)
        {
            // PlayerOne applies strength when moving right (movement.x > 0)
            if (movement.x > 0)
            {
                // Debug.Log("moving right" + TeamsManager.Instance.Teams[teamIndex].attack);
                strengthMultiplier = TeamsManager.Instance.Teams[teamIndex].attack;
            }
            else if (movement.x < 0)
            {
                //   Debug.Log("moving left" + TeamsManager.Instance.Teams[teamIndex].defense);
                strengthMultiplier = TeamsManager.Instance.Teams[teamIndex].defense;
            }
        }
        else
        {
            // PlayerTwo applies strength when moving left (movement.x < 0)
            if (movement.x < 0)
            {
                strengthMultiplier = TeamsManager.Instance.Teams[teamIndex].attack;
            }
            else if (movement.x > 0)
            {
                strengthMultiplier = TeamsManager.Instance.Teams[teamIndex].defense;
            }
        }
        strengthMultiplier /= 5;
        Vector2 newPosition = new Vector2(
            Mathf.Clamp(rb.position.x + movement.normalized.x * adjustedSpeed * strengthMultiplier, -xBoundary, xBoundary),
            Mathf.Clamp(rb.position.y + movement.normalized.y * adjustedSpeed * strengthMultiplier, -yBoundary, yBoundary)
        );

        HandleStaminaAdjustment(previousPosition, newPosition, movement.magnitude);
        rb.MovePosition(newPosition);
    }

    // Local method to adjust stamina based on movement
    void HandleStaminaAdjustment(Vector2 previous, Vector2 current, float movementMagnitude)
    {


        if (previous != current)
        {
            AdjustStamina(-staminaDrainOnMovement * movementMagnitude * Time.fixedDeltaTime);
        }
        else
        {
            AdjustStamina(staminaRecoveryRate * Time.fixedDeltaTime);
        }

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
        if (tag == "PlayerOne")
        {
            if (amount > 0)
            {
                amount *= (TeamsManager.Instance.Teams[TeamsManager.Instance.PlayerOneIndex].durability / 5);
            }
            else if (amount < 0)
            {
                amount /= (TeamsManager.Instance.Teams[TeamsManager.Instance.PlayerOneIndex].durability / 5);
            }
        }
        else
        {
            if (amount > 0)
            {
                amount *= (TeamsManager.Instance.Teams[TeamsManager.Instance.PlayerTwoIndex].durability / 5);
            }
            else if (amount < 0)
            {
                amount /= (TeamsManager.Instance.Teams[TeamsManager.Instance.PlayerTwoIndex].durability / 5);
            }
        }
        currentStamina += amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina); // Ensure health does not exceed maxHealth

        float staminaRatio = (float)currentStamina / maxStamina;
        staminaBar.transform.localScale = new Vector3(staminaRatio, 1f, 1f);
    }
}