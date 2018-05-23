using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryCollision : MonoBehaviour {
	private Vector3 prevLocation;
	// previous location of Player
	private int count = 1;
	void OnCollisionEnter2D(Collision2D other){
		GameObject collider = other.gameObject;
		Vector3 currLocation = collider.transform.position;
		if(collider.tag == "Player"){
			prevLocation = currLocation;
		}
	}
	void OnCollisionStay2D(Collision2D other){
		GameObject collider = other.gameObject;
		if(collider.tag == "Player"){
			// check collision second call of method
			if(count % 2 == 0 && Vector3.Distance(prevLocation, collider.transform.position) == 0){
				Debug.Log("haven't moved");
				GetComponent<Collider2D>().isTrigger = true;
			}
			count++;
		}
	}
}
