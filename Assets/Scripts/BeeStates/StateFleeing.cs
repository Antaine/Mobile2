using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateFleeing : State
{
    public static Vector2 hivePos = SpawnHive.spawnPosition;
    public StateFleeing(Bee connectedBee) : base(connectedBee)
    {
    }

    //Checks When at Hive 
    public override Type UpdateState()
    {
        bee.CheckEnergy(bee);
        bee.flowerFound = false;
        ReturnToHive();
        if(bee.atHive)
            return typeof(StateDancing);

        return typeof(StateFleeing);
    }
    //Moves to Hive
    private void ReturnToHive(){
        bee.energyRate = -0.3f;
        if(bee.transform.position != null){
            float dis3 = Vector2.Distance(bee.transform.position,hivePos);
            if(dis3<0.01)
            {
                bee.atHive = true;
                return;
            }
            bee.transform.position = Vector2.MoveTowards(bee.transform.position, hivePos, bee.beeSpeed*Time.deltaTime);  
        }
    }
}
