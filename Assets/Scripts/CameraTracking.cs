using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour {

public Transform target;
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3(target.position.x-20,
										 target.position.y+10,
										 target.position.z);
	}
}
