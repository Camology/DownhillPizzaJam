﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
	private Rigidbody rb;
	Quaternion initialRotation;
	float MAXROTATION = 0.2f;
	bool onRoad = true;
	int score = 0;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		initialRotation = this.transform.rotation;
		
	}
	
	// Update is called once per frame
	void Update () {
		movement();
		if(rb.velocity.x < 10) {
			rb.AddForce(1f,0,0, ForceMode.Impulse);
		}
	}

	void movement() {
		float steer = Input.GetAxis("steer") * Time.deltaTime * 100.0f;
		float rotation = this.transform.rotation.y;
		float diff = initialRotation.y - rotation;

		if(diff > MAXROTATION) {
			this.transform.Rotate(0,0.3f,0);
		}
		if(diff < -MAXROTATION) {
			this.transform.Rotate(0,-0.3f,0);
		}
		if(onRoad && Mathf.Abs(steer) > 0.1 && Mathf.Abs(diff) <= MAXROTATION ) {
			this.transform.Rotate(0,steer,0);
			rb.AddForce(0,0,-steer/3,ForceMode.Impulse);
		}

		float jumpVal = Input.GetAxis("jump") * Time.deltaTime * 100.0f;

		if(jumpVal > 0 && onRoad) {
			rb.AddForce(0,7f,0, ForceMode.Impulse);
		}
	}

	void OnCollisionEnter(Collision col) { 
		if (col.gameObject.tag == "floor") { 
			onRoad = true;
		}
		if(col.gameObject.tag == "rail") {
			this.transform.rotation = new Quaternion(0,90,0,0);
			onRoad = true;

		} 
	}

	void OnCollisionExit(Collision col) {
		if(col.gameObject.tag == "floor") {
			onRoad = false;
		}
		if(col.gameObject.tag == "rail") {
			onRoad = false;
			this.transform.rotation = initialRotation;
		}
	}
}
