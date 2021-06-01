using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleInput : MonoBehaviour {
    public Text console;
    public InputField consoleInput;
    public Rover Rover;
    public int speed = 10;
    private bool isValidText = false;

    void SetSkyBox() {
        if (RenderSettings.skybox.HasProperty("_Tint"))
            RenderSettings.skybox.SetColor("_Tint", Color.red);
        else if (RenderSettings.skybox.HasProperty("_SkyTint"))
            RenderSettings.skybox.SetColor("_SkyTint", Color.red);
    }

    void CalculateLine() {
        console.supportRichText = false;
        console.text = "";
        for (int i = 0; i <= 2; i++) { console.text += "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n"; }
        Canvas.ForceUpdateCanvases();
        Rglob.GlineCount = console.cachedTextGenerator.lineCount;
        console.text = "";
        console.supportRichText = true;
        consoleInput.ActivateInputField();
    }
    
    // Start is called before the first frame update
    void Start() {
        SetSkyBox();
        CalculateLine();
    }

    // Update is called once per frame
    void Update() {
        if (!consoleInput.isFocused && !Rglob.gameIsPaused) consoleInput.ActivateInputField();
    }

    bool CheckOrder(string Order) {
        string[] tmp = Order.Split(' ');
        float num = 0.0f;
        isValidText=true;
        if ((tmp.Length > 2 || tmp.Length < 2) && tmp[0] != "HELP" && tmp[0] != "STOP" && tmp[0] != "GATHER") {
            isValidText = false;
        } else if (tmp[0] == "MOVE") {
            if (tmp[1] == "FORWARD") Rover.forward();
            else if (tmp[1] == "BACK") Rover.back();
            else if (tmp[1] == "LEFT") Rover.left();
            else if (tmp[1] == "RIGHT") Rover.right();
            else if (tmp[1] == "STOP") Rover.Stop();
            else if (float.TryParse(tmp[1], out num)) Rover.Move(num, 5.0f);
            else isValidText = false;
        } else if (tmp[0] == "ROTATE") {
            if (float.TryParse(tmp[1], out num)) {
                Rover.Rotate(new Vector3(0, num, 0), num, 5.0f);
            }
        } else if (tmp[0] == "HELP" && tmp.Length < 2) {
            consoleInput.text = "";
            SubmitString("<color=blue>Welcome in Help !\nMOVE FORWARD|BACK|RIGHT|LEFT\nMOVE XX\nMOVE STOP\nSTOP\nROTATE XX\nGATHER</color>");
            return false;
        } else if (tmp[0] == "STOP" && tmp.Length < 2) {
            Rover.Stop();
        } else if (tmp[0] == "GATHER" && tmp.Length <2) {
            Rover.Gather();
        } else {
            isValidText = false;
        }
        return true;
    }

    void SendCode(string Order) => console.text += "<color=green>" + Order + "</color>";

    bool CheckString(string theString) {
        if (isValidText == true) {
            isValidText = !isValidText;
            console.text += theString + '\n';
            consoleInput.text = "";
            return true;

        } else {
            console.text += theString + " => <color=red>Syntax Error : type HELP to see help.</color>\n";
            consoleInput.text = "";
        }
        return false;
    }

    public bool SubmitString(string theString) {
        if (console.text.Length > 0) {

            string[] tmp = console.text.Split('\n');
            if (tmp.Length >= Rglob.GlineCount) {
                console.text = "";
                for (int i = 1; i < tmp.Length; i++) {
                    if (i == tmp.Length - 1)
                        console.text += tmp[i];
                    else
                        console.text += tmp[i] + '\n';
                }

                CheckString(theString);
            } else {
                CheckString(theString);
            }

        } else {
            CheckString(theString);
        }
        return true;
    }


    public void SubmitName() {
        if (!Rglob.gameIsPaused) {
            if (consoleInput.text == "\n" || consoleInput.text == "")
                return;

            if (CheckOrder(consoleInput.text.ToUpper())) {
                Rglob.InstructionsCount++;
                SubmitString(consoleInput.text.ToUpper());
            }
            consoleInput.text = "";
            consoleInput.ActivateInputField();
        }
    }

}
