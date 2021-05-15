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
    public GameObject ORover;
    // Start is called before the first frame update


    public IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f) {
        rotating = true;
        Quaternion from = ORover.transform.rotation;
        Quaternion to = ORover.transform.rotation;
        //to *= Quaternion.Euler( axis * angle );
        to *= Quaternion.Euler(axis);

        float elapsed = 0.0f;
        while (elapsed < duration) {
            ORover.transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        ORover.transform.rotation = to;
        rotating = false;
    }

    public IEnumerator Move(int speed) {
        moving = true;
        while (isBack || isForward || isLeft || isRight) {
            if (isBack) ORover.transform.position = Vector3.Lerp(ORover.transform.position, ORover.transform.position + ORover.transform.rotation * ORover.transform.forward * speed, Time.deltaTime);
            if (isForward) ORover.transform.position = Vector3.Lerp(ORover.transform.position, ORover.transform.position + ORover.transform.rotation * -ORover.transform.forward * speed, Time.deltaTime);
            if (isLeft) { StartCoroutine(Rotate(new Vector3(0, -90, 0),-90, 5.0f));
                ORover.transform.position = Vector3.Lerp(ORover.transform.position, ORover.transform.position + ORover.transform.rotation * -ORover.transform.right * speed, Time.deltaTime);
            }
            if (isRight) { StartCoroutine(Rotate(new Vector3(0, 90, 0), 90, 5.0f));
                ORover.transform.position = Vector3.Lerp(ORover.transform.position, ORover.transform.position + ORover.transform.rotation * ORover.transform.right * speed, Time.deltaTime);
            }
            yield return null;
        }
        moving = false;
    }
    /// Override of Move with 2 parameters, Move / Rotating
    public IEnumerator Move(int num, float duration) {
        moving = true;
        float elapsed = 0.0f;
        Vector3 to = ORover.transform.position + ORover.transform.rotation * ORover.transform.forward * num;
        Vector3 from = ORover.transform.position;
        while ((isBack || isForward || isLeft || isRight) && (elapsed < duration)) {
            if (isBack || isForward) ORover.transform.position = Vector3.Slerp(from, to,  elapsed / duration);
            if (isLeft) ORover.transform.position = Vector3.Slerp(from, to,  elapsed / duration);
            if (isRight) ORover.transform.position = Vector3.Slerp(from, to,  elapsed / duration);
             elapsed += Time.deltaTime;
            yield return null;
        }
        moving = false;
    }

    // Update is called once per frame
    void Update() {

    }
}
