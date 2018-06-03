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
	private Dictionary<string, int> damages;
	// enemy tag = key, enemy damage = value
	private int currentHealth;
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
	}
	void OnTriggerEnter2D(Collider2D other){
		GameObject collider = other.gameObject;
		foreach(KeyValuePair<string, int> key in damages){
			if(collider.tag == key.Key){
				// we lost because we hit an enemy
				currentHealth -= key.Value;
				if(currentHealth <= 0){
					SceneManager.LoadScene(4);
				} else{
					SetHealth();
				}
				break;
			}
		}
	}
	void SetHealth(){
		healthBar.value = currentHealth;
	}
}
