using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {
	public GameObject[] enemies;
	public GameObject floor;
	public GameObject ceiling;
	public float spawnMin = 1f;
	public float spawnMax = 1.0f;
	private float spawnIncrease = .3f;
	private int currentStep = 100;
	private float originalPosition;
	private float defaultSpeed;
	private float moveSpeed;
	void Awake(){
		originalPosition = transform.position.x;
		currentStep += (int) originalPosition;
		defaultSpeed = enemies[0].GetComponent<FollowPlayer>().moveSpeed;
		moveSpeed = defaultSpeed;
	}
	void Update(){
		if(transform.position.x > currentStep){
			currentStep += 100;
			spawnMin = spawnMin * (1 - spawnIncrease);
			spawnMax = spawnMax * (1 - spawnIncrease);
			// decrease time in between spawns (increasing spawn rate)
		}
	}
	void Start(){
		Spawn();
	}
	void Spawn(){
		// spawn enemy between floor and ceiling
		float objectHeight = floor.transform.localScale.y / 2.0f;
		float topOfFloor = floor.transform.position.y + objectHeight;
		float bottomOfCeiling = ceiling.transform.position.y - objectHeight;
		// these are the y-axis bounds that we can set our enemy
		Vector3 pos = transform.position;
		GameObject enemy = enemies[Random.Range(0, enemies.GetLength(0))];
		float enemyHeight = (enemy.transform.localScale.y / 2.0f) * 1.414f;
		// enemyHeight is distance from center to one corner of cube (we are a diamond)
		float y = transform.position.y + Random.Range(topOfFloor + enemyHeight, bottomOfCeiling - enemyHeight);
		GameObject curr = Instantiate(enemy, new Vector3(transform.position.x, y, transform.position.z), Quaternion.identity);
		if(moveSpeed != defaultSpeed)
			curr.GetComponent<FollowPlayer>().moveSpeed = moveSpeed;
		curr.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 45.0f);
		Invoke("Spawn", Random.Range(spawnMin, spawnMax));
	}
	public void MoveFaster(){
		// increase default move speed
		if(moveSpeed == defaultSpeed){
			moveSpeed += 2f;
		}
	}
	public void DefaultSpeed(){
		// set enemy move rate back to normal
		if(moveSpeed != defaultSpeed)
			moveSpeed = defaultSpeed;
	}
}
