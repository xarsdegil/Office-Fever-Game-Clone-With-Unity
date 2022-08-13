using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rig : MonoBehaviour
{
    [SerializeField] float lerpSmooth;
    [SerializeField] GameObject player, cam;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 13f);
        var a = this.gameObject.transform.position;
        cam.transform.position = Vector3.Lerp(transform.position, new Vector3(a.x, cam.transform.position.y, a.z), lerpSmooth * Time.deltaTime);
    }
}
