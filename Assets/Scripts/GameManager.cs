using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required for SceneManager

public class GameManager : MonoBehaviour
{
    [SerializeField] Text resultText;

    [SerializeField]
    SlidersController1 slidersController;

    public event Action<int> OnGoalHit;
    public event Action<int> OnOverHit;
    public event Action<int, BongsController> OnBongHit;
    public event Action<int, int> OnStarsHit;
    public event Action<int> OnKeeperHit;
    public event Action<int> OnPlayerHit;
    public event Action OnGameStart;
    public event Action<bool> OnGameEnd;
    public event Action OnGoldenGoal;
    public event Action OnHalfTimeReached;
    private bool isHalfTimeReached = false; // Tracks if halftime logic has been executed
    private bool isInHalftime = false; // Tracks if the game is in halftime
    public event Action OnHalfTimeEnded;




    public static GameManager Instance;

    [SerializeField] private float gameDuration = 180f;
    private float gameTime = 0f;
    public bool isPaused = false;
    public bool isGameStarted = false;
    private bool isGameEnded = false; // Track if the game has ended
    private bool isGoldenGoalActive = false;

    [SerializeField] private StarController[] playerOneStars; // Stars belonging to Player 1
    [SerializeField] private StarController[] playerTwoStars; // Stars belonging to Player 2
    public float GameTime() => gameTime;
    public float GameDuration() => gameDuration;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);  // Ensure only one instance exists.
        }
    }

    private void Start()
    {
        slidersController.LoadSettings();
        Time.timeScale = 0f; // Pause the game at the start.
    }

    private void Update()
    {
        if (isGoldenGoalActive && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            resultText.text = "";
        }
        if (gameTime >= gameDuration && !isGoldenGoalActive) // Time is up, check scores
        {
            if (ScoreManager.Instance.GetPlayerOneScore() == ScoreManager.Instance.GetPlayerTwoScore())
            {
                TriggerGoldenGoal();
            }
            else
            {
                EndGame(false);
            }
        }
        if (isInHalftime && Input.GetKeyDown(KeyCode.Space))
        {
            isInHalftime = false; // End halftime state
            Time.timeScale = 1f;  // Resume game from halftime
                                  //            Debug.Log("Halftime over, game resumed!");
            OnHalfTimeEnded?.Invoke();
        }
        // Start the game when Space is pressed.
        if (!isGameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            isGameStarted = true;
            Time.timeScale = 1f; // Resume game time.
            OnGameStart?.Invoke();
        }

        // Check for game restart if the game has ended.
        if (isGameEnded && Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }

        if (isGameStarted && !isGameEnded && !isInHalftime)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                TogglePause();
            }

            gameTime += Time.deltaTime;
            if (!isHalfTimeReached && gameTime >= gameDuration / 2)
            {
                isHalfTimeReached = true; // Prevent event from being raised again
                OnHalfTimeReached?.Invoke(); // Raise the halftime event
                                             //                Debug.Log("Halftime reached!");
                HandleHalftime();
            }

        }
    }
    private void TriggerGoldenGoal()
    {
        isGoldenGoalActive = true;
        Time.timeScale = 0;
        OnGoldenGoal?.Invoke();
    }
    private void HandleHalftime()
    {
        //        Debug.Log("Halftime reached! Pausing the game...");
        Time.timeScale = 0f; // Pause the game during halftime
        isInHalftime = true; // Set halftime state

        // Display halftime message (implement this in your UI)
        //        Debug.Log("Press Space to resume the game.");
    }

    public void TogglePause()
    {
        if (isGameStarted && !isInHalftime) // Don't toggle pause if it's halftime
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f;
        }
    }

    public void NotifyGoalHit(int scoringPlayer)
    {
        OnGoalHit?.Invoke(scoringPlayer);
    }

    public void NotifyOverHit(int scoringPlayer)
    {
        OnOverHit?.Invoke(scoringPlayer);
    }

    public void NotifyBongHit(int scoringPlayer, BongsController bong)
    {
        OnBongHit?.Invoke(scoringPlayer, bong);
    }

    public void NotifyStarsHit(int scoringPlayer, int index)
    {
        OnStarsHit?.Invoke(scoringPlayer, index);
    }

    public void NotifyKeeperHit(int playersKeeper)
    {
        OnKeeperHit?.Invoke(playersKeeper);
    }

    public void NotifyPlayerHit(int player)
    {
        OnPlayerHit?.Invoke(player);
    }

    public void CheckAllStarsLit(int player)
    {
        StarController[] stars = player == 1 ? playerOneStars : playerTwoStars;

        bool allLit = true;
        foreach (StarController star in stars)
        {
            if (!star.GetIsLit())
            {
                allLit = false;
                break;
            }
        }

        if (allLit)
        {
            HandleAllStarsLit(player);
        }
    }

    private void HandleAllStarsLit(int player)
    {
        StarController[] stars = player == 1 ? playerOneStars : playerTwoStars;
        ResetAllStars(stars);
    }

    private void ResetAllStars(StarController[] stars)
    {
        foreach (StarController star in stars)
        {
            star.ResetStar();
        }
    }

    public StarController[] GetPlayerOneStars() => playerOneStars;
    public StarController[] GetPlayerTwoStars() => playerTwoStars;

    public void EndGame(bool isGoldenGoal)
    {
        isGameEnded = true;
        Time.timeScale = 0f; // Stop the game.
        OnGameEnd?.Invoke(isGoldenGoal);
        //        Debug.Log("Game has ended. Press Space to restart.");
    }

    private void RestartGame()
    {
        isGameEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}
