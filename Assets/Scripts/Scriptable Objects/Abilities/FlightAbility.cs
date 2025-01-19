using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Flight Ability", menuName = "Abilities/Flight")]
public class FlightAbility : BaseAbility
{
    public override void Execute(GameObject player, GameObject ball)
    {
        if (player == null)
        {
            Debug.LogWarning("FlightAbility: Player object is null.");
            return;
        }

        // Get PlayerController component
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogWarning("FlightAbility: PlayerController component not found on player.");
            return;
        }

        // Calculate random position within the field boundaries
        float randomX = Random.Range(-playerController.xBoundary, playerController.xBoundary);
        float randomY = Random.Range(-playerController.yBoundary, playerController.yBoundary);

        // Set the player's position to the new random coordinates
        Vector2 newPosition = new Vector2(randomX, randomY);
        player.transform.position = newPosition;

        Debug.Log($"{abilityName} executed! Player teleported to {newPosition}.");
    }
}
