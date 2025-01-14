using UnityEngine;

[CreateAssetMenu(fileName = "FireballAbility", menuName = "Abilities/Fireball")]
public class FireballAbility : BaseAbility
{
    public GameObject fireballPrefab; // Bullet prefab
    public float fireballSpeed = 10f; // Speed of the bullet
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
        GameObject fireball = Instantiate(fireballPrefab, player.transform.position, Quaternion.identity);
        // Set direction toward the opponent
        Vector2 direction = (opponent.transform.position - player.transform.position).normalized;
        fireball.GetComponent<Fireball>().Initialize(direction, fireballSpeed, opponent);
    }
}
