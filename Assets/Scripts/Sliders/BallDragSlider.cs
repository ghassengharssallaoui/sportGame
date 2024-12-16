using UnityEngine;
using UnityEngine.UI;

public class BallDragSlider : SliderHandlerBase
{
    public override void InitializeSlider()
    {
        slider.value = ballController.gameObject.GetComponent<Rigidbody2D>().drag;
        slider.onValueChanged.AddListener(UpdateValue);
        UpdateDisplay();
    }

    public override void UpdateValue(float value)
    {
        ballController.gameObject.GetComponent<Rigidbody2D>().drag = value;
        UpdateDisplay();
    }

    public override void UpdateDisplay()
    {

        if (displayText != null)
        {
            displayText.text = $"Ball Friction: {ballController.gameObject.GetComponent<Rigidbody2D>().drag:F2}";
        }
    }
}
