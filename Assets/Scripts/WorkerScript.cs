using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerScript : MonoBehaviour
{
    public bool canWork;
    public int givenCount, moneyCount;
    public GameObject[] given, moneys;
    public GameObject moneyPrefab;
    float cdown = 1.2f, a;
    // Start is called before the first frame update
    private void Awake() {
        givenCount = 1;
    }
    void Start()
    {
        canWork = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canWork && a < Time.time && givenCount > 1){
            Work();
            a = Time.time + cdown;
        }
    }

    private void Work()
    {
        Destroy(given[givenCount-1]);
        givenCount--;
        Money();
    }

    private void Money()
    {
        var genPos = moneys[moneyCount-1].transform.position + new Vector3(0,0.12f,0);
        var newMoney = Instantiate(moneyPrefab, genPos, Quaternion.identity);
        moneys[moneyCount] = newMoney;
        moneyCount++;
    }

    public void Balance(GameObject x){
        Destroy(moneys[moneyCount-1]);
        moneyCount--;
        var a = x.GetComponent<TriggerManager>();
        a.balance += 15;
    }
}
