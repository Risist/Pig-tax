using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    [Range(0f,1f)]public float lerpValue;
    public Vector3 offset;

    private void Update()
    {
        Vector3 myPos = transform.position;
        Vector3 targetPosition = target.position + offset;

        transform.position = myPos * lerpValue + targetPosition * (1 - lerpValue);
    }
}
