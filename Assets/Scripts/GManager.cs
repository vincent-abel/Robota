using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    
    private void Awake() => Init();

    public void Init(){
        (GameObject.Find("Main").GetComponent<Rover>()).GMan=this;
        Rglob.Elements.Clear();
        Rglob.Elements.Add("Metals",0);
        Rglob.Elements.Add("Regolith",0);
    }

    public void Parse(){


        /*    foreach (DictionaryEntry entry in UIList) {
                if ((string)entry.Key == value) {
                    ((CanvasGroup)entry.Value).alpha = 1;
                } else ((CanvasGroup)entry.Value).alpha = 0;
            }*/
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
