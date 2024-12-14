using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float duration = 0.4f, magnitude = 0.08f;
    private void OnEnable()
    {
        GameManager.Instance.OnGoalHit += StartShakeCamera;
        GameManager.Instance.OnOverHit += StartShakeCamera;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGoalHit -= StartShakeCamera;
        GameManager.Instance.OnOverHit += StartShakeCamera;
    }
    private void StartShakeCamera(int scoringPlayer)
    {
        StartCoroutine(ShakeCamera());
    }
    private IEnumerator ShakeCamera()
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0f;
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
}