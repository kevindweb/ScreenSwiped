using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public Transform player;

	void Update () {
		if(player){
			transform.position = new Vector3(player.position.x, 0, -10);
			// keep camera following player sideways
		} else{
			Debug.Log("No player!");
			Debug.Break();
		}
	}
}
