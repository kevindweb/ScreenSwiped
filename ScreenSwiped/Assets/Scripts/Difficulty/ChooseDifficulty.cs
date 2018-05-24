using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseDifficulty : MonoBehaviour {
	public Button[] buttons;
	// public Button current;
	// current used for showing player which button is focused
	public int currentIndex = 1;
	private string difficulty;
	private DataLoader access;
	private string file = "difficulty.dat";
	void Start(){
		access = ScriptableObject.CreateInstance("DataLoader") as DataLoader;
		difficulty = access.Load("", file);
		if(difficulty == null){
			// set to default normal difficulty
			difficulty = "normal";
			access.Save(difficulty, file);
		} else{
			if(difficulty == "normal")
				currentIndex = 1;
			else if(difficulty == "easy")
				currentIndex = 0;
			else
				currentIndex = 2;
		}
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
		}
		if(currentIndex != test){
			// arrow was pressed
			buttons[test].GetComponent<Image>().color = Color.white;
			// change previous button color to white again
			buttons[currentIndex].GetComponent<Image>().color = Color.red;
			// focus on new button
		}
	}
	public void EasyDifficulty(){
		if(difficulty != "easy"){
			// set difficulty to easy
			difficulty = "easy";
			access.Save(difficulty, file);
		}
	}
	public void NormalDifficulty(){
		if(difficulty != "normal"){
			// set difficulty to easy
			difficulty = "normal";
			access.Save(difficulty, file);
		}
	}
	public void HardDifficulty(){
		if(difficulty != "hard"){
			// set difficulty to easy
			difficulty = "hard";
			access.Save(difficulty, file);
		}
	}
	public void ClickLoad(){
		// go to load screen
		SceneManager.LoadScene("LoadScene");
	}
}
