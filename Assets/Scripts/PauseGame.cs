using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseGame : MonoBehaviour {

	public GameObject pausePanel;
	private void Awake(){
		pausePanel.SetActive(false);	
	}

	private void LateUpdate(){
		checkPause();	
	}

	void checkPause() {
		if (Input.GetKeyUp(KeyCode.Escape)){
			pauseGame(pausePanel.activeInHierarchy);
		}
	}

	void pauseGame(bool active){
		Time.timeScale = System.Convert.ToSingle(active);
		pausePanel.SetActive(!active);
	}
}