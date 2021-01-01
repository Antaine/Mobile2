using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    Rigidbody2D myRb;
    [SerializeField] float xSpeed = 2.0f;
    [SerializeField] float ySpeed = 2.0f;
    CapsuleCollider2D BirdCollider;
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        BirdCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myRb.velocity = new Vector2(xSpeed,ySpeed);
    }

    private void OnTriggerExit2D(Collider2D collision){     
       xSpeed *= -1f;
       ySpeed *= -1f;
    }
}
