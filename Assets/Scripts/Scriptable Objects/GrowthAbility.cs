using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "GrowthAbility", menuName = "Abilities/Growth")]
public class GrowthAbility : BaseAbility
{
    public float sizeMultiplier = 1.5f; // How much to increase the size
    public float duration = 10f; // Duration of the size boost in seconds

    public override void Execute(GameObject player, GameObject ball)
    {
        // Find keeper references dynamically
        GameObject keeper = GetKeeperReference(player);

        if (player != null)
        {
            // Start the growth coroutine for the player
            player.GetComponent<MonoBehaviour>().StartCoroutine(ApplyGrowth(player));
        }

        if (keeper != null)
        {
            // Start the growth coroutine for the keeper
            keeper.GetComponent<MonoBehaviour>().StartCoroutine(ApplyGrowth(keeper));
        }
    }

    private IEnumerator ApplyGrowth(GameObject target)
    {
        if (target != null)
        {
            // Temporarily increase size
            Transform targetTransform = target.transform;
            Vector3 originalScale = targetTransform.localScale;
            targetTransform.localScale *= sizeMultiplier;

            // Wait for the duration
            yield return new WaitForSeconds(duration);

            // Revert size back to original
            targetTransform.localScale = originalScale;
        }
    }

    private GameObject GetKeeperReference(GameObject player)
    {
        // Pass keeper references based on the player dynamically
        return GameManager.Instance.GetKeeperForPlayer(player);
    }
}
