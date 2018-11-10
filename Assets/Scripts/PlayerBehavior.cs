using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
	private Rigidbody rb;
	float initialRotation;
	float MAXROTATION = 0.2f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		initialRotation = this.transform.rotation.y;
		
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
		float diff = initialRotation - rotation;

		if(diff > MAXROTATION) {
			this.transform.Rotate(0,0.3f,0);
		}
		if(diff < -MAXROTATION) {
			this.transform.Rotate(0,-0.3f,0);
		}
		if(Mathf.Abs(steer) > 0.1 && Mathf.Abs(diff) <= MAXROTATION ) {
			this.transform.Rotate(0,steer,0);
			rb.AddForce(0,0,-steer/3,ForceMode.Impulse);
		}
	}
}
