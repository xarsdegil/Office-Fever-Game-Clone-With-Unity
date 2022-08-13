using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyArea : MonoBehaviour
{
    [SerializeField] GameObject worker;
    public Image image;

    public float paidCount, progress, cost;

    private void Start() {
        progress = 0;
    }

    private void Update() {
        image.fillAmount = progress;
    }

    public void Buy(int money, GameObject player){
        paidCount += money;
        progress = paidCount / cost;
        if(progress >= 1){
            worker.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
