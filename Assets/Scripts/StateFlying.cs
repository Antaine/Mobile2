using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateFlying : State
{
    private float dis1=0;
    private float dis2 = 6;
    private float range = 3f;

    public StateFlying(Bird connectedBird) : base(connectedBird)
    {
    }

    public override Type UpdateState()
    {
        //bee.CheckEnergy(bee);
        //bee.energyRate = -0.1f;
        bird.myRb.velocity = bird.birdDir*bird.speed;
        ScanBees();
        if(bird.isChasing){
            Debug.Log("Found bee");
            return typeof(StateChasing);
        }

        return typeof(StateFlying);
    }

    private void ScanBees(){
        int i =0;
        foreach(GameObject bee in SpawnHive.activeBees)
        {
            if(bee.transform != null){
                this.dis1 = Vector2.Distance(bird.transform.position,bee.transform.position);
                if(this.dis1 < this.dis2){
                    if(Vector2.Distance(bird.transform.position,bee.transform.position) <= range)
                    {
                        this.dis2 = this.dis1;
                        bird.isChasing = true;
                        bird.targetId = i;
                    }
                }      
            }
            i++;
        }
        this.dis1 = 0;
        this.dis2 = range+5;
    }


}
