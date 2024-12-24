using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Image playerOne, playerTwo;

    [SerializeField]
    private Text playerOneNameText, playerOneSpeedText, playerOneStrengthText, playerOneAttackText, playerOneDefenseText, playerOneDurabilityText;
    [SerializeField]
    private Text playerTwoNameText, playerTwoSpeedText, playerTwoStrengthText, playerTwoAttackText, playerTwoDefenseText, playerTwoDurabilityText;

    private TeamsManager teamsManager;

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
        playerOneNameText.text = team.name;
        playerOneSpeedText.text = $"Speed: {team.speed}";
        playerOneStrengthText.text = $"Strength: {team.strength}";
        playerOneAttackText.text = $"Attack: {team.attack}";
        playerOneDefenseText.text = $"Defense: {team.defense}";
        playerOneDurabilityText.text = $"Durability: {team.durability}";
    }

    private void UpdatePlayerTwoUI()
    {
        var team = teamsManager.Teams[teamsManager.PlayerTwoIndex];
        playerTwo.sprite = team.badge;
        playerTwoNameText.text = team.name;
        playerTwoSpeedText.text = $"Speed: {team.speed}";
        playerTwoStrengthText.text = $"Strength: {team.strength}";
        playerTwoAttackText.text = $"Attack: {team.attack}";
        playerTwoDefenseText.text = $"Defense: {team.defense}";
        playerTwoDurabilityText.text = $"Durability: {team.durability}";
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
