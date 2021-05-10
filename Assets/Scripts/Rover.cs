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
    // Start is called before the first frame update


    public IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f) {
        rotating = true;
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;
        //to *= Quaternion.Euler( axis * angle );
        to *= Quaternion.Euler(axis);

        float elapsed = 0.0f;
        while (elapsed < duration) {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = to;
        rotating = false;
    }

    public IEnumerator Move(int speed) {
        moving = true;
        while (isBack || isForward || isLeft || isRight) {
            if (isBack) transform.position = Vector3.Lerp(transform.position, transform.position + transform.rotation * Vector3.back * speed, Time.deltaTime);
            if (isForward) transform.position = Vector3.Lerp(transform.position, transform.position + transform.rotation * Vector3.forward * speed, Time.deltaTime);
            if (isLeft) { StartCoroutine(Rotate(new Vector3(0, -90, 0),-90, 5.0f));
                transform.position = Vector3.Lerp(transform.position, transform.position + transform.rotation * Vector3.left * speed, Time.deltaTime);
            }
            if (isRight) { StartCoroutine(Rotate(new Vector3(0, 90, 0), 90, 5.0f));
                transform.position = Vector3.Lerp(transform.position, transform.position + transform.rotation * Vector3.right * speed, Time.deltaTime);
            }
            yield return null;
        }
        moving = false;
    }
    /// Override of Move with 2 parameters, Move / Rotating
    public IEnumerator Move(int num, float duration) {
        moving = true;
        float elapsed = 0.0f;
        Vector3 to = transform.position + transform.rotation * Vector3.forward * num;
        Vector3 from = transform.position;
        while ((isBack || isForward || isLeft || isRight) && (elapsed < duration)) {
            if (isBack || isForward) transform.position = Vector3.Slerp(from, to,  elapsed / duration);
            if (isLeft) transform.position = Vector3.Slerp(from, to,  elapsed / duration);
            if (isRight) transform.position = Vector3.Slerp(from, to,  elapsed / duration);
             elapsed += Time.deltaTime;
            yield return null;
        }
        moving = false;
    }

    // Update is called once per frame
    void Update() {

    }
}
