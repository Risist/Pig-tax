using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyHolder : MonoBehaviour
{
    public GameObject moneySign;
    Timer gainMoneyTime = new Timer();
    public AudioSource yell;
    [Range(0,1)] public float yellChance;
    public float yellPitch = 1f;

    private void Start()
    {
        LooseMoney();
    }
    private void Update()
    {
        moneySign.transform.LookAt(Camera.main.transform);

        if(!moneySign.activeInHierarchy && gainMoneyTime.IsReady())
        {
            GainMoney();
        }
    }

    void GainMoney()
    {
        moneySign.SetActive(true);
    }
    void LooseMoney()
    {
        moneySign.SetActive(false);
        gainMoneyTime.Restart();
        gainMoneyTime.cd = Random.Range(5, 12);
    }

    private void OnEnable()
    {
        LooseMoney();
        if(MoneyManager.instance)
            MoneyManager.instance.SpawnPig(0.6f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if( collision.gameObject.tag == "Player" && moneySign.activeInHierarchy)
        {
            MoneyManager.instance.AddMoney(1);
            //MoneyManager.instance.currentMoney += 1;
            LooseMoney();

            if (yell && !yell.isPlaying && Random.value > yellChance)
            {
                yell.pitch = yellPitch;
                yell.Play();
            }
        }
    }
}
