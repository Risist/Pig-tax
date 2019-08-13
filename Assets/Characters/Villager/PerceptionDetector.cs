using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionDetector : MonoBehaviour
{
    public Timer paniqueTimer;
    [System.NonSerialized] public Vector3 lastPlayerPosition;

    [Range(0f,1f)] public float yellChance;
    public AudioSource yell;
    float yellPitch;

    public float visionRadius = 10f;
    Transform player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        paniqueTimer.Restart();
        paniqueTimer.actualTime -= paniqueTimer.cd;
        yellPitch = Random.Range(0.8f, 1.2f);

    }

    int seenCount = 0;
    void Update()
    {
        if(IsPlayerInVisionRadius() && !IsVisionBlocked())
        {
            if(yell && !yell.isPlaying && Random.value > yellChance)
            {
                yell.pitch = yellPitch;
                yell.Play();
            }

            paniqueTimer.Restart();
            lastPlayerPosition = player.position;
        }
            
    }

    bool IsPlayerInVisionRadius()
    {
        Vector3 diff = transform.position - player.position;
        diff.y = 0;

        return diff.sqrMagnitude < visionRadius * visionRadius;
    }
    bool IsVisionBlocked()
    {
        //Debug.Log(transform.root.position);

        Vector3 diff = player.position - transform.parent.position;
        var colliders = Physics.RaycastAll(transform.parent.position, diff, diff.magnitude);
        foreach(var it in colliders)
        {
            if (it.collider.GetComponent<VisionBlocker>())
            {
                Debug.DrawRay(transform.parent.position, diff, Color.blue);
                return true;
            }
        }
        Debug.DrawRay(transform.parent.position, diff, Color.red);
        return false;
    }


}
