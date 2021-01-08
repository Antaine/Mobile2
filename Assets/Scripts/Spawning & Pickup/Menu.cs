using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Text flowerNumberText;
    private string FlowerNumberString = "";

    //Manages Menu
    public void getFlowerNum()
    {
        FlowerNumberString = flowerNumberText.text;
        FlowerSpawning.numOfFlowers =  Int16.Parse(FlowerNumberString);
        print(FlowerNumberString);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
}
