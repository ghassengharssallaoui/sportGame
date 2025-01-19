using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Superstar Ability", menuName = "Abilities/SuperstarAbility")]
public class SuperstarAbility : BaseAbility
{
    [SerializeField] private float shootForce = 10f; // Force to shoot the ball

    public override void Execute(GameObject player, GameObject ball)
    {
        // Step 1: Find unlit stars of the player who activated the ability
        string playerName = player.name;
        StarController targetStar = null;
        float closestDistance = float.MaxValue;

        // Search for unlit stars based on player's name
        for (int i = 1; i <= 5; i++)
        {
            string starName = $"{playerName} Star {i}";
            GameObject starObject = GameObject.Find(starName);
            if (starObject != null)
            {
                StarController starController = starObject.GetComponent<StarController>();
                if (starController != null && !starController.GetIsLit())
                {
                    float distance = Vector2.Distance(ball.transform.position, starObject.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        targetStar = starController;
                    }
                }
            }
        }
        if (targetStar != null)
        {
            // Step 2: Calculate direction to the closest unlit star and shoot the ball towards it
            Vector2 direction = (targetStar.transform.position - ball.transform.position);
            Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
            ballRb.velocity = Vector2.zero;
            ballRb.AddForce(direction * shootForce, ForceMode2D.Impulse);
        }

        // Step 3: Temporarily shield the opponent's stars
        string opponentName = playerName == "Player One" ? "Player Two" : "Player One";
        GameManager.Instance.ActivateSheild(opponentName);
        GameManager.Instance.StartCoroutine(RemoveShield(opponentName, duration));

    }
    private IEnumerator RemoveShield(string playerOfTheSheild, float duration)
    {
        yield return new WaitForSeconds(duration);
        GameManager.Instance.DeactivateSheild(playerOfTheSheild);
    }

}
