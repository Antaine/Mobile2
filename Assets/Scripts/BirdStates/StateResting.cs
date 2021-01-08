using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateResting : State
{
    private float dis1=0;
    private float dis2 = 6;
    private float range = 3f;
    public StateResting(Bird connectedBird) : base(connectedBird)
    {
    }

    public override Type UpdateState()
    {
        Rest();
        bird.CheckEnergy(bird);
        ScanBees();
        if(bird.isChasing)
        {
            return typeof(StateChasing);
        }
        if(!bird.isResting){
            return typeof(StateFlying);
        }
        return typeof(StateResting);
    }

    private void Rest(){
        bird.myRb.velocity = new Vector2(0,0);
        bird.energyRate = 0.3f;
        bird.isResting = true;
        if(bird.currEnergy>=bird.maxEnergy){
            bird.isResting = false;
            bird.atNest = false;
        }
    }

    private void ScanBees(){
        int i =0;
        foreach(GameObject bee in SpawnHive.activeBees)
        {
            if(bee != null){
                this.dis1 = Vector2.Distance(bird.transform.position,bee.transform.position);
                if(this.dis1 < this.dis2){
                    if(Vector2.Distance(bird.transform.position,bee.transform.position) <= range)
                    {
                        this.dis2 = this.dis1;
                        bird.isChasing = true;
                        bird.atNest = false;
                        bird.isResting =false;
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
