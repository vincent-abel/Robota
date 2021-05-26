using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rover : MonoBehaviour {

    public bool isForward = false;
    public bool isLeft = false;
    public bool isRight = false;
    public bool isBack = false;
    public bool moving = false;
    public bool rotating = false;
    public int speed = 10;
    public GameObject ORover;
    // Start is called before the first frame update

    private void Awake() {
        StopAnim();
    }

    public void ResetState()
    {
        isForward = isLeft = isRight = isBack = moving = false;
    }

    public IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f) {
        rotating = true;
        StartAnim();
        Quaternion from = ORover.transform.rotation;
        Quaternion to = ORover.transform.rotation;

        to *= Quaternion.Euler(axis);

        float elapsed = 0.0f;
        while (elapsed < duration) {
           
            ORover.transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        ORover.transform.rotation = to;
        rotating = false;
        StopAnim();
    }

    public IEnumerator Move() {
        moving = true;
        StartAnim();
        while (isBack || isForward || isLeft || isRight) {
            if (!StaticVar.gameIsPaused) {Debug.Log("Catch Them All");}
            if (StaticVar.gameIsPaused) {yield return null;}
            if (isBack) ORover.transform.position = Vector3.Lerp(ORover.transform.position, ORover.transform.position + ORover.transform.forward * speed, Time.deltaTime);
            if (isForward) ORover.transform.position = Vector3.Lerp(ORover.transform.position, ORover.transform.position + -ORover.transform.forward * speed, Time.deltaTime);
            if (isLeft) {
                StartCoroutine(Rotate(new Vector3(0, -90, 0), -90, 5.0f));
                ORover.transform.position = Vector3.Lerp(ORover.transform.position, ORover.transform.position + -ORover.transform.right * speed, Time.deltaTime);
            }
            if (isRight) {
                StartCoroutine(Rotate(new Vector3(0, 90, 0), 90, 5.0f));
                ORover.transform.position = Vector3.Lerp(ORover.transform.position, ORover.transform.position + ORover.transform.right * speed, Time.deltaTime);
            }
            yield return null;
        }
        moving = false;
        StopAnim();
    }

    /// Override of Move with 2 parameters, Move / Rotating
    public IEnumerator Move(float num, float duration) {
        moving = true;
        StartAnim();
        Debug.Log("Moving " + num + "space");
        float elapsed = 0.0f;

        Vector3 to = ORover.transform.position + -ORover.transform.forward * speed;
        Vector3 from = ORover.transform.position;
        Debug.Log("back: " + isBack + " forward: " + isForward);
        while ((isBack || isForward || isLeft || isRight) && (elapsed < num)) {
           if(StaticVar.gameIsPaused) {yield return new WaitForSecondsRealtime(0.1f);}
            ORover.transform.position = Vector3.Lerp(ORover.transform.position, ORover.transform.position + -ORover.transform.forward * speed, Time.deltaTime);
            elapsed += Time.deltaTime;
            Debug.Log("kikoo" + elapsed);
            yield return null;
        }
        moving = false;
        StopAnim();
    }

    public void StartAnim() {
        Debug.Log("Starting Anim");
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L").GetComponent<Animator>().enabled = true;
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L.002").GetComponent<Animator>().enabled = true;
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L.003").GetComponent<Animator>().enabled = true;
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L.004").GetComponent<Animator>().enabled = true;
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L.005").GetComponent<Animator>().enabled = true;
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L.006").GetComponent<Animator>().enabled = true;
    }

    public void StopAnim() {
        Debug.Log("Stopping Anim");
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L").GetComponent<Animator>().enabled = false;
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L.002").GetComponent<Animator>().enabled = false;
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L.003").GetComponent<Animator>().enabled = false;
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L.004").GetComponent<Animator>().enabled = false;
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L.005").GetComponent<Animator>().enabled = false;
        ORover.transform.Find("RoverBody/RoverWheels.L/RoverWheelFront.L.006").GetComponent<Animator>().enabled = false;
    }
    // Update is called once per frame
    void Update() {
    }
}
