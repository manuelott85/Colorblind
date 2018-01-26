using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The class purpose is to be a List element. It stores the information needed to control the calibration process.
/// </summary>
public class GameManagerInput
{
    public string inputNameToLookFor;   // the one from the GameManager
    public bool isSet;                  // true, if this input is mapped to something from the input manager
    public string textForCalibration;   // Text that will display the desired behaviour of the player to calibrate that input
    public bool isButton;               // To tell the system if he should accept a button or an axis as a valid input

    public GameManagerInput(string name, bool inputIsSet, string calibrationtext, bool inputIsButton)
    {
        inputNameToLookFor = name;
        isSet = inputIsSet;
        textForCalibration = calibrationtext;
        isButton = inputIsButton;
    }
}

/// <summary>
/// This class purpose is to be a single variable that contains the last moved axis by the player and stores the information if that axis has to be inverted
/// </summary>
public class MoveAxis
{
    public string AxisName;   // the one from the GameManager
    public bool hasToBeInverted;    // true, if this input has to be inverted

    public MoveAxis(string name, bool invert)
    {
        AxisName = name;
        hasToBeInverted = invert;
    }
}

public class calibration : MonoBehaviour {

    public static calibration instance; // singleton class
    [Tooltip("Reference to the calibration canvas")]
    public Canvas canvasInput;
    [Tooltip("The text object that will show the players instructions for the next calibration step")]
    public Text canvasInputText;
    [Tooltip("How much does the player has to move the stick in a direction to be recognized for the calibration process; Has to be between 0 and 1")]
    public float calibrationDeadzone = 0.8f;

    private bool isCalibrationActive = true;   // will skip the whole calibration system if set to false
    public bool getIsCalibrationActive() { return isCalibrationActive; }    // make the variable readable from everywhere
    private bool hasToRecenter = false; // will be set to true, when the player moves an axis out of the deadzone; the calibration process pauses until the player moves the stick back into the deadzone
    private bool hasToReleaseBtn = false; // same as hasToRecenter but for Buttons
    private string joyPrefix = "Joystick";
    private string axisPrefix = "Axis";    // a for loop will create a list of all possible input axes from the input manager with this prefix
    private string buttonPrefix = "Button"; // a for loop will create a list of all possible input buttons from the input manager with this prefix
    private int amountAxes = 28;        // amount of possible controller axes in unity
    private int amountButtons = 20;      // amount of possible buttons in unity
    private List<string> singleInputs;  // list that will contain every axis with its input manager name
    private List<string> singleInputButtons;  // list that will contain every possible button
    private List<GameManagerInput> gmInputs;   // list of all inputs related to the game
    private MoveAxis lastMoveAxis = new MoveAxis("",false); // single variable that contains the last moved axis
    private string lastPushedButton = "";

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        singleInputs = new List<string>();  // create an empty list for all possible axes
        singleInputButtons = new List<string>();    // create an empty List for all possible Buttons
        gmInputs = new List<GameManagerInput>();    // create an empty GM input list

        // Create List of all possible Axes with its input manager name
        for (int i = 1; i < (amountAxes + 1); i++)
        {
            singleInputs.Add(joyPrefix + "1" + axisPrefix + i);
            singleInputs.Add(joyPrefix + "2" + axisPrefix + i);
        }

        // Create List of all possible Buttons with its input manager name
        for (int i = 0; i < (amountButtons); i++)
        {
            singleInputButtons.Add(joyPrefix + "1" + buttonPrefix + i);
            singleInputButtons.Add(joyPrefix + "2" + buttonPrefix + i);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isCalibrationActive)
        {
            //isItSameControllerSetup();
            if (!isItSameControllerSetup())
                resetCalibration(true);
        }

        // Starts the calibration process
        if (isCalibrationActive)
        {
            listenToInput(ref singleInputs, ref singleInputButtons);    // update the lastMoveAxis and lastPushedButton variable if the player moves an axis or pushes a button

            // reset lastPushedButton if no button has been pushed this frame
            if (!Input.anyKey)
                lastPushedButton = "";

            if (!hasToRecenter && !hasToReleaseBtn) // it is somehow a switch, determineing the actual calibration step (between "player has to move an axis" and "player has to realease it again")
            {
                // Iterates through every input needed for the game and checks if it is already set. If not, update the players instruction and wait for input to save it
                foreach (GameManagerInput element in gmInputs)
                {
                    if (!element.isSet)
                    {
                        canvasInputText.text = element.textForCalibration;  // update the canvas text
                        StartCoroutine(checkIfInputChanged(element));   // start the actual process of listening to players input
                        return;
                    }
                    // if all input elements are set, deativate the whole process to safe up system ressources
                    if (element.inputNameToLookFor == gmInputs[(gmInputs.Count - 1)].inputNameToLookFor)
                    {
                        isCalibrationActive = false;
                        canvasInput.enabled = false;

                        // Update the mappings of other scripts
                        //GameManager.instance.pirateA.GetComponent<charMovement>().updateButtons();
                        //GameManager.instance.pirateB.GetComponent<charMovement>().updateButtons();
                        //GameManager.instance.pirateA.GetComponent<paddle>().updateAxes();
                        //GameManager.instance.pirateB.GetComponent<paddle>().updateAxes();

                        // Save the controller names to the PlayerPrefs to compare them later
                        PlayerPrefsX.SetStringArray("controllerNames", Input.GetJoystickNames());
                    }
                }
            }
            else
            {
                if (lastMoveAxis.AxisName == "")   // the other part of the switch mentioned above
                    hasToRecenter = false;
                if (lastPushedButton == "")
                    hasToReleaseBtn = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            foreach (string element in GameManager.instance.getInputsToLookFor())
                PlayerPrefs.DeleteKey(element);
            resetCalibration(true);
        }
    }

