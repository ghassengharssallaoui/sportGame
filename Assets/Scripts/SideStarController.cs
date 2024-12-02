using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideStarController : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] bool isPlayerOneStar;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            gameManager.StarHit(isPlayerOneStar, this.gameObject);
        }
    }
}
