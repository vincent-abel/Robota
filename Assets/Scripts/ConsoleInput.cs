using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleInput : MonoBehaviour
{
    public Text console;
    public InputField consoleInput;
    public GameObject Rover;
    private bool isValidText = false;
    // Start is called before the first frame update
    void Start()
    {
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
    void Update()
    {

    }

    bool CheckOrder(string Order)
    {
        string[] tmp = Order.Split(' ');
        int num = 0;

        if ((tmp.Length > 2 || tmp.Length < 2) && tmp[0] != "HELP")
        {
            isValidText = false;
            return true;
        }
        else if (tmp[0] == "MOVE")
        {
            isValidText = true;
            if (tmp[1] == "FORWARD")
            {
                Rover.transform.position += Rover.transform.rotation*Vector3.forward;
            }
            else if (tmp[1] == "BACK")
            {
                Rover.transform.position += Rover.transform.rotation*Vector3.back;
            }
            else if (tmp[1] == "LEFT")
            {
                Rover.transform.position += Rover.transform.rotation*Vector3.left;
            }
            else if (tmp[1] == "RIGHT")
            {
                Rover.transform.position += Rover.transform.rotation*Vector3.right;
            }
            else if (int.TryParse(tmp[1], out num))
                Rover.transform.position += Rover.transform.rotation*Vector3.forward*num;

        }
        else if (tmp[0] == "ROTATE")
        {
            isValidText = true;
            if (int.TryParse(tmp[1], out num))
                Rover.transform.rotation = Quaternion.Euler(new Vector3(0, num, 0)) * Rover.transform.rotation;
        }
        else if (tmp[0] == "HELP" && tmp.Length < 2)
        {
            consoleInput.text="";
            isValidText = true;
            SubmitString("Welcome in Help !");
            isValidText = true;
            SubmitString("MOVE XX");
            isValidText = true;
            SubmitString("ROTATE XX");
            return false;
        }
        else
        {
            isValidText = false;
            return true;
        }

        Debug.Log(tmp.Length.ToString());

        return true;
    }

    bool SendCode(string Order)
    {
        console.text += "<color=green>" + Order + "</color>";
        return true;
    }

    bool CheckString(string theString)
    {
        if (isValidText == true)
        {
            isValidText = !isValidText;
            console.text += theString + '\n';
            consoleInput.text = "";
            return true;

        }
        else
        {
            console.text += theString + " => <color=red>Syntax Error : type HELP to see help.</color>\n";
            consoleInput.text = "";
        }
        return false;
    }

    bool SubmitString(string theString)
    {
        if (console.text.Length > 0)
        {

            string[] tmp = console.text.Split('\n');
            if (tmp.Length >= Rglob.GlineCount)
            {
                console.text = "";
                for (int i = 1; i < tmp.Length; i++)
                {
                    if (i == tmp.Length - 1)
                        console.text += tmp[i];
                    else
                        console.text += tmp[i] + '\n';
                }

                CheckString(theString);
            }
            else
            {
                CheckString(theString);
            }

        }
        else
        {
            CheckString(theString);
        }
        return true;
    }


    public void SubmitName()
    {
        if (consoleInput.text == "\n" || consoleInput.text == "")
            return;
        if (CheckOrder(consoleInput.text))
        {
            SubmitString(consoleInput.text);
        }
        consoleInput.text = "";
        consoleInput.ActivateInputField();

    }

}
