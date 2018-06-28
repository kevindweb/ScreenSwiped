using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendSeed : MonoBehaviour {
	void OnDisable(){
		string seedText = GetComponent<Text>().text;
		if(seedText.Length >= 8){
			seedText = (seedText.Substring(0, 8)).ToUpper();
			Debug.Log("using seed: " + seedText);
			DataLoader access = ScriptableObject.CreateInstance("DataLoader") as DataLoader;
			access.Save(seedText, "seed.dat");
			// save to file if it meets certain criteria
		}
	}
}
