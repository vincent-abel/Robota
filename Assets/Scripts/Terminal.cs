using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Terminal
{
    public GameObject Panel;
    public Text console;
    public InputField consoleInput;
    public GameObject mainCanvasGO;


    public Terminal()
    {
        GameObject mainCanvasGO = new GameObject();
        GameObject Panel = new GameObject();
        Panel.name = "Terminal2";      
        mainCanvasGO.name = "mainCanvas";
        Panel.transform.parent = mainCanvasGO.transform;
        mainCanvasGO.AddComponent<Canvas>();
        mainCanvasGO.AddComponent<CanvasScaler>();
        mainCanvasGO.AddComponent<GraphicRaycaster>();
        mainCanvasGO.AddComponent>();
        Panel.AddComponent<RectTransform>();

        Canvas mainCanvas = mainCanvasGO.GetComponent<Canvas>();
        mainCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        

        console = Panel.AddComponent<Text>();
        console.name ="console";
        consoleInput = Panel.AddComponent<InputField>();
        consoleInput.name = "Input";

        RectTransform consoleRT = console.GetComponent<RectTransform>();
        RectTransform cInputRT = consoleInput.GetComponent<RectTransform>();
        RectTransform canvasRT = mainCanvasGO.GetComponent<RectTransform>();

        consoleRT.anchoredPosition3D = new Vector3(0f,0f,0f);
        consoleRT.sizeDelta = new Vector2(0f,0f);
        consoleRT.anchorMin = new Vector2(0f,0f);
        consoleRT.anchorMax = new Vector2(1f,1f);
        console.font = Resources.GetBuiltinResource<Font>("Arial.ttf");

        console.text = "HELLOW WORLD";
        
    }

    public GameObject GetGameObject()
    {
        return this.Panel;
    }
}
