using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    Rigidbody2D myRb;
    [SerializeField] float xSpeed = 2.0f;
    [SerializeField] float ySpeed = 2.0f;
    private float maxEnergy =200f;
    private float currEnergy;
    private float midEnergy = 120f;
    private float lowEnergy = 70f;
    private float energyRate;
    private float speed = 2.0f;
    private Vector2 moveDir;
    CapsuleCollider2D BirdCollider;
    SpriteRenderer sprite;
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        BirdCollider = GetComponent<CapsuleCollider2D>();
        currEnergy = maxEnergy;
        energyRate = 0.4f;
        moveDir = Random.insideUnitCircle.normalized;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       // myRb.velocity = new Vector2(xSpeed,ySpeed);
        this.myRb.velocity = this.moveDir*speed;
        this.currEnergy += energyRate;
    }

    private void OnTriggerEnter2D(Collider2D collision){     
        if(collision.tag =="VBorder")
            this.moveDir.y *= -1f;
        if(collision.tag =="HBorder")
            this.moveDir.x *= -1f;
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
}
