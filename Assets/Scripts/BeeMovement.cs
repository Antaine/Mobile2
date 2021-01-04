using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovement : MonoBehaviour
{
    Rigidbody2D myRb;
    [SerializeField] float xSpeed = 2.0f;
    [SerializeField] float speed = 2.0f;
    [SerializeField] float ySpeed = 2.0f;
    [SerializeField] int capacity = 5;
    CapsuleCollider2D BeeCollider;
    private int honey=0;
    private float range =5;
    private bool targetFound;
    private int targetId =0;
    private float dis1=0;
    private float dis2 = 6;
    public static Vector2 hivePos;
    private Vector2 moveDir;
    private bool atHive = false;
    private bool isDancing = false;
    
    //Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        BeeCollider = GetComponent<CapsuleCollider2D>();
        targetFound = false;
        this.myRb.velocity = new Vector2(0,0);
        moveDir = Random.insideUnitCircle.normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isDancing)
        {
            if(!atHive){
                if(honey < capacity){
                    if(targetFound)
                        GoToFlower();
                    
                    else
                        Searching();
                }

                else
                    ReturnToHive();
            }
        
            else{
            Dance();
            }
        }
       
    }

     private void OnTriggerEnter2D(Collider2D collision){     
        if(collision.tag =="VBorder")
            this.moveDir.y *= -1f;
        if(collision.tag =="HBorder")
            this.moveDir.x *= -1f;
        if(collision.tag =="Flowers" && this.honey<capacity && targetFound){
            this.targetFound = false;
            this.honey++;
            print("Capacity "+this.honey);}
        if(collision.tag =="Hive" && this.honey >=capacity){
            this.myRb.velocity = new Vector2(0,0);
            //Dance();
        }
        
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
                        //Debug.Log("Target Found");
                        targetId = i;
                    }
                }      
            }
            i++;
        }
        dis1 = 0;
        dis2 = range+5;
    }

    private void Searching(){
        isDancing = false;
        this.myRb.velocity = this.moveDir;
        //transform.position = Vector2.MoveTowards(transform.position, moveDir, speed*Time.deltaTime);
        atHive = false;
       // print(moveDir);
        Scan();
    }

    private void GoToFlower(){
        this.myRb.velocity = new Vector2(0,0);
        if(FlowerSpawning.activeFlowers[targetId] != null)
        {
            this.transform.position = Vector2.MoveTowards(transform.position, FlowerSpawning.activeFlowers[targetId].transform.position, speed*Time.deltaTime);   
        }
        else{
            targetFound = false;
        }
    }

    private void ReturnToHive(){
        this.myRb.velocity = new Vector2(0,0);
        float dis3 = Vector2.Distance(this.transform.position,hivePos);
         if(dis3<0.1){
             this.myRb.velocity = new Vector2(0,0);
             Dance();
         }

        else{
            this.transform.position = Vector2.MoveTowards(transform.position, hivePos, speed*Time.deltaTime);
        }
        
        print("Returning to Hive");
    }

    private void Dance(){
        print("Dance");
        this.honey = 0;
        isDancing = true;
        moveDir = Random.insideUnitCircle.normalized;
        Invoke("Searching",5.0f);
    }

   // private void SetPath(){
        //moveDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = Vector2.MoveTowards(transform.position, moveDir, speed*Time.deltaTime);
  //  }
}
