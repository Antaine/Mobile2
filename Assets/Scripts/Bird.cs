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
    public virtual void Start()
    {
        //currEnergy = maxEnergy;
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
          //  {typeof(StateChasing), new StateChasing(this)},
          //  {typeof(StateResting), new StateResting(this)},
          //  {typeof(StateEating), new StateEating(this)},
        };
        stateMachine.SetUpStates(allStates,typeof(StateSearching));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
