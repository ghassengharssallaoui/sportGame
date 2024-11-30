using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] Text PlayerOneScoreText;
    [SerializeField] Text PlayerTwoScoreText;
    [SerializeField] float ballInitialMovementSpeed = 1;
    [SerializeField] Camera cam;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject playerOneStar;
    [SerializeField] GameObject playerTwoStar;
    public GameObject ball;
    public Rigidbody2D ballRigidBody;
    float initialSpeed = 1f;
    Vector2 randomDirection;
    [SerializeField] Vector3 playerOneStartPos = new Vector3(7, 0, 0);
    [SerializeField] Vector3 playerTwoStartPos = new Vector3(-7, 0, 0);
    private int playerOneScore = 0;
    private int playerTwoScore = 0;


    void Start()
    {
        ballRigidBody = ball.GetComponent<Rigidbody2D>();
        MoveBallInRandomDirection(ballInitialMovementSpeed);
    }
    public void Goal(bool isPlayerOne, int points)
    {
        UpdateScore(isPlayerOne, points);
        ResetPosition();
        StartCoroutine(ShakeCamera(0.4f, 0.08f));
        MoveBallInRandomDirection(ballInitialMovementSpeed);
    }
    public void StarGoal(bool isPlayerOne)
    {
        UpdateScore(isPlayerOne, 1);
        StartCoroutine(DeactivateForSeconds(1f, isPlayerOne));
    }
    private IEnumerator ShakeCamera(float duration, float magnitude)
    {
        Vector3 originalPosition = cam.transform.localPosition;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            cam.transform.localPosition = originalPosition + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cam.transform.localPosition = originalPosition;
    }
    void UpdateScore(bool isPlayerOne, int points)
    {
        if (isPlayerOne)
        {
            playerTwoScore += points;
            PlayerTwoScoreText.text = ("Score Player 2 : " + playerTwoScore);
        }
        else
        {
            playerOneScore += points;
            PlayerOneScoreText.text = ("Score Player 1 : " + playerOneScore);
        }

    }
    void ResetPosition()
    {
        player1.transform.position = playerOneStartPos;
        player2.transform.position = playerTwoStartPos;
        ball.transform.position = Vector3.zero;
        ballRigidBody.velocity = Vector3.zero;
    }
    void MoveBallInRandomDirection(float movementSpeed)
    {
        randomDirection = Random.insideUnitCircle.normalized;
        ballRigidBody.velocity = randomDirection * movementSpeed;
    }
    private IEnumerator DeactivateForSeconds(float seconds, bool isPlayerOne)
    {
        // Deactivate the game object
        if (isPlayerOne)
            playerOneStar.SetActive(false);
        else
            playerTwoStar.SetActive(false);
        // Wait for the specified duration
        yield return new WaitForSeconds(seconds);
        if (isPlayerOne)
            playerOneStar.SetActive(true);
        else
            playerTwoStar.SetActive(true);
    }
}
