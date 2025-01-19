using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Hypnotise Ability", menuName = "Abilities/Hypnotise")]
public class HypnotiseAbility : BaseAbility
{
    public override void Execute(GameObject player, GameObject ball)
    {
        // Determine the opponent's name based on the current player's name
        string opponentName = player.name == "Player One" ? "Player Two" : "Player One";

        // Find the opponent GameObject by name
        GameObject opponent = GameObject.Find(opponentName);
        if (opponent == null)
        {
            Debug.LogWarning("HypnotiseAbility: Opponent player not found.");
            return;
        }

        // Start the hypnotise effect on the opponent
        opponent.GetComponent<PlayerController>().StartCoroutine(HypnotiseMovement(opponent));
    }

    // Coroutine to make the opponent move randomly for the duration
    private IEnumerator HypnotiseMovement(GameObject opponent)
    {
        PlayerController opponentController = opponent.GetComponent<PlayerController>();
        if (opponentController == null)
        {
            yield break;
        }
        opponentController.isHypnotised = true;
        float elapsedTime = 0f;

        // While the duration has not elapsed
        while (elapsedTime < duration)
        {
            // Randomly change the movement input for the opponent
            opponentController.RandomizeMovement();

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // After the duration ends, stop random movement
        opponentController.StopRandomMovement();
        opponentController.isHypnotised = false;

    }
}
