using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSpawning : MonoBehaviour
{
    Vector2 pos;
    public GameObject beePrefab;
    int j = 0;
    // Start is called before the first frame update
    void Start()
    {
        for(int i =0;i<4;i++){
            Vector2 pos = new Vector2(j+=2, j);
            GameObject go = Instantiate(beePrefab, pos, transform.rotation);
            
        }    
    }
}
