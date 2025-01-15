using System.Collections;
using UnityEngine;

public class IceBall : Bullet
{
    [SerializeField] private float staminaDecrease = 0.15f;

    protected override void OnHitTarget(Collider2D collision)
    {
        PlayerController targetPlayer = collision.GetComponent<PlayerController>();
        if (targetPlayer != null)
        {
            targetPlayer.ChangeStamina(-staminaDecrease);

        }
        PlayerController opponentController = targetPlayer.GetComponent<PlayerController>();
        if (opponentController != null)
        {
            // Start the FreezeOpponent coroutine via the opponent's PlayerController
            opponentController.StartCoroutine(FreezeOpponent(opponentController));
        }
    }
    private IEnumerator FreezeOpponent(PlayerController opponentController)
    {
        // Disable the opponent's movement
        opponentController.canMove = false;

        // Optionally, you can add a visual effect or change the opponent's appearance here
        // e.g., change the color to show that they're frozen

        // Wait for the freeze duration
        yield return new WaitForSeconds(duration);

        // Re-enable the opponent's movement
        opponentController.canMove = true;
    }
}
