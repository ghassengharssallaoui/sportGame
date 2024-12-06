using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeeperController : MonoBehaviour
{
    [SerializeField] float speed = 2f; // Speed of oscillation
    [SerializeField] float minY = -1.5f; // Minimum Y position
    [SerializeField] float maxY = 1.5f; // Maximum Y position
    [SerializeField] Text playerOneSavesText;
    [SerializeField] Text playerTwoSavesText;
    [SerializeField] bool isPlayerOneKeeper;
    public static int playerOneSaves = 0, playerTwoSaves = 0;
    float playerOneLastSaveTime = 0f; // Time of the last save for player one
    float playerTwoLastSaveTime = 0f; // Time of the last save for player two

    GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // Calculate new Y position
        float newY = Mathf.PingPong(Time.time * speed, maxY - minY) + minY;
        // Apply the position to the GameObject
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // If player one is the keeper and it has been more than 1 second since their last save
            if (isPlayerOneKeeper && Time.time - playerOneLastSaveTime >= 0.1f)
            {
                playerOneLastSaveTime = Time.time; // Update player one's last save time
                playerOneSaves++;
                playerOneSavesText.text = "Saves: " + playerOneSaves;
                gameManager.decreaseBallVelocity = true;

                PlayerController.posTwo++;
            }
            // If player two is the keeper and it has been more than 1 second since their last save
            else if (!isPlayerOneKeeper && Time.time - playerTwoLastSaveTime >= 0.1f)
            {
                playerTwoLastSaveTime = Time.time; // Update player two's last save time
                playerTwoSaves++;
                playerTwoSavesText.text = "Saves: " + playerTwoSaves;
                gameManager.decreaseBallVelocity = true;
                PlayerController.posOne++;
            }
        }
    }
}
