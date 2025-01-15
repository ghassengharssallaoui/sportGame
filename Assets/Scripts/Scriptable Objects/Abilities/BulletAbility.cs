using UnityEngine;

[CreateAssetMenu(fileName = "BulletAbility", menuName = "Abilities/Bullet")]
public class BulletAbility : BaseAbility
{
    public GameObject projectilePrefab; // Bullet prefab
    public float projectileSpeed = 10f; // Speed of the bullet
    public override void Execute(GameObject player, GameObject ball)
    {
        // Find the opponent player
        GameObject opponent = player.name == "Player One" ? GameObject.Find("Player Two") : GameObject.Find("Player One");

        if (opponent == null)
        {
            Debug.LogError("Opponent not found!");
            return;
        }

        // Spawn the fireball
        GameObject bullet = Instantiate(projectilePrefab, player.transform.position, Quaternion.identity);
        // Set direction toward the opponent
        Vector2 direction = (opponent.transform.position - player.transform.position).normalized;
        bullet.GetComponent<Bullet>().Initialize(direction, projectileSpeed, opponent, duration);
    }
}
