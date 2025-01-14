using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base Ability ScriptableObject
public abstract class BaseAbility : ScriptableObject
{
    public string abilityName;
    public float cooldown = 3f;
    public float duration;

    private bool isOnCooldown;

    public abstract void Execute(GameObject player, GameObject ball);

    public bool CanActivate()
    {
        return !isOnCooldown;
    }

    public IEnumerator StartCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
    }
}