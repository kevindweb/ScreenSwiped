using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAnimation : MonoBehaviour {
	private float RotateSpeed = 5f;
	private float Radius = 0.2f;
	private Vector2 centre;
	private float angle;
	private void Start(){
		centre = transform.position;
	}
	private void Update(){
		angle += RotateSpeed * Time.deltaTime;
		var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * Radius;
		transform.position = centre + offset;
	}
}
