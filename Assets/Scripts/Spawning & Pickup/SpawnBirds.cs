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
    public static List<GameObject> activeBirds = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        spawnBirds();
    }

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
                BirdMovement.nestPos1 = pos;
            }

            else
                BirdMovement.nestPos2 = pos;
        }
       
        //print("Spawning Birds");
    }
}
