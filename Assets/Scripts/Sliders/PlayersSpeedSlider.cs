using UnityEngine;
using UnityEngine.UI;

public class PlayersSpeedSlider : SliderHandlerBase
{
    [SerializeField] private PlayerController playerOneController;
    [SerializeField] private string playerNumber;
    public override void InitializeSlider()
    {
        slider.value = playerOneController.playerSpeed;
        slider.onValueChanged.AddListener(UpdateValue);
        UpdateDisplay();
    }

    public override void UpdateValue(float value)
    {
        playerOneController.playerSpeed = value;
        UpdateDisplay();
    }

    public override void UpdateDisplay()
    {
        if (displayText != null)
        {
            displayText.text = $" {playerNumber} : {playerOneController.playerSpeed:F2}";
        }
    }
}
