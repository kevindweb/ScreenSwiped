using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityObject : MonoBehaviour {
	public int spinSpeed = 20;
	void Update (){
    	transform.Rotate (0, 0, spinSpeed * Time.deltaTime);
	}
}
