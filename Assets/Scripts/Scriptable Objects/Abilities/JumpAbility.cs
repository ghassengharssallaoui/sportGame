using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Jump Ability", menuName = "Abilities/Jump")]
public class JumpAbility : BaseAbility
{
    [Tooltip("Maximum distance the player can teleport.")]
    public float maxJumpDistance = 5f;

    public override void Execute(GameObject player, GameObject ball)
    {
        if (player == null)
        {
            Debug.LogWarning("JumpAbility: Player object is null.");
            return;
        }

        // Get PlayerController component
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogWarning("JumpAbility: PlayerController component not found on player.");
            return;
        }

        // Get player's current position
        Vector2 currentPosition = player.transform.position;

        // Calculate random jump position
        Vector2 randomOffset = Random.insideUnitCircle * maxJumpDistance;
        Vector2 newPosition = currentPosition + randomOffset;

        // Clamp the new position within player's boundaries
        newPosition.x = Mathf.Clamp(newPosition.x, -playerController.xBoundary, playerController.xBoundary);
        newPosition.y = Mathf.Clamp(newPosition.y, -playerController.yBoundary, playerController.yBoundary);

        // Update player's position
        player.transform.position = newPosition;

        Debug.Log($"{abilityName} executed! Player teleported to {newPosition}.");
    }
}
