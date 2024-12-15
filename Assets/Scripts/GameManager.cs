using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for SceneManager

public class GameManager : MonoBehaviour
{
    [SerializeField]
    SlidersController slidersController;

    public event Action<int> OnGoalHit;
    public event Action<int> OnOverHit;
    public event Action<int, BongsController> OnBongHit;
    public event Action<int, int> OnStarsHit;
    public event Action<int> OnKeeperHit;
    public event Action<int> OnPlayerHit;
    public event Action OnGameStart;
    public event Action OnGameEnd;

    public static GameManager Instance;

    [SerializeField] private float gameDuration = 180f;
    private float gameTime = 0f;
    public bool isPaused = false;
    public bool isGameStarted = false;
    private bool isGameEnded = false; // Track if the game has ended

    [SerializeField] private StarController[] playerOneStars; // Stars belonging to Player 1
    [SerializeField] private StarController[] playerTwoStars; // Stars belonging to Player 2

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

        if (isGameStarted && !isGameEnded)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                TogglePause();
            }

            gameTime += Time.deltaTime;

            // Stop the game at 3 minutes.
            if (gameTime >= gameDuration)
            {
                EndGame();
            }
        }
    }

    public void TogglePause()
    {
        if (isGameStarted)
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

    private void EndGame()
    {
        isGameEnded = true;
        Time.timeScale = 0f; // Stop the game.
        OnGameEnd?.Invoke();
        Debug.Log("Game has ended. Press Space to restart.");
    }

    private void RestartGame()
    {
        isGameEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}
