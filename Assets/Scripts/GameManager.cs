using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Dictionary<string, Sprite> abilitySprites;
    public Sprite BigBong;
    public Sprite ElectricShock;
    public Sprite FireBall;
    public Sprite Flight;
    public Sprite Force;
    public Sprite GoalAttack;
    public Sprite Growth;
    public Sprite Healing;
    public Sprite Hypnotise;
    public Sprite Icebolt;
    public Sprite Jump;
    public Sprite Ranger;
    public Sprite Shield;
    public Sprite SpellBook;
    public Sprite Sting;
    public Sprite SuperSpeed;
    public Sprite SuperStar;
    public Sprite Swoop;
    public Sprite Whirlpool;

    public Image powerOne, powerOnePlayerTwo;
    public Image powerTwo, powerTwoPlayerTwo;
    public Image powerThree, powerThreePlayerTwo;
    public Image powerOneshot, powerOnePlayerTwoshot;
    public Image powerTwoshot, powerTwoPlayerTwoshot;
    public Image powerThreeshot, powerThreePlayerTwoshot;

    [SerializeField]
    private Sprite[] abilitiesSprites;
    [SerializeField]
    private SpriteRenderer playerOne, playerTwo;
    [SerializeField] Image playerOneUiBage, playerTwoUiBage;
    private TeamsManager teamsManager;

    [SerializeField]
    GameObject playerOneStarSheild, playerTwoStarSheild, playerOneBongSheild, playerTwoBongSheild, playerOneGoalSheild, playerTwoGoalSheild;
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
    [SerializeField] private GameObject playerOneKeeper;
    [SerializeField] private GameObject playerTwoKeeper;

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
        playerOne.sprite = teamsManager.Teams[teamsManager.PlayerOneIndex].skins[MenuController.Instance.currentSkinTextIndex];
        playerTwo.sprite = teamsManager.Teams[teamsManager.PlayerTwoIndex].skins[MenuController.Instance.currentSkinTwoTextIndex];
        playerOneUiBage.sprite = teamsManager.Teams[teamsManager.PlayerOneIndex].badge;
        playerTwoUiBage.sprite = teamsManager.Teams[teamsManager.PlayerTwoIndex].badge;
        // playOnePowerOneUi = teamsManager.Teams[teamsManager.PlayerTwoIndex].badge;
        abilitySprites = new Dictionary<string, Sprite>
        {
            { "Big Bong", BigBong },
            { "Electric Shock", ElectricShock },
            { "Fire Ball", FireBall },
            { "Flight", Flight },
            { "Force", Force },
            { "Goal Attack", GoalAttack },
            { "Growth", Growth },
            { "Healing", Healing },
            { "Hypnotise", Hypnotise },
            { "Icebolt", Icebolt },
            { "Jump", Jump },
            { "Ranger", Ranger },
            { "Shield", Shield },
            { "Spell Book", SpellBook },
            { "Sting", Sting },
            { "Super Speed", SuperSpeed },
            { "Super Star", SuperStar },
            { "Swoop", Swoop },
            { "Whirlpool", Whirlpool }
        };

        AssignPlayerOnePowerSprites();
        AssignPlayerOnePowerSpritesShot();
        AssignPlayerTwoPowerSprites();
        AssignPlayerTwoPowerSpritesShot();


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
        SceneManager.LoadScene("MainMenu");
    }
    public GameObject GetKeeperForPlayer(GameObject player)
    {
        // Determine which keeper to return based on player
        if (player.CompareTag("PlayerOne"))
            return playerOneKeeper;
        else if (player.CompareTag("PlayerTwo"))
            return playerTwoKeeper;

        return null; // Default case if no match
    }
    public void ActivateSheild(string opponentName)
    {
        if (opponentName == "Player One")
        {
            playerOneStarSheild.SetActive(true);
        }
        else
        {
            playerTwoStarSheild.SetActive(true);
        }
    }
    public void DeactivateSheild(string opponentName)
    {
        if (opponentName == "Player One")
        {
            playerOneStarSheild.SetActive(false);
        }
        else
        {
            playerTwoStarSheild.SetActive(false);
        }
    }





    public void ActivateBongSheild(string opponentName)
    {
        if (opponentName == "Player One")
        {
            playerOneBongSheild.SetActive(true);
        }
        else
        {
            playerTwoBongSheild.SetActive(true);
        }
    }
    public void DeactivateBongSheild(string opponentName)
    {
        if (opponentName == "Player One")
        {
            playerOneBongSheild.SetActive(false);
        }
        else
        {
            playerTwoBongSheild.SetActive(false);
        }
    }
    public void ActivateGoalSheild(string opponentName)
    {
        if (opponentName == "Player One")
        {
            playerOneGoalSheild.SetActive(true);
        }
        else
        {
            playerTwoGoalSheild.SetActive(true);
        }
    }
    public void DeactivateGoalSheild(string opponentName)
    {
        if (opponentName == "Player One")
        {
            playerOneGoalSheild.SetActive(false);
        }
        else
        {
            playerTwoGoalSheild.SetActive(false);
        }
    }





    private void AssignPlayerOnePowerSprites()
    {
        string abilityName = teamsManager.Teams[teamsManager.PlayerOneIndex].reusableAbility.abilityName;
        //        Debug.Log("" + abilityName);
        if (abilitySprites.TryGetValue(abilityName, out Sprite assignedSprite))
        {
            powerOne.sprite = assignedSprite;
            powerTwo.sprite = assignedSprite;
            powerThree.sprite = assignedSprite;
        }
        else
        {
            // Debug.LogError("Ability name not recognized: " + abilityName);
        }
    }
    private void AssignPlayerOnePowerSpritesShot()
    {
        string abilityName = teamsManager.Teams[teamsManager.PlayerOneIndex].oneShotAbilities[0].abilityName;
        //        Debug.Log("" + abilityName);
        if (abilitySprites.TryGetValue(abilityName, out Sprite assignedSprite))
        {
            powerOneshot.sprite = assignedSprite;
        }
        abilityName = teamsManager.Teams[teamsManager.PlayerOneIndex].oneShotAbilities[1].abilityName;
        //        Debug.Log("" + abilityName);
        if (abilitySprites.TryGetValue(abilityName, out Sprite assin))
        {
            powerTwoshot.sprite = assin;
        }
        abilityName = teamsManager.Teams[teamsManager.PlayerOneIndex].oneShotAbilities[2].abilityName;
        //        Debug.Log("" + abilityName);
        if (abilitySprites.TryGetValue(abilityName, out Sprite assing))
        {
            powerThreeshot.sprite = assing;
        }
    }
    private void AssignPlayerTwoPowerSpritesShot()
    {
        string abilityName = teamsManager.Teams[teamsManager.PlayerTwoIndex].oneShotAbilities[0].abilityName;
        //        Debug.Log("" + abilityName);
        if (abilitySprites.TryGetValue(abilityName, out Sprite prt))
        {
            powerOnePlayerTwoshot.sprite = prt;
        }
        abilityName = teamsManager.Teams[teamsManager.PlayerTwoIndex].oneShotAbilities[1].abilityName;
        //        Debug.Log("" + abilityName);
        if (abilitySprites.TryGetValue(abilityName, out Sprite sprt))
        {
            powerTwoPlayerTwoshot.sprite = sprt;
        }
        abilityName = teamsManager.Teams[teamsManager.PlayerTwoIndex].oneShotAbilities[2].abilityName;
        //        Debug.Log("" + abilityName);
        if (abilitySprites.TryGetValue(abilityName, out Sprite sprite))
        {
            powerThreePlayerTwoshot.sprite = sprite;
        }
    }
    private void AssignPlayerTwoPowerSprites()
    {
        string abilityName = teamsManager.Teams[teamsManager.PlayerTwoIndex].reusableAbility.abilityName;
        //        Debug.Log("" + abilityName);
        if (abilitySprites.TryGetValue(abilityName, out Sprite assignedSprite))
        {
            powerOnePlayerTwo.sprite = assignedSprite;
            powerTwoPlayerTwo.sprite = assignedSprite;
            powerThreePlayerTwo.sprite = assignedSprite;
        }
        else
        {
            //            Debug.LogError("Ability name not recognized: " + abilityName);
        }
    }

}
