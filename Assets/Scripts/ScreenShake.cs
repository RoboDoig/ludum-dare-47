using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    bool shaking = false;
    float shakeDuration;
    float shakeMagnitude;
    float dampingSpeed;
    Vector3 initialPosition;

    public void DoShake(float _shakeDuration, float _shakeMagnitude, float _dampingSpeed) {
        shaking = true;
        shakeDuration = _shakeDuration;
        shakeMagnitude = _shakeMagnitude;
        dampingSpeed = _dampingSpeed;
        initialPosition = transform.position;
    }

    void Update() {
        if (shaking) {    
            if (shakeDuration > 0) {
                transform.position = initialPosition + Random.insideUnitSphere * shakeMagnitude;

                shakeDuration -= Time.deltaTime * dampingSpeed;
            } else {
                shaking = false;
            }
        }
    }
}
