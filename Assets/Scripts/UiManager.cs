using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Text statusText;
    [SerializeField] private float animationDuration;
    [SerializeField] private int animationStartFontSize = 24; // Initial font size
    [SerializeField] private int animationEndFontSize = 44;
    // bool gameEnened = false;

    // UI Text references for player stats
    [SerializeField] private Text playerOneGoalsText, playerOneOversText, playerOneBongsText, playerOneStarsText;
    [SerializeField] private Text playerTwoGoalsText, playerTwoOversText, playerTwoBongsText, playerTwoStarsText;
    [SerializeField] private Text playerOneScoreText, playerTwoScoreText;
    [SerializeField] private Text playerOneAnimationText, playerTwoAnimationText;
    [SerializeField] private Text gameResultText, timerText;
    [SerializeField]
    private Text playerOneTotalStars, playerOneTotalPossission, playerOneTotalSaves,
     playerTwoTotalStars, playerTwoTotalPossission, playerTwoTotalSaves;
    private bool showTime = true;


    private void OnEnable()
    {
        // Subscribe to events
        GameManager.Instance.OnGoalHit += UpdateGoalUI;
        GameManager.Instance.OnOverHit += UpdateOversUI;
        GameManager.Instance.OnBongHit += UpdateBongsUI;
        GameManager.Instance.OnStarsHit += UpdateStarsUI;
        GameManager.Instance.OnKeeperHit += UpdateKeeperStatistics;
        GameManager.Instance.OnPlayerHit += UpdatePlayerStatistics;
        GameManager.Instance.OnGameEnd += UpdateEndGameUis;
        GameManager.Instance.OnHalfTimeReached += UpdateHalfTimeUis;
        GameManager.Instance.OnHalfTimeEnded += UpdateAfterHalfTimeUis;
        GameManager.Instance.OnGameStart += UpdateStartUis;
        GameManager.Instance.OnGoldenGoalReached += UpdateGlodenGoalUis;
        GameManager.Instance.OnGoldenGoalStart += UpdateWhenGoldenGoalStartedTimeUis;



    }

    private void OnDisable()
    {
        // Unsubscribe when the object is destroyed or disabled
        GameManager.Instance.OnGoalHit -= UpdateGoalUI;
        GameManager.Instance.OnOverHit -= UpdateOversUI;
        GameManager.Instance.OnBongHit -= UpdateBongsUI;
        GameManager.Instance.OnStarsHit -= UpdateStarsUI;
        GameManager.Instance.OnKeeperHit += UpdateKeeperStatistics;
        GameManager.Instance.OnPlayerHit += UpdatePlayerStatistics;
        GameManager.Instance.OnGameEnd -= UpdateEndGameUis;
        GameManager.Instance.OnHalfTimeReached -= UpdateHalfTimeUis;
        GameManager.Instance.OnHalfTimeEnded -= UpdateAfterHalfTimeUis;
        GameManager.Instance.OnGameStart -= UpdateStartUis;
        GameManager.Instance.OnGoldenGoalReached -= UpdateGlodenGoalUis;
        GameManager.Instance.OnGoldenGoalStart -= UpdateWhenGoldenGoalStartedTimeUis;





    }
    private void UpdateGlodenGoalUis()
    {
        /*
        gameEnened = true;
        */
        gameResultText.text = "Tie! Press Space to Head for the Golden Goal!";
        showTime = false;
    }

    // Update scoring UI and animation for goals, overs, bongs, and stars

    // Scoring update and animation for goals
    private void UpdateGoalUI(int playerScored)
    {
        // Update the score UI for both players
        playerOneGoalsText.text = "" + ScoreManager.Instance.GetPlayerOneGoalHits();
        playerTwoGoalsText.text = "" + ScoreManager.Instance.GetPlayerTwoGoalHits();

        // Trigger animation for goals (6 points for goal)
        UpdateScoreAndAnimation(playerScored, 6, playerScored == 1 ? playerOneAnimationText : playerTwoAnimationText);
    }

    // Update overs UI and animation (3 points for overs)
    private void UpdateOversUI(int playerScored)
    {
        playerOneOversText.text = "" + ScoreManager.Instance.GetPlayerOneOverHits();
        playerTwoOversText.text = "" + ScoreManager.Instance.GetPlayerTwoOverHits();

        // Trigger animation for overs (3 points for overs)
        UpdateScoreAndAnimation(playerScored, 3, playerScored == 1 ? playerOneAnimationText : playerTwoAnimationText);

    }

    // Update bongs UI and animation (1 point for bongs)
    private void UpdateBongsUI(int playerScored, BongsController bong)
    {
        playerOneBongsText.text = "" + ScoreManager.Instance.GetPlayerOneBongHits();
        playerTwoBongsText.text = "" + ScoreManager.Instance.GetPlayerTwoBongHits();

        // Trigger animation for bongs (1 point for bongs)
        UpdateScoreAndAnimation(playerScored, 1, playerScored == 1 ? playerOneAnimationText : playerTwoAnimationText);

    }

    // Update stars UI and animation (2 points for stars)
    private void UpdateStarsUI(int playerScored, int index)
    {
        playerOneStarsText.text = "" + ScoreManager.Instance.GetPlayerOneStarHits();
        playerTwoStarsText.text = "" + ScoreManager.Instance.GetPlayerTwoStarHits();
        playerOneTotalStars.text = "StarHits ☆ \t\t      " + ScoreManager.Instance.GetPlayerOneTotalStarHits();
        playerTwoTotalStars.text = "StarHits ☆ \t\t      " + ScoreManager.Instance.GetPlayerTwoTotalStarHits();

        UpdateScoreAndAnimation(playerScored, 2, playerScored == 1 ? playerOneAnimationText : playerTwoAnimationText);
    }
    private void UpdateKeeperStatistics(int playerScored)
    {

        playerOneTotalPossission.text = "" + ScoreManager.Instance.GetPlayerOnePossession();
        playerTwoTotalPossission.text = "" + ScoreManager.Instance.GetPlayerTwoPossession();
        playerOneTotalSaves.text = "" + ScoreManager.Instance.GetPlayerOneSaves();
        playerTwoTotalSaves.text = "" + ScoreManager.Instance.GetPlayerTwoSaves();
    }
    private void UpdatePlayerStatistics(int playerScored)
    {

        playerOneTotalPossission.text = "" + ScoreManager.Instance.GetPlayerOnePossession();
        playerTwoTotalPossission.text = "" + ScoreManager.Instance.GetPlayerTwoPossession();
    }


    private void UpdateScoreAndAnimation(int playerScored, int points, Text playerAnimationText)
    {
        playerOneScoreText.text = "" + ScoreManager.Instance.GetPlayerOneScore();
        playerTwoScoreText.text = "" + ScoreManager.Instance.GetPlayerTwoScore();

        // Update the player's specific score UI
        playerAnimationText.text = points.ToString();

        // Trigger animation for the scored player with the corresponding points
        if (playerScored == 1)
        {
            StartCoroutine(AnimateFloatDisplay(points, playerAnimationText));
        }
        else if (playerScored == 2)
        {
            StartCoroutine(AnimateFloatDisplay(points, playerAnimationText));
        }
    }

    // Animate float display of scored points
    private IEnumerator AnimateFloatDisplay(float scoredPoints, Text animationText)
    {
        animationText.gameObject.SetActive(true);
        animationText.text = scoredPoints > 0 ? "+" + scoredPoints.ToString() : scoredPoints.ToString();
        animationText.fontSize = animationStartFontSize;

        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            animationText.fontSize = Mathf.RoundToInt(Mathf.Lerp(animationStartFontSize, animationEndFontSize, t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        animationText.fontSize = animationEndFontSize;
        yield return new WaitForSeconds(0.3f); // Optional delay before hiding
        animationText.gameObject.SetActive(false);
    }

    // Timer update method
    void UpdateTimerDisplay(float gameTime)
    {
        if (showTime)
        {
            float remainingTime = Mathf.Clamp(GameManager.Instance.GameDuration() - gameTime, 0, GameManager.Instance.GameDuration());
            // Ensure it doesn't go below 0
            // int minutes = Mathf.FloorToInt(remainingTime / 60);
            // int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = "" + Mathf.FloorToInt(remainingTime);
            // Update the additional public text based on gameTime
            if (gameTime < GameManager.Instance.GameDuration() / 2)
            {
                statusText.text = "P1"; // Replace 'statusText' with your public Text variable
            }
            else
            {
                statusText.text = "P2";
            }
        }
        else
        {
            timerText.text = "";
            statusText.text = "GP"; // Replace 'statusText' with your public Text variable
        }
    }

    private void UpdateHalfTimeUis()
    {
        gameResultText.text = "Half Time\n PRESS SPACE";
    }
    private void UpdateStartUis()
    {
        gameResultText.text = "";

    }
    private void UpdateAfterHalfTimeUis()
    {
        gameResultText.text = "";
    }
    private void UpdateWhenGoldenGoalStartedTimeUis()
    {
        gameResultText.text = "";
        timerText.text = "";
    }
    private void UpdateEndGameUis()
    {
        if (ScoreManager.Instance.GetPlayerOneScore() > ScoreManager.Instance.GetPlayerTwoScore())
        {
            gameResultText.text += "\n Player One Wins";

        }
        else if (ScoreManager.Instance.GetPlayerOneScore() < ScoreManager.Instance.GetPlayerTwoScore())
        {
            gameResultText.text += "\n Player Two Wins";

        }
    }
    private void Update()
    {
        UpdateTimerDisplay(GameManager.Instance.GameTime());
    }
}
