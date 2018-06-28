using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour {
	public int startHealth = 100;
	public string[] enemyTags;
	public int[] enemyDamages;
	public Slider healthBar;
	public float transitionTime = .3f;
	public GameObject ourCollider;
	private Dictionary<string, int> damages;
	// enemy tag = key, enemy damage = value
	private int currentHealth;
	private Abilities aScript;
	private GameObject lastCollider;
	// our abilities script
	void Awake(){
		damages = new Dictionary<string, int>();
		for(int i=0; i < enemyTags.Length; i++){
			damages.Add(enemyTags[i], enemyDamages[i]);
		}
		healthBar.maxValue = startHealth;
		healthBar.minValue = 0f;
		healthBar.wholeNumbers = false;
		healthBar.value = startHealth;
		currentHealth = startHealth;
		aScript = GetComponent<Abilities>();
	}
	void OnTriggerEnter2D(Collider2D other){
		GameObject collider = other.gameObject;
		if(ourCollider == collider || lastCollider == collider)
			return;
		lastCollider = collider;
		foreach(KeyValuePair<string, int> key in damages){
			if(collider.tag == key.Key){
				// we lost because we hit an enemy
				currentHealth -= key.Value;
				if(currentHealth <= 0){
					SceneManager.LoadScene(4);
				} else{
					SetHealth();
				}
				return;
			}
		}
		if(collider.tag == "Collectable"){
			// if we are here, we hit a collectable
			aScript.GetRandom();
			Destroy(collider);
			// find a random ability, and initialize it
		}
	}
	void OnCollisionEnter2D(Collision2D other){
		GameObject collider = other.gameObject;
		if(collider.tag == "Healthy"){
			currentHealth += collider.GetComponent<Healthy>().raiseHealth;
			// increase health by SaveMe raiseHealth value
			currentHealth = (currentHealth >= 100) ? 100 : currentHealth;
			SetHealth();
		}
	}
	void SetHealth(){
		healthBar.value = currentHealth;
	}
}
