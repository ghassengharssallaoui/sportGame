using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : Bullet
{
    [SerializeField] private float staminaDecrease = 0.07f;

    protected override void OnHitTarget(Collider2D collision)
    {
        PlayerController targetPlayer = collision.GetComponent<PlayerController>();
        if (targetPlayer != null)
        {
            targetPlayer.ChangeStamina(-staminaDecrease);
        }
    }
}
