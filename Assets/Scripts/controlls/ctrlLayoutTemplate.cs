using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "XBoxOne", menuName = "Controller Calibration/Controller Layout", order = 1)]
public class ctrlLayoutTemplate : ScriptableObject
{
    [Header("Name of the Controller")]
    [Tooltip("Enter the name of the controller; activate 'debugShowConnectedCtrlByName' in the GameManager Object to see all valid names of the current machine")]
    public string controllerName = "Controller (Xbox One For Windows)";
    [Header("Regular mapping")]
    public string P1DPad_H = "Joystick1Axis2";
    public string P1DPad_V = "Joystick1Axis1";
    public string P1Btn_A = "Joystick1Button0";
    public string P1Btn_B = "Joystick1Button1";
    [Header("Debug mapping (second player is on the same controller, too)")]
    public string P2DPad_H = "Joystick1Axis5";
    public string P2DPad_V = "Joystick1Axis4";
    public string P2Btn_A = "Joystick1Button2";
    public string P2Btn_B = "Joystick1Button3";
}