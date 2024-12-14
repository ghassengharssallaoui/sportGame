using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BongsController : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.OnBongHit += BongScored;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnBongHit -= BongScored;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            int scoringPlayer = gameObject.CompareTag("PlayerOne") ? 2 : 1;
            // Notify the GameManager
            GameManager.Instance.NotifyBongHit(scoringPlayer, this);
        }
    }
    private void BongScored(int player, BongsController bong)
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(DeactivateForSeconds(1f, bong));
        }
    }
    private IEnumerator DeactivateForSeconds(float seconds, BongsController bong)
    {
        // Deactivate the game object
        bong.gameObject.SetActive(false);
        yield return new WaitForSeconds(seconds);
        bong.gameObject.SetActive(true);
    }

}