using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Terminal {
    public GameObject Panel;
    public Text console;
    public InputField consoleInput;
    public GameObject mainCanvasGO;


    public Terminal() {
        GameObject mainCanvasGO = new GameObject();
        GameObject Panel = new GameObject();
        GameObject consoleGO = new GameObject();
        GameObject consoleInputGO = new GameObject();

        Panel.name = "Terminal2";
        mainCanvasGO.name = "mainCanvas";
        Panel.transform.parent = mainCanvasGO.transform;
        mainCanvasGO.AddComponent<Canvas>();
        mainCanvasGO.AddComponent<CanvasScaler>();
        mainCanvasGO.AddComponent<GraphicRaycaster>();
        Panel.AddComponent<CanvasRenderer>();
        Panel.AddComponent<RectTransform>();

        Canvas mainCanvas = mainCanvasGO.GetComponent<Canvas>();
        mainCanvas.renderMode = RenderMode.ScreenSpaceOverlay;


        console = consoleGO.AddComponent<Text>();
        console.name = "console";
        console.transform.parent = Panel.transform;
        consoleGO.AddComponent<CanvasRenderer>();
        consoleInput = consoleInputGO.AddComponent<InputField>();
        consoleInput.name = "Input";
        consoleInputGO.AddComponent<CanvasRenderer>();

        consoleInput.transform.parent = Panel.transform;

        RectTransform consoleRT = console.GetComponent<RectTransform>();
        RectTransform cInputRT = consoleInput.GetComponent<RectTransform>();
        RectTransform canvasRT = mainCanvasGO.GetComponent<RectTransform>();
        RectTransform PanelRT = Panel.GetComponent<RectTransform>();


        consoleRT.anchoredPosition3D = new Vector3(0f, 0f, 0f);
        consoleRT.sizeDelta = new Vector2(0f, 0f);
        consoleRT.anchorMin = new Vector2(0f, 0f);
        consoleRT.anchorMax = new Vector2(1f, 1f);
        PanelRT.anchoredPosition3D = new Vector3(0f, 0f, 0f);
        PanelRT.sizeDelta = new Vector2(0f, 0f);
        PanelRT.anchorMin = new Vector2(0f, 0f);
        PanelRT.anchorMax = new Vector2(1f, 1f);
        console.font = Resources.GetBuiltinResource<Font>("Arial.ttf");

        console.text = "HELLOW WORLD";

    }

    public GameObject GetGameObject() {
        return this.Panel;
    }
}
