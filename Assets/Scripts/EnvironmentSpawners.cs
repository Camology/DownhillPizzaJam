using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawners : MonoBehaviour {
	public GameObject player;
	public GameObject car;
	public GameObject building;
	public GameObject streetObj;
	public GameObject street;
	public float levelLength = 500f;
	float playerHeight = 5f;

	// Use this for initialization
	void Start () {
		street = Instantiate(streetObj);
		MeshRenderer streetMesh = street.GetComponent<MeshRenderer>();
		float initPosX = street.transform.position.x - streetMesh.bounds.size.x / 2.0f;
		Vector3 temp = street.transform.localScale;
		float logScale = Mathf.Ceil(Mathf.Log10(temp.x));
		temp.x = levelLength * (logScale * 10);
		street.transform.localScale = temp;

        temp = street.transform.position;
		temp.x = initPosX + streetMesh.bounds.size.x / 2.0f;
		temp.y -= playerHeight;
		street.transform.position = temp;
		InvokeRepeating("SpawnCar",0,1f);
		SpawnBuildings();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnCar() {
        GameObject newCar = Instantiate(car);
		newCar.transform.position = player.transform.position;
		Vector3 temp = new Vector3(30.0f,0,0);
		newCar.transform.position += temp;
		Destroy(newCar,3f);
    }
	void SpawnBuildings() {
		float lengthCovered = 0f;
		float streetWidth = street.GetComponent<MeshRenderer>().bounds.size.z;
		float streetLength = street.GetComponent<MeshRenderer>().bounds.size.x;
		float nextBuildingX = 0f;
		float alleyLength = 3f;
		bool firstLoop = true;

		Vector3 temp;
		Debug.Log("street wdith"+levelLength);
		while (lengthCovered < streetLength) {
			Debug.Log("length covered: "+lengthCovered);
			GameObject newBuilding = Instantiate(building);
			MeshRenderer mesh = newBuilding.GetComponent<MeshRenderer>();
			
			// Randomize building size
			Vector3 tempScale = newBuilding.transform.localScale;
			var previousBottom = mesh.bounds.center.y - mesh.bounds.size.y / 2;
			tempScale.y *= Random.Range(0.5f,2f);
			tempScale.z *= Random.Range(0.5f,2f);
			newBuilding.transform.localScale = tempScale;

			// Update building position
			if(firstLoop) {
				nextBuildingX = newBuilding.transform.position.x - mesh.bounds.size.x / 2;
				firstLoop = false;
			}
			nextBuildingX += mesh.bounds.size.x / 2;
			lengthCovered += mesh.bounds.size.x / 2;

			var newBottom = mesh.bounds.center.y - mesh.bounds.size.y / 2;
			Vector3 tempPos = newBuilding.transform.position;
			tempPos.x = nextBuildingX;
			tempPos.y += (previousBottom-newBottom);
			newBuilding.transform.position = tempPos;
			
			// Update position of next building
			nextBuildingX += mesh.bounds.size.x / 2 + alleyLength;
			lengthCovered += mesh.bounds.size.x / 2 + alleyLength;

			newBuilding.transform.Rotate(30f,0,0);
			// Make identical building on other side of street
			GameObject mirrorBuilding = Instantiate(newBuilding);
			MeshRenderer mirrorRender = mirrorBuilding.GetComponent<MeshRenderer>();
			tempPos = mirrorBuilding.transform.position;
			mirrorBuilding.transform.Rotate(0,0,180f);

			float buildingWdith = mirrorRender.bounds.size.z;
			tempPos.z -= (streetWidth + buildingWdith);

			mirrorBuilding.transform.position = tempPos;
		}
	}
}
