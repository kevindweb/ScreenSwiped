using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
	public float moveSpeed = 3.0f;
	public int enemyDamage = 10;
	private Transform player;
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
