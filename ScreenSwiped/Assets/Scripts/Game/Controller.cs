using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {
	public float horizontalGravity = 4.9f;
	public float magnetForce = 20;
	public SpawnEnemy enemySpawnScript;
	private Rigidbody2D rg2d;
	private string difficulty;
	private DataLoader access;
	private string file = "difficulty.dat";
	private Dictionary<string, float> difficulties = new Dictionary<string, float>();
	private Dictionary<int, Transform> magnetField;
	private float startTime;
	private float checkTime;
	private float yVelocity;
	private float prevVelocity;
	// used to check if player is not moving up or down
	private bool increasedMovement = false;
	private bool unsetVariables = true;
	void Awake(){
		startTime = Time.time;
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
		if(Time.time - startTime > 2){
			// wait a few seconds before testing for no movement
			if(unsetVariables){
				unsetVariables = false;
				Debug.Log("being set");
				// set time and current y velocity
				yVelocity = Mathf.Abs(rg2d.velocity.y);
				prevVelocity = 0.0f;
				checkTime = Time.time;
			} else{
				prevVelocity = yVelocity;
				// previous frame's velocity
				yVelocity = Mathf.Abs(rg2d.velocity.y);
				// current frame's velocity
			}
			if(Time.time - checkTime > 1){
				// check after 1 second
				checkTime = Time.time;
				enemySpawnScript.MoveFaster();
				increasedMovement = true;
			} else{
				if(yVelocity > .4f && prevVelocity > .4f){
					// player hasn't moved a significant amount
					if(increasedMovement){
						increasedMovement = false;
						// only change speed back to default if we ever changed it
						enemySpawnScript.DefaultSpeed();
					}
					checkTime = Time.time;
				}
			}
		}
		if(Input.GetKeyDown(KeyCode.R)){
			// check for restart
			SceneManager.LoadScene(4);
			// load game over scene
		}
	}

	void FixedUpdate(){
		rg2d.AddForce(new Vector2(horizontalGravity, 0));
		// make horizontal gravity
		if(magnetField.Count != 0){
			float step = magnetForce * Time.deltaTime;
			transform.position = Vector2.MoveTowards(transform.position, magnetField[1].position, step);
		}
	}

	public void Magnet(Transform magnet, bool pull){
		if(pull){
			magnetField.Add(1, magnet);
		} else{
			// stop being pulled by magnet
			magnetField.Remove(1);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		GameObject collider = other.gameObject;
		if(collider.tag == "Enemy"){
			// we lost because we hit an enemy
			SceneManager.LoadScene(4);
		}
	}
}
