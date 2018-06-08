using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollow : MonoBehaviour {
	public GameObject parent;
	private Vector3 prevPosition;
	void Awake(){
		if(parent != null){
			prevPosition = parent.transform.position;
		} else{
			prevPosition = new Vector3(10, 10, 10);
		}
	}
	void Update(){
		if(parent!=null){
			Vector3 pos = parent.transform.position;
			int zIndex = 5;
			if(parent.GetComponent<DragHandler>().clicked)
				zIndex = 3;
			pos = new Vector3(pos.x, pos.y, zIndex);
			if(pos != prevPosition){
				transform.position = pos;
				prevPosition = pos;
			}
		}
	}
}
