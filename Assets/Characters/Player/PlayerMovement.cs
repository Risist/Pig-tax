using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHolder)), RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    const float minInputValue = 0.1f;

    [Range(0f, 1f)] public float rotationSpeed = 1.0f;
    float lastDesiredRotation = 0f;

    InputHolder inputHolder;
    Rigidbody body;

    void Start()
    {
        inputHolder = GetComponent<InputHolder>();
        body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // fixed update runs event if object is turned off
        if (!enabled)
            return;

        if (inputHolder.positionInput.sqrMagnitude > minInputValue * minInputValue)
        {
            Vector2 v = inputHolder.positionInput.normalized;
            body.AddForce(new Vector3(v.x, 0, v.y) * movementSpeed);

            Vector2 input = inputHolder.positionInput;
            input.x = -input.x;
            lastDesiredRotation = Vector2.SignedAngle(Vector2.up, input);
        }

        float rot = Mathf.LerpAngle(body.rotation.eulerAngles.y, lastDesiredRotation, rotationSpeed);
        body.rotation = Quaternion.Euler(0, rot, 0);
    }
}