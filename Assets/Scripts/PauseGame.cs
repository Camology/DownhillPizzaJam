using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseGame : MonoBehaviour {

	public GameObject pausePanel;
	public GameObject gameOverPanel;
	public PlayerBehavior playerScript;
	private void Awake(){
		pausePanel.SetActive(false);
		gameOverPanel.SetActive(false);	
	}

	private void Update(){
		checkHealth();
		checkPause();			
	}

	void checkPause() {
		if (Input.GetKeyUp(KeyCode.Escape)){
			pauseGame(pausePanel.activeInHierarchy);
		}
	}

	void checkHealth(){
		int health = playerScript.getHealth();
		if (health < 1){
			gameOver();
		} 
	}

	void pauseGame(bool active){
		Time.timeScale = System.Convert.ToSingle(active);
		pausePanel.SetActive(!active);
	}

	void gameOver() {
		Time.timeScale = 0;
		gameOverPanel.SetActive(true);
	}
}