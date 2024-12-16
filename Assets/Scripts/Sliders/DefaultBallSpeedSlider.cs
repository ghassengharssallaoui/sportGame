using UnityEngine;
using UnityEngine.UI;

public class DefaultBallSpeedSlider : SliderHandlerBase
{
    public override void InitializeSlider()
    {
        slider.value = ballController.defaultBallSpeed;
        slider.onValueChanged.AddListener(UpdateValue);
        UpdateDisplay();
    }

    public override void UpdateValue(float value)
    {
        ballController.defaultBallSpeed = value;
        UpdateDisplay();
    }

    public override void UpdateDisplay()
    {
        if (displayText != null)
        {
            displayText.text = $"Default Ball Speed: {ballController.defaultBallSpeed:F2}";
        }
    }
}
