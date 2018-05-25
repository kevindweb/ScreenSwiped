using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {
	public float horizontalGravity = 4.9f;
	private Rigidbody2D rg2d;
	private string difficulty;
	private DataLoader access;
	private string file = "difficulty.dat";
	private Dictionary<string, float> difficulties = new Dictionary<string, float>();
	void Awake(){
		difficulties.Add("easy", 0.3f);
		difficulties.Add("normal", 0.5f);
		difficulties.Add("hard", 0.75f);
		rg2d = GetComponent<Rigidbody2D>();
		access = ScriptableObject.CreateInstance("DataLoader") as DataLoader;
		difficulty = access.Load("", file);
		if(difficulty == null){
			difficulty = "normal";
		}
		float timeSetting = difficulties[difficulty];
		// set time setting according to difficulty setting
		Time.timeScale = timeSetting;
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.R)){
			// check for restart
			SceneManager.LoadScene("GameOver");
			// load game over scene
		}
	}

	void FixedUpdate(){
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
