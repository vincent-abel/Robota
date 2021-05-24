using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{

    public void goToMain() 
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void goToIntro()
    {
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }

    public void SetVolume(Slider slider ) {
        Debug.Log(" Value : " + slider.value);
        StaticVar.SetVolume(slider.value);
    }

    
}
