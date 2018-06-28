using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMeHeldPlatform : MonoBehaviour {
	public float rotation = 60.0f;
	private float moveSpeed = 14.0f;
	private float width;
	private GameObject player;
	private Transform myTransform;
	void Start(){
		width = transform.localScale.x / 2;
		player = GameObject.FindWithTag("Player");
		transform.localRotation = Quaternion.Euler(0.0f, 0.0f, rotation);
		myTransform = transform;
	}
	void Update(){
		Vector3 pos = new Vector3(player.transform.position.x + width, player.transform.position.y - width, 10);
		float transition = moveSpeed * Time.deltaTime;
		if(Vector2.Distance(new Vector2(pos.x, pos.y), new Vector2(myTransform.position.x, myTransform.position.y)) < .7f){
			myTransform.position = pos;
		} else{
			if(pos.y > myTransform.position.y && pos.x > myTransform.position.x){
				myTransform.position = new Vector3(transition + myTransform.position.x, transition + myTransform.position.y, myTransform.position.z);
			} else if(pos.y > myTransform.position.y && pos.x < myTransform.position.x){
				myTransform.position = new Vector3(myTransform.position.x - transition, transition + myTransform.position.y, myTransform.position.z);
			} else if(pos.y < myTransform.position.y && pos.x > myTransform.position.x){
				myTransform.position = new Vector3(transition + myTransform.position.x, myTransform.position.y - transition, myTransform.position.z);
			} else{
				myTransform.position = new Vector3(myTransform.position.x - transition, myTransform.position.y - transition, myTransform.position.z);
			}
		}
	}
}
