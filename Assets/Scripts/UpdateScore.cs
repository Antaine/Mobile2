﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateScore : MonoBehaviour
{
    public Text scoreText;
    public static int score;
    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }
}
