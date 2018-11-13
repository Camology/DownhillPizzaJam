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
	public GameObject goal;
	public float levelLength = 100f;
	float playerHeight = 5f;

	// Use this for initialization
	void Start () {
		Vector3 temp;

		street = Instantiate(streetObj);
		streetMesh = street.GetComponent<MeshRenderer>();

		temp = player.transform.position;
		temp.x += streetMesh.bounds.size.x / 2;
		temp.x -= player.GetComponent<BoxCollider>().bounds.size.x / 2;
		temp.y -= 1f;
		street.transform.position = temp;

		float initPosX = street.transform.position.x - streetMesh.bounds.size.x / 2.0f;
		temp = street.transform.localScale;
		float scaleAmount = levelLength / streetMesh.bounds.size.x;
		temp.x *= scaleAmount;
		street.transform.localScale = temp;

        temp = street.transform.position;
		temp.x = initPosX + streetMesh.bounds.size.x / 2.0f;
		//temp.y -= playerHeight;
		street.transform.position = temp;

		InvokeRepeating("SpawnCar",1,1f);
		SpawnBuildings();
		spawnTrash();
		SpawnFlowers();
		makeGoal();
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
		float alleyLength = 5f;
		bool firstLoop = true;

		Vector3 temp;
		while (lengthCovered < streetLength) {
			GameObject newBuilding = Instantiate(building);
			MeshRenderer mesh = newBuilding.GetComponent<MeshRenderer>();
			
			temp = player.transform.position;
			temp.x = street.transform.position.x - streetLength / 2 + mesh.bounds.size.x / 2;
			temp.z += streetWidth / 2 + mesh.bounds.size.z / 2;
			temp.y = street.transform.position.y - 7.33f;
			newBuilding.transform.position = temp;

			// Randomize building height and width
			temp = newBuilding.transform.localScale;
			var prevBottom = mesh.bounds.center.y - mesh.bounds.size.y / 2;
			temp.y *= Random.Range(0.5f,2f);
			temp.z *= Random.Range(0.5f,2f);
			newBuilding.transform.localScale = temp;

			// Update building position
			if(firstLoop) {
				nextBuildingX = newBuilding.transform.position.x - mesh.bounds.size.x / 2;
				firstLoop = false;
			}
			nextBuildingX += mesh.bounds.size.x / 2;
			lengthCovered += mesh.bounds.size.x / 2;

			var newBottom1 = mesh.bounds.center.y - mesh.bounds.size.y / 2;
			Vector3 tempPos1 = newBuilding.transform.position;
			tempPos1.x = nextBuildingX;
			tempPos1.y += (prevBottom-newBottom1);
			newBuilding.transform.position = tempPos1;
			
			// Update position of next building
			nextBuildingX += mesh.bounds.size.x / 2 + alleyLength;
			lengthCovered += mesh.bounds.size.x / 2 + alleyLength;

			newBuilding.transform.Rotate(30f,0,0);

			// Make identical building on other side of street
			GameObject mirrorBuilding = Instantiate(newBuilding);
			MeshRenderer mirrorLast = mirrorBuilding.GetComponent<MeshRenderer>();
			tempPos1 = mirrorBuilding.transform.position;
			mirrorBuilding.transform.Rotate(0,0,180f);

			float buildingWdith1 = mirrorLast.bounds.size.z;
			tempPos1.z -= (streetWidth + buildingWdith1);

			mirrorBuilding.transform.position = tempPos1;
		}

		/*
		GameObject lastBuilding = Instantiate(building);
		MeshRenderer lastMesh = lastBuilding.GetComponent<MeshRenderer>();
		
		temp = lastBuilding.transform.position;
		temp.x = levelLength;
		temp.y = street.transform.position.y;
		lastBuilding.transform.position = temp;

		temp = lastBuilding.transform.localScale;
		temp.y *= 10f;
		lastBuilding.transform.localScale = temp;

		lastBuilding.transform.Rotate(30f,0,0);

		// Make identical building on other side of street
		GameObject lastMirror = Instantiate(lastBuilding);
		MeshRenderer mirrorRender = lastMirror.GetComponent<MeshRenderer>();
		temp = lastMirror.transform.position;
		lastMirror.transform.Rotate(0,0,180f);

		float buildingWdith = mirrorRender.bounds.size.z;
		temp.z -= (streetWidth + buildingWdith);

		lastMirror.transform.position = temp;
		*/
	}

	void spawnTrash() {
		float lengthCovered = 20f;
		Vector3 temp;
		GameObject newTrash = trashCan;
		while (lengthCovered < levelLength) {
			float separation = Random.Range(5f,30f);
			newTrash = Instantiate(trashCan);
			newTrash.transform.position = player.transform.position;
			temp = newTrash.transform.position;
			temp.x = player.transform.position.x + lengthCovered;
			temp.y = street.transform.position.y;
			temp.z = street.transform.position.z - streetMesh.bounds.size.z / 2f + 2.3f;
			newTrash.transform.position = temp;

			GameObject  trashBody = newTrash.transform.GetChild (0).gameObject;
			float trashWidth = trashBody.GetComponent<MeshRenderer>().bounds.size.x;
			lengthCovered += separation + trashWidth;
		}

		while (lengthCovered > levelLength) {
			temp = newTrash.transform.position;
			temp.x -=1f;
			lengthCovered -= 1f;
			newTrash.transform.position = temp;
		}
	}
	
	void SpawnFlowers() {
		float lengthCovered = 20f;
		GameObject newBed;
		float bedWidth;
		while (lengthCovered < levelLength) {
			float separation = 30f;
			newBed = Instantiate(flowerPot);
			newBed.transform.position = player.transform.position;
			Vector3 temp = newBed.transform.position;
			temp.x = player.transform.position.x + lengthCovered;
			temp.y = street.transform.position.y;
			temp.z = 0f;
			newBed.transform.position = temp;
			
			bedWidth = newBed.GetComponent<BoxCollider>().bounds.size.x;
			lengthCovered += separation + bedWidth;
		}
	}

	void makeGoal() {
		GameObject newGoal = Instantiate(goal);

		Vector3 temp = player.transform.position;
		temp.x = levelLength;
		newGoal.transform.position = temp;

		GameObject endBuilding = Instantiate(building);
		endBuilding.transform.Rotate(0,0,-90f);

		float streetWidth = street.GetComponent<MeshRenderer>().bounds.size.z;
		float scaleAmount = streetWidth / endBuilding.GetComponent<MeshRenderer>().bounds.size.z;
		temp = endBuilding.transform.localScale;
		temp *= scaleAmount;
		endBuilding.transform.localScale = temp;

		temp = newGoal.transform.position;
		temp.y -= scaleAmount * 7.4f;
		temp.x += endBuilding.GetComponent<MeshRenderer>().bounds.size.x / 2
				  - newGoal.GetComponent<BoxCollider>().bounds.size.x / 2;
		endBuilding.transform.position = temp;
	}
}
