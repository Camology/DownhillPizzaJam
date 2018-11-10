using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
	private Rigidbody rb;
	float initialRotation;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		initialRotation = this.transform.rotation.y;
		
	}
	
	// Update is called once per frame
	void Update () {
		movement();
		
	}

	void movement() {
		float steer = Input.GetAxis("steer") * Time.deltaTime * 100.0f;
		float rotation = this.transform.rotation.y;
		float diff = initialRotation - rotation;
		Debug.Log(diff);
		if(diff > 0.2) {
			this.transform.Rotate(0,0.5f,0);
		}
		if(diff < -0.2) {
			this.transform.Rotate(0,-0.5f,0);
		}
		if(Mathf.Abs(steer) > 0.1 && Mathf.Abs(diff) <= 0.2 ) {
			this.transform.Rotate(0,steer,0);
			rb.AddForce(0,0,-steer/4,ForceMode.Impulse);
		}
	}
}
