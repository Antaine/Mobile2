using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovement : MonoBehaviour
{
    Rigidbody2D myRb;
    [SerializeField] float xSpeed = 2.0f;
    [SerializeField] float ySpeed = 2.0f;
    CapsuleCollider2D BeeCollider;
    private float range =5;
    private bool targetFound;
    
    //Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        BeeCollider = GetComponent<CapsuleCollider2D>();
        targetFound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(targetFound)
            goToFlower();
        else
            Invoke("Searching",1);
    }

    private void OnTriggerExit2D(Collider2D collision){     
       xSpeed *= -1f;
       ySpeed *= -1f;
    }

    private void Scan(){
        if(Vector2.Distance(this.transform.position,FlowerSpawning.activeFlowers[0].transform.position) <= range)
        {
            Debug.Log("Player in range");
            targetFound = true;
        }
    }

    private void Searching(){
        myRb.velocity = new Vector2(xSpeed,ySpeed);
        Scan();
    }

    private void goToFlower(){
        myRb.velocity = new Vector2(0,0);
        transform.position = Vector2.MoveTowards(transform.position, FlowerSpawning.activeFlowers[0].transform.position, 1.0f*Time.deltaTime);
        
    }
}
