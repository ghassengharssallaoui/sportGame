using UnityEngine;
using UnityEngine.UI;

public class BallRandomMovementSpeedSlider : SliderHandlerBase
{
    public override void InitializeSlider()
    {
        slider.value = ballController.ballRandomMovementSpeed;
        slider.onValueChanged.AddListener(UpdateValue);
        UpdateDisplay();
    }

    public override void UpdateValue(float value)
    {
        ballController.ballRandomMovementSpeed = value;
        UpdateDisplay();
    }

    public override void UpdateDisplay()
    {

        if (displayText != null)
        {
            displayText.text = $"Ball Random Movement After Goal Speed: {ballController.ballRandomMovementSpeed:F2}";
        }
    }
}
