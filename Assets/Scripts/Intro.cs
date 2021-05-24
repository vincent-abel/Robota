using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//[ExecuteInEditMode]
public class Intro : MonoBehaviour {
    public Intro instance { get; private set; }
    public GameObject mainO { get; private set; }

    IEnumerator FadeImageIO(GameObject t, bool fadeAway, float duration) {
        var img = t.GetComponent<Image>();
        img.color = new Color(1, 1, 1, 0);
        // fade from opaque to transparent
        yield return new WaitForSeconds(0.5f);
        for (float i = 0; i <= 1; i += Time.deltaTime / duration) {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            yield return null;

        }
        yield return new WaitForSeconds(2.0f);

        // loop over 1 second backwards
        for (float i = 1; i >= -1; i -= Time.deltaTime) {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }
        Destroy(t, 0f);
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    private void Awake() {
        StaticVar.Init(this.transform);

        Camera Mine = StaticVar.MainCam;
        instance = this;
        Init();
    }

    void Init() {
        mainO = this.transform.gameObject;
        if (!this.transform.Find("Canvas")) AddCanvas(this.transform);
        if (!this.transform.Find("Canvas").Find("Logo")) AddLogo(this.transform.Find("Canvas").transform);
    }



    void AddCanvas(Transform parent) {
        GameObject Canvas = new GameObject();

        Canvas.AddComponent<Canvas>();
        Canvas.AddComponent<RectTransform>();
        var rectTransform = Canvas.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(1, 0);
        rectTransform.anchorMax = new Vector2(0, 1);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);

        Canvas.name = "Canvas";
        Canvas.transform.SetParent(parent);
        
    }

    void AddLogo(Transform parent) {

        GameObject Panel = new GameObject();

        Panel.AddComponent<CanvasRenderer>();
        Panel.name = "Logo";
        Panel.transform.SetParent(parent);
        Panel.AddComponent<Image>();
        var image = Panel.GetComponent<Image>();
        Panel.GetComponent<Image>().sprite = Resources.Load<Sprite>("Logo/CyDevGamesNoBG");
        var test = Panel.GetComponent<Image>().material.color;

        StartCoroutine(FadeImageIO(Panel, false, 2.0f));
        //Destroy(Panel, 20.0f);
    }

    void AddMenu(Transform parent) {
        GameObject Panel = new GameObject();

        Panel.AddComponent<CanvasRenderer>();
        Panel.AddComponent<RectTransform>();
        Panel.name = "Panel";
        Panel.transform.SetParent(parent);
        var panelRectTransform = Panel.GetComponent<RectTransform>();
        Panel.AddComponent<Image>();
        var image = Panel.GetComponent<Image>();
        panelRectTransform.anchorMin = new Vector2(1, 0);
        panelRectTransform.anchorMax = new Vector2(0, 1);
        panelRectTransform.pivot = new Vector2(0.5f, 0.5f);
        image.color = new Color(26, 26, 26, 1);
    }

}
