using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Healing Ability", menuName = "Abilities/Healing")]
public class HealingAbility : BaseAbility
{
    public float healAmount = 0.4f;
    public override void Execute(GameObject player, GameObject ball)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.ChangeStamina(healAmount);
        }
    }
}
