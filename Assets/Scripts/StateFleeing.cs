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

    public override Type UpdateState()
    {
        bee.CheckEnergy(bee);
        bee.flowerFound = false;
        ReturnToHive();
        if(bee.atHive)
            return typeof(StateDancing);

        return typeof(StateFleeing);
    }

     private void ReturnToHive(){
        //bee.myRb.velocity = new Vector2(0,0);
        //Debug.Log(hivePos);
        bee.energyRate = -0.3f;
        //bee.myRb.velocity *= 0;
        if(bee.transform.position != null){
            float dis3 = Vector2.Distance(bee.transform.position,hivePos);
            if(dis3<0.01)
            {
               // Debug.Log("At Hive");
                bee.atHive = true;
                return;
            }
            bee.transform.position = Vector2.MoveTowards(bee.transform.position, hivePos, bee.beeSpeed*Time.deltaTime);  
        }
    }

}
