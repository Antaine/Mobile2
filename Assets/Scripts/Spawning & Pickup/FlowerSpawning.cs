﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FlowerSpawning : MonoBehaviour
{
    public GameObject flowerPrefab;
    public static List<GameObject> activeFlowers = new List<GameObject>();
    public GameObject quad;
    public static int numOfFlowers = 5;
    public static int curFlowers =0;
    private float screenX,screenY;
    Vector2 pos;

    void Start(){
        while(curFlowers<numOfFlowers){
            spawnFlowers();
        }
    }

    // Spawns Flowers
    void Update(){
        if(curFlowers<numOfFlowers){
            spawnFlowers();
        } 
    }

    void spawnFlowers(){
        BoxCollider2D c = quad.GetComponent<BoxCollider2D>();
        screenX = Random.Range(c.bounds.min.x, c.bounds.max.x);
        screenY = Random.Range(c.bounds.min.y, c.bounds.max.y);
        pos = new Vector2(screenX, screenY);
        GameObject go = Instantiate(flowerPrefab, pos, transform.rotation);
        activeFlowers.Add(go);
        curFlowers++;
    }


}
