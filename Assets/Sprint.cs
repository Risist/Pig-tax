using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour
{
    public Timer sprintCd;
    public Timer sprintTime;
    public float sprintMovementSpeed;
    float oldSpeed;
    new public AudioSource audio;
    PlayerMovement movement;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        oldSpeed = movement.movementSpeed;
    }

    private void Update()
    {
        if (!audio.isPlaying && Input.GetKeyDown(KeyCode.LeftShift) && sprintTime.IsReady() && sprintCd.IsReadyRestart())
        {
            audio.Play();
            sprintTime.Restart();
        }

        movement.movementSpeed = audio.isPlaying && !sprintTime.IsReady() ? sprintMovementSpeed : oldSpeed;

    }
}
