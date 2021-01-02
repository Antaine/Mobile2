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
    private int targetId =0;
    private float dis1=0;
    private float dis2 = 6;
    
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
            Searching();
    }

    private void OnTriggerExit2D(Collider2D collision){     

        if(collision.tag =="MainCamera")
            xSpeed *= -1f;
            ySpeed *= -1f;
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag =="Flowers")
            targetFound = false;
    }


    private void Scan(){
        int i =0;
        foreach(GameObject flower in FlowerSpawning.activeFlowers)
        {
            if(flower!= null){
                dis1 = Vector2.Distance(this.transform.position,flower.transform.position);
                if(dis1 < dis2){
                    print("Scanning Loop 1");
                    if(Vector2.Distance(this.transform.position,flower.transform.position) <= range)
                    {
                        dis2 = dis1;
                        print("Scanning If");
                        Debug.Log("Flower in range");
                        targetFound = true;
                        Debug.Log("Target Found");
                        targetId = i;
                        //return;
                    }
                }      
            }
            i++;
        }
    }

    private void Searching(){
        this.myRb.velocity = new Vector2(xSpeed,ySpeed);
        print("Searching");
        Scan();
    }

    private void goToFlower(){
        this.myRb.velocity = new Vector2(0,0);
        if(FlowerSpawning.activeFlowers[targetId] != null)
        {
            this.transform.position = Vector2.MoveTowards(transform.position, FlowerSpawning.activeFlowers[targetId].transform.position, 1.0f*Time.deltaTime);   
        }
        else{
            targetFound = false;
        }
    }
}
