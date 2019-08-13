using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionBlocker : MonoBehaviour
{
    public static List<Transform> avoidPoints = new List<Transform>();
    public bool avoid = true;
    private void Start()
    {
        if (avoid)
            avoidPoints.Add(transform);
    }

    public static Vector2 ToClosestAvoidPoint(Vector3 myPos)
    {
        Vector3 best = Vector3.zero;
        float bestDistSq = float.PositiveInfinity;

        foreach (var it in avoidPoints)
        {
            float pretDistSq = (it.position - myPos).sqrMagnitude;
            if ( pretDistSq < bestDistSq)
            {
                best = it.position - myPos;
                bestDistSq = pretDistSq;
            }
        }

        return new Vector2(best.x, best.z);
    }

    public static Vector2 ToSecondClosestAvoidPoint(Vector3 myPos)
    {
        Vector3 bestPre = Vector3.zero;
        float bestDistSqPre = float.PositiveInfinity;

        Vector3 best = Vector3.zero;
        float bestDistSq = float.PositiveInfinity;

        foreach (var it in avoidPoints)
        {
            float pretDistSq = (it.position - myPos).sqrMagnitude;
            if (pretDistSq < bestDistSq)
            {
                bestPre = best;
                bestDistSqPre = bestDistSq;
                best = it.position - myPos;
                bestDistSq = pretDistSq;
            }
        }

        return new Vector2(bestPre.x, bestPre.z);
    }

    public static Vector2 ToThirdClosestAvoidPoint(Vector3 myPos)
    {
        Vector3 bestPrePre = Vector3.zero;
        float bestDistSqPrePre = float.PositiveInfinity;

        Vector3 bestPre = Vector3.zero;
        float bestDistSqPre = float.PositiveInfinity;

        Vector3 best = Vector3.zero;
        float bestDistSq = float.PositiveInfinity;

        foreach (var it in avoidPoints)
        {
            float pretDistSq = (it.position - myPos).sqrMagnitude;
            if (pretDistSq < bestDistSq)
            {

                bestPrePre = bestPre;
                bestDistSqPrePre = bestDistSqPre;
                bestPre = best;
                bestDistSqPre = bestDistSq;
                best = it.position - myPos;
                bestDistSq = pretDistSq;
            }
        }

        return new Vector2(bestPrePre.x, bestPrePre.z);
    }
}
