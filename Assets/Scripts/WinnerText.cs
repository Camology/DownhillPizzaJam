using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// this.transform.rotation = new Quaternion(this.transform.rotation.x,this.transform.rotation.y,)
		// transform.Rotate(Vector3.up, 10f * Time.deltaTime);

		this.transform.Rotate(0,0,this.transform.rotation.z * Time.deltaTime * 20);
	}
}
