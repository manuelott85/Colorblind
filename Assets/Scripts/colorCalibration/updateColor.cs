using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateColor : MonoBehaviour {

    [TextArea(0, 20)]
    [Tooltip("This is just a comment. This parameter is not used in game!")]
    public string ClassDescription = "This component will register the objects' material to the colorCalibration system. Only one option can be set to true! Only the first material is considered";
    
    public bool isItGreen = false;
    public bool isItRed = false;
    public bool isItGrey = false;

    Material mat;

    // Use this for initialization
    void Start () {
        mat = GetComponent<Renderer>().material; // get a reference to the material
    }
	
	// Update is called once per frame
	void Update () {
        mat = GetComponent<Renderer>().material;    // update the reference

        // Apply the parameter of the selected color to the material
        if (isItGreen)
        {
            mat.SetFloat("_Sat", GameManager.instance.green_saturation);
            mat.SetFloat("_HueShift", GameManager.instance.green_hueShift);
            mat.SetFloat("_Val", GameManager.instance.green_value);
        }
        if(isItRed)
        {
            mat.SetFloat("_Sat", GameManager.instance.red_saturation);
            mat.SetFloat("_HueShift", GameManager.instance.red_hueShift);
            mat.SetFloat("_Val", GameManager.instance.red_value);
        }
        if (isItGrey)
        {
            mat.SetFloat("_Val", GameManager.instance.grey_value);
        }
    }
}
