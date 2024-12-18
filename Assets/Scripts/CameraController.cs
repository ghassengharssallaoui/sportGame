using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float elapsed;
    [SerializeField]
    float duration = 0.4f, magnitude = 0.08f;
    private void OnEnable()
    {
        GameManager.Instance.OnGoalHit += StartShakeCamera;
        GameManager.Instance.OnOverHit += StartShakeCamera;
        GameManager.Instance.OnGameEnd += StopCameraMovement;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGoalHit -= StartShakeCamera;
        GameManager.Instance.OnOverHit += StartShakeCamera;
        GameManager.Instance.OnGameEnd -= StopCameraMovement;
    }
    private void StartShakeCamera(int scoringPlayer)
    {
        StartCoroutine(ShakeCamera());
    }
    private IEnumerator ShakeCamera()
    {
        Vector3 originalPosition = transform.localPosition;
        elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = originalPosition + new Vector3(x, y);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
    private void StopCameraMovement(bool isGoldenGoal)
    {
        elapsed = 100f;
        transform.localPosition = new Vector3(0, 0, -10);
    }
}