using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMeHeldPlatform : MonoBehaviour {
	public float rotation = 30.0f;
	private float width;
	private GameObject player;
	void Start(){
		width = transform.localScale.x / 2;
		player = GameObject.FindWithTag("Player");
		transform.localRotation = Quaternion.Euler(0.0f, 0.0f, rotation);
	}
	void Update(){
		transform.position = new Vector3(player.transform.position.x + width, player.transform.position.y - width, 10);
	}
}
