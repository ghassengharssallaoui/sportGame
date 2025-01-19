using UnityEngine;

[CreateAssetMenu(fileName = "Ranger Ability", menuName = "Abilities/RangerAbility")]
public class RangerAbility : BaseAbility
{
    [SerializeField] private float pushForce = 10f; // Adjustable push force

    public override void Execute(GameObject player, GameObject ball)
    {
        if (ball == null)
        {
            Debug.LogWarning("Ball is not assigned!");
            return;
        }

        // Find goals in the scene by name
        GameObject playerOneGoal = GameObject.Find("Player One Goal");
        GameObject playerTwoGoal = GameObject.Find("Player Two Goal");

        if (playerOneGoal == null || playerTwoGoal == null)
        {
            Debug.LogError("One or both goals not found! Ensure they are named correctly in the scene.");
            return;
        }

        // Determine the opponent's goal based on the player's tag
        Transform opponentGoal = null;
        if (player.CompareTag("PlayerOne"))
        {
            opponentGoal = playerTwoGoal.transform;
        }
        else if (player.CompareTag("PlayerTwo"))
        {
            opponentGoal = playerOneGoal.transform;
        }

        if (opponentGoal == null)
        {
            Debug.LogWarning("Opponent goal not determined. Check player tags.");
            return;
        }

        // Calculate the direction towards the opponent's goal
        Vector2 direction = (opponentGoal.position - ball.transform.position);

        // Apply force to the ball
        Rigidbody2D ballRigidbody = ball.GetComponent<Rigidbody2D>();
        if (ballRigidbody != null)
        {
            ballRigidbody.AddForce(direction * pushForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogWarning("Ball does not have a Rigidbody2D!");
        }
    }
}
