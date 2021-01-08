using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBirds : MonoBehaviour
{
    public GameObject birdPrefab;
    public GameObject nestPrefab;
    public GameObject quad;
    public int numOfBirds = 2;
    private int i =0;
    private float screenX,screenY;
    Vector2 pos;
    public static Vector2 nest1,nest2;
    public static List<GameObject> activeBirds = new List<GameObject>();

    void Start()
    {
        spawnBirds();
    }

    //Spawns Birds
   private void spawnBirds(){
        BoxCollider2D c = quad.GetComponent<BoxCollider2D>();
        for(i =0;i<numOfBirds;i++){
            screenX = Random.Range(c.bounds.min.x, c.bounds.max.x);
            screenY = Random.Range(c.bounds.min.y, c.bounds.max.y);
            pos = new Vector2(screenX, screenY);
            GameObject nest = Instantiate(nestPrefab, pos, transform.rotation);
            GameObject bird = Instantiate(birdPrefab, pos, transform.rotation);
            activeBirds.Add(bird);
           if(i==0){
                nest1 = pos;
            }
            else
                nest2 = pos;
        }
    }
}
