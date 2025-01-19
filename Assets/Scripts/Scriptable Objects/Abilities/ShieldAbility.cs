using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield Ability", menuName = "Abilities/ShieldAbility")]
public class ShieldAbility : BaseAbility
{
    public override void Execute(GameObject player, GameObject ball)
    {
        // Step 1: Find the closest bong of the player who activated the ability
        string playerName = player.name;



        // Step 3: Temporarily shield the opponent's bongs
        string opponentName = playerName == "Player One" ? "Player Two" : "Player One";
        GameManager.Instance.ActivateGoalSheild(opponentName);
        GameManager.Instance.StartCoroutine(RemoveShield(opponentName, duration));
    }

    private IEnumerator RemoveShield(string playerOfTheSheild, float duration)
    {
        yield return new WaitForSeconds(duration);
        GameManager.Instance.DeactivateGoalSheild(playerOfTheSheild);
    }
}
