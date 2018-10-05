using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateColor : MonoBehaviour {

    [TextArea(0, 20)]
    [Tooltip("This is just a comment. This parameter is not used in game!")]
    public string ClassDescription = "This component will register the objects' material to the colorCalibration system. Only one option can be set to true! Only the first material is considered";

    public selectColor colorSelected;

    Material mat;    

    // Use this for initialization
    void Start () {
        mat = GetComponent<Renderer>().material; // get a reference to the material
    }
	
	// Update is called once per frame
	void Update () {
        mat = GetComponent<Renderer>().material;    // update the reference

        // Apply the parameter of the selected color to the material
        if (colorSelected == selectColor.isGreen)
        {
            //mat.SetFloat("_Sat", GameManager.instance.green_saturation);
            //mat.SetFloat("_HueShift", GameManager.instance.green_hueShift);
            //mat.SetFloat("_Val", GameManager.instance.green_value);

            mat.color = createMatHSV(selectColor.isGreen);
        }
        if(colorSelected == selectColor.isRed)
        {
            //mat.SetFloat("_Sat", GameManager.instance.red_saturation);
            //mat.SetFloat("_HueShift", GameManager.instance.red_hueShift);
            //mat.SetFloat("_Val", GameManager.instance.red_value);

            mat.color = createMatHSV(selectColor.isRed);
        }
        if (colorSelected == selectColor.isGrey)
        {
            //mat.SetFloat("_Val", GameManager.instance.grey_value);
            mat.color = createMatHSV(selectColor.isGrey);
        }
    }

    public Color createMatHSV(selectColor selectedColor)
    {
        float m_Hue = 1f;
        float m_Saturation = 1f;
        float m_Value = 1f;

        if (selectedColor == selectColor.isGreen)
        {
            m_Hue = GameManager.instance.green_hueShift;
            m_Saturation = GameManager.instance.green_saturation;
            m_Value = GameManager.instance.green_value;
        }
        if(selectedColor == selectColor.isRed)
        {
            m_Hue = GameManager.instance.red_hueShift;
            m_Saturation = GameManager.instance.red_saturation;
            m_Value = GameManager.instance.red_value;
        }
        if(selectedColor == selectColor.isGrey)
        {
            //m_Hue = GameManager.instance.grey_hueShift;
            //m_Saturation = GameManager.instance.grey_saturation;
            m_Value = GameManager.instance.grey_value;
        }

        //Create an RGB color from the HSV values
        return Color.HSVToRGB(m_Hue, m_Saturation, m_Value);
    }
}
