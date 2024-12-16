using UnityEngine;
using UnityEngine.UI;

public abstract class SliderHandlerBase : MonoBehaviour
{
    protected Slider slider;
    protected Text displayText;
    protected BallController ballController;
    private void Start()
    {
        Setup();
        InitializeSlider(); // Call the derived class method to initialize slider-specific behavior
    }
    public virtual void Setup()
    {
        ballController = FindObjectOfType<BallController>();
        slider = GetComponent<Slider>();
        displayText = GetComponentInChildren<Text>();

        if (slider == null)
        {
            Debug.LogError($"{nameof(Slider)} component not found on {gameObject.name}");
        }

        if (displayText == null)
        {
            Debug.LogError($"{nameof(Text)} component not found in children of {gameObject.name}");
        }
    }

    public abstract void InitializeSlider(); // To be implemented in derived classes
    public abstract void UpdateValue(float value); // To be implemented in derived classes
    public abstract void UpdateDisplay(); // To be implemented in derived classes
}
