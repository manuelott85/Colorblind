using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateColor : MonoBehaviour {

    Material mat;
    public bool isItGreen = true;

    // Use this for initialization
    void Start () {
        mat = GetComponent<SpriteRenderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
        if (isItGreen)
        {
            mat.SetFloat("_Sat", GameManager.instance.green_saturation);
            mat.SetFloat("_HueShift", GameManager.instance.green_hueShift);
            mat.SetFloat("_Val", GameManager.instance.green_value);
        }
        else
        {
            mat.SetFloat("_Sat", GameManager.instance.red_saturation);
            mat.SetFloat("_HueShift", GameManager.instance.red_hueShift);
            mat.SetFloat("_Val", GameManager.instance.red_value);
        }
    }
}
