using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour {
	public Button[] buttons;
	public Button quitButton;
	// public Button current;
	// current used for showing player which button is focused
	public int currentIndex = 1;
	void Start(){
		buttons[currentIndex].GetComponent<Image>().color = Color.red;
	}
	void Update(){
		int test = currentIndex;
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			if(currentIndex >= buttons.GetLength(0) - 1)
				currentIndex = 0;
			else
				currentIndex++;
		} else if(Input.GetKeyDown(KeyCode.LeftArrow)){
			if(currentIndex <= 0)
				currentIndex = buttons.GetLength(0) - 1;
			else
				currentIndex--;
		} else if(Input.GetKeyDown(KeyCode.Return)){
			// click this button
			buttons[currentIndex].onClick.Invoke();
		} else if(Input.GetKeyDown(KeyCode.Q)){
			// click this button
			quitButton.onClick.Invoke();
		}
		if(currentIndex != test){
			// arrow was pressed
			buttons[test].GetComponent<Image>().color = Color.white;
			// change previous button color to white again
			buttons[currentIndex].GetComponent<Image>().color = Color.red;
			// focus on new button
		}
	}
	public void ClickGame(){
		SceneManager.LoadScene(1);
	}
	public void ClickDifficulty(){
		SceneManager.LoadScene(2);
	}
	public void ClickInstructions(){
		SceneManager.LoadScene(3);
	}
	public void ClickQuit(){
		Application.Quit();
	}
}
