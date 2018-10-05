using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    // singleton class
    public static GameManager instance;

    [Header("Color calibration")]
    public float green_saturation = 1;
    public float green_hueShift = 0;
    public float green_value = 1;
    public float red_saturation = 1;
    public float red_hueShift = 0;
    public float red_value = 1;
    public float grey_value = 1;

    public float def_green_saturation = 1;
    public float def_green_hueShift = 0;
    public float def_green_value = 1;
    public float def_red_saturation = 1;
    public float def_red_hueShift = 0;
    public float def_red_value = 1;
    public float def_grey_value = 1;

    [Header("References")]
    public Canvas titleScreen;
    public Transform playerA;
    public Transform playerB;
    public Transform ColorCalibration;
    public Transform lastActiveCheckpoint;
    [Space(5)]
    [SerializeField]
    private ctrlLayoutTemplate[] layoutArray;

    private bool playerA_isAlive = true;
    private bool playerA_hasInformed = false;
    private bool playerB_isAlive = true;
    private bool playerB_hasInformed = false;

    private bool showTitleScreen = true;

    /// <summary>
    /// Return if the requested player is dead
    /// </summary>
    /// <param name="askForPlayerOne">Player One = true; player two = false</param>
    /// <returns>true = is alive</returns>
    public bool getIsAlive(bool askForPlayerOne)
    {
        if (askForPlayerOne)
            return playerA_isAlive;
        else
            return playerB_isAlive;
    }

    /// <summary>
    /// Revive the requested player
    /// </summary>
    /// <param name="revivePlayerOne">Player One = true; player two = false</param>
    public void revivePlayer(bool revivePlayerOne)
    {
        if (revivePlayerOne)
        {
            playerB_isAlive = true;
            playerB_hasInformed = false;
            playerA_isAlive = true;
            playerA_hasInformed = false;
            playerA.transform.gameObject.SetActive(true);
        }
        else
        {
            playerA_isAlive = true;
            playerA_hasInformed = false;
            playerB_isAlive = true;
            playerB_hasInformed = false;
            playerB.transform.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Kill the requested player
    /// </summary>
    /// <param name="killPlayerOne">Player One = true; player two = false</param>
    public void killPlayer(bool killPlayerOne)
    {
        if (killPlayerOne)
        {
            playerA_isAlive = false;
        }
        else
        {
            playerB_isAlive = false;
        }
    }

    // ToDo: replace by an event driven system
    /// <summary>
    /// This function will be called from the dieingsystem.cs after processing the equivalent code
    /// </summary>
    /// <param name="informPlayerOne">Player One = true; player two = false</param>
    public void ackInformation(bool informPlayerOne)
    {
        if (informPlayerOne)
            playerA_hasInformed = true;
        else
            playerB_hasInformed = true;
    }

    // ToDo: replace by an event driven system
    /// <summary>
    /// Return if the requested player has been informed
    /// This function is used by the dieingsystem.cs to process the equivalent code just once
    /// </summary>
    /// <param name="informPlayerOne">Player One = true; player two = false</param>
    /// <returns>true = has informed</returns>
    public bool hasInformed(bool informPlayerOne)
    {
        if (informPlayerOne)
            return playerA_hasInformed;
        else
            return playerB_hasInformed;
    }

    private string[] inputsToLookFor = new string[] { "P1DPad_H", "P1DPad_V", "P1Btn_A", "P1Btn_B", "P2DPad_H", "P2DPad_V", "P2Btn_A", "P2Btn_B" };
    //private string[] inputsToLookFor = new string[] { "P1DPad_H" };
    //private string[] inputsToLookForDavidQuickFix = new string[] { "P1DPad_H", "P1DPad_V", "P1Btn_A", "P1Btn_B"};

    [Header("Gamepad mapping")]
    //public string LS_H = "";
    //    public string LS_H_Text = "Move the LEFT Thumbstick UP";
    ////private bool invertLS_H = false;
    //public string LS_V = "";
    //    public string LS_V_Text = "Move the LEFT Thumbstick RIGHT";
    ////private bool invertLS_V = false;
    //public string RS_H = "";
    //    public string RS_H_Text = "Move the RIGHT Thumbstick UP";
    ////private bool invertRS_H = false;
    //public string RS_V = "";
    //    public string RS_V_Text = "Move the RIGHT Thumbstick RIGHT";
    ////private bool invertRS_V = false;
    public string P1DPad_H = "";
        public string P1DPad_H_Text = "Player 1: Move the DPad UPWARDS";
    //private bool invertDPad_H = false;
    public string P1DPad_V = "";
        public string P1DPad_V_Text = "Player 1: Move the DPad to the RIGHT";
    //private bool invertDPad_V = false;
    public string P2DPad_H = "";
        public string P2DPad_H_Text = "Player 2: Move the DPad UPWARDS";
    //private bool invertDPad_H = false;
    public string P2DPad_V = "";
        public string P2DPad_V_Text = "Player 2: Move the DPad to the RIGHT";
    //private bool invertDPad_V = false;
    public string P1Btn_A = "";
        public string P1Btn_A_Text = "Player 1: Push Button A (XBOX) or CROSS (PS)";
    public string P1Btn_B = "";
        public string P1Btn_B_Text = "Player 1: Push Button B (XBOX) or CIRCLE (PS)";
    public string P2Btn_A = "";
        public string P2Btn_A_Text = "Player 2: Push Button A (XBOX) or CROSS (PS)";
    public string P2Btn_B = "";
        public string P2Btn_B_Text = "Player 2: Push Button B (XBOX) or CIRCLE (PS)";
    //public string Btn_X = "";
    //    public string Btn_X_Text = "Push Button X (XBOX) or SQUARE (PS)";
    //public string Btn_Y = "";
    //    public string Btn_Y_Text = "Push Button Y (XBOX) or TRIANGLE (PS)";
    //public string Btn_LB = "";
    //    public string Btn_LB_Text = "Push LEFT Bumper";
    //public string Btn_RB = "";
    //    public string Btn_RB_Text = "Push RIGHT Bumper";

    [Header ("Debug")]
    [SerializeField]
    [Tooltip("If true: calibrate both player to one controller")]
    private bool onlyOneController = false;
    [SerializeField]
    [Tooltip("If true: the calibration system will not use the hardcoded controller profiles")]
    private bool skipHardcodedCtrlProfiles = false;
    [SerializeField]
    [Tooltip("Print all connected controller names into the debug console")]
    private bool debugShowConnectedCtrlByName = false;

    // Getter and Setter for Input
    public string[] getInputsToLookFor() { return inputsToLookFor; }
    //public string getLS_H() { return LS_H; }
    //public string getLS_V() { return LS_V; }
    //public string getRS_H() { return RS_H; }
    //public string getRS_V() { return RS_V; }
    //public string getDPad_H() { return DPad_H; }
    //public string getDPad_V() { return DPad_V; }
    //public string getBtn_A() { return Btn_A; }
    //public string getBtn_B() { return Btn_B; }
    //public string getBtn_X() { return Btn_X; }
    //public string getBtn_Y() { return Btn_Y; }
    //public string getBtn_LB() { return Btn_LB; }
    //public string getBtn_RB() { return Btn_RB; }
    //public void setLS_H(string newInput) { LS_H = newInput; }
    //public void setLS_V(string newInput) { LS_V = newInput; }
    //public void setRS_H(string newInput) { RS_H = newInput; }
    //public void setRS_V(string newInput) { RS_V = newInput; }
    //public void setDPad_H(string newInput) { DPad_H = newInput; }
    //public void setDPad_V(string newInput) { DPad_V = newInput; }
    //public void setBtn_A(string newInput) { Btn_A = newInput; }
    //public void setBtn_B(string newInput) { Btn_B = newInput; }
    //public void setBtn_X(string newInput) { Btn_X = newInput; }
    //public void setBtn_Y(string newInput) { Btn_Y = newInput; }
    //public void setBtn_LB(string newInput) { Btn_LB = newInput; }
    //public void setBtn_RB(string newInput) { Btn_RB = newInput; }

    // Use this for initialization
    void Start()
    {
        //// QuickFix for David's Controller
        //if (onlyOneController)
        //    inputsToLookFor = inputsToLookForDavidQuickFix;
        check4HardcodedProfil();
        updateColors();
       // if (showTitleScreen)
            titleScreen.enabled = true;
        if (!showTitleScreen)
            titleScreen.enabled = false;
    }

    public void Startgame()
    {
        titleScreen.enabled = false;
        showTitleScreen = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            if(ColorCalibration.gameObject.activeSelf)
                ColorCalibration.gameObject.SetActive(false);
            else
                ColorCalibration.gameObject.SetActive(true);
        }

        if(!playerA_isAlive && !playerB_isAlive)
            Restart();
    }

    void Awake()
    {
        instance = this;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void check4HardcodedProfil()
    {
        // Print all connected controller names into the debug console
        if (debugShowConnectedCtrlByName)
        {
            string[] temp = Input.GetJoystickNames();
            foreach (string element in temp)
                Debug.Log(element);
        }

        // if there is something wrong with the controller layout; skip the profiles
        if (skipHardcodedCtrlProfiles)
        {
            LoadInput(false);   // start the regular calibration process
            return; // skip everything beneath
        }

        bool playerAIsSet = false;
        bool playerBIsSet = false;

        // Check each connected controller if it is a "known" product, if it is: use static values
        // if both players have a valid controller connected, skip manual calibration process
        for (int ctrlIndex = 0; ctrlIndex < Input.GetJoystickNames().Length; ctrlIndex++) // for each controller
        {
            for (int layoutIndex = 0; layoutIndex < layoutArray.Length; layoutIndex++)  // for each layout
            {
                // if connected controller matches the current tested layout
                if (Input.GetJoystickNames()[ctrlIndex] == layoutArray[layoutIndex].controllerName)
                {
                    // Check for PlayerA
                    if (ctrlIndex == 0 && !playerAIsSet)
                    {
                        P1DPad_H = layoutArray[layoutIndex].P1DPad_H;
                        P1DPad_V = layoutArray[layoutIndex].P1DPad_V;
                        P1Btn_A = layoutArray[layoutIndex].P1Btn_A;
                        P1Btn_B = layoutArray[layoutIndex].P1Btn_B;
                        playerAIsSet = true;

                        // if there is only on controller connected and debugging is activated
                        if (Input.GetJoystickNames().Length == 1 && onlyOneController)
                        {
                            P2DPad_H = layoutArray[layoutIndex].P2DPad_H;
                            P2DPad_V = layoutArray[layoutIndex].P2DPad_V;
                            P2Btn_A = layoutArray[layoutIndex].P2Btn_A;
                            P2Btn_B = layoutArray[layoutIndex].P2Btn_B;
                            playerBIsSet = true;
                        }
                    }
                    // Check for PlayerB
                    if (ctrlIndex == 1 && !playerBIsSet)
                    {
                        P2DPad_H = layoutArray[layoutIndex].P1DPad_H.Replace("Joystick1", "Joystick2");
                        P2DPad_V = layoutArray[layoutIndex].P1DPad_V.Replace("Joystick1", "Joystick2");
                        P2Btn_A = layoutArray[layoutIndex].P1Btn_A.Replace("Joystick1", "Joystick2");
                        P2Btn_B = layoutArray[layoutIndex].P1Btn_B.Replace("Joystick1", "Joystick2");
                        playerBIsSet = true;
                    }
                }
            }
        }

        if (playerAIsSet && playerBIsSet)
        {
            calibration.instance.deactivateCalibration();
            updateControlls();
            LoadInput(true);
        }
        else
            LoadInput(false);
    }

    /// <summary>
    /// Read the InputData from the PlayerPrefs and set everything up accordingly
    /// </summary>
    void LoadInput(bool skipCalibration = false)
    {
        foreach(string element in inputsToLookFor)
        {
            bool isSet = false;
            bool isAButton = true;
            string textToShow = "Unknown Input: " + element;
            string[] inputData = new string[2];
            inputData = LoadInputFromPlayerPrefs(element);
            if(inputData[0] != "")
            {
                bool invert;
                if (inputData[1] == "t")
                    invert = true;
                else
                    invert = false;
                if (!skipCalibration)
                    setInput(element, inputData[0], invert, false);

                isSet = true;
            }
            
            if (element == "P1DPad_H")
            {
                textToShow = P1DPad_H_Text;
                isAButton = false;
            }
            if (element == "P1DPad_V")
            {
                textToShow = P1DPad_V_Text;
                isAButton = false;
            }
            if (element == "P2DPad_H")
            {
                textToShow = P2DPad_H_Text;
                isAButton = false;
            }
            if (element == "P2DPad_V")
            {
                textToShow = P2DPad_V_Text;
                isAButton = false;
            }
            if (element == "P1Btn_A")
            {
                textToShow = P1Btn_A_Text;
                isAButton = true;
            }
            if (element == "P1Btn_B")
            {
                textToShow = P1Btn_B_Text;
                isAButton = true;
            }
            if (element == "P2Btn_A")
            {
                textToShow = P2Btn_A_Text;
                isAButton = true;
            }
            if (element == "P2Btn_B")
            {
                textToShow = P2Btn_B_Text;
                isAButton = true;
            }

            GetComponent<calibration>().addInputForCalibration(element, isSet, textToShow, isAButton);

        }
        if (!skipCalibration)
            GetComponent<calibration>().resetCalibration(false);
    }

    /// <summary>
    /// Map an input to a reference
    /// </summary>
    /// <param name="inputReference">Game Manager Reference</param>
    /// <param name="newInput">Input Manager Item Name</param>
    /// <param name="invert">If the Input has to be inverted</param>
    /// <param name="shouldSave">Should the information be stored in the PlayerPrefs</param>
    public void setInput(string inputReference, string newInput, bool invert, bool shouldSave)
    {
        if (inputReference == "P1DPad_H")
        {
            P1DPad_H = newInput;
            //invertLS_H = invert;
        }
        if (inputReference == "P1DPad_V")
        {
            P1DPad_V = newInput;
            //invertLS_V = invert;
        }
        if (inputReference == "P2DPad_H")
        {
            P2DPad_H = newInput;
            //invertRS_H = invert;
        }
        if (inputReference == "P2DPad_V")
        {
            P2DPad_V = newInput;
            //invertRS_V = invert;
        }
        if (inputReference == "P1Btn_A")
        {
            P1Btn_A = newInput;
            //invertDPad_H = invert;
        }
        if (inputReference == "P1Btn_B")
        {
            P1Btn_B = newInput;
            //invertDPad_V = invert;
        }
        if (inputReference == "P2Btn_A")
        {
            P2Btn_A = newInput;
        }
        if (inputReference == "P2Btn_B")
        {
            P2Btn_B = newInput;
        }

        if (shouldSave)
            SaveInputInPlayerPrefs(inputReference, newInput, invert);   // save the Input in PlayerPrefs
    }

    /// <summary>
    /// Save the input information in playerprefs
    /// </summary>
    /// <param name="inputReference">The GameManager Inputname</param>
    /// <param name="newInput">The actual inputname from the Input Manager</param>
    /// <param name="invert">If the Input has to be inverted</param>
    void SaveInputInPlayerPrefs(string inputReference, string newInput, bool invert)
    {
        string[] singleInputArray = new string[2];
        singleInputArray[0] = newInput;
        if (invert)
            singleInputArray[1] = "t";
        else
            singleInputArray[1] = "f";
        PlayerPrefsX.SetStringArray(inputReference, singleInputArray);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Load the input information from playerprefs
    /// </summary>
    /// <param name="inputReference">The Input Name from the GameManager to look for</param>
    /// <returns></returns>
    string[] LoadInputFromPlayerPrefs(string inputReference)
    {
        string[] singleInputArray = new string[2];
        if (PlayerPrefs.HasKey(inputReference))
        {
           singleInputArray = PlayerPrefsX.GetStringArray(inputReference);
        }
        else
        {
            singleInputArray[0] = "";
            singleInputArray[1] = "";
        }

        return singleInputArray;
    }

    public void updateControlls()
    {
        // Player A
        playerA.GetComponent<playerMovementNew>().setForceValueRL(P1DPad_V);
        //playerA.GetComponent<playerMovement>().forceValueUD = P1DPad_H;
        playerA.GetComponent<playerMovementNew>().setForceButtonA(P1Btn_A);

        //// Player B
        playerB.GetComponent<playerMovementNew>().setForceValueRL(P2DPad_V);
        ////playerB.GetComponent<playerMovement>().forceValueUD = P2DPad_H;
        playerB.GetComponent<playerMovementNew>().setForceButtonA(P2Btn_A);
    }

    public void updateColors()
    {
        if (PlayerPrefs.HasKey("green_saturation"))
        {
            green_saturation = PlayerPrefs.GetFloat("green_saturation");
            
        }
        if (PlayerPrefs.HasKey("green_hueShift"))
        {
            green_hueShift = PlayerPrefs.GetFloat("green_hueShift");
        }
        if (PlayerPrefs.HasKey("green_value"))
        {
            green_value = PlayerPrefs.GetFloat("green_value");
        }
        if (PlayerPrefs.HasKey("red_saturation"))
        {
            red_saturation = PlayerPrefs.GetFloat("red_saturation");
        }
        if (PlayerPrefs.HasKey("red_hueShift"))
        {
            red_hueShift = PlayerPrefs.GetFloat("red_hueShift");
        }
        if (PlayerPrefs.HasKey("red_value"))
        {
            red_value = PlayerPrefs.GetFloat("red_value");
        }
        if (PlayerPrefs.HasKey("grey_value"))
        {
            grey_value = PlayerPrefs.GetFloat("grey_value");
        }
    }
}