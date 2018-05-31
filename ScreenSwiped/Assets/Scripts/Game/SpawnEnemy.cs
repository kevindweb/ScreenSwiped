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
	void Awake(){
		originalPosition = transform.position.x;
		currentStep += (int) originalPosition;
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
		curr.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 45.0f);
		Invoke("Spawn", Random.Range(spawnMin, spawnMax));
	}
}
