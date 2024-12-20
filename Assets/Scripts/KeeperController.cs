using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperController : MonoBehaviour
{
    [SerializeField] float speed = 2f; // Speed of oscillation
    [SerializeField] float minY = -1.5f; // Minimum Y position
    [SerializeField] float maxY = 1.5f; // Maximum Y position
    void Update()
    {
        // Calculate new Y position
        float newY = Mathf.PingPong(Time.time * speed, maxY - minY) + minY;
        // Apply the position to the GameObject
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            int playersKeeper = gameObject.CompareTag("PlayerOne") ? 1 : 2;
            // Notify the GameManager
            GameManager.Instance.NotifyKeeperHit(playersKeeper);
        }
    }

}
