using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance;
    public int northStart = 0;
    public int southStart = 21;
    public int westStart = 44;
    public int eastStart = 63;
    // public int eastStart = 63;

    public bool isNeon = false;
    public string[] stadiums;
    public string[] balls;
    public string[] skins;
    // public string[] skinsTwo;
    public string[] Regins;

    public Text stadiumText;
    public Text ballText;
    public Text skinText;
    public Text skinTextGoal;
    public Text skinTextPlayerTwoGoal;
    public Text reginText;
    public Text reginTextPlayerTwo;
    public Text skinTwoText;
    public int currentStadiumTextIndex = 0;
    public int currentBallTextIndex = 0;
    public int currentSkinTextIndex = 0;
    public int currentSkinTextIndexGoal = 0;
    public int currentSkinTextIndexGoalPlayerTwo = 0;
    public int currentReginTextIndex = 0;
    public int currentReginTextIndexPlayerTwo = 0;
    public int currentSkinTwoTextIndex = 0;
    public int currentSkinTwoTextIndexPlayerTwo = 0;

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

    public void NextBadge(bool isPlayerOne)
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

    public void PreviousBadge(bool isPlayerOne)
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
        if (TeamsManager.Instance.PlayerOneIndex >= northStart && TeamsManager.Instance.PlayerOneIndex <= southStart - 1)
        {
            currentReginTextIndex = 0;
        }
        else if (TeamsManager.Instance.PlayerOneIndex >= southStart && TeamsManager.Instance.PlayerOneIndex <= westStart - 1)
        {
            currentReginTextIndex = 1;
        }
        else if (TeamsManager.Instance.PlayerOneIndex >= westStart && TeamsManager.Instance.PlayerOneIndex <= eastStart - 1)
        {
            currentReginTextIndex = 2;
        }
        else if (TeamsManager.Instance.PlayerOneIndex >= eastStart && TeamsManager.Instance.PlayerOneIndex < teamsManager.Teams.Length)
        {
            currentReginTextIndex = 3;
        }









        reginText.text = Regins[currentReginTextIndex];
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

        if (TeamsManager.Instance.PlayerTwoIndex >= northStart && TeamsManager.Instance.PlayerTwoIndex <= southStart - 1)
        {
            currentReginTextIndexPlayerTwo = 0;
        }
        else if (TeamsManager.Instance.PlayerTwoIndex >= southStart && TeamsManager.Instance.PlayerTwoIndex <= westStart - 1)
        {
            currentReginTextIndexPlayerTwo = 1;
        }
        else if (TeamsManager.Instance.PlayerTwoIndex >= westStart && TeamsManager.Instance.PlayerTwoIndex <= eastStart - 1)
        {
            currentReginTextIndexPlayerTwo = 2;
        }
        else if (TeamsManager.Instance.PlayerTwoIndex >= eastStart && TeamsManager.Instance.PlayerOneIndex < teamsManager.Teams.Length)
        {
            currentReginTextIndexPlayerTwo = 3;
        }


        reginTextPlayerTwo.text = Regins[currentReginTextIndexPlayerTwo];




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
    public void NextRegin()
    {
        currentReginTextIndex = (currentReginTextIndex + 1) % Regins.Length;
        reginText.text = Regins[currentReginTextIndex];
        switch (currentReginTextIndex)
        {
            case 0:
                TeamsManager.Instance.PlayerOneIndex = northStart;
                break;
            case 1:
                TeamsManager.Instance.PlayerOneIndex = southStart;
                break;
            case 2:
                TeamsManager.Instance.PlayerOneIndex = westStart;
                break;
            case 3:
                TeamsManager.Instance.PlayerOneIndex = eastStart;
                break;
        }
        UpdatePlayerOneUI();

    }
    public void PreviousRegin()
    {
        currentReginTextIndex = (currentReginTextIndex - 1 + Regins.Length) % Regins.Length;
        reginText.text = Regins[currentReginTextIndex];
        switch (currentReginTextIndex)
        {
            case 0:
                TeamsManager.Instance.PlayerOneIndex = northStart;
                break;
            case 1:
                TeamsManager.Instance.PlayerOneIndex = southStart;
                break;
            case 2:
                TeamsManager.Instance.PlayerOneIndex = westStart;
                break;
            case 3:
                TeamsManager.Instance.PlayerOneIndex = eastStart;
                break;
        }
        UpdatePlayerOneUI();
    }
    public void NextReginPlayerTwo()
    {
        currentReginTextIndexPlayerTwo = (currentReginTextIndexPlayerTwo + 1) % Regins.Length;
        reginTextPlayerTwo.text = Regins[currentReginTextIndexPlayerTwo];
        switch (currentReginTextIndexPlayerTwo)
        {
            case 0:
                TeamsManager.Instance.PlayerTwoIndex = northStart;
                break;
            case 1:
                TeamsManager.Instance.PlayerTwoIndex = southStart;
                break;
            case 2:
                TeamsManager.Instance.PlayerTwoIndex = westStart;
                break;
            case 3:
                TeamsManager.Instance.PlayerTwoIndex = eastStart;
                break;
        }
        UpdatePlayerTwoUI();

    }
    public void PreviousReginPlayerTwo()
    {
        currentReginTextIndexPlayerTwo = (currentReginTextIndexPlayerTwo - 1 + Regins.Length) % Regins.Length;
        reginTextPlayerTwo.text = Regins[currentReginTextIndexPlayerTwo];
        switch (currentReginTextIndexPlayerTwo)
        {
            case 0:
                TeamsManager.Instance.PlayerTwoIndex = northStart;
                break;
            case 1:
                TeamsManager.Instance.PlayerTwoIndex = southStart;
                break;
            case 2:
                TeamsManager.Instance.PlayerTwoIndex = westStart;
                break;
            case 3:
                TeamsManager.Instance.PlayerTwoIndex = eastStart;
                break;
        }
        UpdatePlayerTwoUI();
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
    public void NextSkin(bool isPlayerOne)
    {
        if (isPlayerOne)
        {// Increment the index and wrap around if needed
            currentSkinTextIndex = (currentSkinTextIndex + 1) % skins.Length;

            // Update the text to the current stadium
            skinText.text = skins[currentSkinTextIndex];
        }
        else
        {
            currentSkinTwoTextIndex = (currentSkinTwoTextIndex + 1) % skins.Length;

            // Update the text to the current stadium
            skinTwoText.text = skins[currentSkinTwoTextIndex];
        }

    }
    public void NextSkinGoal(bool isPlayerOne)
    {
        if (isPlayerOne)
        {// Increment the index and wrap around if needed
            currentSkinTextIndexGoal = (currentSkinTextIndexGoal + 1) % skins.Length;

            // Update the text to the current stadium
            skinTextGoal.text = skins[currentSkinTextIndexGoal];
        }
        else
        {
            currentSkinTextIndexGoalPlayerTwo = (currentSkinTextIndexGoalPlayerTwo + 1) % skins.Length;

            // Update the text to the current stadium
            skinTextPlayerTwoGoal.text = skins[currentSkinTextIndexGoalPlayerTwo];
        }

    }
    public void PreviousSkin(bool isPlayerOne)
    {
        if (isPlayerOne)
        {
            // Decrement the index and wrap around if needed
            currentSkinTextIndex = (currentSkinTextIndex - 1 + skins.Length) % skins.Length;

            // Update the text to the current stadium
            skinText.text = skins[currentSkinTextIndex];
        }
        else
        {
            // Decrement the index and wrap around if needed
            currentSkinTwoTextIndex = (currentSkinTwoTextIndex - 1 + skins.Length) % skins.Length;

            // Update the text to the current stadium
            skinTwoText.text = skins[currentSkinTwoTextIndex];
        }
    }
    public void PreviousSkinGoal(bool isPlayerOne)
    {
        if (isPlayerOne)
        {
            // Decrement the index and wrap around if needed
            currentSkinTextIndexGoal = (currentSkinTextIndexGoal - 1 + skins.Length) % skins.Length;

            // Update the text to the current stadium
            skinTextGoal.text = skins[currentSkinTextIndexGoal];
        }
        else
        {
            // Decrement the index and wrap around if needed
            currentSkinTextIndexGoalPlayerTwo = (currentSkinTextIndexGoalPlayerTwo - 1 + skins.Length) % skins.Length;

            // Update the text to the current stadium
            skinTextPlayerTwoGoal.text = skins[currentSkinTextIndexGoalPlayerTwo];
        }
    }

}
