using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Touch touch;
    public float rotationSpeed = 1, moveSpeed = 1;
    public bool isDragging;
    Vector3 touchDown, touchUp;
    // Start is called before the first frame update
    void Start()
    {
        isDragging = false;
    }

    // Update is called once per frame
    void Update()
    {

        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        var animator = gameObject.GetComponent<Animator>();

        if(Input.touchCount > 0){

            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began){
                isDragging = true;
                touchDown = touch.position;
                touchUp = touch.position;
            }
        }

        if(isDragging){
            
            if(touch.phase == TouchPhase.Ended){
                touchDown = touch.position;
                isDragging = false;
            }else{
                //if(touch.phase == TouchPhase.Moved){
                touchDown = touch.position;
            //}
            }

            //animator.SetBool("isDragging", isDragging);
            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, Rotation(), rotationSpeed * Time.deltaTime);
            gameObject.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);          
            
        }
        
        

    }

    Quaternion Rotation(){
        Quaternion a = Quaternion.LookRotation(Direction(), Vector3.up);
        return a;
    }

    Vector3 Direction(){
        Vector3 a = (touchDown - touchUp).normalized;
        a.z = a.y;
        a.y = 0;
        return a; 
    }

}
