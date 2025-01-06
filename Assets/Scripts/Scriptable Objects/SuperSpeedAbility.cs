using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SuperSpeedAbility", menuName = "Abilities/Super Speed")]
public class SuperSpeedAbility : BaseAbility
{
    public float speedMultiplier = 2f; // How much to increase the speed
    public float duration = 10f; // Duration of the speed boost in seconds

    public override void Execute(GameObject player, GameObject ball)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.StartCoroutine(ApplySuperSpeed(playerController));
        }
    }

    private IEnumerator ApplySuperSpeed(PlayerController playerController)
    {
        // Temporarily boost player speed
        playerController.playerSpeed *= speedMultiplier;

        // Wait for the duration of the boost
        yield return new WaitForSeconds(duration);

        // Revert player speed
        playerController.playerSpeed /= speedMultiplier;
    }
}
