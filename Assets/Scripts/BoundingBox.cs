using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBox : MonoBehaviour {

	public Transform transformHolder;
	private void OnTriggerExit(Collider other) {
		GameObject gameObject = other.gameObject;
		if (gameObject.tag == "Player"){
			gameObject.transform.position = new Vector3(-30,
														17,
														1);
		}
	}
}
