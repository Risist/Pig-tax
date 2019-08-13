using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHolder))]
public class VillagerInput : MonoBehaviour
{
    public GameObject dangerIcon;
    public bool canBeAccomondated = true;
    [System.NonSerialized] public InputHolder input;
    [System.NonSerialized] public PerceptionDetector perception;



    void Start()
    {
        input = GetComponent<InputHolder>();
        perception = GetComponentInChildren<PerceptionDetector>();

    }

    // Update is called once per frame
    void Update()
    {
        bool panique = perception.paniqueTimer.IsReady();
        dangerIcon.SetActive(!panique);
        dangerIcon.transform.LookAt(Camera.main.transform);

        if (panique)
        {
            BehaviourNormal();
        }else
        {
            BehaviourPanique();
        }
        //BehaviourNormal();


        /*Vector2 v = VisionBlocker.ToClosestAvoidPoint(transform.position).normalized*6;
        Debug.DrawLine(transform.position, transform.position + new Vector3(v.x, 0, v.y), Color.gray);

        v = VisionBlocker.ToSecondClosestAvoidPoint(transform.position).normalized * 4;
        Debug.DrawLine(transform.position, transform.position + new Vector3(v.x, 0, v.y), Color.gray);

        v = VisionBlocker.ToThirdClosestAvoidPoint(transform.position).normalized * 2.5f;
        Debug.DrawLine(transform.position, transform.position + new Vector3(v.x, 0, v.y), Color.gray);

        v = ToClosestHome();
        Debug.DrawLine(transform.position, transform.position + new Vector3(v.x, 0, v.y), Color.cyan);*/

    }


    void BehaviourNormal()
    {
        if (behaviourNormalTimer.IsReadyRestart())
        {
            if (Random.value > 0.6f)
                StartRandomWalk();
            else
                StartStand();
        }
    }
    Timer behaviourNormalTimer = new Timer(0);
    void StartStand()
    {
        behaviourNormalTimer.cd = Random.Range(0.4f,0.75f);
        input.positionInput = Vector2.zero;
    }
    void StartRandomWalk()
    {
        behaviourNormalTimer.cd = Random.Range(0.4f, 0.75f);

        input.positionInput = Random.insideUnitCircle;

        const float lerpHome = 0.9f;
        input.positionInput = input.positionInput * lerpHome - ToClosestHome() * (1 - lerpHome);

        const float lerpAvoid3 = 0.85f;
        input.positionInput = input.positionInput * lerpAvoid3 - VisionBlocker.ToThirdClosestAvoidPoint(transform.position) * (1 - lerpAvoid3);

        const float lerpAvoid2 = 0.75f;
        input.positionInput = input.positionInput * lerpAvoid2 - VisionBlocker.ToSecondClosestAvoidPoint(transform.position) * (1 - lerpAvoid2);

        const float lerpAvoid = 0.65f;
        input.positionInput = input.positionInput * lerpAvoid - VisionBlocker.ToClosestAvoidPoint(transform.position) * (1 - lerpAvoid);

    }

    void BehaviourPanique()
    {
        if (behaviourPaniqueTimer.IsReadyRestart())
        {
            StartRandomFlee();
        }
    }
    Timer behaviourPaniqueTimer = new Timer(0);


    void StartRandomFlee()
    {
        behaviourPaniqueTimer.cd = Random.Range(0.115f, 0.2f);

        Vector3 toPlayer = (transform.position - perception.lastPlayerPosition);
        toPlayer.y = 0;
        toPlayer.Normalize();

        input.positionInput = Random.insideUnitCircle;

        //const float lerpAvoid3 = 0.7f;
        //input.positionInput = input.positionInput * lerpAvoid3 - VisionBlocker.ToThirdClosestAvoidPoint(transform.position) * (1 - lerpAvoid3);

        //const float lerpAvoid2 = 0.9f;
        //input.positionInput = input.positionInput * lerpAvoid2 - VisionBlocker.ToSecondClosestAvoidPoint(transform.position) * (1 - lerpAvoid2);

        const float lerpAvoid = 0.95f;
        input.positionInput = input.positionInput * lerpAvoid - VisionBlocker.ToClosestAvoidPoint(transform.position) * (1 - lerpAvoid);

        
        if (canBeAccomondated)
        {
            const float lerpFactor = 0.75f;
            input.positionInput = input.positionInput * lerpFactor + new Vector2(toPlayer.x, toPlayer.z) * (1 - lerpFactor);


            const float lerpHome = 0.85f;
            input.positionInput = input.positionInput * lerpHome + ToClosestHome() * (1 - lerpHome);
        }
        else
        {
            const float lerpFactor = 0.65f;
            input.positionInput = input.positionInput * lerpFactor - new Vector2(toPlayer.x, toPlayer.z) * (1 - lerpFactor);

        }
        Debug.DrawRay(transform.position, toPlayer, Color.green, 0.5f);
    }

    Vector2 ToClosestHome()
    {
        Vector3 best = Vector3.zero;
        float bestDistSq = float.PositiveInfinity;

        foreach(var it in HouseController.houses)
        {
            if (!it.accomondationCd.IsReady() || it.HoldedVillager)
                continue;

            float pretDistSq = (it.transform.position - transform.position).sqrMagnitude;
            if(pretDistSq < bestDistSq)
            {
                best = it.transform.position - transform.position;
                bestDistSq = pretDistSq;
            }
        }

        return new Vector2(best.x, best.z);
    }
}
