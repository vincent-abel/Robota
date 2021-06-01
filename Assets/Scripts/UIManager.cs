using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    Hashtable UIList = new Hashtable();
    InputField InputField;


    void Start() {
        /* Here you all CanvasGroup goes to the main Hashtable */
        UIList.Add("Pause", GameObject.Find("Pause").GetComponent<CanvasGroup>());
        UIList.Add("Main", GameObject.Find("MainCanvas").GetComponent<CanvasGroup>());
        UIList.Add("Obj", GameObject.Find("ObjCanvas").GetComponent<CanvasGroup>());
        UIList.Add("Win", GameObject.Find("Win").GetComponent<CanvasGroup>());
        UIList.Add("Lose", GameObject.Find("GameOver").GetComponent<CanvasGroup>());

        /* The inputfield we need to activate / deactivate when flippings UIs. */
        InputField = GameObject.Find("InputField").GetComponent<InputField>();

        /* Not very proper but good enough to initialize this here. */
        Rglob.instructionText = GameObject.Find("InstructionText").GetComponent<Text>();
        Rglob.ElementsText = GameObject.Find("ElementsText").GetComponent<Text>();
        Rglob.InstructionsCount = 0;
    }

    void Manager(string value) {
        if (value == "Main") {
            // Do Main Thingies;
        }
        if (value == "Pause") {
            if (Rglob.gameIsPaused) {                           // Unpausing Game
                Rglob.gameIsPaused = false;
                PauseUnpause();
                ((CanvasGroup)UIList["Pause"]).alpha = 0;
                ((CanvasGroup)UIList["Main"]).alpha = 1;
                ((CanvasGroup)UIList["Obj"]).alpha = 0;
            } else {                                            // Pausing Game
                Rglob.gameIsPaused = true;
                PauseUnpause();
                ((CanvasGroup)UIList["Pause"]).alpha = 1;
                ((CanvasGroup)UIList["Main"]).alpha = 0;
                ((CanvasGroup)UIList["Obj"]).alpha = 0;
            }
        }
        if (value == "Obj") {                                   // Display Objectives
            if (Rglob.gameIsPaused && (((CanvasGroup)UIList["Obj"]).alpha == 0)) { // IF paused and just sumonned
                ((CanvasGroup)UIList["Pause"]).alpha = 0;
                InputField.DeactivateInputField();
                InputField.text = "";
                ((CanvasGroup)UIList["Main"]).alpha = 1;
                ((CanvasGroup)UIList["Obj"]).alpha = 1;
            } else if (!Rglob.gameIsPaused && (((CanvasGroup)UIList["Obj"]).alpha == 0)) { // or jusst summon but Pause wasn't enforced
                Rglob.gameIsPaused = true;
                PauseUnpause();
                InputField.DeactivateInputField();
                InputField.text = "";
                ((CanvasGroup)UIList["Main"]).alpha = 1;
                ((CanvasGroup)UIList["Obj"]).alpha = 1;
            } else if (Rglob.gameIsPaused && (((CanvasGroup)UIList["Obj"]).alpha == 1)) { //Otherwise Just unpause and leave
                Rglob.gameIsPaused = false;
                PauseUnpause();
                InputField.ActivateInputField();
                ((CanvasGroup)UIList["Obj"]).alpha = 0;
            }
        }
        if (value == "Win" || value == "Lose") {
            foreach (DictionaryEntry entry in UIList) {
                if ((string)entry.Key == value) {
                    ((CanvasGroup)entry.Value).alpha = 1;
                } else ((CanvasGroup)entry.Value).alpha = 0;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (!Rglob.Lose && !Rglob.Win) {
            if (Input.GetKeyDown(KeyCode.Escape)) Manager("Pause");
            if (Input.GetKeyDown(KeyCode.F1)) Manager("Obj");
            if (Input.GetKeyDown(KeyCode.F12)) Manager("Win");
            if (Input.GetKeyDown(KeyCode.F11)) Manager("Lose");
        }
        if (Rglob.Lose) Manager("Lose");
        if (Rglob.Win) Manager("Win");
    }
    public void ResumeGame() => Manager("Pause");
    public void PauseGame() => Manager("Pause");
    public void ObjPanel() => Manager("Obj");
    public void MainPanel() => Manager("Main");

    void PauseUnpause() => Time.timeScale = (Rglob.gameIsPaused) ? 0 : 1;
}

