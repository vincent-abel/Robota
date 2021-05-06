using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rover : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    public IEnumerator Rotate( Vector3 axis, float angle, float duration = 1.0f)
   {
     Quaternion from = transform.rotation;
     Quaternion to = transform.rotation;
     //to *= Quaternion.Euler( axis * angle );
     to *= Quaternion.Euler( axis );
    
     float elapsed = 0.0f;
     while( elapsed < duration )
     {
       transform.rotation = Quaternion.Slerp(from, to, elapsed / duration );
       elapsed += Time.deltaTime;
       yield return null;
     }
     transform.rotation = to;
   }

    // Update is called once per frame
    void Update() {

    }
}
