using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[System.Serializable]
public class Rover : MonoBehaviour {

    [SerializeField] public bool isForward = false;
    [SerializeField] public bool isLeft = false;
    [SerializeField] public bool isRight = false;
    [SerializeField] public bool isBack = false;
    [SerializeField] public bool moving = false;
    [SerializeField] public bool rotating = false;
    [SerializeField] public bool isGathering = false;
    public int speed = 10;
    List<WheelCollider> Wheels = new List<WheelCollider>();
    [SerializeField] public GameObject ORover;
    [SerializeField] public TerrainTools TTools;
    [SerializeField] public string[] TerrainName = {"Metal","Regolith"};
    // Start is called before the first frame update

    private void Awake() {
        StopWheelAnim();
        StopGatherAnim();
    }

    void Start() {
        Wheels.Add(GameObject.Find("RoverWheelFront.L").GetComponent<WheelCollider>());
        Wheels.Add(GameObject.Find("RoverWheelFront.L.002").GetComponent<WheelCollider>());
        Wheels.Add(GameObject.Find("RoverWheelFront.L.003").GetComponent<WheelCollider>());
        Wheels.Add(GameObject.Find("RoverWheelFront.L.004").GetComponent<WheelCollider>());
        Wheels.Add(GameObject.Find("RoverWheelFront.L.005").GetComponent<WheelCollider>());
        Wheels.Add(GameObject.Find("RoverWheelFront.L.006").GetComponent<WheelCollider>());
        TTools.SetPlayer(ORover.transform);
        TTools.SetTerrain(GameObject.Find("Terrain").GetComponent<Terrain>());
    }
    /// Settings states
    public void forward() {     isForward = true;   isBack = false;     Move();     }
    public void back() {        isBack = true;      isForward = false;  Move();     }
    public void right() {       isRight = true;     isLeft = false;     Rotate();   }
    public void left() {        isLeft = true;      isRight = false;    Rotate();   }
    public void ResetState() {  isForward = isLeft = isRight = isBack = moving = false; }

    /// Stopping movements
    public void Stop() {
        ResetState();
        if (rotating) StopRot();
        if (moving) StopMov();
        StopWheelAnim();
        StopGatherAnim();
    }
    /// implementation for global movement stopping
    public void StopMov() {
        if (Rglob.CorMovSave != null) {
            StopCoroutine(Rglob.CorMovSave);
            Rglob.CorMovSave = null;
        }
    }
    /// implementation for global rotation stopping
    public void StopRot() {
        if (Rglob.CorRotSave != null) {
            StopCoroutine(Rglob.CorRotSave);
            Rglob.CorRotSave = null;
        }
    }
    /// Rotate() and his sister Rotate(Vector3 axis, float angle, float duration = 1.0f)
    public void Rotate() {
        if (rotating) StopRot();

        if (isLeft) Rglob.CorRotSave = StartCoroutine(cRotate(new Vector3(0, -90, 0), -90, 5.0f));
        if (isRight) Rglob.CorRotSave = StartCoroutine(cRotate(new Vector3(0, 90, 0), 90, 5.0f));
    }
    public void Rotate(Vector3 axis, float angle, float duration = 1.0f) {
        if (rotating) StopRot();
        isLeft = true;
        Rglob.CorRotSave = StartCoroutine(cRotate(axis, angle, duration));
    }
    /// Move() and his sister Move(float num, float duration)
    public void Move() {
        if (moving) StopMov();
        Rglob.CorMovSave = StartCoroutine(cMove());
    }
    public void Move(float num, float duration) {
        if (moving) StopMov();
        if (num > 0) forward();
        if (num < 0) back();
        Rglob.CorMovSave = StartCoroutine(cMove(num, duration));
    }

    public void Gather() 
    {
        if (!isGathering) {
        isGathering=true;
        Stop();
        StartGatherAnim();
        Debug.Log("First implementation, Gathering intel");
        TTools.GetTerrainTexture();
        var iOfA=ArrayUtility.IndexOf(TTools.textureValues,TTools.textureValues.Max());
        GameObject.Find("Logs").GetComponent<Text>().text="Log : Found Terrain "+TerrainName[iOfA].ToString();
        //StopGatherAnim();
        isGathering=!isGathering;
        }
    }


    /// Various Coroutines Implementations

        
    public IEnumerator WaitInit() {
        yield return new WaitForSeconds(5.0f);
        Rglob.WaitforLanding = true;
    }

