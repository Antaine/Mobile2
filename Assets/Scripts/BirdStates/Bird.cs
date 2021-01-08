using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bird : MonoBehaviour
{
    private StateMachine stateMachine;
    public Rigidbody2D myRb;
    public GameObject beeGO { get; private set;}
    public SpriteRenderer sprite{ get; private set;}
    public CapsuleCollider2D BirdCollider;
    public Vector2 birdDir;
    public float speed =2.5f;
    public int targetId;
    public bool isChasing;
    public bool isEating;
    public bool isFleeing;
    public bool atNest;
    public float maxEnergy =10000;
    public float currEnergy;
    public float midEnergy =500f;
    public float lowEnergy =50f;
    public float energyRate;
    public bool isResting;
    public Vector2 nestPos1,nestPos2;
    private float dis1=0;
    private float dis2 = 6;
    private float range = 3f;


    public virtual void Start()
    {
        currEnergy = maxEnergy;
        birdDir = UnityEngine.Random.insideUnitCircle.normalized;
        myRb =GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        BirdCollider = GetComponent<CapsuleCollider2D>();
    }
     private void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
        Dictionary<Type,State> allStates = new Dictionary<Type,State>()
        {
            {typeof(StateFlying), new StateFlying(this)},
            {typeof(StateChasing), new StateChasing(this)},
            {typeof(StateEating), new StateEating(this)},
            {typeof(StateResting), new StateResting(this)},

        };
        stateMachine.SetUpStates(allStates,typeof(StateFlying));
    }

    private void OnTriggerEnter2D(Collider2D collision){     
        if(collision.tag =="VBorder")
            this.birdDir.y *= -1f;
        if(collision.tag =="HBorder")
            this.birdDir.x *= -1f;
        if(collision.tag =="Bees")
        {
            this.isEating = true;
            this.isChasing = false;
        }
        if(collision.tag =="Hive")
        {
            this.isChasing = false;
            this.isFleeing = true;
        }

        if(collision.tag =="Bees" ){
            this.isEating = true;
        }
    }

      

    public void CheckEnergy(Bird bird){
        bird.currEnergy += bird.energyRate;
        if(bird.currEnergy>bird.midEnergy){
            bird.sprite.color = new Color (0, 128, 0, 1);
        }

        else if(bird.currEnergy<=bird.midEnergy && bird.currEnergy>bird.lowEnergy){
            bird.sprite.color = new Color (255, 255, 0, 1);
        }

        else if(bird.currEnergy<=bird.lowEnergy && bird.currEnergy>0f){
            bird.sprite.color = new Color (255, 0, 0, 1);
            bird.isFleeing = true; 
        }
    }

    public void ReturnToNest(Bird bird){
        bird.energyRate = -0.4f;
        nestPos1 = SpawnBirds.nest1;
        nestPos2 = SpawnBirds.nest2;

        if(Vector2.Distance(bird.transform.position,nestPos1)> Vector2.Distance(bird.transform.position,nestPos2)){
            nestPos1 = nestPos2;
        }
        float dis3 = Vector2.Distance(bird.transform.position,nestPos1);
        if(dis3<0.1){
            bird.atNest = true;
            bird.isFleeing = false;
            bird.isChasing = false;
        }
        else{
            bird.myRb.velocity = new Vector2(0,0);
            bird.transform.position = Vector2.MoveTowards(transform.position, nestPos1, speed*Time.deltaTime);
        } 
    }

        public void ScanBees(Bird bird){
        int i =0;
        foreach(GameObject bee in SpawnHive.activeBees)
        {
            if(bee != null){
                bird.dis1 = Vector2.Distance(bird.transform.position,bee.transform.position);
                if(bird.dis1 < bird.dis2){
                    if(Vector2.Distance(bird.transform.position,bee.transform.position) <= range)
                    {
                        bird.dis2 = bird.dis1;
                        bird.isChasing = true;
                        bird.atNest = false;
                        bird.isResting =false;
                        bird.targetId = i;
                    }
                }      
            }
            i++;
        }
        bird.dis1 = 0;
        bird.dis2 = range+5;
    }
}
