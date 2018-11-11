using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
	public GameObject player;
	public GameObject pizzaUI;
	public PlayerBehavior playerScript;
	public int playerHealth;
	public GameObject slice1;
	public GameObject slice2;
	public GameObject slice3;
	public GameObject slice4;
	public GameObject slice5;
	public GameObject slice6;
	public GameObject[] slices = new GameObject[6];
	// Use this for initialization
	void Start () {
		playerScript = player.GetComponent<PlayerBehavior>();
		slices[0] = slice1;
		slices[1] = slice2;
		slices[2] = slice3;
		slices[3] = slice4;
		slices[4] = slice5;
		slices[5] = slice6;
	}
	
	// Update is called once per frame
	void Update () {
		playerHealth = playerScript.getHealth();
		for (int i = 5; i >= playerHealth; i--) {
			slices[i].SetActive(false);
		}
	}
}
