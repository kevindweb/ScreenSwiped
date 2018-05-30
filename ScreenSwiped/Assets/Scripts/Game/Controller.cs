using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {
	public float horizontalGravity = 4.9f;
	public float magnetForce = 20;
	private Rigidbody2D rg2d;
	private string difficulty;
	private DataLoader access;
	private string file = "difficulty.dat";
	private Dictionary<string, float> difficulties = new Dictionary<string, float>();
	public Dictionary<int, Transform> magnetField;
	void Awake(){
		magnetField = new Dictionary<int, Transform>();
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
		// rg2d.AddForce(new Vector2(horizontalGravity, 0));
		// make horizontal gravity
		if(magnetField.Count != 0){
			Debug.Log("lskdjf");
			float step = magnetForce * Time.deltaTime;
			transform.position = Vector2.MoveTowards(transform.position, magnetField[1].position, step);
			// Vector2 distance = new Vector2(magnetField.position.x - transform.position.x, magnetField.position.y - transform.position.y);
			// rg2d.AddForce(distance * magnetForce * Time.deltaTime);
		}
	}

	public void Magnet(Transform magnet, bool pull){
		if(pull){
			magnetField.Add(1, magnet);
			Debug.Log(magnetField.Count);
		} else{
			// stop being pulled by magnet
			magnetField.Remove(1);
			Debug.Log(magnetField.Count);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		GameObject collider = other.gameObject;
		if(collider.tag == "Enemy"){
			// we lost because we hit an enemy
			SceneManager.LoadScene("GameOver");
		}
	}
}
