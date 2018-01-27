using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colorCalibration : MonoBehaviour {

    Material mat;
    public bool isItGreen = true;

    // Use this for initialization
    void Start () {
        mat = GetComponent<SpriteRenderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(mat.GetFloat("_HueShift"));
    }

    public void setSaturation(Slider slider)
    {
        mat.SetFloat("_Sat", slider.value);
        if(isItGreen)
            GameManager.instance.green_saturation = slider.value;
        else
            GameManager.instance.red_saturation = slider.value;
    }
    public void setValue(Slider slider)
    {
        mat.SetFloat("_Val", slider.value);
        if (isItGreen)
            GameManager.instance.green_saturation = slider.value;
        else
            GameManager.instance.red_saturation = slider.value;
    }
    public void setHueShift(Slider slider)
    {
        mat.SetFloat("_HueShift", slider.value);
        if (isItGreen)
            GameManager.instance.green_saturation = slider.value;
        else
            GameManager.instance.red_saturation = slider.value;
    }
}
