using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateSliderValue : MonoBehaviour {
    
    public bool isItGreen = true;
    public bool hueShift = false;
    public bool value = false;
    public bool saturation = false;

    // Use this for initialization
    void Start () {
        if (isItGreen)
        {
            if (hueShift)
                GetComponent<Slider>().value = GameManager.instance.green_hueShift;
            if (value)
                GetComponent<Slider>().value = GameManager.instance.green_value;
            if (saturation)
                GetComponent<Slider>().value = GameManager.instance.green_saturation;
        }
        else
        {
            if (hueShift)
                GetComponent<Slider>().value = GameManager.instance.red_hueShift;
            if (value)
                GetComponent<Slider>().value = GameManager.instance.red_value;
            if (saturation)
                GetComponent<Slider>().value = GameManager.instance.red_saturation;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
