using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance;

    public bool isNeon = false;
    public string[] stadiums;
    public string[] balls;

    public Text stadiumText;
    public Text ballText;
    public static int currentStadiumTextIndex = 0;
    public static int currentBallTextIndex = 0;

    [SerializeField]
    private Image playerOne, playerTwo;

    [SerializeField]
    private Text playerOneNameText, playerOneSpeedText, playerOneStrengthText, playerOneAttackText, playerOneDefenseText, playerOneDurabilityText,
                 playerOneReusableAbility;
    [SerializeField]
    private Text playerTwoNameText, playerTwoSpeedText, playerTwoStrengthText, playerTwoAttackText, playerTwoDefenseText, playerTwoDurabilityText,
                 playerTwoReusableAbility;

    [SerializeField]
    private Text[] playerOneOneShotAbilities = new Text[5];
    [SerializeField]
    private Text[] playerTwoOneShotAbilities = new Text[5];

    private TeamsManager teamsManager;
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
        DontDestroyOnLoad(gameObject);
    }
    public void ChangeIsNeon()
    {
        isNeon = !isNeon;
    }
    private void Start()
    {
        teamsManager = TeamsManager.Instance;

        UpdatePlayerOneUI();
        UpdatePlayerTwoUI();
    }

    public void NextSkin(bool isPlayerOne)
    {
        if (isPlayerOne)
        {
            teamsManager.PlayerOneIndex = (teamsManager.PlayerOneIndex + 1) % teamsManager.Teams.Length;
            UpdatePlayerOneUI();
        }
        else
        {
            teamsManager.PlayerTwoIndex = (teamsManager.PlayerTwoIndex + 1) % teamsManager.Teams.Length;
            UpdatePlayerTwoUI();
        }
    }

    public void PreviousSkin(bool isPlayerOne)
    {
        if (isPlayerOne)
        {
            teamsManager.PlayerOneIndex = (teamsManager.PlayerOneIndex - 1 + teamsManager.Teams.Length) % teamsManager.Teams.Length;
            UpdatePlayerOneUI();
        }
        else
        {
            teamsManager.PlayerTwoIndex = (teamsManager.PlayerTwoIndex - 1 + teamsManager.Teams.Length) % teamsManager.Teams.Length;
            UpdatePlayerTwoUI();
        }
    }

    private void UpdatePlayerOneUI()
    {


        var team = teamsManager.Teams[teamsManager.PlayerOneIndex];
        playerOne.sprite = team.badge;
        string[] words = team.name.Split(' '); // Split the string into words
        string lastWord = words[words.Length - 1]; // Get the last word
        string remainingWords = string.Join(" ", words, 0, words.Length - 1); // Get the rest of the string

        playerOneNameText.text = $"<size=50>{remainingWords}</size>\n<b><size=100>{lastWord}</size></b>";

        playerOneSpeedText.text = $"Speed: {team.speed}";
        playerOneStrengthText.text = $"Strength: {team.strength}";
        playerOneAttackText.text = $"Attack: {team.attack}";
        playerOneDefenseText.text = $"Defense: {team.defense}";
        playerOneDurabilityText.text = $"Durability: {team.durability}";
        playerOneReusableAbility.text = $"Reusable Ability: {team.reusableAbility.abilityName}";

        // Update Player One's one-shot abilities
        for (int i = 0; i < playerOneOneShotAbilities.Length; i++)
        {
            if (team.oneShotAbilities != null && i < team.oneShotAbilities.Count && team.oneShotAbilities[i] != null)
            {
                playerOneOneShotAbilities[i].text = $"One-Shot Ability {i + 1}: {team.oneShotAbilities[i].abilityName}";
            }
            else
            {
                playerOneOneShotAbilities[i].text = $"";
            }
        }
    }

    private void UpdatePlayerTwoUI()
    {
        var team = teamsManager.Teams[teamsManager.PlayerTwoIndex];

        string[] words = team.name.Split(' '); // Split the string into words
        string lastWord = words[words.Length - 1]; // Get the last word
        string remainingWords = string.Join(" ", words, 0, words.Length - 1); // Get the rest of the string

        playerTwoNameText.text = $"<size=50>{remainingWords}</size>\n<b><size=100>{lastWord}</size></b>";

        playerTwo.sprite = team.badge;
        playerTwoSpeedText.text = $"Speed: {team.speed}";
        playerTwoStrengthText.text = $"Strength: {team.strength}";
        playerTwoAttackText.text = $"Attack: {team.attack}";
        playerTwoDefenseText.text = $"Defense: {team.defense}";
        playerTwoDurabilityText.text = $"Durability: {team.durability}";
        playerTwoReusableAbility.text = $"Reusable Ability: {team.reusableAbility.abilityName}";

        // Update Player Two's one-shot abilities
        for (int i = 0; i < playerTwoOneShotAbilities.Length; i++)
        {
            if (team.oneShotAbilities != null && i < team.oneShotAbilities.Count && team.oneShotAbilities[i] != null)
            {
                playerTwoOneShotAbilities[i].text = $"One-Shot Ability {i + 1}: {team.oneShotAbilities[i].abilityName}";
            }
            else
            {
                playerTwoOneShotAbilities[i].text = $"";
            }
        }
    }

    public void Play()
    {

        SceneManager.LoadScene("Stadium");

    }

    public void NextStadium()
    {
        // Increment the index and wrap around if needed
        currentStadiumTextIndex = (currentStadiumTextIndex + 1) % stadiums.Length;

        // Update the text to the current stadium
        stadiumText.text = stadiums[currentStadiumTextIndex];
    }


    // Method to show the previous stadium
    public void PreviousStadium()
    {
        // Decrement the index and wrap around if needed
        currentStadiumTextIndex = (currentStadiumTextIndex - 1 + stadiums.Length) % stadiums.Length;

        // Update the text to the current stadium
        stadiumText.text = stadiums[currentStadiumTextIndex];
    }
    public void NextBall()
    {
        // Increment the index and wrap around if needed
        currentBallTextIndex = (currentBallTextIndex + 1) % balls.Length;

        // Update the text to the current stadium
        ballText.text = balls[currentBallTextIndex];
    }
    public void PreviousBall()
    {
        // Decrement the index and wrap around if needed
        currentBallTextIndex = (currentBallTextIndex - 1 + balls.Length) % balls.Length;

        // Update the text to the current stadium
        ballText.text = balls[currentBallTextIndex];
    }
}
