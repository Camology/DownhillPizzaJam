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
	public GameObject car;
	public GameObject building;
	public GameObject street;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		initialRotation = this.transform.rotation;

		InvokeRepeating("SpawnCar",0,1f);
		SpawnBuildings(10);
	}
	
	// Update is called once per frame
	void Update () {
		movement();
		if(rb.velocity.x < 10) {
			rb.AddForce(1f,0,0, ForceMode.Impulse);
		}

		if(rb.velocity.y < 0 && !onRoad) {
			rb.AddForce(0,-10f,0,ForceMode.Acceleration);
		}
	}

	void movement() {
		float steer = Input.GetAxis("steer") * Time.deltaTime * 100.0f;
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
			rb.AddForce(0,0,-steer/3,ForceMode.Impulse);
		}

		float jumpVal = Input.GetAxis("jump") * Time.deltaTime * 100.0f;

		if(jumpVal > 0 && onRoad) {
			onRoad = false;
			rb.AddForce(0,7f,0, ForceMode.Impulse);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (playerHealth > 0) {
			--playerHealth;
			Debug.Log(playerHealth);
			if (playerHealth == 0) {
				//Death
			}
		}
	}
	void OnCollisionEnter(Collision col) { 
		if (col.gameObject.tag == "damage" && playerHealth > 0) {
            rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotationY;
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

	void SpawnCar() {
        GameObject newCar = Instantiate(car);
		newCar.transform.position = transform.position;
		Vector3 temp = new Vector3(30.0f,0,0);
		newCar.transform.position += temp;
		Destroy(newCar,3f);
    }

	void SpawnBuildings(int numBuildings) {
		float streetWidth = street.GetComponent<MeshRenderer>().bounds.size.z;
		float nextBuildingX = 0f;
		for (int i = 0; i < numBuildings; i++) {
			GameObject newBuilding = Instantiate(building);
			MeshRenderer mesh = newBuilding.GetComponent<MeshRenderer>();
			
			// Randomize building size
			Vector3 tempScale = newBuilding.transform.localScale;
			var previousBottom = mesh.bounds.center.y - mesh.bounds.size.y / 2;
			tempScale.y *= Random.Range(0.5f,2f);
			tempScale.z *= Random.Range(0.5f,2f);
			newBuilding.transform.localScale = tempScale;

			// Update building position
			if(i == 0) {
				nextBuildingX = newBuilding.transform.position.x - mesh.bounds.size.x / 2;
			}
			nextBuildingX += mesh.bounds.size.x / 2;

			var newBottom = mesh.bounds.center.y - mesh.bounds.size.y / 2;
			Vector3 tempPos = newBuilding.transform.position;
			tempPos.x = nextBuildingX;
			tempPos.y += (previousBottom-newBottom);
			newBuilding.transform.position = tempPos;
			
			// Update position of next building
			nextBuildingX += mesh.bounds.size.x / 2;

			// Make identical building on other side of street
			GameObject mirrorBuilding = Instantiate(newBuilding);
			MeshRenderer mirrorRender = mirrorBuilding.GetComponent<MeshRenderer>();
			tempPos = mirrorBuilding.transform.position;
			mirrorBuilding.transform.Rotate(0,0,180f);

			// I couldn't figure out how to calculate much to translate the mirror by
			/*
			float weirdOffset = (mesh.bounds.center.z - mesh.bounds.size.z/2) - 
								(mirrorRender.bounds.center.z - mirrorRender.bounds.size.z/2);
			float buildingWdith = mirrorRender.bounds.size.z;
			tempPos.z -= (streetWidth + buildingWdith + weirdOffset);
			*/
			tempPos.z -= 29.5f;
			mirrorBuilding.transform.position = tempPos;
		}
	}
}