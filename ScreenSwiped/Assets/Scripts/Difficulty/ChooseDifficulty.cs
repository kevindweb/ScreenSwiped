using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseDifficulty : MonoBehaviour {
	public Button[] buttons;
	public Button backButton;
	// public Button current;
	// current used for showing player which button is focused
	public int currentIndex = 1;
	public GameObject radiusObject;
	public float invisibleTime = 1f;
	private GameObject instantiatedRadius;
	private Color radiusColor;
	private Color invisibleColor;
	private float startTime;
	private Camera cam;
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
		cam = Camera.main;
		radiusColor = Color.black;
		invisibleColor = cam.backgroundColor;
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
		} else if(Input.GetKeyDown(KeyCode.Escape)){
			// press back button
			backButton.onClick.Invoke();
		}
		if(currentIndex != test){
			// arrow was pressed
			buttons[test].GetComponent<Image>().color = Color.white;
			// change previous button color to white again
			buttons[currentIndex].GetComponent<Image>().color = Color.red;
			// focus on new button
		}
		// radius animation here
		if(instantiatedRadius != null){
			float t = (Time.time - startTime) / invisibleTime;
			Color ourColor = Color.Lerp(radiusColor, invisibleColor, t);
			instantiatedRadius.GetComponent<Renderer>().material.SetColor("_Color", ourColor);
			if(t > 1){
				Destroy(instantiatedRadius);
				instantiatedRadius = null;
			}
		}
	}
	public void EasyDifficulty(){
		if(difficulty != "easy"){
			// set difficulty to easy
			DifficultySetBubble();
			difficulty = "easy";
			access.Save(difficulty, file);
		}
	}
	public void NormalDifficulty(){
		if(difficulty != "normal"){
			// set difficulty to normal
			DifficultySetBubble();
			difficulty = "normal";
			access.Save(difficulty, file);
		}
	}
	public void HardDifficulty(){
		if(difficulty != "hard"){
			// set difficulty to hard
			DifficultySetBubble();
			difficulty = "hard";
			access.Save(difficulty, file);
		}
	}
	public void ClickLoad(){
		// go to load screen
		SceneManager.LoadScene(0);
	}
	private void DifficultySetBubble(){
		startTime = Time.time;
		Vector3 pos = Camera.main.ScreenToWorldPoint(buttons[currentIndex].transform.position);
		pos.z = 0;
		instantiatedRadius = Instantiate(radiusObject, pos, Quaternion.identity);
		instantiatedRadius.transform.localScale *= .4f;
	}
}
