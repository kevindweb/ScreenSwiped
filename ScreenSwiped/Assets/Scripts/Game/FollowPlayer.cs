using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
	private Transform player;
	public float moveSpeed = 3.0f;
	private Transform myTransform;
	void Awake () {
		player = GameObject.FindWithTag("Player").transform;
		myTransform = transform;
	}
	void Update () {
		if(player.position.y > myTransform.position.y)
			myTransform.position += myTransform.up * moveSpeed * Time.deltaTime;
		else if(player.position.y < myTransform.position.y)
			myTransform.position += myTransform.up * -moveSpeed * Time.deltaTime;
	}
}
