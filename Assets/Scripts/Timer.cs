﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

    float timer = 50.00f;
    public Text text_box;

    // Update is called once per frame
    void Update () {
        timer -= Time.deltaTime;
        text_box.text = "Tip: $"+ timer.ToString("0.00");
        if(timer < 0.0f) {
            text_box.text = "GAMEOVER!";
        }
    }

    public float getTime(){
        return timer;
    }
}
