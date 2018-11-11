using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
	private Rigidbody rb;
	Quaternion initialRotation;
	float MAXROTATION = 0.2f;
	bool onRoad = true;
	int score = 0;
	int playerHealth = 6;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		initialRotation = this.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 1){
			movement();
			if(rb.velocity.x < 10) {
				rb.AddForce(1f,0,0, ForceMode.Impulse);
			}

			if(rb.velocity.y < 4 && !onRoad) {
				rb.AddForce(0,-20f,0,ForceMode.Acceleration);
			}
			if(Mathf.Abs(rb.rotation.z) > 0.2f) {
				Vector3 newDir = Vector3.RotateTowards(transform.forward,new Vector3(rb.rotation.x,rb.rotation.y,0), 0.05f, 0.0f);
				transform.rotation = Quaternion.LookRotation(newDir);
			}
		}
	}

	void movement() {
		float steer = Input.GetAxis("steer") * Time.deltaTime * 75.0f;
		float rotation = this.transform.rotation.y;
		float diff = initialRotation.y - rotation;
		
		if(diff > MAXROTATION) {
			this.transform.Rotate(0,0.3f,0);
		}
        if (diff < -MAXROTATION){
            this.transform.Rotate(0, -0.3f, 0);
        }
        if(onRoad && Mathf.Abs(steer) > 0.1 && Mathf.Abs(diff) <= MAXROTATION ) {
			this.transform.Rotate(0,steer,0);
			rb.AddForce(0,0,-steer/1.5f,ForceMode.Impulse);
		}

		float jumpVal = Input.GetAxis("jump") * Time.deltaTime * 100.0f;

		if(jumpVal > 0 && onRoad) {
			onRoad = false;
			rb.AddForce(2f,10f,0, ForceMode.Impulse);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (playerHealth > 0) {
			--playerHealth;
			if (playerHealth == 0) {
				//Death
			}
		}
	}
	void OnCollisionEnter(Collision col) { 
		if (col.gameObject.tag == "damage" && playerHealth > 0) {
            rb = GetComponent<Rigidbody>();
            //rb.constraints = RigidbodyConstraints.FreezeRotationY;
			--playerHealth;
            if (playerHealth == 0)
            {
                //Death
            }
		}
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
		if(col.gameObject.tag == "pizza") {
			score++;
		}
	}

	public int getHealth() {
		return playerHealth;
	}

	public void setHealth(int h) {
		this.playerHealth = h;
	}
}