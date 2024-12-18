using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallImpactWithWallsSlider : SliderHandlerBase
{
    public override void InitializeSlider()
    {
        slider.value = ballController.impactWithWalls;
        slider.onValueChanged.AddListener(UpdateValue);
        UpdateDisplay();
    }

    public override void UpdateValue(float value)
    {
        ballController.impactWithWalls = value;
        UpdateDisplay();
    }

    public override void UpdateDisplay()
    {
        if (displayText != null)
        {
            displayText.text = $"Ball Impact With Walls:  {ballController.impactWithWalls:F2}";
        }
    }
}
