using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DriftingCamera : MonoBehaviour
{
    private float cameraZ;
    private Vector2 min = new Vector2(-128, -90);
    private Vector2 max = new Vector2(128, 90);
    //private Vector2 yRotatingRange;
    private float lerpSpeed = 0.05f;
    private Vector3 _newPosition;
    //private Quaternion _newRotation;
    
    private void Awake() {
        _newPosition = transform.position;
        cameraZ = transform.position.z;
        //_newRotation = transform.rotation;
        Debug.Log("cameraZ: " + cameraZ);
    }

    private void Update() {
        transform.position = Vector3.Lerp(transform.position, _newPosition, Time.deltaTime * lerpSpeed);
        //transform.rotation = Quaternion.Lerp(transform.rotation, _newRotation, Time.deltaTime * lerpSpeed);

        if (Vector3.Distance(transform.position, _newPosition) < 3f) {
            GetNewPosition();
        }
    }

    void GetNewPosition() {
        float xPos = Random.Range(min.x, max.x);
        float yPos = Random.Range(min.y, max.y);
        Debug.Log("moving camera to " + xPos + ", " + yPos);
        //_newRotation = Quaternion.Euler(0, Random.Range(yRotatingRange.x, yRotatingRange.y), 0);
        _newPosition = new Vector3(xPos, yPos, cameraZ); 
    }
}
