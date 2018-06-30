using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMeHeldPlatform : MonoBehaviour {
	public float rotation = 15.0f;
	private float width;
	private GameObject player;
	private Transform myTransform;
	void Start(){
		myTransform = transform;
		width = myTransform.localScale.x / 2;
		myTransform.localRotation = Quaternion.Euler(0.0f, 0.0f, rotation);
		player = GameObject.FindWithTag("Player");
	}
	void Update(){
		myTransform.position = new Vector3(player.transform.position.x + width, player.transform.position.y - width, 10);
	}
}
