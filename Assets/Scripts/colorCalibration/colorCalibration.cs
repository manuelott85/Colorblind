using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum selectColor { isGreen, isRed, isGrey };

public class colorCalibration : MonoBehaviour {

    [TextArea(0, 20)]
    [Tooltip("This is just a comment. This parameter is not used in game!")]
    public string ClassDescription = "This component looks for a material of a reference sprite to alter saturation, hue and gamma. The functions that are a part of this component are designed to be called by a sliders' 'on value change' event";

    Material mat;

    [SerializeField]
    private selectColor colorselected;

    // Update is called once per frame
    void Update ()
    {
        // Get a reference to the material
        if (mat == null)
            mat = GetComponent<SpriteRenderer>().material;
    }

    /// <summary>
    /// This function sets the saturation; called by the slider on a value change event
    /// </summary>
    /// <param name="slider">Reference to the slider to get the value from</param>
    public void setSaturation(Slider slider)
    {
        applySaturation(slider.value);
    }

    private void applySaturation(float value)
    {
        // Only apply the values if there is a valid material
        if (mat == null)
            return;

        // Read out the slider value and apply it onto the according material
        mat.SetFloat("_Sat", value);
        if (colorselected == selectColor.isGreen)
        {
            GameManager.instance.green_saturation = value;
            PlayerPrefs.SetFloat("green_saturation", value);
        }
        if (colorselected == selectColor.isRed)
        {
            GameManager.instance.red_saturation = value;
            PlayerPrefs.SetFloat("red_saturation", value);
        }
    }

    /// <summary>
    /// This function sets the gamma?!; called by the slider on a value change event
    /// </summary>
    /// <param name="slider">Reference to the slider to get the value from</param>
    public void setValue(Slider slider)
    {
        applyValue(slider.value);
    }

    private void applyValue(float value)
    {
        // Only apply the values if there is a valid material
        if (mat == null)
            return;

        // Read out the slider value and apply it onto the according material
        mat.SetFloat("_Val", value);
        if (colorselected == selectColor.isGreen)
        {
            GameManager.instance.green_value = value;
            PlayerPrefs.SetFloat("green_value", value);
        }
        if (colorselected == selectColor.isRed)
        {
            GameManager.instance.red_value = value;
            PlayerPrefs.SetFloat("red_value", value);
        }
        if (colorselected == selectColor.isGrey)
        {
            GameManager.instance.grey_value = value;
            PlayerPrefs.SetFloat("grey_value", value);
        }
    }

    /// <summary>
    /// This function sets the hue; called by the slider on a value change event
    /// </summary>
    /// <param name="slider">Reference to the slider to get the value from</param>
    public void setHueShift(Slider slider)
    {
        // Read out the slider value and apply it onto the according material
        applyHueShift(slider.value);
    }

    private void applyHueShift(float value)
    {
        // Only apply the values if there is a valid material
        if (mat == null)
            return;

        mat.SetFloat("_HueShift", value);
        if (colorselected == selectColor.isGreen)
        {
            GameManager.instance.green_hueShift = value;
            PlayerPrefs.SetFloat("green_hueShift", value);
        }
        if (colorselected == selectColor.isRed)
        {
            GameManager.instance.red_hueShift = value;
            PlayerPrefs.SetFloat("red_hueShift", value);
        }
    }

    /// <summary>
    /// Reset the colors to default settings
    /// </summary>
    public void resetColor()
    {
        if (colorselected == selectColor.isGreen)
        {
            applySaturation(GameManager.instance.def_green_saturation);
            applyValue(GameManager.instance.def_green_value);
            applyHueShift(GameManager.instance.def_green_hueShift);
        }
        if (colorselected == selectColor.isRed)
        {
            applySaturation(GameManager.instance.def_red_saturation);
            applyValue(GameManager.instance.def_red_value);
            applyHueShift(GameManager.instance.def_red_hueShift);
        }
    }
}
