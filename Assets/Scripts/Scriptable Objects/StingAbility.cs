using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Sting Ability", menuName = "Abilities/Sting")]
public class StingAbility : BaseAbility
{
    public float freezeDuration = 5f;  // Duration for which the opponent will be frozen

    public override void Execute(GameObject player, GameObject ball)
    {
        // Determine the opponent's name based on the current player's name
        string opponentName = player.name == "Player One" ? "Player Two" : "Player One";

        // Find the opponent GameObject by name
        GameObject opponent = GameObject.Find(opponentName);

        // Check if the opponent exists and is active in the scene
        if (opponent != null && opponent.activeInHierarchy)
        {
            // Get the PlayerController component of the opponent
            PlayerController opponentController = opponent.GetComponent<PlayerController>();
            if (opponentController != null)
            {
                // Start the FreezeOpponent coroutine via the opponent's PlayerController
                opponentController.StartCoroutine(FreezeOpponent(opponentController));
            }
        }
    }

    private IEnumerator FreezeOpponent(PlayerController opponentController)
    {
        // Disable the opponent's movement
        opponentController.canMove = false;

        // Optionally, you can add a visual effect or change the opponent's appearance here
        // e.g., change the color to show that they're frozen

        // Wait for the freeze duration
        yield return new WaitForSeconds(freezeDuration);

        // Re-enable the opponent's movement
        opponentController.canMove = true;
    }
}
