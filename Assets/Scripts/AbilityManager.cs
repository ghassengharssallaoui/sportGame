using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    int i = 0;
    int j = 0;
    [SerializeField]
    private GameObject powerOnePlayerOne, powerTwoPlayerOne, powerThreePlayerOne, powerOnePlayerTwo, powerTwoPlayerTwo, powerThreePlayerTwo; [SerializeField] private float cooldDownBetweenOneShotAbilities = 5f;
    private bool canActivateNextOneShotPlayerOne = true;
    private bool canActivateNextOneShotPlayerTwo = true;
    [SerializeField] private GameObject playerOne, playerTwo, ball;
    private int currentOneShotIndexPlayerOne = 0;
    private int currentOneShotIndexPlayerTwo = 0;
    private Team playerOneTeam;
    private Team playerTwoTeam;

    // Cooldown tracking for each team's reusable ability
    private bool playerOneAbilityOnCooldown;
    private bool playerTwoAbilityOnCooldown;

    // Limit for reusable ability uses
    private int playerOneReusableAbilityUses = 0;
    private int playerTwoReusableAbilityUses = 0;
    private const int maxReusableAbilityUses = 3;

    [SerializeField] private Text playerOneAbilityText, playerTwoAbilityText;

    private void Start()
    {
        playerOneTeam = TeamsManager.Instance.GetPlayerOneTeam();
        playerTwoTeam = TeamsManager.Instance.GetPlayerTwoTeam();
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState() != GameState.GamePlay) return;

        if (tag == "PlayerOne")
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                ActivateNextOneShotAbilityPlayerOne(playerOne, ball);
            }
            if (Input.GetKeyDown(KeyCode.LeftCommand))
            {
                ActivateReusableAbilityPlayerOne(playerOne, ball);

            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                ActivateNextOneShotAbilityPlayerTwo(playerTwo, ball);
            }
            if (Input.GetKeyDown(KeyCode.RightCommand))
            {
                ActivateReusableAbilityPlayerTwo(playerTwo, ball);

            }
        }
    }

    public void ActivateNextOneShotAbilityPlayerOne(GameObject player, GameObject ball)
    {
        if (!canActivateNextOneShotPlayerOne) return;

        if (currentOneShotIndexPlayerOne < playerOneTeam.oneShotAbilities.Count)
        {
            BaseAbility ability = playerOneTeam.oneShotAbilities[currentOneShotIndexPlayerOne];
            ability.Execute(player, ball);
            StartCoroutine(DisplayAbilityText(playerOneAbilityText, ability.abilityName));

            currentOneShotIndexPlayerOne++;
            StartCoroutine(StartCooldownForNextOneShotPlayerOne());
        }
    }

    public void ActivateNextOneShotAbilityPlayerTwo(GameObject player, GameObject ball)
    {
        if (!canActivateNextOneShotPlayerTwo) return;

        if (currentOneShotIndexPlayerTwo < playerTwoTeam.oneShotAbilities.Count)
        {
            BaseAbility ability = playerTwoTeam.oneShotAbilities[currentOneShotIndexPlayerTwo];
            ability.Execute(player, ball);
            StartCoroutine(DisplayAbilityText(playerTwoAbilityText, ability.abilityName));

            currentOneShotIndexPlayerTwo++;
            StartCoroutine(StartCooldownForNextOneShotPlayerTwo());
        }
    }

    public void ActivateReusableAbilityPlayerOne(GameObject player, GameObject ball)
    {
        if (playerOneTeam.reusableAbility != null && !playerOneAbilityOnCooldown && playerOneReusableAbilityUses < maxReusableAbilityUses)
        {
            if (powerOnePlayerOne.activeSelf)
            {
                powerOnePlayerOne.SetActive(false);
            }
            else if (powerTwoPlayerOne.activeSelf)
            {
                powerTwoPlayerOne.SetActive(false);
            }
            else if (powerThreePlayerOne.activeSelf)
            {
                powerThreePlayerOne.SetActive(false);
            }
            playerOneTeam.reusableAbility.Execute(player, ball);
            playerOneReusableAbilityUses++;
            StartCoroutine(DisplayAbilityText(playerOneAbilityText, playerOneTeam.reusableAbility.abilityName));
            StartCoroutine(StartCooldownPlayerOne());
        }
    }

    public void ActivateReusableAbilityPlayerTwo(GameObject player, GameObject ball)
    {
        if (playerTwoTeam.reusableAbility != null && !playerTwoAbilityOnCooldown && playerTwoReusableAbilityUses < maxReusableAbilityUses)
        {
            if (powerOnePlayerTwo.activeSelf)
            {
                powerOnePlayerTwo.SetActive(false);
            }
            else if (powerTwoPlayerTwo.activeSelf)
            {
                powerTwoPlayerTwo.SetActive(false);
            }
            else if (powerThreePlayerTwo.activeSelf)
            {
                powerThreePlayerTwo.SetActive(false);
            }
            playerTwoTeam.reusableAbility.Execute(player, ball);
            playerTwoReusableAbilityUses++;
            StartCoroutine(DisplayAbilityText(playerTwoAbilityText, playerTwoTeam.reusableAbility.abilityName));
            StartCoroutine(StartCooldownPlayerTwo());
        }
    }

    private IEnumerator StartCooldownPlayerOne()
    {
        playerOneAbilityOnCooldown = true;
        yield return new WaitForSeconds(playerOneTeam.reusableAbility.cooldown + playerOneTeam.reusableAbility.duration);
        playerOneAbilityOnCooldown = false;
    }

    private IEnumerator StartCooldownPlayerTwo()
    {
        playerTwoAbilityOnCooldown = true;
        yield return new WaitForSeconds(playerTwoTeam.reusableAbility.cooldown + playerTwoTeam.reusableAbility.duration);
        playerTwoAbilityOnCooldown = false;
    }

    private IEnumerator DisplayAbilityText(Text abilityText, string abilityName)
    {
        Color beginingColor = abilityText.color;
        abilityText.text = abilityName;
        abilityText.transform.localScale = Vector3.one;

        float scaleTime = 1f;
        float maxScale = 2f;
        float elapsedTime = 0f;
        while (elapsedTime < scaleTime)
        {
            float scale = Mathf.Lerp(1f, maxScale, elapsedTime / scaleTime);
            abilityText.transform.localScale = new Vector3(scale, scale, 1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        float fadeDuration = 1f;
        Color startColor = abilityText.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            abilityText.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        abilityText.text = "";
        abilityText.color = beginingColor;
    }

    private IEnumerator StartCooldownForNextOneShotPlayerOne()
    {
        canActivateNextOneShotPlayerOne = false;
        yield return new WaitForSeconds(cooldDownBetweenOneShotAbilities + playerOneTeam.oneShotAbilities[i].duration); // Cooldown duration for Player One
        canActivateNextOneShotPlayerOne = true;
        i++;
    }

    private IEnumerator StartCooldownForNextOneShotPlayerTwo()
    {
        canActivateNextOneShotPlayerTwo = false;
        yield return new WaitForSeconds(cooldDownBetweenOneShotAbilities + playerTwoTeam.oneShotAbilities[j].duration); // Cooldown duration for Player Two
        canActivateNextOneShotPlayerTwo = true;
        j++;
    }
}
