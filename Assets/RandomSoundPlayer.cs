using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    public AudioSource source;
    [Range(0f, 1f) ] public float chance;

    private void Update()
    {
        if(Time.timeScale != 0 && !source.isPlaying && Random.value > chance)
        {
            source.Play();
        }
    }
}
