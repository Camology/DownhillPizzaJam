using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
	private Rigidbody rbody;
	public ParticleSystem explosionObject;
	void Start() {
		rbody = GetComponent<Rigidbody>();
	}

	void Update() {
		if (Time.timeScale != 0){
			rbody.AddForce(-20f,0,0);
		} 
		
	}
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Player") {
			ParticleSystem explosion = Instantiate(explosionObject) as ParticleSystem;
			explosion.transform.position = transform.position;
			//explosion.loop = false;
			explosion.Play();
			Destroy(explosion.gameObject,explosion.main.duration);
			Destroy(gameObject);
		}
	}
}
