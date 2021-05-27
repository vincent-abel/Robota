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


    // Start is called before the first frame update
    void Start() {
        if (RenderSettings.skybox.HasProperty("_Tint"))
            RenderSettings.skybox.SetColor("_Tint", Color.red);
        else if (RenderSettings.skybox.HasProperty("_SkyTint"))
            RenderSettings.skybox.SetColor("_SkyTint", Color.red);
        console.supportRichText = false;
        console.text = "";
        for (int i = 0; i <= 2; i++) { console.text += "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n"; }
        Canvas.ForceUpdateCanvases();
        Rglob.GlineCount = console.cachedTextGenerator.lineCount;
        Debug.Log(Rglob.GlineCount);
        console.text = "";
        console.supportRichText = true;
        consoleInput.ActivateInputField();

    }

    // Update is called once per frame
    void Update() {
        if (!consoleInput.isFocused && !Rglob.gameIsPaused) consoleInput.ActivateInputField();
    }

    bool CheckOrder(string Order) {
        string[] tmp = Order.Split(' ');
        float num = 0.0f;
        Debug.Log(string.Join(",", tmp));
        if ((tmp.Length > 2 || tmp.Length < 2) && tmp[0] != "HELP" && tmp[0] != "STOP") {
            isValidText = false;
            return true;
        } else if (tmp[0] == "MOVE") {
            isValidText = true;
            if (tmp[1] == "FORWARD") { Rover.isForward = true; Rover.isBack = Rover.isLeft = Rover.isRight = false; } else if (tmp[1] == "BACK") { Rover.isBack = true; Rover.isForward = Rover.isLeft = Rover.isRight = false; } else if (tmp[1] == "LEFT") { Rover.isLeft = true; Rover.isBack = Rover.isForward = Rover.isRight = false; } else if (tmp[1] == "RIGHT") { Rover.isRight = true; Rover.isBack = Rover.isForward = Rover.isLeft = false; } else if (tmp[1] == "STOP") {
                Rover.isForward = Rover.isBack = Rover.isLeft = Rover.isRight = false;
                if (Rover.rotating && Rglob.CorRotSave != null) StopCoroutine(Rglob.CorRotSave);
                if (Rover.moving && Rglob.CorMovSave != null) StopCoroutine(Rglob.CorMovSave);
                Rover.StopAnim();
                return true;
            } else if (float.TryParse(tmp[1], out num)) {
                if (num > 0) { Rover.isForward = true; Rover.isBack = Rover.isLeft = Rover.isRight = false; }
                if (num < 0) { Rover.isBack = true; Rover.isForward = Rover.isLeft = Rover.isRight = false; }

                if (Rover.moving && Rglob.CorMovSave != null) StopCoroutine(Rglob.CorMovSave);
                Rglob.CorMovSave = StartCoroutine(Rover.Move(num, 5.0f));
                //Rover.transform.position += Rover.transform.rotation * Vector3.forward * num;
                return true;
            } else { isValidText = false; Debug.Log("Shouldn't be here"); return true; }
            if ((Rover.isBack || Rover.isForward || Rover.isLeft || Rover.isRight) && Rover.moving == false) {
                Rglob.CorMovSave = StartCoroutine(Rover.Move());
            }
        } else if (tmp[0] == "ROTATE") {
            isValidText = true;
            if (Rover.rotating && Rglob.CorRotSave != null)
                StopCoroutine(Rglob.CorRotSave);
            if (float.TryParse(tmp[1], out num)) {
                Rglob.CorRotSave = StartCoroutine(Rover.Rotate(new Vector3(0, num, 0), num, 5.0f));
                return true;
            }
            // Rover.transform.rotation = Quaternion.Euler(new Vector3(0, num, 0)) * Rover.transform.rotation;
        } else if (tmp[0] == "HELP" && tmp.Length < 2) {
            consoleInput.text = "";
            isValidText = true;
            SubmitString("<color=blue>Welcome in Help !");
            isValidText = true;
            SubmitString("MOVE FORWARD|BACK|RIGHT|LEFT");
            isValidText = true;
            SubmitString("MOVE XX");
            isValidText = true;
            SubmitString("MOVE STOP");
            isValidText = true;
            SubmitString("STOP");
            isValidText = true;
            SubmitString("ROTATE XX</color>");
            return false;
        } else if (tmp[0] == "STOP" && tmp.Length < 2) {
            isValidText = true;
            Rover.isForward = Rover.isBack = Rover.isLeft = Rover.isRight = false;
            if (Rover.rotating && Rglob.CorRotSave != null) StopCoroutine(Rglob.CorRotSave); Rglob.CorRotSave = null;
            if (Rover.moving && Rglob.CorMovSave != null) StopCoroutine(Rglob.CorMovSave); Rglob.CorMovSave = null;
            Rover.StopAnim();
            return true;
        } else {
            isValidText = false;
            return true;
        }

        Debug.Log(tmp.Length.ToString());

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

    bool SubmitString(string theString) {
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
                SubmitString(consoleInput.text.ToUpper());
            }
            consoleInput.text = "";
            consoleInput.ActivateInputField();
        } 
    }

}
