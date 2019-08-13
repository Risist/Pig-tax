using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(InputHolder))]
public class PlayerInput : MonoBehaviour
{
    InputHolder input;
    public string positionInputCodeX = "Horizontal";
    public string positionInputCodeY = "Vertical";

    void Start()
    {
        input = GetComponent<InputHolder>();
    }
    private void Update()
    {
        input.positionInput = new Vector2(
            Input.GetAxisRaw(positionInputCodeX),
            Input.GetAxisRaw(positionInputCodeY)).normalized;
    }
    
}
