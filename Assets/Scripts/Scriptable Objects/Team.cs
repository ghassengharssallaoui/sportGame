using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Team", menuName = "Team")]
public class Team : ScriptableObject
{
    public new string name;
    public Sprite badge;
    public Sprite[] skins = new Sprite[4];
    public float speed;
    public float strength;
    public float attack;
    public float defense;
    public float durability;
    public BaseAbility reusableAbility;
    public List<BaseAbility> oneShotAbilities;
}
