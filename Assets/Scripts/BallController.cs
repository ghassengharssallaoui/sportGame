using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [HideInInspector]
    public float velocityMultiplierOnImpact = 0.95f;
    [HideInInspector]
    public float ballRandomMovementSpeed = 1;
    [HideInInspector]
    public float defaultBallSpeed = 2.0f; // Default ball speed
    [HideInInspector]
    public float impactForce = 1.5f; // New variable for additional force on impact

    private Vector2 randomDirection;
    public Rigidbody2D ballRigidbody;
    private bool decreaseBallVelocity;
    private bool applyImpactForce; // New flag to track if impact force needs to be applied

    [SerializeField] Vector2 PlayerOneOverRestPosition = new Vector2(-7.3f, 1.5f);
    [SerializeField] Vector2 PlayerTwoOverRestPosition = new Vector2(7.3f, 1.5f);

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += GameStart;
        GameManager.Instance.OnGoalHit += GoalScored;
        GameManager.Instance.OnOverHit += OverScored;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= GameStart;
        GameManager.Instance.OnGoalHit -= GoalScored;
        GameManager.Instance.OnOverHit -= OverScored;
    }

    private void OverScored(int playerScored)
    {
        if (playerScored == 1)
        {
            transform.position = PlayerTwoOverRestPosition;
        }
        else if (playerScored == 2)
        {
            transform.position = PlayerOneOverRestPosition;
        }
        ballRigidbody.velocity = Vector2.zero;
    }

    private void GameStart()
    {
        MoveBallInRandomDirection();
    }

    private void GoalScored(int player)
    {
        ResetBallToCenter();
        ballRigidbody.velocity = Vector2.zero;
        MoveBallInRandomDirection();
    }

    void MoveBallInRandomDirection()
    {
        randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
        ballRigidbody.velocity = randomDirection * ballRandomMovementSpeed * defaultBallSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetBallToCenter();
        }
    }

    void ResetBallToCenter()
    {
        transform.position = Vector2.zero;
        ballRigidbody.velocity = Vector2.zero;
    }

    void FixedUpdate()
    {
        if (decreaseBallVelocity)
        {
            if (ballRigidbody.velocity != Vector2.zero)
            {
                if (applyImpactForce)
                {
                    ballRigidbody.velocity += ballRigidbody.velocity.normalized * impactForce;
                    applyImpactForce = false; // Ensure the force is applied only once
                }

                ballRigidbody.velocity *= velocityMultiplierOnImpact; // Gradual decrease
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Keeper") || collision.gameObject.name.Contains("Bong"))
        {
            decreaseBallVelocity = true;
            applyImpactForce = true; // Ready to apply impact force
        }
        else if (collision.gameObject.name == "Player One" || collision.gameObject.name == "Player Two")
        {
            decreaseBallVelocity = false;
            applyImpactForce = false; // Reset to prevent unintended behavior

            // Calculate new velocity after collision with player
            Vector2 playerVelocity = collision.rigidbody.velocity; // Player's current velocity
            Vector2 newDirection = ballRigidbody.velocity.normalized + playerVelocity.normalized;
            //newDirection.Normalize(); // Ensure the direction is normalized

            // Set the ball's velocity
            float newSpeed = Mathf.Max(defaultBallSpeed, playerVelocity.magnitude);
            ballRigidbody.velocity = newDirection * defaultBallSpeed;
        }
    }

}