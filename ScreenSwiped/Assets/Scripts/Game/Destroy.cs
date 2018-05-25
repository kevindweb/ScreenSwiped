using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// used to find gameover scene when we lose

public class Destroy : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other){
		GameObject collider = other.gameObject;
		if(collider.tag == "Player"){
			// Debug.Log("player killed!");
			SceneManager.LoadScene("GameOver");
			// load game over scene
			return;
		} else if(collider.tag == "HeldPlatform")
			// do not delete platform that is not set
			return;
		Destroy(collider);
	}
}
