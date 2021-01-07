using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    Rigidbody2D myRb;
    [SerializeField] float xSpeed = 2.0f;
    [SerializeField] float ySpeed = 2.0f;
    private float maxEnergy =300f;
    private float currEnergy;
    private float midEnergy = 120f;
    private float lowEnergy = 70f;
    private float energyRate;
    private float speed = 4.0f;
    private Vector2 moveDir;
    public static Vector2 nestPos1,nestPos2;
    CapsuleCollider2D BirdCollider;
    SpriteRenderer sprite;
    private float range =5;
    private bool targetFound;
    private int targetId =0;
    private float dis1=0;
    private float dis2 = 6;
    private bool isEating = false;
    private bool isResting = false;
    private bool isFleeing = false;
    private bool atNest = false;
    private bool isChasing = false;
    
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        BirdCollider = GetComponent<CapsuleCollider2D>();
        currEnergy = maxEnergy;
        energyRate = 0f;
        moveDir = Random.insideUnitCircle.normalized;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.currEnergy += energyRate;
        CheckEnergy();
        if(isResting)
            Rest();
        else if(currEnergy<= lowEnergy && isResting == false){
            ReturnToNest();
        }
        else{
            if(isEating){
                this.currEnergy = maxEnergy;
                isResting = false;
            }
            else if(isChasing){
                Chase();
            }

            else{
                Search();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){     
        if(collision.tag =="VBorder"){
            this.myRb.velocity *= -1f;
        }
        if(collision.tag =="HBorder")
            this.moveDir.x *= -1f;
        if(collision.tag =="Hive")
            this.moveDir.x *= -1f;
            this.moveDir.y *= -1f;
            this.targetFound = false;
            ReturnToNest();
        if(collision.tag =="Bees" ){
            this.currEnergy = maxEnergy;
            SpawnHive.activeBees.RemoveAll(bee => bee == null);
            this.myRb.velocity = new Vector2(0,0);
            this.isEating = true;
            Invoke("Eating",3);
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
    }

    private void Scan(){
        int i =0;
        foreach(GameObject bee in SpawnHive.activeBees)
        { 
            if(bee!= null){
                dis1 = Vector2.Distance(this.transform.position,bee.transform.position);
                if(dis1 < dis2){
                    if(Vector2.Distance(this.transform.position,bee.transform.position) <= range)
                    {
                        dis2 = dis1;
                        targetFound = true;
                        targetId = i;
                    }
                }      
            }
            i++;
        }
        dis1 = 0;
        dis2 = range+5;
    }

    private void Search(){
        Scan();
        if(this.targetFound){
            Chase();
            energyRate = -0.8f;
        }

        else{
            isChasing = false;
            energyRate = -0.4f;
            this.myRb.velocity = this.moveDir*speed;
        }
    }

    private void Chase(){
      
        if(SpawnHive.activeBees[targetId] != null && SpawnHive.activeBees != null)
        {
            isChasing = true;
            this.myRb.velocity = new Vector2(0,0);
            this.transform.position = Vector2.MoveTowards(transform.position, SpawnHive.activeBees[targetId].transform.position, speed*Time.deltaTime);   
        }
        else{
            this.targetFound = false;
        }
    }

    private void ReturnToNest(){
        this.energyRate = -0.4f;
        float dis3 = Vector2.Distance(this.transform.position,nestPos1);
        if(dis3<0.1){
            this.isResting = true;
            this.isFleeing = false;
            Rest();
        }
        else{
            this.myRb.velocity = new Vector2(0,0);
            this.transform.position = Vector2.MoveTowards(transform.position, nestPos1, speed*Time.deltaTime);
        } 
        }

    private void Rest(){
        this.myRb.velocity = new Vector2(0,0);
        this.energyRate = 0.3f;
        this.isResting = true;
        this.moveDir = Random.insideUnitCircle.normalized;
        if(this.currEnergy>=maxEnergy){
            this.isResting = false;
        }
    }

    private void Eating(){
        this.isEating = false;
    }
}
