using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovement : MonoBehaviour
{
    Rigidbody2D myRb;
    [SerializeField] float xSpeed = 2.0f;
    [SerializeField] float ySpeed = 2.0f;
    CapsuleCollider2D BeeCollider;
    private float range =5;
    public static bool targetFound;
    public static int targetId =0;
    
    //Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        BeeCollider = GetComponent<CapsuleCollider2D>();
        targetFound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(targetFound)
            goToFlower();
        else
            Invoke("Searching",1);
    }

    private void OnTriggerExit2D(Collider2D collision){     
       xSpeed *= -1f;
       ySpeed *= -1f;
    }

    private void Scan(){
        int i =0;
        foreach(GameObject flower in FlowerSpawning.activeFlowers)
        {
            if(flower!= null){
                print("Scanning Loop 1");
                if(Vector2.Distance(this.transform.position,flower.transform.position) <= range)
                {
                    print("Scanning If");
                    Debug.Log("Flower in range");
                    targetFound = true;
                    targetId = i;
                    return;
                }
            }
            i++;
        }
    }

    private void Searching(){
        myRb.velocity = new Vector2(xSpeed,ySpeed);
        print("Searching");
        Scan();
    }

    private void goToFlower(){
        myRb.velocity = new Vector2(0,0);
        transform.position = Vector2.MoveTowards(transform.position, FlowerSpawning.activeFlowers[targetId].transform.position, 1.0f*Time.deltaTime);   
    }
}
