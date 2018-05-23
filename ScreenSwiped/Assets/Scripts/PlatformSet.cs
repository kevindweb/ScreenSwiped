using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSet : MonoBehaviour {
	public float rotationDefault = 0.0f;
	public void RotateTo(float rotation){
		// rotate to
		transform.localRotation = Quaternion.Euler(0.0f, 0.0f, rotation);
	}
	public void SetTrigger(){
		Debug.Log("setting trigger");
		GameObject child = transform.GetChild(0).gameObject;
		child.GetComponent<Collider2D>().isTrigger = true;
	}
}
