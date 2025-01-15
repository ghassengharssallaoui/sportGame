using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricShock : Bullet
{
    [SerializeField] private float staminaDecrease = 0.15f;

    protected override void OnHitTarget(Collider2D collision)
    {
        PlayerController targetPlayer = collision.GetComponent<PlayerController>();
        if (targetPlayer != null)
        {
            targetPlayer.ChangeStamina(-staminaDecrease);
        }
    }
}
