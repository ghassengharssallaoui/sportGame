using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleStarController : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] bool isPlayerOne;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     // Start the coroutine to deactivate the game object
    //     StartCoroutine(DeactivateForSeconds(1f));
    // }

    // private IEnumerator DeactivateForSeconds(float seconds)
    // {
    //     // Deactivate the game object
    //     gameObject.SetActive(false);

    //     // Wait for the specified duration
    //     yield return new WaitForSeconds(seconds);

    //     // Reactivate the game object
    //     gameObject.SetActive(true);
    // }
    private Collider2D objectCollider;
    // private Renderer objectRenderer;

    // private void Start()
    // {
    //     // Cache references to the Collider and Renderer
    //     objectCollider = GetComponent<Collider2D>();
    //     // objectRenderer = GetComponent<Renderer>();
    // }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     // Start the coroutine to disable and re-enable the object
    //     StartCoroutine(DisableForSeconds(30f));
    // }

    // private IEnumerator DisableForSeconds(float seconds)
    // {
    //     // Disable the collider and renderer
    //     objectCollider.enabled = false;
    //     //  objectRenderer.enabled = false;

    //     // Wait for the specified duration
    //     yield return new WaitForSeconds(seconds);

    //     // Re-enable the collider and renderer
    //     objectCollider.enabled = true;
    //     // objectRenderer.enabled = true;
    // }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.StarGoal(isPlayerOne);
    }
}
