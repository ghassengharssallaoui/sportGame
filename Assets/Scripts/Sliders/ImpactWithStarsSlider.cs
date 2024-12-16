using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactWithStarsSlider : SliderHandlerBase
{
    public override void InitializeSlider()
    {
        slider.value = ballController.impactWithStars;
        slider.onValueChanged.AddListener(UpdateValue);
        UpdateDisplay();
    }

    public override void UpdateValue(float value)
    {
        ballController.impactWithStars = value;
        UpdateDisplay();
    }

    public override void UpdateDisplay()
    {
        if (displayText != null)
        {
            displayText.text = $"Ball Impact With Stars:  {ballController.impactWithStars:F2}";
        }
    }
}
