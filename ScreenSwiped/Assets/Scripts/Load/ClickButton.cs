using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour {
	public Button[] buttons;
	// used for focusing on specific button
	public Button quitButton;
	public Button playButton;
	public int currentIndex = 1;
	private int buttonRow = 0;
	void Start(){
		playButton.GetComponent<Image>().color = Color.red;
	}
	void Update(){
		int test = currentIndex;
		int testRow = buttonRow;
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
			if(buttonRow == 0)
				playButton.onClick.Invoke();
			else
				buttons[currentIndex].onClick.Invoke();
		} else if(Input.GetKeyDown(KeyCode.Q)){
			// click this button
			quitButton.onClick.Invoke();
		} else if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)){
			// if up or down arrow pressed, switch button row focused
			if(buttonRow == 0){
				buttonRow = 1;
			} else{
				buttonRow = 0;
			}
		}
		if(buttonRow != testRow){
			if(buttonRow == 0){
				// we just switched to top row
				buttons[test].GetComponent<Image>().color = Color.white;
				playButton.GetComponent<Image>().color = Color.red;
			}
			else{
				playButton.GetComponent<Image>().color = Color.white;
				buttons[test].GetComponent<Image>().color = Color.red;
			}
		} else if(currentIndex != test && buttonRow == 1){
			// right or left arrow was pressed
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
	public void ClickPlatforms(){
		SceneManager.LoadScene(5);
	}
	public void ClickQuit(){
		Application.Quit();
	}
}
