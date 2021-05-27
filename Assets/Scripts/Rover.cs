using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rover : MonoBehaviour {

    [SerializeField] public bool isForward = false;
    [SerializeField] public bool isLeft = false;
    [SerializeField] public bool isRight = false;
    [SerializeField] public bool isBack = false;
    [SerializeField] public bool moving = false;
    [SerializeField] public bool rotating = false;
    public int speed = 10;
    [SerializeField] public GameObject ORover;
    // Start is called before the first frame update

    private void Awake() {
        StopAnim();
    }

    public void ResetState() {
        isForward = isLeft = isRight = isBack = moving = false;
    }

    public IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f) {
        rotating = true;
        if (!moving) StartAnim();
        Quaternion from = ORover.transform.rotation;
        Quaternion to = ORover.transform.rotation;

        to *= Quaternion.Euler(axis);
        float elapsed = 0.0f;

        while ((isLeft || isRight ) && (elapsed < duration)) {
            if (Rglob.gameIsPaused) { yield return null; }
            ORover.transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        ORover.transform.rotation = to;
        rotating = isLeft = isRight = false;
        Rglob.CorRotSave = null;
        if (!moving) StopAnim();
    }

    public void Rotate() {
        Debug.Log("In the rotate");
        if (isLeft) {
            if (Rglob.CorRotSave == null) {
                Rglob.CorRotSave = StartCoroutine(Rotate(new Vector3(0, -90, 0), -90, 5.0f));
            }
        }
        if (isRight) {
            if (Rglob.CorRotSave == null) {
                Rglob.CorRotSave = StartCoroutine(Rotate(new Vector3(0, 90, 0), 90, 5.0f));
            }
        }
    }

    public IEnumerator Move() {
        moving = true;
        StartAnim();
        while (isBack || isForward) {
            if (!Rglob.gameIsPaused) { Debug.Log("Catch Them All"); }
            if (Rglob.gameIsPaused) { yield return null; }
            if (isBack) ORover.transform.position = Vector3.Lerp(ORover.transform.position, ORover.transform.position + ORover.transform.forward * speed, Time.deltaTime);
            if (isForward) ORover.transform.position = Vector3.Lerp(ORover.transform.position, ORover.transform.position + -ORover.transform.forward * speed, Time.deltaTime);

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
        while ((isBack || isForward) && (elapsed < num)) {
            if (Rglob.gameIsPaused) { yield return null; }
            if (Rglob.gameIsPaused) { yield return new WaitForSecondsRealtime(0.1f); }
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
