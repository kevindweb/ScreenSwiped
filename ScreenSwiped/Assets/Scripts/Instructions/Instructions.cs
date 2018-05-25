using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Instructions : MonoBehaviour {
	public Button backButton;
	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			// press back button
			backButton.onClick.Invoke();
		}
	}
	public void ClickLoad(){
		// go to load screen
		SceneManager.LoadScene("LoadScene");
	}
}
