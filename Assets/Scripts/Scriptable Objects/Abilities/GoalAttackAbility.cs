using UnityEngine;

[CreateAssetMenu(fileName = "Goal Attack Ability", menuName = "Abilities/GoalAttackAbility")]
public class GoalAttackAbility : BaseAbility
{
    [SerializeField] private float teleportOffset = 2f; // Distance from the goal to place the ball

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

        // Teleport the ball close to the opponent's goal
        Vector3 teleportPosition = opponentGoal.position;
        if (player.CompareTag("PlayerOne"))
        {
            teleportPosition.x -= teleportOffset; // Adjust position slightly above the goal
        }
        else
        {
            teleportPosition.x += teleportOffset; // Adjust position slightly above the goal
        }
        ball.transform.position = teleportPosition;

        // Reset ball velocity to avoid unintended motion
        Rigidbody2D ballRigidbody = ball.GetComponent<Rigidbody2D>();
        if (ballRigidbody != null)
        {
            ballRigidbody.velocity = Vector2.zero;
        }
        else
        {
            Debug.LogWarning("Ball does not have a Rigidbody2D!");
        }
    }
}
