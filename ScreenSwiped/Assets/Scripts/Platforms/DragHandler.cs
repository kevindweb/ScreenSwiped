using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHandler : MonoBehaviour{
	private Vector3 screenSpace;
	private Vector3 offset;
	private Vector3 originalPosition;
	public bool mouseHere = false;
	public bool clicked = false;
	public bool hovering = false;
	public PlatformInformation script = null;
	void OnMouseDown(){
	    //translate the cubes position from the world to Screen Point
		originalPosition = transform.position;
	    screenSpace = Camera.main.WorldToScreenPoint(transform.position);
	    //calculate any difference between the cubes world position and the mouses Screen position converted to a world point
	    offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, screenSpace.z + 10));
		clicked = true;
	}
	void OnMouseDrag () {
	    //keep track of the mouse position
	    var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
	    //convert the screen mouse position to world point and adjust with offset
	    var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
	    //update the position of the object in the world
	    transform.position = curPosition;
	}
	void OnMouseUp(){
		if(!hovering){
			transform.position = originalPosition;
			Debug.Log("back!");
		} else{
			Debug.Log("swap!");
			// tell camera to switch platform positions
			script.SwapUs();
		}
		clicked = false;
		hovering = false;
	}
	void OnMouseOver(){
		mouseHere = true;
	}
	void OnMouseExit(){
		mouseHere = false;
	}
}
