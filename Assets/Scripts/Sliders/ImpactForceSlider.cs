using UnityEngine;
using UnityEngine.UI;

public class ImpactForceSlider : SliderHandlerBase
{
    public override void InitializeSlider()
    {
        slider.value = ballController.impactForce;
        slider.onValueChanged.AddListener(UpdateValue);
        UpdateDisplay();
    }

    public override void UpdateValue(float value)
    {
        ballController.impactForce = value;
        UpdateDisplay();
    }

    public override void UpdateDisplay()
    {
        if (displayText != null)
        {
            displayText.text = $"After Save Added Speed: {ballController.impactForce:F2}";
        }
    }
}
