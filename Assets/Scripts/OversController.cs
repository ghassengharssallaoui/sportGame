using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OversController : MonoBehaviour
{

    GameManager gameManager;
    [SerializeField] Vector2 ballResetPosition;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    [SerializeField] bool isPlayerOneGoal;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ball")
            gameManager.Goal(isPlayerOneGoal, 3, ballResetPosition, false);
    }
}
