using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colorCalibration : MonoBehaviour {

    Material mat;
    public bool isItGreen = false;
    public bool isItRed = false;
    public bool isItGrey = false;

    // Use this for initialization
    void Start () {
        mat = GetComponent<SpriteRenderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(mat.GetFloat("_HueShift"));
        if (mat == null)
            mat = GetComponent<SpriteRenderer>().material;
    }

    public void setSaturation(Slider slider)
    {
        if (mat == null)
            return;

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
    public void setValue(Slider slider)
    {
        if (mat == null)
            return;

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
    public void setHueShift(Slider slider)
    {
        if (mat == null)
            return;

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
