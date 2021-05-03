using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleInput : MonoBehaviour
{
    public Text console;
    public InputField consoleInput;
    public GameObject Rover;
    // Start is called before the first frame update
    void Start()
    {
        console.supportRichText =false;
        for (int i=0;i<=2;i++) {console.text += "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";}
        Canvas.ForceUpdateCanvases();
        Rglob.GlineCount =console.cachedTextGenerator.lineCount;
        Debug.Log(Rglob.GlineCount);
        console.supportRichText=true;
        consoleInput.ActivateInputField();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool CheckOrder(string Order)
    {
        string[] tmp = Order.Split(' ');
        int num=0;

        if (tmp.Length > 2 || tmp.Length < 2)
        {
            console.text += "Syntax ERROR"+'\n';
            consoleInput.text="";
            return false;
        }
        if (tmp[0] == "MOVE") 
        {

            if (int.TryParse(tmp[1],out num))
                Rover.transform.position = Rover.transform.position + new Vector3(num,0,0);
            
        } 
        else if (tmp[0] == "ROTATE")
        {
            if (int.TryParse(tmp[1],out num))
                Rover.transform.position = Rover.transform.position + new Vector3(num,0,0);
        }
        else 
        {
            console.text += "WTF are you Doing ?"+'\n';
            consoleInput.text="";
            return false; 
        }

        Debug.Log(tmp.Length.ToString());

        return true;
    }

    bool SendCode(string Order)
    {
        console.text += "<color=green>"+Order+"</color>";
        return true;
    }

    public void SubmitName()
    {
        if (SendCode(consoleInput.text))
        {
            if (console.text.Length > 0)
            {

                string[] tmp = console.text.Split('\n');
                if (tmp.Length != console.cachedTextGenerator.lineCount)
                {
                    console.text = "";
                    for (int i = 1; i < tmp.Length; i++)
                    {
                        if (i == tmp.Length - 1)
                            console.text += tmp[i];
                        else
                            console.text += tmp[i] + '\n';
                    }
                    console.text += consoleInput.text + '\n';
                }
                else
                {
                    console.text += consoleInput.text + '\n';
                }

            }
            else
            {
                console.text = consoleInput.text + '\n';
            }
        }
        //Debug.Log(consoleInput.text);
        consoleInput.text = "";
        consoleInput.ActivateInputField();
    }

}
