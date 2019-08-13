using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{
    public static List<HouseController> houses = new List<HouseController>();
    public Transform spawnPosition;
    public GameObject houseLit;
    public GameObject houseUnlit;
    public Timer accomondationCd;
    Timer stayTime = new Timer();
    [System.NonSerialized] public GameObject HoldedVillager;
    
    void Accomodate(GameObject villager)
    {
        if (!accomondationCd.IsReady())
            return;



        HoldedVillager = villager;
        stayTime.cd = Random.Range(3, 7);
        stayTime.Restart();
        villager.SetActive(false);

        houseLit.SetActive(true);
        houseUnlit.SetActive(false);
    }
    
    void Start()
    {
        houses.Add(this);

        houseLit.SetActive(false);
        houseUnlit.SetActive(true);

        accomondationCd.Restart();
        accomondationCd.actualTime -= accomondationCd.cd;
    }
    
    void Update()
    {
        if(HoldedVillager && stayTime.IsReady())
        {
            var perception = HoldedVillager.GetComponentInChildren<PerceptionDetector>();
            if (perception)
            {
                perception.paniqueTimer.actualTime -= perception.paniqueTimer.cd;
            }

            HoldedVillager.SetActive(true);

            var pos = spawnPosition.position;
            pos.y = HoldedVillager.transform.position.y;
            HoldedVillager.transform.position = pos;
            HoldedVillager.transform.rotation = spawnPosition.rotation;
            HoldedVillager = null;

            houseLit.SetActive(false);
            houseUnlit.SetActive(true);

            accomondationCd.Restart();

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (HoldedVillager)
            return;

        var vill = collision.collider.GetComponent<VillagerInput>();
        if (vill != null && vill.canBeAccomondated)
        {
            Accomodate(collision.gameObject);
        }
    }
}
