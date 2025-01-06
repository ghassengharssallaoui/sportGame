using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    private Team playerOneTeam;
    private Team playerTwoTeam;
    private int currentOneShotIndexPlayerOne = 0;
    private int currentOneShotIndexPlayerTwo = 0;

    [SerializeField] private Text playerOneAbilityText, playerTwoAbilityText;

    private void Start()
    {
        playerOneTeam = TeamsManager.Instance.GetPlayerOneTeam();
        playerTwoTeam = TeamsManager.Instance.GetPlayerTwoTeam();
    }

    public void ActivateReusableAbilityPlayerOne(GameObject player, GameObject ball)
    {
        if (playerOneTeam.reusableAbility != null && playerOneTeam.reusableAbility.CanActivate())
        {
            playerOneTeam.reusableAbility.Execute(player, ball);
            StartCoroutine(DisplayAbilityText(playerOneAbilityText, playerOneTeam.reusableAbility.abilityName));
            StartCoroutine(playerOneTeam.reusableAbility.StartCooldown());
        }
    }

    public void ActivateNextOneShotAbilityPlayerOne(GameObject player, GameObject ball)
    {
        if (currentOneShotIndexPlayerOne < playerOneTeam.oneShotAbilities.Count)
        {
            BaseAbility ability = playerOneTeam.oneShotAbilities[currentOneShotIndexPlayerOne];
            ability.Execute(player, ball);
            StartCoroutine(DisplayAbilityText(playerOneAbilityText, ability.abilityName));
            currentOneShotIndexPlayerOne++;
        }
    }

    public void ActivateReusableAbilityPlayerTwo(GameObject player, GameObject ball)
    {
        if (playerTwoTeam.reusableAbility != null && playerTwoTeam.reusableAbility.CanActivate())
        {
            playerTwoTeam.reusableAbility.Execute(player, ball);
            StartCoroutine(DisplayAbilityText(playerTwoAbilityText, playerTwoTeam.reusableAbility.abilityName));
            StartCoroutine(playerTwoTeam.reusableAbility.StartCooldown());
        }
    }

    public void ActivateNextOneShotAbilityPlayerTwo(GameObject player, GameObject ball)
    {
        if (currentOneShotIndexPlayerTwo < playerTwoTeam.oneShotAbilities.Count)
        {
            BaseAbility ability = playerTwoTeam.oneShotAbilities[currentOneShotIndexPlayerTwo];
            ability.Execute(player, ball);
            StartCoroutine(DisplayAbilityText(playerTwoAbilityText, ability.abilityName));
            currentOneShotIndexPlayerTwo++;
        }
    }

    // Coroutine to animate the ability name text
    private IEnumerator DisplayAbilityText(Text abilityText, string abilityName)
    {
        abilityText.text = abilityName;  // Set the text to the ability name
        abilityText.transform.localScale = Vector3.one;  // Reset the scale

        // Animate the text growing
        float scaleTime = 1f;  // Duration to grow the text
        float maxScale = 2f;  // Maximum scale
        float elapsedTime = 0f;

        while (elapsedTime < scaleTime)
        {
            float scale = Mathf.Lerp(1f, maxScale, elapsedTime / scaleTime);
            abilityText.transform.localScale = new Vector3(scale, scale, 1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Wait for 1 second before starting to fade
        yield return new WaitForSeconds(1f);

        // Fade out the ability text
        float fadeDuration = 1f;  // Fade duration
        Color startColor = abilityText.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);  // Fully transparent

        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            abilityText.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Hide the text after fading
        abilityText.text = "";
        abilityText.color = startColor;  // Reset the color for the next ability
    }
}
