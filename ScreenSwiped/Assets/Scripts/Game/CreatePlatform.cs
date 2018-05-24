using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlatform : MonoBehaviour {
	public GameObject platform;
	// static platform after being placed at certain position
	public GameObject heldVersion;
	// version of platform that uses script to follow mouse
	private GameObject heldObj;
	// used to destroy instantiated object following mouse
	private bool holding = false;
	// are we holding the object
	void Update () {
		Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mouseLocation.z = 0;
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			// initialize platform at mouse location
			if(heldObj){
				Destroy(heldObj);
			}
			holding = true;
			heldObj = Instantiate(heldVersion, mouseLocation, Quaternion.identity);
		} else if(Input.GetKeyDown(KeyCode.Escape) && holding){
			// stop holding object and remove it from screen
			Destroy(heldObj);
			heldObj = null;
			holding = false;
		} else if(Input.GetKeyDown(KeyCode.Space) && holding){
			// if holding, object follows mouse until we instantiate it with spacebar
			FollowMouse script = heldObj.GetComponent<FollowMouse>();
			float rotation = script.rotation;
			Color col = script.currColor;
			if(col == Color.red){
				// do not place platform if we are inside player
				return;
			}
			Destroy(heldObj);
			heldObj = null;
			GameObject currPlatform = Instantiate(platform, mouseLocation, Quaternion.identity);
			// instantiate actual static platform at location
			PlatformSet newScript = currPlatform.GetComponent<PlatformSet>();
			newScript.RotateTo(rotation);
			holding = false;
		}

	}
}
