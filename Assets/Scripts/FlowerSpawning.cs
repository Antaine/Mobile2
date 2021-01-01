using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawning : MonoBehaviour
{
    public GameObject flowerPrefab;
    public GameObject quad;
    public int numOfFlowers = 5;
    public static int curFlowers =0;
    private float screenX,screenY;
    Vector2 pos;


    // Update is called once per frame
    void Update(){
        if(curFlowers<numOfFlowers){
            spawnFlowers();
        }
        
    }

    void spawnFlowers(){
        BoxCollider2D c = quad.GetComponent<BoxCollider2D>();
        while(curFlowers<numOfFlowers){
            screenX = Random.Range(c.bounds.min.x, c.bounds.max.x);
            screenY = Random.Range(c.bounds.min.y, c.bounds.max.y);
            pos = new Vector2(screenX, screenY);
            Instantiate(flowerPrefab, pos, transform.rotation);
            curFlowers++;
        }
    }


}
