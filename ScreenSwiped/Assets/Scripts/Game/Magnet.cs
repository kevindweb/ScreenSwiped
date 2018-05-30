using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour {
	public GameObject player;
	public float horizontalGravity = 4.9f;
	public float magnetForce = 20;
	private bool connected = false;
	private Controller script;
	void Awake(){
		script = player.GetComponent<Controller>();
		Debug.Log("controller: " + controller);
		script.Magnet(transform, true);
	}
	// void Update () {
		// if(!connected){
			// if we have not touched the player yet, pull as a magnet
			// float step = magnetForce * Time.deltaTime;
			// player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, step);
			// player.GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontalGravity, 0));
			// player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 50, ForceMode2D.Impulse);
		// }
	// }
	void OnCollisionEnter2D(Collision2D other){
		if(!connected){
			GameObject collided = other.gameObject;
			if(collided.tag == "Player"){
				Debug.Log("magnet: " + script.magnetField.Count);
				connected = true;
				script.Magnet(transform, false);
			}
		}
	}
}
