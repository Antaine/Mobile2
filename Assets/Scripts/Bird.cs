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
    public float speed =2f;
    public int targetId;
    public bool isChasing;
    public bool isEating;
    public bool isFleeing;
    public float maxEnergy =400;
    public float currEnergy;
    public float midEnergy =300f;
    public float lowEnergy =150f;
    public float energyRate;

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
          //  {typeof(StateResting), new StateResting(this)},

        };
        stateMachine.SetUpStates(allStates,typeof(StateFlying));
    }

    private void OnTriggerEnter2D(Collider2D collision){     
        if(collision.tag =="VBorder")
            this.birdDir.y *= -1f;
        if(collision.tag =="HBorder")
            this.birdDir.x *= -1f;
        if(collision.tag =="Bees")
            this.isEating = true;
            this.isChasing = false;
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
}
