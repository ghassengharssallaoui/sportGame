using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperController : MonoBehaviour
{
    [SerializeField] float speed = 2f; // Speed of oscillation
    [SerializeField] float minY = -1.5f; // Minimum Y position
    [SerializeField] float maxY = 1.5f; // Maximum Y position
    float newY;
    void Start()
    {
        if (tag == "PlayerOne")
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = TeamsManager.Instance.Teams[TeamsManager.Instance.PlayerOneIndex].skins[3];
        }
        else
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = TeamsManager.Instance.Teams[TeamsManager.Instance.PlayerTwoIndex].skins[3];

        }
    }
    void Update()
    {
        // Calculate new Y position
        if (tag == "PlayerOne")
        {
            newY = Mathf.PingPong(Time.time * speed * (TeamsManager.Instance.Teams[TeamsManager.Instance.PlayerOneIndex].defense / 5), maxY - minY) + minY;
        }
        else
        {
            newY = Mathf.PingPong(Time.time * speed * (TeamsManager.Instance.Teams[TeamsManager.Instance.PlayerTwoIndex].defense / 5), maxY - minY) + minY;
        }
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
