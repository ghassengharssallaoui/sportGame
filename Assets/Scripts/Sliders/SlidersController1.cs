using UnityEngine;

public class SlidersController1 : MonoBehaviour
{
    [SerializeField] private BallController ballController;
    [SerializeField] private PlayerController playerOneController;
    [SerializeField] private PlayerController playerTwoController;


    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("DefaultBallSpeed", ballController.defaultBallSpeed);
        PlayerPrefs.SetFloat("BallDrag", ballController.ballRigidbody.drag);
        PlayerPrefs.SetFloat("ImpactForce", ballController.impactForce);
        PlayerPrefs.SetFloat("SlowDownMultiplier", ballController.velocityMultiplierOnImpact);
        PlayerPrefs.SetFloat("BallRandomMovementSpeed", ballController.ballRandomMovementSpeed);
        PlayerPrefs.SetFloat("PlayerOneSpeed", playerOneController.playerSpeed);
        PlayerPrefs.SetFloat("PlayerTwoSpeed", playerTwoController.playerSpeed);
        PlayerPrefs.SetFloat("ImpactWithStars", ballController.impactWithStars);
        PlayerPrefs.SetFloat("ImpactWithWalls", ballController.impactWithWalls);


        PlayerPrefs.Save();
        Debug.Log("Settings Saved!");
    }

    public void LoadSettings()
    {
        ballController.defaultBallSpeed = PlayerPrefs.GetFloat("DefaultBallSpeed", ballController.defaultBallSpeed);
        ballController.ballRigidbody.drag = PlayerPrefs.GetFloat("BallDrag", ballController.ballRigidbody.drag);
        ballController.impactForce = PlayerPrefs.GetFloat("ImpactForce", ballController.impactForce);
        ballController.velocityMultiplierOnImpact = PlayerPrefs.GetFloat("SlowDownMultiplier", ballController.velocityMultiplierOnImpact);
        ballController.ballRandomMovementSpeed = PlayerPrefs.GetFloat("BallRandomMovementSpeed", ballController.ballRandomMovementSpeed);
        playerOneController.playerSpeed = PlayerPrefs.GetFloat("PlayerOneSpeed", playerOneController.playerSpeed);
        playerTwoController.playerSpeed = PlayerPrefs.GetFloat("PlayerTwoSpeed", playerTwoController.playerSpeed);
        ballController.impactWithStars = PlayerPrefs.GetFloat("ImpactWithStars", ballController.impactWithStars);
        ballController.impactWithWalls = PlayerPrefs.GetFloat("ImpactWithWalls", ballController.impactWithWalls);
    }

    public void OpenSettings()
    {
        GameManager.Instance.TogglePause();
        gameObject.SetActive(true);
    }
    public void CloseSettings()
    {
        GameManager.Instance.TogglePause();
        gameObject.SetActive(false);
    }
}
