using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovement : MonoBehaviour
{
    Rigidbody2D myRb;
    [SerializeField] float speed = 2.0f;
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
    private bool isResting = false;
    private bool atCapacity = false;
    private float maxEnergy =150f;
    private float currEnergy;
    private float midEnergy =100f;
    private float lowEnergy =50f;
    private float energyRate = 0f;
    SpriteRenderer sprite;
    
    //Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        BeeCollider = GetComponent<CapsuleCollider2D>();
        targetFound = false;
        this.myRb.velocity = new Vector2(0,0);
        moveDir = Random.insideUnitCircle.normalized;
        this.currEnergy = maxEnergy;
        sprite = GetComponent<SpriteRenderer>();
        this.Searching();
    }

    void FixedUpdate()
    {
        CheckEnergy();
        CheckCapacity();
        if(!this.atCapacity){
            if(this.isResting == true){
                if(this.currEnergy>= maxEnergy){
                    this.atHive = false;
                    this.isResting = false;
                    this.moveDir = Random.insideUnitCircle.normalized;
                    this.Searching();
                }
                else
                    this.Rest();
            }

            else if(isResting == false)
            {
                if(this.currEnergy>0f){
                    if(this.currEnergy <= lowEnergy)
                        ReturnToHive();
                    
                    if(!this.isResting){
                        if(targetFound){
                            GoToFlower();
                        }
                        else
                        {
                            Searching();
                        }
                    }
                }
            }
        }    
        else{
            ReturnToHive();
        }
        this.currEnergy += energyRate;
        print(currEnergy);
    }

    private void OnTriggerEnter2D(Collider2D collision){     
        if(collision.tag =="VBorder")
            this.moveDir.y *= -1f;
        if(collision.tag =="HBorder")
            this.moveDir.x *= -1f;
        if(collision.tag =="Flowers" && this.honey<capacity && targetFound){
            this.targetFound = false;
            this.honey++;
            //print("Capacity "+this.honey);
            }
        if(collision.tag =="Hive" && this.honey >=capacity){
            this.myRb.velocity = new Vector2(0,0);
            Rest();
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
                       // print("Scanning If");
                        //Debug.Log("Flower in range");
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
        this.energyRate = -0.1f;
        this.myRb.velocity = this.moveDir;
        Scan();
    }

    private void GoToFlower(){
        //this.myRb.velocity = new Vector2(0,0);
        if(FlowerSpawning.activeFlowers[targetId] != null)
        {
            this.transform.position = Vector2.MoveTowards(transform.position, FlowerSpawning.activeFlowers[targetId].transform.position, speed*Time.deltaTime);   
        }
        else{
            targetFound = false;
        }
    }

    private void ReturnToHive(){
        //this.myRb.velocity = new Vector2(0,0);
        energyRate = -0.2f;
        float dis3 = Vector2.Distance(this.transform.position,hivePos);
        if(dis3<0.1){
            isResting = true;
            Rest();
           // Time.timeScale = 0f;
        }

        else{
            this.transform.position = Vector2.MoveTowards(transform.position, hivePos, speed*Time.deltaTime);
        }     
        //print("Returning to Hive");
    }

    private void Rest(){
        this.myRb.velocity = new Vector2(0,0);
        energyRate = 0.2f;
        this.honey = 0;
        atCapacity = false;
        isResting = true;
        moveDir = Random.insideUnitCircle.normalized;
        if(this.currEnergy>=maxEnergy){
            //this.Searching();
            this.isResting = false;
        }
    }

    private void CheckEnergy(){
        if(this.currEnergy>midEnergy){
            this.sprite.color = new Color (0, 128, 0, 1);
        }

        else if(this.currEnergy<=midEnergy && this.currEnergy>lowEnergy){
            this.sprite.color = new Color (255, 255, 0, 1);
        }

        else if(this.currEnergy<=lowEnergy && this.currEnergy>0f){
            this.sprite.color = new Color (255, 0, 0, 1); 

        }

        else
        {
            this.myRb.velocity = new Vector2(0,0);
        }
    }

    private void CheckCapacity(){
        if(this.honey>=capacity){
            ReturnToHive();
            this.atCapacity = true;
        }

        else{
            this.atCapacity = false;
        }
    }
}
