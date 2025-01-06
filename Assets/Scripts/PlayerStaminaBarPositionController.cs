using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaminaBarPositionController : MonoBehaviour
{
    public Transform player;
    void FixedUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y
            + (player.transform.localScale.y - 0.3f), player.transform.position.z);

    }
}
