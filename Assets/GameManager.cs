using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int PlayerOneScore = 0;
    int PlayerTwoScore = 0;
    [SerializeField] Camera cam;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    public GameObject ball;
    public void Goal(bool isPlayerOne)
    {
        if (isPlayerOne)
        {
            PlayerTwoScore += 3;
            Debug.Log("Player Two Score: " + PlayerTwoScore);
        }
        else
        {
            PlayerOneScore += 3;
            Debug.Log("Player One Score: " + PlayerOneScore);
        }
        player1.transform.position = new Vector3(7, 0, 0);
        player2.transform.position = new Vector3(-7, 0, 0);
        ball.transform.position = Vector3.zero;
        ball.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        StartCoroutine(ShakeCamera(0.4f, 0.08f));
    }
    private IEnumerator ShakeCamera(float duration, float magnitude)
    {
        Vector3 originalPosition = cam.transform.localPosition;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            cam.transform.localPosition = originalPosition + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cam.transform.localPosition = originalPosition;
    }
}
