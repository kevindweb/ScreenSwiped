using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour {
	public string[] abilities;
	public int[] rareIndex;
	public int[] abilityTimer;
	// how long do the abilities last?
	public int[] howRare;
	// how rare each list of abilities is by probability chosen /100
	// private StartAbility[] methodCalls;
	private delegate void abilityDelegate();
	// private abilityDelegate[] methodCalls;
	// call functions to do certain things with
	private List<List<string>> ourValues;
	private int seed;
	void Awake(){
		ourValues = new List<List<string>>();
		int aLengthy = abilities.Length;
		// methodCalls = new abilityDelegate[] {LogThis, LogThis2};
		// create method for each ability (needs to run some code to operate)
		for(int n=0; n < aLengthy; n++){
			ourValues.Add(new List<string>());
			ourValues[rareIndex[n]].Add(abilities[n]);
		}
	}
	void Start(){
		seed = GetComponent<Controller>().seed;
		UnityEngine.Random.InitState(seed);
		// get the randomized seed from player controller
	}
	int TestRare(){
		// returns index of random list based on their rarity
		int index = UnityEngine.Random.Range(0, 100);
		int sum = 0;
        int i=0;
        while(sum < index) {
			sum += howRare[i++];
        }
        return Mathf.Max(0, i-1);
	}
	public void GetRandom(){
		// get a random ability based on
		List<string> abilitySection = ourValues[TestRare()];
		string currentAbility = abilitySection[UnityEngine.Random.Range(0, abilitySection.Count - 1)];
		// methodCalls[Array.IndexOf(abilities, currentAbility)]();
	}
	void LogThis(){
		Debug.Log("here!");
	}
	void LogThis2(){
		Debug.Log("here!");
	}
}
