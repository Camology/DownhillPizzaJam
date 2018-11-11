using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
	public ParticleSystem explosionObject;
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
