using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BongsController : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] bool isPlayerOneBong;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {

            gameManager.decreaseBallVelocity = true;
            gameManager.BongsGoal(isPlayerOneBong, this.gameObject);
        }
    }
}



/*












public class BongsController : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] bool isPlayerOneBong;
    [SerializeField] float decelerationForce = 5f; // The amount of force to apply to slow down the ball

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (ballRb != null)
            {
                ballRb.velocity /= 8;
            }

            // Notify the GameManager about the goal
            gameManager.BongsGoal(isPlayerOneBong, this.gameObject);
        }
    }
}


























using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BongsController : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] bool isPlayerOneBong;
    [SerializeField] float velocityDecayFactor = 0.5f; // How fast the ball's velocity decays
    [SerializeField] float minSpeed = 0.5f; // Minimum speed the ball can reach after deceleration

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Reduce velocity by applying a decay factor to the ball's Rigidbody2D velocity
            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (ballRb != null)
            {
                StartCoroutine(ReduceBallVelocity(ballRb));
            }

            // Notify the GameManager about the goal
            gameManager.BongsGoal(isPlayerOneBong, this.gameObject);
        }
    }

    private IEnumerator ReduceBallVelocity(Rigidbody2D ballRb)
    {
        // Gradually reduce the velocity over time
        while (ballRb.velocity.magnitude > minSpeed)
        {
            ballRb.velocity *= velocityDecayFactor;
            yield return new WaitForSeconds(0.1f); // Adjust this for smoother or faster deceleration
        }

        // Stop the ball completely after deceleration
        ballRb.velocity = Vector2.zero;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BongsController : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] bool isPlayerOneBong;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            gameManager.BongsGoal(isPlayerOneBong, this.gameObject);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BongsController : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] bool isPlayerOneStar;
    [SerializeField] float speedMultiplier = 1.2f; // Factor to increase speed
    [SerializeField] float bounceFriction = 0.8f;  // Factor to add friction to the bounce

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is the ball
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (ballRb != null)
            {
                // Increase the ball's speed
                Vector2 newVelocity = ballRb.velocity * speedMultiplier;

                // Apply friction effect
                newVelocity = new Vector2(
                    newVelocity.x * (1 - bounceFriction),
                    newVelocity.y * (1 - bounceFriction)
                );

                ballRb.velocity = newVelocity;
            }
        }

        gameManager.BongsGoal(isPlayerOneStar, this.gameObject);
    }
}
*/