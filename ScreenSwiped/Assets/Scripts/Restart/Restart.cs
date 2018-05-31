using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// used to start game over again
using UnityEngine.UI;

public class Restart : MonoBehaviour {
	public Button backButton;
	public Button restartButton;
	public Font myFont;
	private GUIStyle myStyle;
	private DataLoader access;
	private int highScore;
	private string scoreFile = "score.dat";
	private int prevScore;
	private string prevScoreFile = "prevScore.dat";
	private int currScore;
	private string currScoreFile = "currScore.dat";
	private bool isHighScore = false;
	void Start(){
		myStyle = new GUIStyle();
		myStyle.font = myFont;
		myStyle.alignment = TextAnchor.MiddleCenter;
		access = ScriptableObject.CreateInstance("DataLoader") as DataLoader;
		highScore = access.Load(0, scoreFile);
		prevScore = access.Load(0, prevScoreFile);
		currScore = access.Load(0, currScoreFile);
		if(highScore > prevScore)
			isHighScore = true;
	}
	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			// press back button
			backButton.onClick.Invoke();
		} else if(Input.GetKeyDown(KeyCode.R)){
			// restart game
			restartButton.onClick.Invoke();
		}
	}
	void OnGUI(){
		int h = 30;
		int w = 100;
		// Debug.Log("highScore: " + highScore);
		float height = (Screen.height-h) * .25f;
		float width = (Screen.width - w) * .5f;
		GUI.Label(new Rect(width, height - h, w, h), "Score: " + currScore, myStyle);
		GUI.Label(new Rect(width, height, w, h), "GAME OVER", myStyle);
		if(isHighScore)
			GUI.Label(new Rect(width, height + h, w, h), "HIGH SCORE", myStyle);
	}
	public void RestartGame(){
		SceneManager.LoadScene("StartScene");
	}
	public void ClickLoad(){
		SceneManager.LoadScene("LoadScene");
	}
}
