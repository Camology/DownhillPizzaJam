using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawners : MonoBehaviour {
	public GameObject player;
	public GameObject car;
	public GameObject van;
	public GameObject building;
	public GameObject streetObj;
	public GameObject street;
	public MeshRenderer streetMesh;
	public GameObject trashCan;
	public GameObject flowerPot;
	public float levelLength = 100f;
	float playerHeight = 5f;

	// Use this for initialization
	void Start () {
		street = Instantiate(streetObj);
		streetMesh = street.GetComponent<MeshRenderer>();
		float initPosX = street.transform.position.x - streetMesh.bounds.size.x / 2.0f;
		Vector3 temp = street.transform.localScale;
		float logScale = Mathf.Ceil(Mathf.Log10(temp.x));
		temp.x = levelLength * (logScale * 10);
		street.transform.localScale = temp;

        temp = street.transform.position;
		temp.x = initPosX + streetMesh.bounds.size.x / 2.0f;
		temp.y -= playerHeight;
		street.transform.position = temp;
		InvokeRepeating("SpawnCar",1,1f);
		SpawnBuildings();
		spawnTrash();
		SpawnFlowers();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnCar() {
		float carOrVan= Random.Range(0f,2f);
		GameObject vehicle;
		if (carOrVan < 1) {
			vehicle = Instantiate(car);
		}
		else {
			vehicle = Instantiate(van);
		}
		float leftVsRight = Random.Range(0f,2f);
		float zPos;
		if (leftVsRight < 1) {
			zPos = Random.Range(1f,8f);
		}
		else {
			zPos = Random.Range(-8f, -1f);
		}
		Vector3 temp = player.transform.position;
		temp.x += 30.0f;
		temp.z = zPos;
		vehicle.transform.position = temp;
		Destroy(vehicle,3f);
    }
	void SpawnBuildings() {
		float lengthCovered = 0f;
		float streetWidth = street.GetComponent<MeshRenderer>().bounds.size.z;
		float streetLength = street.GetComponent<MeshRenderer>().bounds.size.x;
		float nextBuildingX = 0f;
		float alleyLength = 3f;
		bool firstLoop = true;

		Vector3 temp;
		while (lengthCovered < streetLength) {
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

	void spawnTrash() {
		float lengthCovered = 20f;
		while (lengthCovered < levelLength) {
			float separation = Random.Range(5f,30f);
			GameObject newTrash = Instantiate(trashCan);
			newTrash.transform.position = player.transform.position;
			Vector3 temp = newTrash.transform.position;
			temp.x = -100f + lengthCovered;
			temp.y = street.transform.position.y;
			temp.z = street.transform.position.z - streetMesh.bounds.size.z / 2f + 2.3f;
			newTrash.transform.position = temp;

			float trashWidth = 0f;
			lengthCovered += separation + trashWidth;
		}
	}

	void SpawnFlowers() {
		float lengthCovered = 20f;
		while (lengthCovered < levelLength) {
			float separation = 30f;
			GameObject newBed = Instantiate(flowerPot);
			newBed.transform.position = player.transform.position;
			Vector3 temp = newBed.transform.position;
			temp.x = -100f + lengthCovered;
			temp.y = street.transform.position.y;
			temp.z = 0f;
			newBed.transform.position = temp;

			float bedWidth = 0f;
			lengthCovered += separation + bedWidth;
		}
	}
}
