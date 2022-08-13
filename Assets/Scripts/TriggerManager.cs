using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerManager : MonoBehaviour
{
    public float countDown = 0.5f, cDown = 0, collectCooldown = 0.3f, cDownForBalance = 0, workCooldown;
    GameObject[] papersOnPlayer;
    public int paperCountOnPlayer, balance;
    Vector3 genPointOnPlayer;
    public Transform point;
    [SerializeField] Text balanceText;

    private void Start() {
        balance = 0;
    }

    private void Update() {
        genPointOnPlayer = point.position;
        balanceText.text = "Balance: " + balance.ToString();
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.name == "PaperworkArea"){
            var generator = GameObject.Find("Manager").GetComponent<PaperworkGenerator>();
            generator.canGen = false;
            if(cDown <= Time.time){
                generator.CollectPapers();
                cDown = Time.time + collectCooldown;
            }
            
        }
        if(other.gameObject.name == "WorkerArea"){
            //cDown = 0;
            var wsc = other.gameObject.GetComponentInParent<WorkerScript>();
            var generator = GameObject.Find("Manager").GetComponent<PaperworkGenerator>();
            wsc.canWork = false;
            if(cDown <= Time.time && wsc.givenCount != 11){
                generator.GiveWork(other.gameObject.transform.GetChild(0).gameObject);
                cDown = Time.time + workCooldown;
            }
            if(wsc.moneyCount > 1 && cDownForBalance <= Time.time){
                wsc.Balance(this.gameObject);
                cDownForBalance = Time.time + 0.4f;
            }
        }

        if(other.gameObject.name == "BuyWorker"){
            var sc = other.gameObject.GetComponent<BuyArea>();
            if(balance >= 10){
                sc.Buy(10, this.gameObject);
                balance -= 10;
            }else if(balance > 0){
                sc.Buy(balance, this.gameObject);
                balance = 0;
            }
            
        }

    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.name == "PaperworkArea"){
            var generator = GameObject.Find("Manager").GetComponent<PaperworkGenerator>();
            generator.canGen = true;
        }
        if(other.gameObject.name == "WorkerArea"){
            var wsc = other.gameObject.GetComponentInParent<WorkerScript>();
            wsc.canWork = true;
        }
    }
    

}