    /// <summary>
    /// Wait for a player input and save it to the given input in the game manager
    /// </summary>
    /// <param name="element">the input to look for</param>
    /// <returns></returns>
    IEnumerator checkIfInputChanged(GameManagerInput element)
    {
        if (!element.isButton)
        {
            // If the player havnt move an axis, do nothing; wait until he does
            while (lastMoveAxis.AxisName == "")
                yield return null;
            // If player has moved an axis, save the information in the game manager
            GameManager.instance.setInput(element.inputNameToLookFor, lastMoveAxis.AxisName, lastMoveAxis.hasToBeInverted, true);
            element.isSet = true;   // update the list, that this single input is now set
            hasToRecenter = true;   // tell the system to wait until the player has moved the axis back to its origin
        }
        else
        {
            // If the player havnt pushed a button, do nothing; wait until he does
            while (lastPushedButton == "")
                yield return null;
            // If player has pushed a button, save the information in the game manager
            GameManager.instance.setInput(element.inputNameToLookFor, lastPushedButton, false, true);
            element.isSet = true;
            hasToReleaseBtn = true;
        }
    }

    /// <summary>
    /// Will check every input posibility of the given list, if it has been moved and stores the result in "lastMoveAxis"
    /// </summary>
    /// <param name="singleInputs">List of all the axes from the input manager as a string</param>
    /// <param name="singleInputsButtons">List of all the buttons from the input manager as a string</param>
    private void listenToInput(ref List<string> singleInputs, ref List<string> singleInputsButtons)
    {
        foreach (string element in singleInputs)
        {
            if (Input.GetAxis(element) > calibrationDeadzone)
            {
                lastMoveAxis.AxisName = element;
                lastMoveAxis.hasToBeInverted = false;
            }
            else if (Input.GetAxis(element) < -calibrationDeadzone)
            {
                lastMoveAxis.AxisName = element;
                lastMoveAxis.hasToBeInverted = true;
            }
            else
            {
                lastMoveAxis.AxisName = "";
            }
            if (lastMoveAxis.AxisName != "")
                break;
        }

        foreach (string element in singleInputsButtons)
        {
            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), element)))
            {
                lastPushedButton = element;
            }
        }
    }

    /// <summary>
    /// Reset/restart the calibration process
    /// </summary>
    public void resetCalibration(bool hardReset)
    {
        if (hardReset)
        {
            foreach (GameManagerInput element in gmInputs)
            {
                element.isSet = false;
            }
        }
        canvasInput.enabled = true; // activate the canvas
        isCalibrationActive = true; // activate the calibration process

        string[] joysticks = Input.GetJoystickNames();
        string joysticksWritten = "Controllers: ";
        foreach (string element in joysticks)
        {
            joysticksWritten = joysticksWritten + element;
        }
        Debug.Log(joysticksWritten);
    }

    /// <summary>
    /// Register a new input to the calibration process
    /// </summary>
    /// <param name="nameGameManager">Input Name inside the GameManager</param>
    /// <param name="isSet">if this input is allready set</param>
    /// <param name="textForCalibration">The text to display to the player while calibrating</param>
    /// <param name="isAButton">if the input should look for a button</param>
    public void addInputForCalibration(string nameGameManager, bool isSet, string textForCalibration, bool isAButton)
    {
        gmInputs.Add(new GameManagerInput(nameGameManager, isSet, textForCalibration, isAButton));
    }

    /// <summary>
    /// Determine if the connected controller setup has changed
    /// </summary>
    /// <returns></returns>
    bool isItSameControllerSetup()
    {
        bool returnValue = false;
        
        if (PlayerPrefs.HasKey("controllerNames"))
        {
            try
            {
                string[] controllerNamesNew = Input.GetJoystickNames();
                string[] controllerNamesOld = PlayerPrefsX.GetStringArray("controllerNames");
                if (controllerNamesNew[0] == controllerNamesOld[0])
                    returnValue = true;
            }
            catch (System.Exception)
            {

                
            }
            
        }

        return returnValue;
    }
}
