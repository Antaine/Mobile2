using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovement : MonoBehaviour
{
    Rigidbody2D myRb;
    [SerializeField] float speed = 4.0f;
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
    public  bool atHive = false;
    private bool isResting = false;
    private bool atCapacity = false;
    private float maxEnergy =150f;
    private float currEnergy;
    private float midEnergy =100f;
    private float lowEnergy =50f;
    private float energyRate = 0.3f;
    private bool isFleeing = false;
    SpriteRenderer sprite;
    private int deadBees = 0;
    
    //Start is called before the first frame update
    void Start()
    {
        this.myRb = GetComponent<Rigidbody2D>();
        this.BeeCollider = GetComponent<CapsuleCollider2D>();
        this.targetFound = false;
        this.myRb.velocity = new Vector2(0,0);
        this.moveDir = Random.insideUnitCircle.normalized;
        this.currEnergy = maxEnergy;
        this.sprite = GetComponent<SpriteRenderer>();
        this.Searching();
        deadBees = 0;
    }

    void FixedUpdate()
    {
        if(deadBees>=4){
            print("GameOVer");
        }
        else
        {
            CheckEnergy();
            CheckCapacity();
            DetectBird();
            if(isFleeing)
            {
                this.ReturnToHive();
            }

            else
            {
                if(!this.atCapacity)
                {
                    if(this.isResting == true)
                    {
                        if(this.currEnergy>= maxEnergy){
                            this.atHive = false;
                            this.isResting = false;
                            this.moveDir = Random.insideUnitCircle.normalized;
                            this.Searching();
                        }
                        else
                            this.Rest();
                    }

                    else if(this.isResting == false)
                    {
                        if(this.currEnergy>0f)
                        {
                            if(this.currEnergy <= lowEnergy)
                                this.ReturnToHive();
                                
                            else if(!this.isResting)
                            {
                                if(targetFound)
                                {
                                    this.GoToFlower();
                                }
                                else
                                {
                                    this.Searching();
                                }
                            }
                        }
                    }
                }    
            else
            {
            this.ReturnToHive();
            }
            }

            this.currEnergy += energyRate;
            print(currEnergy);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){     
        if(collision.tag =="VBorder")
            this.moveDir.y *= -1f;
        if(collision.tag =="HBorder")
            this.moveDir.x *= -1f;
        if(collision.tag =="Flowers" && this.honey<capacity && this.targetFound){
            this.targetFound = false;
            this.honey++;
            //print("Capacity "+this.honey);
            }
        if(collision.tag =="Hive" && this.honey >=this.capacity){
            this.myRb.velocity = new Vector2(0,0);
            this.Rest();
        }

        if(collision.tag =="Bird" && !this.atHive ){
            Destroy(gameObject);
            SpawnHive.activeBees.RemoveAll(bee => bee == null);
            deadBees++;
            return;
        }
        
    }

    private void ScanFlowers(){
        int i =0;
        foreach(GameObject flower in FlowerSpawning.activeFlowers)
        {
            if(flower != null){
                this.dis1 = Vector2.Distance(this.transform.position,flower.transform.position);
                if(this.dis1 < this.dis2){
                    //print("Scanning Loop 1");
                    if(Vector2.Distance(this.transform.position,flower.transform.position) <= range)
                    {
                        this.dis2 = this.dis1;
                        this.targetFound = true;
                        this.targetId = i;
                    }
                }      
            }
            i++;
        }
        this.dis1 = 0;
        this.dis2 = range+5;
    }

    private void DetectBird(){
        int i =0;
        foreach(GameObject bird in SpawnBirds.activeBirds)
        {
            if(this.BeeCollider != null){
                this.dis1 = Vector2.Distance(this.transform.position,bird.transform.position);
                if(Vector2.Distance(this.transform.position,bird.transform.position) <= range)
                {
                    isFleeing = true;
                    ReturnToHive();
                    return;
                }
                
                i++;
            }
            this.dis1 = 0;
            this.dis2 = range+5;
            //ScanFlowers();
        }
    }

    private void Searching(){
        this.energyRate = -0.1f;
        this.myRb.velocity = this.moveDir*speed;
        DetectBird();
        if(!isFleeing){
            ScanFlowers();
        }
    }

    private void GoToFlower(){
        this.myRb.velocity = new Vector2(0,0);
        if(FlowerSpawning.activeFlowers[targetId] != null)
        {
            this.transform.position = Vector2.MoveTowards(transform.position, FlowerSpawning.activeFlowers[targetId].transform.position, speed*Time.deltaTime);   
        }
        else{
            this.targetFound = false;
        }
    }

    private void ReturnToHive(){
        this.myRb.velocity = new Vector2(0,0);
        this.energyRate = -0.2f;
        if(this.transform.position != null){
            float dis3 = Vector2.Distance(this.transform.position,hivePos);
            if(dis3<0.1){
                this.isResting = true;
                this.isFleeing = false;
                Rest();
            // Time.timeScale = 0f;
            }

            else{
                this.transform.position = Vector2.MoveTowards(transform.position, hivePos, speed*Time.deltaTime);
            }   
        }
  
        //print("Returning to Hive");
    }

    private void Rest(){
        //this.myRb.velocity = new Vector2(0,0);
        this.energyRate = 0.2f;
        this.honey = 0;
        this.atCapacity = false;
        this.isResting = true;
        if(this.currEnergy>=maxEnergy){
            this.moveDir = Random.insideUnitCircle.normalized;
            this.Searching();
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

        if(this.currEnergy<0)
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
