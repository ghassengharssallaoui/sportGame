using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer playerOne, playerTwo;
    private TeamsManager teamsManager;


    [SerializeField] private SlidersController1 slidersController;

    public event Action<int> OnGoalHit;
    public event Action<int> OnOverHit;
    public event Action<int, BongsController> OnBongHit;
    public event Action<int, int> OnStarsHit;
    public event Action<int> OnKeeperHit;
    public event Action<int> OnPlayerHit;
    public event Action OnGameStart;
    public event Action OnGameEnd;
    public event Action OnHalfTimeReached;
    public event Action OnHalfTimeEnded;
    public event Action OnGoldenGoalReached;
    public event Action OnGoldenGoalStart;

    public static GameManager Instance;

    [SerializeField] private float gameDuration = 180f;
    private float gameTime = 0f;
    private GameState currentState = GameState.Initialization;
    private bool isGoldenGoalActive = false;
    private bool isHalfTimeReached = false;
    [SerializeField] private StarController[] playerOneStars;
    [SerializeField] private StarController[] playerTwoStars;

    public float GameTime() => gameTime;
    public float GameDuration() => gameDuration;
    public GameState CurrentState() => currentState;
    public StarController[] GetPlayerOneStars() => playerOneStars;
    public StarController[] GetPlayerTwoStars() => playerTwoStars;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        teamsManager = TeamsManager.Instance;
        slidersController.LoadSettings();
        TransitionToState(GameState.WaitingForStart);
        playerOne.sprite = teamsManager.Teams[teamsManager.PlayerOneIndex].skin;
        playerTwo.sprite = teamsManager.Teams[teamsManager.PlayerTwoIndex].skin;
    }
    private void Update()
    {
        switch (currentState)
        {
            case GameState.WaitingForStart:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    OnGameStart?.Invoke();
                    TransitionToState(GameState.GamePlay);
                }
                break;

            case GameState.GamePlay:
                HandleGamePlayState();
                break;

            case GameState.Pause:
                if (Input.GetKeyDown(KeyCode.P))
                {
                    TransitionToState(GameState.GamePlay);
                }
                break;

            case GameState.Halftime:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    OnHalfTimeEnded?.Invoke();
                    TransitionToState(GameState.GamePlay);
                }
                break;

            case GameState.GoldenGoal:

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    OnGoldenGoalStart?.Invoke();
                    TransitionToState(GameState.GamePlay);
                }
                break;

            case GameState.GameEnd:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    TransitionToState(GameState.Restart);
                }
                break;

            case GameState.Restart:
                RestartGame();
                break;
        }
    }
    private void HandleGamePlayState()
    {
        gameTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.P))
        {
            TransitionToState(GameState.Pause);
        }

        if (gameTime >= gameDuration && !isGoldenGoalActive)
        {
            if (ScoreManager.Instance.GetPlayerOneScore() == ScoreManager.Instance.GetPlayerTwoScore())
            {
                isGoldenGoalActive = true;
                TransitionToState(GameState.GoldenGoal);
            }
            else
            {
                TransitionToState(GameState.GameEnd);
            }
        }

        if (gameTime >= gameDuration / 2 && !isHalfTimeReached)
        {
            isHalfTimeReached = true;
            OnHalfTimeReached?.Invoke();
            TransitionToState(GameState.Halftime);
        }
        if (isGoldenGoalActive)
        {
            if (ScoreManager.Instance.GetPlayerOneScore() != ScoreManager.Instance.GetPlayerTwoScore())
            {
                TransitionToState(GameState.GameEnd);
            }
        }
    }

    public void TransitionToState(GameState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GameState.WaitingForStart:
                Time.timeScale = 0f;
                break;

            case GameState.GamePlay:
                Time.timeScale = 1f;
                break;

            case GameState.Pause:
                Time.timeScale = 0f;
                break;

            case GameState.Halftime:
                Time.timeScale = 0f;
                break;

            case GameState.GoldenGoal:
                Time.timeScale = 0f;
                OnGoldenGoalReached?.Invoke();
                break;

            case GameState.GameEnd:
                Time.timeScale = 0f;
                OnGameEnd?.Invoke();
                break;

            case GameState.Restart:
                RestartGame();
                break;
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
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
