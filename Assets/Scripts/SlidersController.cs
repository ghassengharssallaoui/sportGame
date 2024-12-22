using UnityEngine;
using UnityEngine.UI;

public class SlidersController : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider DefaultBallSpeed;
    [SerializeField] private Slider ImpactWithStars;
    [SerializeField] private Slider BallDragSlider;
    [SerializeField] private Slider ImpactForceSlider; // After save speed
    [SerializeField] private Slider SlowDownAfterSaveSlider; // velocityMultiplierOnImpact
    [SerializeField] private Slider BallRandomMovementSpeedSlider; // Random movement speed
    [SerializeField] private Slider playerOneSpeedSlider;
    [SerializeField] private Slider playerTwoSpeedSlider;

    [Header("Texts")]
    [SerializeField] private Text ballSpeedText;
    [SerializeField] private Text ballDragText;
    [SerializeField] private Text impactForceText;
    [SerializeField] private Text slowDownText;
    [SerializeField] private Text ballRandomMovementSpeedText;
    [SerializeField] private Text playerOneSpeedText;
    [SerializeField] private Text playerTwoSpeedText;
    [SerializeField] private Text impactWithStarsText;

    [SerializeField] private BallController ballController;
    [SerializeField] private PlayerController playerOneController;
    [SerializeField] private PlayerController playerTwoController;

    private void Start()
    {
        // Load settings from PlayerPrefs
        LoadSettings();

        // Initialize sliders
        if (DefaultBallSpeed != null)
        {
            DefaultBallSpeed.value = ballController.defaultBallSpeed;
            DefaultBallSpeed.onValueChanged.AddListener(UpdateDefaultBallSpeed);
        }
        if (ImpactWithStars != null)
        {
            ImpactWithStars.value = ballController.impactWithStars;
            ImpactWithStars.onValueChanged.AddListener(UpdateImpactWithStars);
        }

        if (BallDragSlider != null)
        {
            BallDragSlider.value = ballController.ballRigidbody.drag;
            BallDragSlider.onValueChanged.AddListener(UpdateBallDrag);
        }

        if (ImpactForceSlider != null)
        {
            ImpactForceSlider.value = ballController.impactForce;
            ImpactForceSlider.onValueChanged.AddListener(UpdateImpactForce);
        }

        if (SlowDownAfterSaveSlider != null)
        {
            SlowDownAfterSaveSlider.value = ballController.velocityMultiplierOnImpact;
            SlowDownAfterSaveSlider.onValueChanged.AddListener(UpdateSlowDownMultiplier);
        }

        if (BallRandomMovementSpeedSlider != null)
        {
            BallRandomMovementSpeedSlider.value = ballController.ballRandomMovementSpeed;
            BallRandomMovementSpeedSlider.onValueChanged.AddListener(UpdateBallRandomMovementSpeed);
        }

        if (playerOneSpeedSlider != null)
        {
            playerOneSpeedSlider.value = playerOneController.playerSpeed;
            playerOneSpeedSlider.onValueChanged.AddListener(UpdateOnePlayerSpeed);
        }

        if (playerTwoSpeedSlider != null)
        {
            playerTwoSpeedSlider.value = playerTwoController.playerSpeed;
            playerTwoSpeedSlider.onValueChanged.AddListener(UpdatePlayerTwoSpeed);
        }

        // Update text displays
        UpdateBallSpeedText();
        UpdateBallDragText();
        UpdateImpactForceText();
        UpdateSlowDownText();
        UpdateBallRandomMovementSpeedText();
        UpdatePlayerOneSpeedText();
        UpdatePlayerTwoSpeedText();
        UpdateImpactWithStarsText();
    }

    private void UpdateDefaultBallSpeed(float newSpeed)
    {
        ballController.defaultBallSpeed = newSpeed;
        UpdateBallSpeedText();
    }
    private void UpdateImpactWithStars(float newSpeed)
    {
        ballController.impactWithStars = newSpeed;
        UpdateImpactWithStarsText();
    }

    private void UpdateBallDrag(float newDrag)
    {
        ballController.ballRigidbody.drag = newDrag;
        UpdateBallDragText();
    }

    private void UpdateImpactForce(float newForce)
    {
        ballController.impactForce = newForce;
        UpdateImpactForceText();
    }

    private void UpdateSlowDownMultiplier(float newMultiplier)
    {
        ballController.velocityMultiplierOnImpact = newMultiplier;
        UpdateSlowDownText();
    }

    private void UpdateBallRandomMovementSpeed(float newSpeed)
    {
        ballController.ballRandomMovementSpeed = newSpeed;
        UpdateBallRandomMovementSpeedText();
    }

    private void UpdateOnePlayerSpeed(float newSpeed)
    {
        playerOneController.playerSpeed = newSpeed;
        UpdatePlayerOneSpeedText();
    }

    private void UpdatePlayerTwoSpeed(float newSpeed)
    {
        playerTwoController.playerSpeed = newSpeed;
        UpdatePlayerTwoSpeedText();
    }

    // Text update methods
    private void UpdateBallSpeedText()
    {
        if (ballSpeedText != null)
        {
            ballSpeedText.text = $"Default Ball Speed: {ballController.defaultBallSpeed:F2}";
        }
    }
    private void UpdateImpactWithStarsText()
    {
        if (impactWithStarsText != null)
        {
            impactWithStarsText.text = $"Impact With Stars: {ballController.impactWithStars:F2}";
        }
    }

    private void UpdateBallDragText()
    {
        if (ballDragText != null)
        {
            ballDragText.text = $"Ball Friction: {ballController.ballRigidbody.drag:F2}";
        }
    }

    private void UpdateImpactForceText()
    {
        if (impactForceText != null)
        {
            impactForceText.text = $"After Save Speed: {ballController.impactForce:F2}";
        }
    }

    private void UpdateSlowDownText()
    {
        if (slowDownText != null)
        {
            slowDownText.text = $"Slow Down After Save: {ballController.velocityMultiplierOnImpact:F2}";
        }
    }

    private void UpdateBallRandomMovementSpeedText()
    {
        if (ballRandomMovementSpeedText != null)
        {
            ballRandomMovementSpeedText.text = $"Ball Random Movement After Goal Speed: {ballController.ballRandomMovementSpeed:F2}";
        }
    }

    private void UpdatePlayerOneSpeedText()
    {
        if (playerOneSpeedText != null)
        {
            playerOneSpeedText.text = $"Player One Speed: {playerOneController.playerSpeed:F2}";
        }
    }

    private void UpdatePlayerTwoSpeedText()
    {
        if (playerTwoSpeedText != null)
        {
            playerTwoSpeedText.text = $"Player Two Speed: {playerTwoController.playerSpeed:F2}";
        }
    }

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

        PlayerPrefs.Save();
        Debug.Log("Settings Saved!");
    }

    public void LoadSettings()
    {
        ballController.defaultBallSpeed = PlayerPrefs.GetFloat("DefaultBallSpeed", ballController.defaultBallSpeed);
        ballController.impactWithStars = PlayerPrefs.GetFloat("ImpactWithStars", ballController.impactWithStars);
        ballController.ballRigidbody.drag = PlayerPrefs.GetFloat("BallDrag", ballController.ballRigidbody.drag);
        ballController.impactForce = PlayerPrefs.GetFloat("ImpactForce", ballController.impactForce);
        ballController.velocityMultiplierOnImpact = PlayerPrefs.GetFloat("SlowDownMultiplier", ballController.velocityMultiplierOnImpact);
        ballController.ballRandomMovementSpeed = PlayerPrefs.GetFloat("BallRandomMovementSpeed", ballController.ballRandomMovementSpeed);
        playerOneController.playerSpeed = PlayerPrefs.GetFloat("PlayerOneSpeed", playerOneController.playerSpeed);
        playerTwoController.playerSpeed = PlayerPrefs.GetFloat("PlayerTwoSpeed", playerTwoController.playerSpeed);
    }

    public void OpenSettings()
    {
        GameManager.Instance.TransitionToState(GameState.Pause);
        gameObject.SetActive(true);
    }
    public void CloseSettings()
    {
        GameManager.Instance.TransitionToState(GameState.GamePlay);
        gameObject.SetActive(false);
    }
}
