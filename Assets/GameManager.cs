using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int playerOneScore = 0;
    [SerializeField] int playerTwoScore = 0;
    [SerializeField] float ballInitialMovementSpeed = 1;

    [SerializeField] Camera cam;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    public GameObject ball;
    public Rigidbody2D ballRigidBody;
    float initialSpeed = 1f;
    Vector2 randomDirection;
    [SerializeField] Vector3 playerOneStartPos = new Vector3(7, 0, 0);
    [SerializeField] Vector3 playerTwoStartPos = new Vector3(-7, 0, 0);


    void Start()
    {
        ballRigidBody = ball.GetComponent<Rigidbody2D>();
        MoveBallInRandomDirection(ballInitialMovementSpeed);
    }
    public void Goal(bool isPlayerOne)
    {
        UpdateScore(isPlayerOne);
        ResetPosition();
        StartCoroutine(ShakeCamera(0.4f, 0.08f));
        MoveBallInRandomDirection(ballInitialMovementSpeed);
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
    void UpdateScore(bool isPlayerOne)
    {
        if (isPlayerOne)
        {
            playerTwoScore += 3;
            Debug.Log("Player Two Score: " + playerTwoScore);
        }
        else
        {
            playerOneScore += 3;
            Debug.Log("Player One Score: " + playerOneScore);
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
}
