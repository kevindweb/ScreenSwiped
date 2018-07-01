using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour {
	public GameObject radiusObject;
	public float invisibleTime = 0.3f;
	private GameObject instantiatedRadius;
	private Color radiusColor;
	private Color invisibleColor;
	private float startTime;
	private Camera cam;
	void Awake(){
		// destroy all enemies within our radiusOfDestruction
		cam = Camera.main;
		Vector3 radiusLocation = new Vector3(transform.position.x, transform.position.y, 11);
		instantiatedRadius = Instantiate(radiusObject, radiusLocation, Quaternion.identity);
		// radiusColor = instantiatedRadius.GetComponent<Renderer>().material.color;
		radiusColor = Color.blue;
		invisibleColor = cam.backgroundColor;
		startTime = Time.time;
		Transform ourRadius = instantiatedRadius.transform;
		Collider2D[] objectsInside = Physics2D.OverlapCircleAll(transform.position, ourRadius.localScale.x / 2);
		foreach(Collider2D collider in objectsInside){
			GameObject obj = collider.gameObject;
			// Debug.Log("object: " + obj);
			if(obj.tag == "Enemy" || obj.tag == "ClusterHolder"){
				Destroy(obj);
			}
		}
	}
	void Update(){
		// make radius color invisible over time
		if(instantiatedRadius != null){
			float t = (Time.time - startTime) / invisibleTime;
			Color ourColor = Color.Lerp(radiusColor, invisibleColor, t);
			instantiatedRadius.GetComponent<Renderer>().material.SetColor("_Color", ourColor);
			if(t > 1){
				Destroy(instantiatedRadius);
				instantiatedRadius = null;
			}
		}
	}
}
