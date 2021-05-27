using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    GameObject PauseCanvas;
    Canvas MainCanvas;
    Rover Rover;
    InputField InputField;

    // Start is called before the first frame update
    void Start() {
        PauseCanvas = GameObject.Find("PauseCanvas").gameObject;
        MainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        Rover = GameObject.Find("Main").GetComponent<Rover>();
        InputField = GameObject.Find("InputField").GetComponent<InputField>();
        PauseCanvas.SetActive(false);
        MainCanvas.enabled = true;
    }

    void flipActive()
    {
        PauseCanvas.SetActive(!PauseCanvas.activeSelf);
        MainCanvas.enabled = !MainCanvas.enabled;
        InputField.enabled = !InputField.enabled;
    }
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Rglob.gameIsPaused = !Rglob.gameIsPaused;
            PauseGame();
           
        }
    }
    public void ResumeGame() {
        
        if (Rglob.gameIsPaused) {
            Rglob.gameIsPaused = !Rglob.gameIsPaused;
            Debug.Log("In Resume : GamePause ="+Rglob.gameIsPaused);
            PauseGame();
        }
    }
    void PauseGame() {
         flipActive();
        if (Rglob.gameIsPaused) {
          /*  Rover.ResetState();
                
            StopAllCoroutines();*/
            Time.timeScale = 0;
        } else {
            Debug.Log("In PAuse : TimeScale set to 1");
            Time.timeScale = 1;
        }
    }
}
