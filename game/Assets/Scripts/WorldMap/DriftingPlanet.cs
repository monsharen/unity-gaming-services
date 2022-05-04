using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DriftingPlanet : MonoBehaviour
{

    private float start = 1;
    private float distanceUpDown = 10;

    void Awake() {
        start = Random.Range(0, 1f);
    }

    void Start() {
        LeanTween.moveY(gameObject, transform.position.y + distanceUpDown, 1)
            .setLoopPingPong()
            .setEase(LeanTweenType.easeInOutQuad)
            .setDelay(start);
    }
}
