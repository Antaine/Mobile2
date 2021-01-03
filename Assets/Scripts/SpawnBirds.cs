using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBirds : MonoBehaviour
{
    public GameObject birdPrefab;
    public GameObject nestPrefab;
    public GameObject quad;
    public int numOfBirds = 2;
    private float screenX,screenY;
    Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        spawnBirds();
        spawnBirds();
    }

    void spawnBirds(){
        BoxCollider2D c = quad.GetComponent<BoxCollider2D>();
        screenX = Random.Range(c.bounds.min.x, c.bounds.max.x);
        screenY = Random.Range(c.bounds.min.y, c.bounds.max.y);
        pos = new Vector2(screenX, screenY);
        GameObject nest = Instantiate(nestPrefab, pos, transform.rotation);
        GameObject bird = Instantiate(birdPrefab, pos, transform.rotation);
        print("Spawning Birds");
    }
}
