using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Sprite randoimage;
    [SerializeField] SpriteRenderer[] PlayerOneStars;
    [SerializeField] SpriteRenderer[] PlayerTwoStars;
    private int playerOneLitStarsNumber = 0;
    private int playerTwoLitStarsNumber = 0;
    [SerializeField] Sprite litStar;
    [SerializeField] Sprite unlitStar;
    public bool decreaseBallVelocity;
    [SerializeField] Text PlayerOneScoreText;
    [SerializeField] Text PlayerTwoScoreText;
    [SerializeField] Text playerOneAnimationText;
    [SerializeField] Text playerTwoAnimationText;

    [SerializeField] float ballInitialMovementSpeed = 1;
    [Range(0f, 1f)]
    [SerializeField] float velocityMultiplierOnImpact = 0.95f;
    [SerializeField] float animationDuration;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] Vector2 playerOneStartPos = new Vector2(7, 0);
    [SerializeField] Vector2 playerTwoStartPos = new Vector2(-7, 0);
    public GameObject ball;
    public Rigidbody2D ballRigidBody;
    Vector2 randomDirection;
    [SerializeField] private int startFontSize = 24; // Initial font size
    [SerializeField] private int endFontSize = 44;
    private int playerOneScore = 0;
    private int playerTwoScore = 0;


    void Start()
    {
        ballRigidBody = ball.GetComponent<Rigidbody2D>();
        MoveBallInRandomDirection(ballInitialMovementSpeed);
    }
    public void Goal(bool isPlayerOne, int points, Vector2 ballRestPostion, bool isBallMoving)
    {
        UpdateScore(isPlayerOne, points);
        ResetPosition(ballRestPostion);
        StartCoroutine(ShakeCamera(0.4f, 0.08f));
        if (isBallMoving)
            MoveBallInRandomDirection(ballInitialMovementSpeed);
    }

    public void StarHit(bool isPlayerOneStar, GameObject starHit)
    {
        if (starHit.GetComponent<SpriteRenderer>().sprite == litStar)
        {
            starHit.GetComponent<SpriteRenderer>().sprite = unlitStar;
            UpdateScore(isPlayerOneStar, -2);
            if (isPlayerOneStar)
                playerOneLitStarsNumber--;
            else
                playerTwoLitStarsNumber--;

        }
        else
        {
            starHit.GetComponent<SpriteRenderer>().sprite = litStar;
            UpdateScore(isPlayerOneStar, 2);
            if (isPlayerOneStar)
            {
                playerOneLitStarsNumber++;
                Debug.Log("playerOneLitStarsNumber" + playerOneLitStarsNumber);
                if (playerOneLitStarsNumber % 5 == 0)
                {
                    foreach (SpriteRenderer sprite in PlayerTwoStars)
                    {
                        sprite.sprite = unlitStar;
                    }
                }
            }
            else
            {
                playerTwoLitStarsNumber++;
                Debug.Log("playerTwoLitStarsNumber" + playerTwoLitStarsNumber);
                if (playerTwoLitStarsNumber % 5 == 0)
                {
                    foreach (SpriteRenderer sprite in PlayerOneStars)
                    {

                        sprite.sprite = unlitStar;
                    }
                }
            }
        }
    }
    public void BongsGoal(bool isPlayerOne, GameObject bongHit)
    {
        UpdateScore(isPlayerOne, 1);
        StartCoroutine(DeactivateForSeconds(1f, bongHit));
    }
    private IEnumerator ShakeCamera(float duration, float magnitude)
    {
        Vector3 originalPosition = mainCamera.transform.localPosition;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            mainCamera.transform.localPosition = originalPosition + new Vector3(x, y);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.localPosition = originalPosition;
    }
    void UpdateScore(bool isPlayerOne, int points)
    {
        if (isPlayerOne)
        {
            playerTwoScore += points;
            PlayerTwoScoreText.text = ("Score Player 2 : " + playerTwoScore);
            StartCoroutine(AnimateFloatDisplay(points, playerTwoAnimationText));
        }
        else
        {
            playerOneScore += points;
            PlayerOneScoreText.text = ("Score Player 1 : " + playerOneScore);
            StartCoroutine(AnimateFloatDisplay(points, playerOneAnimationText));
        }
    }
    void ResetPosition(Vector2 ballRestesrestPostion)
    {
        player1.transform.position = playerOneStartPos;
        player2.transform.position = playerTwoStartPos;
        ball.transform.position = ballRestesrestPostion;
        ballRigidBody.velocity = Vector2.zero;
    }
    void MoveBallInRandomDirection(float movementSpeed)
    {
        randomDirection = Random.insideUnitCircle.normalized;
        ballRigidBody.velocity = randomDirection * movementSpeed;
    }
    private IEnumerator DeactivateForSeconds(float seconds, GameObject bongHit)
    {
        // Deactivate the game object
        bongHit.SetActive(false);
        yield return new WaitForSeconds(seconds);
        bongHit.SetActive(true);
    }
    private IEnumerator AnimateFloatDisplay(float value, Text floatText)
    {
        floatText.gameObject.SetActive(true);
        if (value > 0)
            floatText.text = "+" + value.ToString(); // Display the float value
        else
            floatText.text = value.ToString();
        floatText.fontSize = startFontSize; // Reset font size to start size

        float elapsedTime = 0f;

        // Animate font size growth
        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            floatText.fontSize = Mathf.RoundToInt(Mathf.Lerp(startFontSize, endFontSize, t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        floatText.fontSize = endFontSize; // Ensure final font size is set
        yield return new WaitForSeconds(0.3f); // Optional delay before hiding
        floatText.gameObject.SetActive(false); // Deactivate the text GameObject

    }
    void FixedUpdate()
    {
        if (decreaseBallVelocity)
        {
            if (ballRigidBody.velocity != Vector2.zero)
            {
                ballRigidBody.velocity *= velocityMultiplierOnImpact;
            }
        }
    }
}
