using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleInput : MonoBehaviour
{
    public Text console;
    public InputField consoleInput;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubmitName()
    {
        if (console.text.Length > 0) {
        string [] tmp = console.text.Split('\n');
            if (tmp.Length > 12) {
                console.text= "";
                for (int i=1;i < tmp.Length;i++) 
                {
                    if (i == tmp.Length-1)
                    console.text += tmp[i];
                    else
                    console.text += tmp[i]+'\n';
                }
                console.text += consoleInput.text+'\n';
            } else {
                console.text += consoleInput.text+'\n';
            }     
        } else {
            console.text = consoleInput.text+'\n';
        }
        Debug.Log(consoleInput.text);
        consoleInput.text = "";
    }

}
