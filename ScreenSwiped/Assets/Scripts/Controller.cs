using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
	public float sideSpeed = 5.0f;

	void FixedUpdate(){
		transform.Translate(new Vector3(sideSpeed * Time.deltaTime, 0, 0));
	}
}