    public IEnumerator cRotate(Vector3 axis, float angle, float duration = 1.0f) {
        StopGatherAnim();
        rotating = true;
        if (!moving) StartWheelAnim();
        Quaternion from = ORover.transform.rotation;
        Quaternion to = ORover.transform.rotation;

        to *= Quaternion.Euler(axis);
        float elapsed = 0.0f;

        while ((isLeft || isRight) && (elapsed < duration)) {
            if (Rglob.gameIsPaused) { yield return null; }
            ORover.transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        ORover.transform.rotation = to;
        rotating = isLeft = isRight = false;
        Rglob.CorRotSave = null;
        if (!moving) StopWheelAnim();
    }

    public IEnumerator cMove() {
        StopGatherAnim();
        moving = true;
        StartWheelAnim();
        while (isBack || isForward) {
            //if (!Rglob.gameIsPaused) { Debug.Log("Catch Them All"); }
            if (Rglob.gameIsPaused) { yield return null; }
            if (isBack) ORover.transform.position = Vector3.Lerp(ORover.transform.position, ORover.transform.position + ORover.transform.forward * speed, Time.deltaTime);
            if (isForward) ORover.transform.position = Vector3.Lerp(ORover.transform.position, ORover.transform.position + -ORover.transform.forward * speed, Time.deltaTime);

            yield return null;
        }
        moving = false;
        StopWheelAnim();
    }

    /// Override of Move with 2 parameters, Move / Rotating
    public IEnumerator cMove(float num, float duration) {
        StopGatherAnim();
        moving = true;
        StartWheelAnim();
        //Debug.Log("Moving " + num + "space");
        float elapsed = 0.0f;

        Vector3 to = ORover.transform.position + -ORover.transform.forward * speed;
        Vector3 from = ORover.transform.position;
        //Debug.Log("back: " + isBack + " forward: " + isForward);
        while ((isBack || isForward) && (elapsed < num)) {
            if (Rglob.gameIsPaused) { yield return null; }
            if (Rglob.gameIsPaused) { yield return new WaitForSecondsRealtime(0.1f); }
            ORover.transform.position = Vector3.Lerp(ORover.transform.position, ORover.transform.position + -ORover.transform.forward * speed, Time.deltaTime);
            elapsed += Time.deltaTime;
            //Debug.Log("kikoo" + elapsed);
            yield return null;
        }
        moving = false;
        StopWheelAnim();
    }

    public void StartWheelAnim() {
        //Debug.Log("Starting Anim");
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L").GetComponent<Animator>().enabled = true;
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L.002").GetComponent<Animator>().enabled = true;
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L.003").GetComponent<Animator>().enabled = true;
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L.004").GetComponent<Animator>().enabled = true;
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L.005").GetComponent<Animator>().enabled = true;
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L.006").GetComponent<Animator>().enabled = true;
    }

    public void StopWheelAnim() {
        //Debug.Log("Stopping Anim");
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L").GetComponent<Animator>().enabled = false;
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L.002").GetComponent<Animator>().enabled = false;
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L.003").GetComponent<Animator>().enabled = false;
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L.004").GetComponent<Animator>().enabled = false;
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L.005").GetComponent<Animator>().enabled = false;
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L.006").GetComponent<Animator>().enabled = false;
        
    }

    public void StartGatherAnim() {
        /*GameObject.Find("RoverArmLower").GetComponent<Animator>().enabled = true;
        GameObject.Find("RoverArmUpper").GetComponent<Animator>().enabled = true;
        GameObject.Find("RoverArmDrill").GetComponent<Animator>().enabled = true;*/
        GameObject.Find("RoverArmConnector").GetComponent<Animator>().enabled = true;
        GameObject.Find("RoverArmConnector").GetComponent<Animator>().Play("Gather");
        
//        GameObject.Find("RoverArmUpper").GetComponent<Animator>().Play("Gather");
 //       GameObject.Find("RoverArmDrill").GetComponent<Animator>().Play("Gather");
        
    }

    public void StopGatherAnim() {
        GameObject.Find("RoverArmConnector").GetComponent<Animator>().enabled = false;
//        GameObject.Find("RoverArmUpper").GetComponent<Animator>().enabled = false;
//        GameObject.Find("RoverArmDrill").GetComponent<Animator>().enabled = false;
    }
    // Update is called once per frame
    void Update() {

    }
    void FixedUpdate() {
        //    Debug.Log("Waiting:"+Rglob.WaitforLanding);
        if (Rglob.WaitforLanding) {
            var canifly = 0;
            WheelHit hit = new WheelHit();

            foreach (WheelCollider wc in Wheels) {

               // Debug.Log(wc.isGrounded + " " + wc.radius);
                if (wc.GetGroundHit(out hit))
                    canifly++;
            }
            //Debug.Log(canifly);
            if (canifly <= 3) {
                Rglob.Lose = true;// Rglob.Lose=true;

            }
        } else {
            //Debug.Log("CorRotWrapper:"+Rglob.CorRotWrapper);
            if (Rglob.CorRotWrapper == null)
                Rglob.CorRotWrapper = StartCoroutine(WaitInit());
        }
    }
}
