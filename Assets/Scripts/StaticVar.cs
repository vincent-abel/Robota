using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticVar
{
    public static Camera MainCam;// {get; private set;}
    public static GameObject CamO {get; private set;}
    public static GameObject LightO {get; private set;}
    public static float Volume {get; private set;}

    public static void Init(Transform Main)
    {
        if (!CamO && !Main.Find("Cam")){
       GameObject Cam = new GameObject();
       Cam.AddComponent<AudioListener>();
       Cam.AddComponent<Camera>();
       Cam.name = "Cam";
       Cam.transform.SetParent(Main);
       Cam.transform.position = new Vector3(0,1,-70);
       Cam.GetComponent<Camera>().depth=-1;
       Cam.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
       Cam.GetComponent<Camera>().backgroundColor = new Color(17/255f,23/255f,36/255f,1);
       MainCam = Cam.GetComponent<Camera>();
       CamO=Cam;
        }
        if (!LightO  && !Main.Find("Light")){
       GameObject Light = new GameObject();
       Light.AddComponent<Light>();
       Light.name = "Light";
       Light.transform.SetParent(Main);
       Light.transform.position = new Vector3(0,3,0);
       Light.transform.rotation = Quaternion.Euler(50,-30,0);
       Light.GetComponent<Light>().type = LightType.Directional;
       //Light.GetComponent<Light>().lightmapBakeType = LightmapBakeType.Mixed;
       Light.GetComponent<Light>().shadows = LightShadows.Soft;
       LightO = Light;
        }
       Debug.Log("Awake !"); 
        
    }

    public static float Getvolume() {return Volume;}

    public static float SetVolume(float Volumetoset) {Volume = Volumetoset; return Volume;}

    
}
