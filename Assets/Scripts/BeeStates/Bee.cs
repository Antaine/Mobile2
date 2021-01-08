using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bee : MonoBehaviour
{
    public bool flowerFound;
    public bool atFlower;
    public bool isFleeing;
    public bool atHive;
    public bool isRested;
    public int targetId;
    public bool atCapacity = false;
    public int honey;
    [SerializeField] public int capacity = 5;
    public float maxEnergy =400;
    public float currEnergy;
    public float midEnergy =300f;
    public float lowEnergy =150f;
    public float energyRate;
    private StateMachine stateMachine;
    public Rigidbody2D myRb;
    public GameObject beeGO { get; private set;}
    public SpriteRenderer sprite{ get; private set;}
    public CapsuleCollider2D BeeCollider;
    public Vector2 moveDir;
    public float beeSpeed =2.0f;

    public virtual void Start()
    {
        currEnergy = maxEnergy;
        moveDir = UnityEngine.Random.insideUnitCircle.normalized;
        myRb =GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        BeeCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
        Dictionary<Type,State> allStates = new Dictionary<Type,State>()
        {
            {typeof(StateSearching), new StateSearching(this)},
            {typeof(StateGathering), new StateGathering(this)},
            {typeof(StateFleeing), new StateFleeing(this)},
            {typeof(StateDancing), new StateDancing(this)},
            {typeof(StateAtHive), new StateAtHive(this)},
        };
        stateMachine.SetUpStates(allStates,typeof(StateSearching));
    }

    private void OnTriggerEnter2D(Collider2D collision){     
        if(collision.tag =="VBorder")
            this.moveDir.y *= -1f;
        if(collision.tag =="HBorder")
           this.moveDir.x *= -1f;
        if(collision.tag =="Flowers")
            this.atFlower = true;
        if(collision.tag =="Bird" && !this.atHive ){
            Destroy(gameObject);
            SpawnHive.activeBees.RemoveAll(bee => bee == null);
        }
    }

    public void CheckEnergy(Bee bee){
        bee.currEnergy += bee.energyRate;
        if(bee.currEnergy>bee.midEnergy){
            bee.sprite.color = new Color (0, 128, 0, 1);
        }

        else if(bee.currEnergy<=bee.midEnergy && bee.currEnergy>bee.lowEnergy){
            bee.sprite.color = new Color (255, 255, 0, 1);
        }

        else if(bee.currEnergy<=bee.lowEnergy && bee.currEnergy>0f){
            bee.sprite.color = new Color (255, 0, 0, 1);
            bee.isFleeing = true; 
        }
    }
}
