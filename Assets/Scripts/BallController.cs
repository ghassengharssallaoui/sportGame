using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    bool collidedWithPlayerOneKeeper = true;
    [SerializeField]
    [Range(0f, 1f)]
    // float ballDragAfterWallHit = 0.5f;
    [HideInInspector]
    public float velocityMultiplierOnImpact = 0.95f;
    [HideInInspector]
    public float ballRandomMovementSpeed = 1;
    [HideInInspector]
    public float defaultBallSpeed = 2.0f, impactWithStars = 2.0f, impactWithWalls = 2.0f;
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
        GameManager.Instance.OnHalfTimeReached += ResetBallToCenter;
        GameManager.Instance.OnHalfTimeEnded += MoveBallInRandomDirection;

        GameManager.Instance.OnGoldenGoalStart += ResetBallToCenter;
        GameManager.Instance.OnGoldenGoalStart += MoveBallInRandomDirection;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= GameStart;
        GameManager.Instance.OnGoalHit -= GoalScored;
        GameManager.Instance.OnOverHit -= OverScored;
        GameManager.Instance.OnHalfTimeReached -= ResetBallToCenter;
        GameManager.Instance.OnHalfTimeEnded -= MoveBallInRandomDirection;
        GameManager.Instance.OnGoldenGoalStart -= ResetBallToCenter; ;
        GameManager.Instance.OnGoldenGoalStart -= MoveBallInRandomDirectionHorizontally;
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
    void MoveBallInRandomDirectionHorizontally()
    {
        float angle;

        // Randomly pick an angle from the desired ranges
        float rangeSelector = UnityEngine.Random.value;

        if (rangeSelector > 0.66f)
        {
            // First range: 315° to 360° (or 0°)
            angle = UnityEngine.Random.Range(315f, 360f);
        }
        else if (rangeSelector > 0.33f)
        {
            // Second range: 0° to 45°
            angle = UnityEngine.Random.Range(0f, 45f);
        }
        else
        {
            // Third range: 135° to 225°
            angle = UnityEngine.Random.Range(135f, 225f);
        }

        // Convert the angle to a direction vector
        Vector2 randomDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;

        // Set the ball's velocity
        ballRigidbody.velocity = randomDirection * ballRandomMovementSpeed * defaultBallSpeed;
    }




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetBallToCenter();
            MoveBallInRandomDirection();
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
                    if (collidedWithPlayerOneKeeper)
                        ballRigidbody.velocity += ballRigidbody.velocity.normalized * impactForce * TeamsManager.Instance.Teams[TeamsManager.Instance.PlayerOneIndex].defense / 5;
                    else
                        ballRigidbody.velocity += ballRigidbody.velocity.normalized * impactForce * TeamsManager.Instance.Teams[TeamsManager.Instance.PlayerTwoIndex].defense / 5;
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
            if (collision.gameObject.name.Contains("Keeper"))
            {
                if (collision.gameObject.tag == "PlayerOne")
                    collidedWithPlayerOneKeeper = true;
                else
                    collidedWithPlayerOneKeeper = false;
                decreaseBallVelocity = true;
                applyImpactForce = true; // Ready to apply impact force
            }
        }
        else if (collision.gameObject.name == "Player One")
        {


            decreaseBallVelocity = false;
            applyImpactForce = false;

            Vector2 newDirection = ballRigidbody.velocity.normalized;
            float attackMultiplier = Vector2.Dot(newDirection, Vector2.right) > 0
                ? TeamsManager.Instance.Teams[TeamsManager.Instance.PlayerOneIndex].attack / 5
                : 1;

            ballRigidbody.velocity = newDirection * defaultBallSpeed * attackMultiplier * TeamsManager.Instance.Teams[TeamsManager.Instance.PlayerOneIndex].strength / 5;

        }
        else if (collision.gameObject.name == "Player Two")
        {


            decreaseBallVelocity = false;
            applyImpactForce = false;

            Vector2 newDirection = ballRigidbody.velocity.normalized;
            float attackMultiplier = Vector2.Dot(newDirection, Vector2.left) > 0
                ? TeamsManager.Instance.Teams[TeamsManager.Instance.PlayerTwoIndex].attack / 5
                : 1;

            ballRigidbody.velocity = newDirection * defaultBallSpeed * attackMultiplier * TeamsManager.Instance.Teams[TeamsManager.Instance.PlayerTwoIndex].strength / 5;


        }
        else if (collision.gameObject.name.Contains("Star"))
        {
            Vector2 newDirection = ballRigidbody.velocity.normalized;
            ballRigidbody.velocity = newDirection * impactWithStars;
        }
        else if (collision.gameObject.name.Contains("Walls"))
        {
            // ballRigidbody.drag = ballDragAfterWallHit;
            //Vector2 newDirection = ballRigidbody.velocity.normalized;
            // ballRigidbody.velocity = newDirection * defaultBallSpeed;
        }
    }




}
