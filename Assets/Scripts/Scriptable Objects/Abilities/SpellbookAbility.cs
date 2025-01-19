using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spellbook Ability", menuName = "Abilities/SpellbookAbility")]
public class SpellbookAbility : BaseAbility
{
    [SerializeField] private List<BaseAbility> availableAbilities; // List of abilities to choose from

    public override void Execute(GameObject player, GameObject ball)
    {
        if (availableAbilities == null || availableAbilities.Count == 0)
        {
            Debug.LogWarning("No abilities available for the Spellbook to execute!");
            return;
        }

        // Pick a random ability from the list
        int randomIndex = Random.Range(0, availableAbilities.Count);
        BaseAbility randomAbility = availableAbilities[randomIndex];

        if (randomAbility == null)
        {
            Debug.LogWarning("Randomly selected ability is null! Check your Spellbook setup.");
            return;
        }

        // Execute the selected ability
        abilityName = "Random " + randomAbility.abilityName;
        duration = randomAbility.duration;
        cooldown = randomAbility.cooldown;
        randomAbility.Execute(player, ball);
    }
}
