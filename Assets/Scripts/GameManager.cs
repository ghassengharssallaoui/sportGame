using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{


    [SerializeField] float gameDuration = 180f;

    [SerializeField] float ballInitialMovementSpeed = 1;
    [Range(0f, 1f)]
    [SerializeField] float velocityMultiplierOnImpact = 0.95f;
    [SerializeField] float animationDuration;
    [SerializeField] Vector2 playerOneStartPos = new Vector2(7, 0);
    [SerializeField] Vector2 playerTwoStartPos = new Vector2(-7, 0);
    public bool decreaseBallVelocity;


    [SerializeField] private int startFontSize = 24; // Initial font size
    [SerializeField] private int endFontSize = 44;


    [SerializeField]
    private Text playerOneGoalsText, playerOneOversText, playerOneBongsText, playerOneStarsText, playerTwoGoalsText, playerTwoOversText, timerText,
     playerTwoBongsText, playerTwoStarsText;
    private bool isPaused = false;

    [SerializeField] SpriteRenderer[] PlayerOneStars;
    [SerializeField] SpriteRenderer[] PlayerTwoStars;

    [SerializeField] Sprite litStar;
    [SerializeField] Sprite unlitStar;

    [SerializeField] Text PlayerOneScoreText;
    [SerializeField] Text PlayerTwoScoreText;
    [SerializeField] Text playerOneAnimationText;
    [SerializeField] Text playerTwoAnimationText;
    private int playerOneLitStarsNumber = 0;
    private int playerTwoLitStarsNumber = 0;
    private int playerOneGoals, playerOneOvers, playerOneBongs, playerOneStars, playerTwoGoals, playerTwoOvers, playerTwoBongs, playerTwoStars;



    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;

    public GameObject ball;
    public Rigidbody2D ballRigidBody;
    Vector2 randomDirection;
    private int playerOneScore = 0;
    private int playerTwoScore = 0;
    private float gameTime = 0f; // Tracks elapsed game time
    private bool gameActive = true; // Determines if the game is active


    void Start()
    {
        ballRigidBody = ball.GetComponent<Rigidbody2D>();
        MoveBallInRandomDirection(ballInitialMovementSpeed);
    }
    public void Goal(bool isPlayerOne, int points, Vector2 ballRestPostion, bool isBallMoving)
    {
        if (points == 6)
        {
            if (isPlayerOne)
                playerOneGoals++;
            else
                playerTwoGoals++;
        }
        if (points == 3)
        {
            if (isPlayerOne)
                playerOneOvers++;
            else
                playerTwoOvers++;
        }
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
            {
                playerOneLitStarsNumber--;
                playerOneStars--;
            }
            else
            {
                playerTwoLitStarsNumber--;
                playerTwoStars--;
            }
        }
        else
        {
            starHit.GetComponent<SpriteRenderer>().sprite = litStar;
            UpdateScore(isPlayerOneStar, 2);
            if (isPlayerOneStar)
            {
                playerOneLitStarsNumber++;


                playerOneStars++;




                // Debug.Log("playerOneLitStarsNumber" + playerOneLitStarsNumber);
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

                playerTwoStars++;
                //  Debug.Log("playerTwoLitStarsNumber" + playerTwoLitStarsNumber);
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

        if (isPlayerOne)
            playerOneBongs++;
        else
            playerTwoBongs++;

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
    void Update()
    {

        playerOneGoalsText.text = "Goals \n" + playerOneGoals;
        playerTwoGoalsText.text = "Goals \n" + playerTwoGoals;
        playerOneOversText.text = "Overs \n" + playerOneOvers;
        playerTwoOversText.text = "Overs \n" + playerTwoOvers;
        playerOneBongsText.text = "Bongs \n" + playerOneBongs;
        playerTwoBongsText.text = "Bongs \n" + playerTwoBongs;
        playerOneStarsText.text = "Stars \n" + playerOneStars;
        playerTwoStarsText.text = "Stars \n" + playerTwoStars;

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetBallToCenter();
        }

        // Check if the 'P' key is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
        if (gameActive)
        {
            gameTime += Time.deltaTime;

            // Stop the game at 3 minutes
            if (gameTime >= gameDuration)
            {
                EndGame();
            }
            else

                UpdateTimerDisplay();
        }
    }
    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(gameTime / 60); // Calculate minutes
        int seconds = Mathf.FloorToInt(gameTime % 60); // Calculate seconds
        timerText.text = $"{minutes:D2}:{seconds:D2}"; // Format as MM:SS
    }

    void ResetBallToCenter()
    {
        // Reset the ball to the middle of the play area
        ball.transform.position = Vector2.zero;
        ballRigidBody.velocity = Vector2.zero;

        // Optionally, re-initiate ball movement
        MoveBallInRandomDirection(ballInitialMovementSpeed);
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
    void EndGame()
    {
        gameActive = false; // Stop the timer updates
        timerText.text = "Game Over! Time is up!";
        if (playerOneScore > playerTwoScore)
        {
            timerText.text += "\n Player One Wins";

        }
        else if (playerOneScore < playerTwoScore)
        {
            timerText.text += "\n Player Two Wins";

        }
        else
        {
            timerText.text += "\n Tie";
        }
        Time.timeScale = 0f; // Freeze the game
        Debug.Log("Game Over! Time is up!");
        // Optionally, trigger an end-game screen or message here
    }

}
