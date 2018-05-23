using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetector : MonoBehaviour {
	private bool trigger = false;
	void OnTriggerEnter2D(Collider2D other){
		// player should never interact with this object (inside real platform)
		// use this to determine if player is stuck
		if(!trigger){
			trigger = true;
			GameObject collider = other.gameObject;
			if(collider.tag == "Player"){
				Debug.Log("hit player!!");
				foreach (Transform child in transform.parent){
					if(child.name != this.name){
						// make siblings triggers to remove collision with player
						child.GetComponent<Collider2D>().isTrigger = true;
						Debug.Log("unstuck!");
					}
			    }
			}
		}
	}
}
