using System.Collections;
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
	private List<List<string>> ourValues;
	delegate void StartAbility();
	void Awake(){
		ourValues = new List<List<string>>();
		int lengthy = howRare.Length;
		for(int i=0; i < lengthy; i++)
			ourValues.Add(new List<string>());
		int aLengthy = abilities.Length;
		// methodCalls = new StartAbility[aLengthy];
		// create method for each ability (needs to run some code to operate)
		for(int n=0; n < aLengthy; n++)
			ourValues[rareIndex[n]].Add(abilities[n]);
	}
	int TestRare(){
		// returns index of random list based on their rarity
		int index = Random.Range(0, 100);
		int sum = 0;
        int i=0;
        while(sum < index) {
			sum += howRare[i++];
        }
        return Mathf.Max(0, i-1);
	}
	public void GetRandom(){
		List<string> abilitySection = ourValues[TestRare()];
		string currentAbility = abilitySection[Random.Range(0, abilitySection.Count - 1)];
		Debug.Log("ability: " + currentAbility);
	}
}
