using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    private float moveSpeed = 1f;
    private float scrollSpeed = 10f;

    float horizontalInput;
    float verticalInput;
    float wheelInput;
    public float sensitivity = 10f;
    public float maxYAngle = 80f;
    private Vector2 currentRotation;
    

    void Start() {
        transform.eulerAngles = new Vector3(20, 180, 0);
        transform.localRotation = Quaternion.Euler(20,180,0);
        transform.rotation = Quaternion.Euler(20,180,0);
        currentRotation.x = transform.rotation.x;
        currentRotation.y = transform.rotation.y;
    }
    void Update() {
        /*      horizontalInput = Input.GetAxisRaw("Horizontal");
              verticalInput = Input.GetAxisRaw("Vertical");
              wheelInput = Input.GetAxis("Mouse ScrollWheel");*/
    }

    void FixedUpdate() {
        
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetAxisRaw("Horizontal") != 0) {
            transform.position += moveSpeed * transform.right * horizontalInput;
        }
        if (Input.GetAxisRaw("Vertical") != 0) {
            transform.position += moveSpeed * transform.forward * verticalInput;
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0) {
            transform.position += scrollSpeed * new Vector3(0, -Input.GetAxis("Mouse ScrollWheel"), 0);
        }
        
            if (Input.GetAxis("Mouse X") != 0) {
                currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
            }
            if (Input.GetAxis("Mouse Y") != 0) {
                currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
            }
            currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
            currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
            transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
        
        Cursor.lockState = CursorLockMode.Locked;
    }
}
