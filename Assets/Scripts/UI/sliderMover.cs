using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*========================================================================>
* Moves slider with game pad controller defined in Project Input
*   example: SliderHorizontal uses right joystick on all attached Gamepads
* USE AT YOUR OWN RISK
* Feel free to use and improve.
* T. Womack 5 - 2018 Unity 2018
*  ======================================================================>
*/
// Source: https://forum.unity.com/threads/gamepad-precision-with-sliders.381802/
public class sliderMover : MonoBehaviour
{
    private Slider mySlider;
    private GameObject thisSlider;
    private float sliderChange;
    private float maxSliderValue;
    private float minSliderValue;
    private float sliderRange;
    [SerializeField]
    private float sliderstep = 100.0f; //used to detrime how fine to change value
    private string slidermove = "";

    //Initialize values
    private void Awake()
    {
        slidermove = GameManager.instance.P1DPad_V;
        mySlider = GetComponentInParent<Slider>();
        thisSlider = gameObject; //used to deterine when slider has 'focus'
        maxSliderValue = mySlider.maxValue;
        minSliderValue = mySlider.minValue;
        sliderRange = maxSliderValue - minSliderValue;
    }

    private void Update()
    {
        //If slider has 'focus'
        if (thisSlider == EventSystem.current.currentSelectedGameObject)
        {
            sliderChange = Input.GetAxis(axisName: slidermove) * sliderRange / sliderstep;
            float sliderValue = mySlider.value;
            float tempValue = sliderValue + sliderChange;
            if (tempValue <= maxSliderValue && tempValue >= minSliderValue)
            {
                sliderValue = tempValue;
            }
            mySlider.value = sliderValue;
        }
    }
}