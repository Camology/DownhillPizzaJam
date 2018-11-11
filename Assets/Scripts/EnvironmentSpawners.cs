using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawners : MonoBehaviour {
	public GameObject car;
	public GameObject building;
	public GameObject street;

	// Use this for initialization
	void Start () {
		InvokeRepeating("SpawnCar",0,1f);
		SpawnBuildings(10);
	}
	
	// Update is called once per frame
	void Update () {
		
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
