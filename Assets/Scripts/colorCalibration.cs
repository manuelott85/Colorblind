using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colorCalibration : MonoBehaviour {

    Material mat;

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
        GameManager.instance.green_saturation = slider.value;
    }
}
