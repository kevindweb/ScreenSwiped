using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// used to start game over again

public class Restart : MonoBehaviour {
	void OnGUI(){
		int h = 30;
		int w = 100;
		// Debug.Log("highScore: " + highScore);
		float height = (Screen.height-h) * .25f;
		float width = (Screen.width-w) * .5f;
		GUI.Label(new Rect(width, height, w, h), "GAME OVER!!");
	}

	public void RestartGame(){
		SceneManager.LoadScene("StartScene");
	}
}
