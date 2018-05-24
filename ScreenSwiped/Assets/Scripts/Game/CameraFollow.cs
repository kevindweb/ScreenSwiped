using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// used to find gameover scene when we lose

public class CameraFollow : MonoBehaviour {
	public Transform player;
	private float xOffset;
	private float yOffset;

	void Awake(){
		// yOffset = player.position.y * -1;
		xOffset = player.position.x * -1;
		// set camera ahead and below player
	}

	void Update () {
		if(player){
			transform.position = new Vector3(player.position.x + xOffset, 0, -10);
			// keep camera following player sideways
		} else{
			// means we lost
			Destroy(player);
			SceneManager.LoadScene("GameOver");
		}
	}
}
