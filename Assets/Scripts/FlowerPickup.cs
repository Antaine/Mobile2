﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Bees"){
            Destroy(gameObject);
            FlowerSpawning.curFlowers--;
        }
        print(collision.tag);
    }

}