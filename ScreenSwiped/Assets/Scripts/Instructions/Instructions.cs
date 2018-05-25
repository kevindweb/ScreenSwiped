using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour {

	public void ClickLoad(){
		// go to load screen
		SceneManager.LoadScene("LoadScene");
	}
}
