using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    public Text moneyText;
    public int currentMoney;
    public int maxMoney;

    [Space]
    public GameObject[] pigPrefab;
    public Transform pigSpawnPoint;

    private void Awake() { instance = this; }
    
    void Update() { moneyText.text = currentMoney + " \\ " + maxMoney; }

    public void AddMoney(int gain)
    {
        currentMoney += Random.Range(5, 125);
    }

    public void SpawnPig(float probabilityPig)
    {
        if (Random.value > probabilityPig)
        {
            var spawned = Instantiate(pigPrefab[Random.Range(0, pigPrefab.Length)]);
            spawned.transform.position = pigSpawnPoint.position;
            spawned.transform.rotation = pigSpawnPoint.rotation;
        }
    }

}
