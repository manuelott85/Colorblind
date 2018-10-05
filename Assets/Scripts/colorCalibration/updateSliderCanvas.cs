using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateSliderCanvas : MonoBehaviour {

    private enum selectParameter { isSaturation, isHue, isValue };

    [SerializeField]
    private Slider sliderRef = null;

    [SerializeField]
    private selectColor selectedColor = selectColor.isGreen;
    [SerializeField]
    private selectParameter selectedParameter = selectParameter.isSaturation;

    private void OnEnable()
    {
        if(sliderRef)
            StartCoroutine("updateSliderToCurrentValue", sliderRef);
    }

    IEnumerator updateSliderToCurrentValue(Slider slider)
    {
        while (!GameManager.instance)
            yield return null;

        // Update Green
        if (selectedColor == selectColor.isGreen)
        {
            if(selectedParameter == selectParameter.isSaturation)
                slider.value = GameManager.instance.green_saturation;
            if (selectedParameter == selectParameter.isHue)
                slider.value = GameManager.instance.green_hueShift;
            if (selectedParameter == selectParameter.isValue)
                slider.value = GameManager.instance.green_value;
        }

        //Update Red
        if (selectedColor == selectColor.isRed)
        {
            if (selectedParameter == selectParameter.isSaturation)
                slider.value = GameManager.instance.red_saturation;
            if (selectedParameter == selectParameter.isHue)
                slider.value = GameManager.instance.red_hueShift;
            if (selectedParameter == selectParameter.isValue)
                slider.value = GameManager.instance.red_value;
        }
    }
}
