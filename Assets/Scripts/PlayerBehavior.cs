using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
	//private RigidBody rb;

	// Use this for initialization
	void Start () {
		//rb = GetComponent<RigidBody>();
		
	}
	
	// Update is called once per frame
	void Update () {
		movement();
		
	}

	void movement() {
		float steer = Input.GetAxis("steer") * Time.deltaTime * 150.0f;
		this.transform.Rotate(0,steer,0);
	}
}
