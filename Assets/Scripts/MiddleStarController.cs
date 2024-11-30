using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleStarController : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] bool isPlayerOneStar;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.StarGoal(isPlayerOneStar);
    }
}
