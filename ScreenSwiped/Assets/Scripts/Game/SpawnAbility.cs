using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAbility : MonoBehaviour {
	public GameObject abilityObject;
	public GameObject floor;
	public GameObject ceiling;
	public float spawnMin = 1f;
	public float spawnMax = 2f;
	void Start(){
		Spawn();
	}
	void Spawn(){
		float objectHeight = floor.transform.localScale.y / 2.0f;
		float topOfFloor = floor.transform.position.y + objectHeight;
		float bottomOfCeiling = ceiling.transform.position.y - objectHeight;
		float y = transform.position.y + Random.Range(topOfFloor + 0.5f, bottomOfCeiling - 0.5f);
		Instantiate(abilityObject, new Vector3(transform.position.x, y, transform.position.z), Quaternion.identity);
		Invoke("Spawn", Random.Range(spawnMin, spawnMax));
	}
}
