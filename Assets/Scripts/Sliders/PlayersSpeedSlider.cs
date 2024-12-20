using UnityEngine;
using UnityEngine.UI;

public class PlayersSpeedSlider : SliderHandlerBase
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private string sliderDisplayText;
    public override void InitializeSlider()
    {
        slider.value = playerController.playerSpeed;
        slider.onValueChanged.AddListener(UpdateValue);
        UpdateDisplay();
    }

    public override void UpdateValue(float value)
    {
        playerController.playerSpeed = value;
        UpdateDisplay();
    }

    public override void UpdateDisplay()
    {
        if (displayText != null)
        {
            displayText.text = $" {sliderDisplayText} : {playerController.playerSpeed:F2}";
        }
    }
}
