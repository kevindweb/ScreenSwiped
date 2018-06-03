using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {
	private bool error = false;
	private SpriteRenderer rend;
	public Color newCol = Color.red;
	public Color originalColor;
	public Color currColor;
	public float rotation = 0.0f;
	// we can pass around the rotation between files
	void Start(){
		rend = GetComponent<SpriteRenderer>();
		originalColor = rend.color;
		currColor = originalColor;
		if(rotation != 0){
			RotateTo();
		}
	}
	void Update () {
		if(error && rend.color != newCol){
			// change to red if we are colliding
			currColor = newCol;
			rend.color = newCol;
		} else if(!error && rend.color != originalColor){
			// change back to original if we are OK!
			currColor = originalColor;
			rend.color = originalColor;
		}
		// we will follow the mouse location around the screen
		Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mouseLocation.z = 0;
		transform.position = mouseLocation;
		if(Input.GetKeyDown(KeyCode.S)){
			rotation = 0.0f;
			transform.localRotation = Quaternion.Euler(0.0f, 0.0f, rotation);
		} else if(Input.GetKeyDown(KeyCode.A)){
			rotation = -30.0f;
			transform.localRotation = Quaternion.Euler(0.0f, 0.0f, rotation);
		} else if(Input.GetKeyDown(KeyCode.D)){
			rotation = 30.0f;
			transform.localRotation = Quaternion.Euler(0.0f, 0.0f, rotation);
		}
		// use awd keys rotate platform
	}
	// need to make sure we don't spawn inside player
	// if player inside platform, turn platform bright red
	// allow them to place platform, just remove ridigbody if player is inside platform
	void OnTriggerEnter2D(Collider2D other){
		// Debug.Log("collided!");
		GameObject collider = other.gameObject;
		if(collider.tag == "RadiusCollider"){
			error = true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		GameObject collider = other.gameObject;
		if(collider.tag == "RadiusCollider"){
			error = false;
		}
	}
	void RotateTo(){
		// rotate to default rotation
		transform.localRotation = Quaternion.Euler(0.0f, 0.0f, rotation);
	}
}
