using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StateSearching : State
{
    private float range = 3f;
    private float dis1=0;
    private float dis2 = 6;
    public StateSearching(Bee connectedBee) : base(connectedBee)
    {
    }

    public override Type UpdateState()
    {
        bee.CheckEnergy(bee);
        bee.energyRate = -0.1f;
        bee.myRb.velocity = bee.moveDir*bee.beeSpeed;
        DetectBird();
        if(bee.isFleeing)
        {
            bee.flowerFound = false;
            bee.myRb.velocity *= 0;
            return typeof(StateFleeing);
        }
        ScanFlowers();
        if(bee.flowerFound){
            GoToFlower();
            if(bee.atFlower){
                return typeof(StateGathering);
            }
            bee.moveDir = UnityEngine.Random.insideUnitCircle.normalized;
        }
        return typeof(StateSearching);
    }

    private void DetectBird(){
        int i =0;
        foreach(GameObject bird in SpawnBirds.activeBirds)
        {
            if(Vector2.Distance(bee.transform.position,bird.transform.position) <= range)
            {
                bee.energyRate = 0.6f;
                bee.isFleeing = true;
                return;
            }
            i++;
        }
    }

    private void ScanFlowers(){
        int i =0;
        foreach(GameObject flower in FlowerSpawning.activeFlowers)
        {
            if(flower != null){
                this.dis1 = Vector2.Distance(bee.transform.position,flower.transform.position);
                if(this.dis1 < this.dis2){
                    if(Vector2.Distance(bee.transform.position,flower.transform.position) <= range && bee.isFleeing == false)
                    {
                        this.dis2 = this.dis1;
                        bee.flowerFound = true;
                        bee.targetId = i;
                    }
                }      
            }
            i++;
        }
        this.dis1 = 0;
        this.dis2 = range+5;
    }

    private void GoToFlower(){
        bee.myRb.velocity *= 0;
        if(FlowerSpawning.activeFlowers[bee.targetId] != null)
        {
            bee.transform.position = Vector2.MoveTowards(bee.transform.position, FlowerSpawning.activeFlowers[bee.targetId].transform.position, bee.beeSpeed*Time.deltaTime);   
        }
        else{
            bee.flowerFound = false;
        }
    }

    



}
