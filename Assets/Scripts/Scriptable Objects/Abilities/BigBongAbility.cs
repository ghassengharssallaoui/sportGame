using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "BigBong Ability", menuName = "Abilities/BigBongAbility")]
public class BigBongAbility : BaseAbility
{
    [SerializeField] private float shootForce = 10f; // Force to shoot the ball

    public override void Execute(GameObject player, GameObject ball)
    {
        // Step 1: Find the closest bong of the player who activated the ability
        string playerName = player.name;
        GameObject targetBong = null;
        float closestDistance = float.MaxValue;

        // Search for bongs based on player's name
        for (int i = 1; i <= 2; i++) // Each player has only 2 bongs
        {
            string bongName = $"{playerName} Bong {i}";
            GameObject bongObject = GameObject.Find(bongName);
            if (bongObject != null)
            {
                float distance = Vector2.Distance(ball.transform.position, bongObject.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    targetBong = bongObject;
                }
            }
        }

        if (targetBong != null)
        {
            // Step 2: Calculate direction to the closest bong and shoot the ball towards it
            Vector2 direction = (targetBong.transform.position - ball.transform.position);
            Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
            ballRb.velocity = Vector2.zero;
            ballRb.AddForce(direction * shootForce, ForceMode2D.Impulse);
        }

        // Step 3: Temporarily shield the opponent's bongs
        string opponentName = playerName == "Player One" ? "Player Two" : "Player One";
        GameManager.Instance.ActivateBongSheild(opponentName);
        GameManager.Instance.StartCoroutine(RemoveShield(opponentName, duration));
    }

    private IEnumerator RemoveShield(string playerOfTheSheild, float duration)
    {
        yield return new WaitForSeconds(duration);
        GameManager.Instance.DeactivateBongSheild(playerOfTheSheild);
    }
}
