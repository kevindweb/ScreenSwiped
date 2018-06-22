using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// used to find gameover scene when we lose

public class DestroyPlayer : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other){
		GameObject collider = other.gameObject;
		if(collider.tag == "Player"){
			SceneManager.LoadScene(4);
			// load game over scene
			return;
		}
	}
}
