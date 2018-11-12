using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseGame : MonoBehaviour {

	public GameObject pausePanel;
	public GameObject gameOverPanel;

	public GameObject winPanel;
	public PlayerBehavior playerScript;
	bool hitEnd = false;
	private void Awake(){
		pausePanel.SetActive(false);
		gameOverPanel.SetActive(false);	
		winPanel.SetActive(false);
	}

	private void LateUpdate(){
		checkHealth();
		checkPause();
		checkWin();			
	}

	public void setHitEnd(bool status) {
		this.hitEnd = status;
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

	void checkWin(){
		if(hitEnd) { 
			winner();
		}
	}

	void pauseGame(bool active){
		Time.timeScale = System.Convert.ToSingle(active);
		pausePanel.SetActive(!active);
	}
	

	void gameOver() {
		Time.timeScale = 0;
		playerScript.setHealth(6);
		gameOverPanel.SetActive(true);
	}

	void winner() {
		Time.timeScale = 0;
		winPanel.SetActive(true);
	}
}