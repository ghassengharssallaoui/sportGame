using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    [SerializeField] bool isPlayerOneGoal;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ball")
            gameManager.Goal(isPlayerOneGoal, 6);
    }
}
