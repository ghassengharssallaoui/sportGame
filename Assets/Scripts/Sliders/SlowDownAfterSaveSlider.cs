using UnityEngine;
using UnityEngine.UI;

public class SlowDownAfterSaveSlider : SliderHandlerBase
{

    public override void InitializeSlider()
    {
        slider.value = ballController.velocityMultiplierOnImpact;
        slider.onValueChanged.AddListener(UpdateValue);
        UpdateDisplay();
    }

    public override void UpdateValue(float value)
    {
        ballController.velocityMultiplierOnImpact = value;
        UpdateDisplay();
    }

    public override void UpdateDisplay()
    {
        if (displayText != null)
        {
            displayText.text = $"Slow Down After Save Multiplier: {ballController.velocityMultiplierOnImpact:F2}";
        }
    }
}
