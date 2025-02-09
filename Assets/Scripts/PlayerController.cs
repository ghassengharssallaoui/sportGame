using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float randomizeInterval = 0.35f; // Time between direction changes
    private float timeSinceLastRandomMove = 0f;
    public bool canMove = true;
    public bool isHypnotised = false;
    public GameObject ball;
    private Vector2 previousPosition;
    [HideInInspector]
    public float playerSpeed = 15f;
    Rigidbody2D rb;
    Vector2 input;
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
    public float xBoundary = 10f;
    public float yBoundary = 4.35f;
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
        if (!canMove || isHypnotised) return;
        if (GameManager.Instance.CurrentState() != GameState.GamePlay) return;

        if (gameObject.tag == "PlayerOne")
        {
            input.x = Input.GetAxisRaw("PlayerOneHorizontal");
            input.y = Input.GetAxisRaw("PlayerOneVertical");

        }
        else
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
        }
    }

    public void RandomizeMovement()
    {
        if (!isHypnotised) return;
        // Update random input only after the interval
        if (timeSinceLastRandomMove >= randomizeInterval)
        {
            input.x = Random.Range(-1f, 1f);
            input.y = Random.Range(-1f, 1f);

            timeSinceLastRandomMove = 0f; // Reset timer
        }
        else
        {
            timeSinceLastRandomMove += Time.deltaTime; // Increment timer
        }
    }

    public void StopRandomMovement()
    {
        isHypnotised = false;
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
        float attackDefenseModifier = 1;

        if (isPlayerOne)
        {
            // PlayerOne applies strength when moving right (movement.x > 0)
            if (input.x > 0)
            {
                // Debug.Log("moving right" + TeamsManager.Instance.Teams[teamIndex].attack);
                attackDefenseModifier = TeamsManager.Instance.Teams[teamIndex].attack;
            }
            else if (input.x < 0)
            {
                //   Debug.Log("moving left" + TeamsManager.Instance.Teams[teamIndex].defense);
                attackDefenseModifier = TeamsManager.Instance.Teams[teamIndex].defense;
            }
        }
        else
        {
            // PlayerTwo applies strength when moving left (movement.x < 0)
            if (input.x < 0)
            {
                attackDefenseModifier = TeamsManager.Instance.Teams[teamIndex].attack;
            }
            else if (input.x > 0)
            {
                attackDefenseModifier = TeamsManager.Instance.Teams[teamIndex].defense;
            }
        }
        if (attackDefenseModifier != 1) attackDefenseModifier /= 7;



        Vector2 moveVector = input;

        // Only normalize if both horizontal and vertical speeds are non-zero
        if (rb.velocity.x != 0 && rb.velocity.y != 0)
        {
            moveVector = moveVector.normalized;
        }
        // Dynamically adjust boundaries based on size
        float baseSize = 1.3f;
        float baseXBoundary = 10f;
        float baseYBoundary = 4.35f;

        float sizeIncrease = gameObject.transform.localScale.x - baseSize;
        xBoundary = baseXBoundary - sizeIncrease / 2;
        yBoundary = baseYBoundary - sizeIncrease / 2;

        // Clamp the player's position within the adjusted boundaries
        Vector2 newPosition = new Vector2(
            Mathf.Clamp(rb.position.x + moveVector.x * adjustedSpeed * attackDefenseModifier, -xBoundary, xBoundary),
            Mathf.Clamp(rb.position.y + moveVector.y * adjustedSpeed * attackDefenseModifier, -yBoundary, yBoundary)
        );


        HandleStaminaAdjustment(previousPosition, newPosition, input.magnitude);
        if (canMove) rb.MovePosition(newPosition);
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
        ChangeStamina(amount);
    }

    public void ChangeStamina(float amount)
    {
        currentStamina += amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina); // Ensure health does not exceed maxHealth

        float staminaRatio = (float)currentStamina / maxStamina;
        staminaBar.transform.localScale = new Vector3(staminaRatio, 1f, 1f);
    }
}