using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swoop : Bullet
{
    [SerializeField] private float staminaDecrease = 0.05f;

    protected override void OnHitTarget(Collider2D collision)
    {
        PlayerController targetPlayer = collision.GetComponent<PlayerController>();
        if (targetPlayer != null)
        {
            targetPlayer.ChangeStamina(-staminaDecrease);
        }
    }
}
