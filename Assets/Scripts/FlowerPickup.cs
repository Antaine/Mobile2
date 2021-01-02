using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Bees"){
            Destroy(gameObject);
            //FlowerSpawning.activeFlowers.RemoveAt(collision.index);
            FlowerSpawning.curFlowers--;
        }
        print(collision.tag);
    }

  //  public void DeleteTile(){
  //%      Destroy(activeFlowers[0]);
  //      activeTiles.RemoveAt(0);
   // }

}
