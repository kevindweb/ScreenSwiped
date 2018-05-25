using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {
	private DataLoader access;
	private float score = 0.0f;
	private int highScore;
	private int newHighScore;
	private string difficulty;
	private string difficultyFile = "difficulty.dat";
	private string scoreFile = "score.dat";
	private string prevScoreFile = "prevScore.dat";
	void Start(){
		access = ScriptableObject.CreateInstance("DataLoader") as DataLoader;
		difficulty = access.Load("", difficultyFile);
		highScore = access.Load(0, scoreFile);
		if(difficulty == null){
			// set to default normal difficulty
			difficulty = "normal";
			access.Save(difficulty, difficultyFile);
		}
		newHighScore = highScore;
	}
	void Update(){
		float currPos = transform.position.x;
		if(currPos > score)
			score = currPos;
			if(score > newHighScore)
				newHighScore = (int) score;
			// score changes when player (camera) moves
	}
	void OnDestroy(){
		if(newHighScore > highScore){
			// save the new high score
			access.Save(newHighScore, scoreFile);
		}
		access.Save(highScore, prevScoreFile);
		// overwrite previous score and set current
	}
	void OnGUI(){
		int h = 30;
		int difficultyWidth = 200;
		int scoreWidth = 100;
		GUI.color = Color.red;
		GUI.Label(new Rect(0, Screen.height - (3*h), difficultyWidth, h), "Difficulty: " + difficulty);
		GUI.Label(new Rect(0, Screen.height - (2*h), scoreWidth, h), "Score: " + (int) score);
		GUI.Label(new Rect(0, Screen.height - h, scoreWidth, h), "High Score: " + (int) newHighScore);
		// put difficulty setting, score, and high score in bottom left of screen
	}
}
