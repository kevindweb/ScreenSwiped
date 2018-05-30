using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour {
	public float magnetForce = 20;
	private GameObject player;
	private bool connected = false;
	private Controller script;
	void Awake(){
		player = GameObject.FindWithTag("Player");
		script = player.GetComponent<Controller>();
		script.Magnet(transform, true);
	}
	void OnCollisionEnter2D(Collision2D other){
		if(!connected){
			GameObject collided = other.gameObject;
			if(collided.tag == "Player"){
				connected = true;
				script.Magnet(transform, false);
			}
		}
	}
}
