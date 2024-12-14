using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OversController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            int scoringPlayer = gameObject.CompareTag("PlayerOne") ? 2 : 1;
            // Notify the GameManager
            GameManager.Instance.NotifyOverHit(scoringPlayer);
        }
    }
}
