using UnityEngine;

public class Fireball : Bullet
{
    [SerializeField] private float staminaDecrease = 0.2f;

    protected override void OnHitTarget(Collider2D collision)
    {
        PlayerController targetPlayer = collision.GetComponent<PlayerController>();
        if (targetPlayer != null)
        {
            targetPlayer.ChangeStamina(-staminaDecrease);
        }
    }
}
