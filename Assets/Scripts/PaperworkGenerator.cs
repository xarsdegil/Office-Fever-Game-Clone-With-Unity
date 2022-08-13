using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperworkGenerator : MonoBehaviour
{
    private Vector3 genPoint;
    public Transform point1, point2, collectPos;
    public GameObject paperPrefab;
    public int paperCount, collectedCount;
    public float countdown;
    private float cdown = 0f;
    public GameObject[] papers, collected;
    public bool canGen = true, canWork = true;
    float nextY, nextY2;
    // Start is called before the first frame update
    void Start()
    {
        genPoint = point1.position;
        nextY = collectPos.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(cdown <= Time.time && paperCount != 40 && canGen){
            GeneratePaper();
            cdown = Time.time + countdown;
        }
    }

    private void GeneratePaper()
    {
        if(paperCount == 20){
            genPoint = point2.position;
        }else if(paperCount == 0){
            genPoint = point1.position;
        }else{
            genPoint = papers[paperCount-1].transform.position + new Vector3(0,0.12f,0);
        }

        var newGen = Instantiate(paperPrefab, genPoint, Quaternion.identity);
        papers[paperCount] = newGen.gameObject;
        paperCount++;
        //var a = newGen.transform;
        
    }

    public void CollectPapers(){
        if(collectedCount != 21 && paperCount != 0){
            var genPos = collected[collectedCount-1].transform.position + new Vector3(0,0.12f,0);
            var paper = Instantiate(paperPrefab, collectPos);
            paper.transform.position = genPos;
            collected[collectedCount] = paper;
            collectedCount++;
            Destroy(papers[paperCount-1]);
            paperCount--;
        }
    }

    public void GiveWork(GameObject x){
        if(collectedCount != 1){
            var wsc = x.GetComponentInParent<WorkerScript>();
            var givenCount = wsc.givenCount;
            var given = wsc.given;
            var worker = x;
            var paper = Instantiate(paperPrefab, x.transform);
            nextY2 = given[givenCount-1].transform.position.y + 0.12f;
            given[givenCount] = paper;
            wsc.givenCount++;
            paper.transform.position = new Vector3(x.transform.position.x, nextY2, x.transform.position.z);
            Destroy(collected[collectedCount-1]);
            collectedCount--;
        }
    }

}
