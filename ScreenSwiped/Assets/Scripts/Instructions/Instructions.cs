using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Instructions : MonoBehaviour {
	public Button backButton;
	public Button quitButton;
	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			// press back button
			backButton.onClick.Invoke();
		} else if(Input.GetKeyDown(KeyCode.Q)){
			// press back button
			quitButton.onClick.Invoke();
		}
	}
	public void ClickLoad(){
		// go to load screen
		SceneManager.LoadScene(0);
	}
	public void ClickQuit(){
		// quit application
		Application.Quit();		
	}
}
