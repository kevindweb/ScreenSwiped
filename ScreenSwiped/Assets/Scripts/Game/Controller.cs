using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {
	public float horizontalGravity = 4.9f;
	private Rigidbody2D rg2d;

	void Awake(){
		rg2d = GetComponent<Rigidbody2D>();
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.R)){
			// check for restart
			Debug.Log("restart!");
			SceneManager.LoadScene("GameOver");
			// load game over scene
		}
	}

	void FixedUpdate(){
		// transform.Translate(new Vector3(sideSpeed * Time.deltaTime, 0, 0));
		rg2d.AddForce(new Vector2(horizontalGravity, 0));
		// make horizontal gravity
	}

	void OnTriggerEnter2D(Collider2D other){
		GameObject collider = other.gameObject;
		if(collider.tag == "Enemy"){
			// we lost because we hit an enemy
			SceneManager.LoadScene("GameOver");
		}
	}
}
