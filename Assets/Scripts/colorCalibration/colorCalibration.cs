using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colorCalibration : MonoBehaviour {

    [TextArea(0, 20)]
    [Tooltip("This is just a comment. This parameter is not used in game!")]
    public string ClassDescription = "This component looks for a material of a reference sprite to alter saturation, hue and gamma. The functions that are a part of this component are designed to be called by a sliders' 'on value change' event";

    Material mat;
    public bool isItGreen = false;
    public bool isItRed = false;
    public bool isItGrey = false;

    // Use this for initialization
    void Start () {
        
    }
	
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
        // Only apply the values if there is a valid material
        if (mat == null)
            return;

        // Read out the slider value and apply it onto the according material
        mat.SetFloat("_Sat", slider.value);
        if (isItGreen)
        {
            GameManager.instance.green_saturation = slider.value;
            PlayerPrefs.SetFloat("green_saturation", slider.value);
        }
        if(isItRed)
        {
            GameManager.instance.red_saturation = slider.value;
            PlayerPrefs.SetFloat("red_saturation", slider.value);
        }
    }

    /// <summary>
    /// This function sets the gamma?!; called by the slider on a value change event
    /// </summary>
    /// <param name="slider">Reference to the slider to get the value from</param>
    public void setValue(Slider slider)
    {
        // Only apply the values if there is a valid material
        if (mat == null)
            return;

        // Read out the slider value and apply it onto the according material
        mat.SetFloat("_Val", slider.value);
        if (isItGreen)
        {
            GameManager.instance.green_value = slider.value;
            PlayerPrefs.SetFloat("green_value", slider.value);
        }
        if(isItRed)
        {
            GameManager.instance.red_value = slider.value;
            PlayerPrefs.SetFloat("red_value", slider.value);
        }
        if (isItGrey)
        {
            GameManager.instance.grey_value = slider.value;
            PlayerPrefs.SetFloat("grey_value", slider.value);
        }
    }

    /// <summary>
    /// This function sets the hue; called by the slider on a value change event
    /// </summary>
    /// <param name="slider">Reference to the slider to get the value from</param>
    public void setHueShift(Slider slider)
    {
        // Only apply the values if there is a valid material
        if (mat == null)
            return;

        // Read out the slider value and apply it onto the according material
        mat.SetFloat("_HueShift", slider.value);
        if (isItGreen)
        {
            GameManager.instance.green_hueShift = slider.value;
            PlayerPrefs.SetFloat("green_hueShift", slider.value);
        }
        if (isItRed)
        {
            GameManager.instance.red_hueShift = slider.value;
            PlayerPrefs.SetFloat("red_hueShift", slider.value);
        }
    }
}
