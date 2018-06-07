using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlatformInformation : MonoBehaviour{
	public Button backButton;
	public Button quitButton;
	public GameObject platformHolder;
	public GameObject[] platformList;
	public GameObject tCanvas;
	public GameObject platformName;
	public GameObject platformBackground;
	private GameObject[] platformParents;
	private Color[] platformColors;
	private DataLoader access;
	private string temp;
	private int[] currentList;
	private int[] testList;
	private Vector3[] locations;
	private string file = "platformlist.dat";
	private int swap1 = 0;
	private int swap2 = 0;
	void Awake(){
		access = ScriptableObject.CreateInstance("DataLoader") as DataLoader;
		int lengthy = platformList.Length;
		temp = access.Load("", file);
		if(temp == null){
			currentList = new int[lengthy];
			testList = new int[lengthy];
			for(int i=0; i < lengthy; i++){
				currentList[i] = i;
				testList[i] = i;
			}
			// default list is the order of platforms (store this order, that's how we will retrieve platforms)
			access.Save(ArrayToString(currentList), file);
		} else{
			// we receive the list in string form
			currentList = StringToArray(temp);
			testList = StringToArray(temp);
			if(currentList.Length != lengthy){
				// we updated the platforms list
				int[] newList = new int[lengthy];
				for(int y=0; y < lengthy; y++){
					if(y < currentList.Length){
						newList[y] = currentList[y];
					} else{
						newList[y] = y;
					}
				}
				currentList = newList;
				Debug.Log("updated");
			}
		}
		locations = new Vector3[lengthy];
		platformColors = new Color[] {Color.black, Color.green, Color.blue};
		int num = 0;
		platformParents = new GameObject[lengthy];
		Vector3 pos = platformHolder.transform.position;
		ShowPlatform("Default", pos, platformColors[num], num);
		float change = .85f;
		if(lengthy > 1){
			if(lengthy < 5){
				for(int x = 1; x < lengthy; x++){
					num++;
					pos = new Vector3(pos.x, pos.y - change, pos.z);
					ShowPlatform("Name " + (num + 1), pos, platformColors[num], num);
					change = .6f;
				}
			} else{
				for(int x = 1; x < 5; x++){
					num++;
					pos = new Vector3(pos.x, pos.y - change, pos.z);
					ShowPlatform("Name " + (num + 1), pos, platformColors[num], num);
					change = .6f;
				}
				// ran function on 4 platforms, now put the rest in a list to choose from at the bottom
			}
		}
		// SwapPlatformPositions(1, 2);
	}
	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			// press back button
			backButton.onClick.Invoke();
		} else if(Input.GetKeyDown(KeyCode.Q)){
			// press back button
			quitButton.onClick.Invoke();
		}
		int lengthy = platformList.Length;
		lengthy = (lengthy > 5) ? 5 : lengthy;
		// make sure lengthy is not greater than 5
		int clicked = 0;
		int moused = 0;
		DragHandler clickPlat = null;
		for(int i=1; i < lengthy; i++){
			DragHandler script = platformParents[i].GetComponent<DragHandler>();
			if(script.clicked){
				clicked = i;
				clickPlat = script;
			} else if(script.mouseHere){
				moused = i;
			}
		}
		if(clicked > 0 && moused > 0){
			// holding platform one over another
			clickPlat.hovering = true;
			swap1 = clicked;
			swap2 = moused;
			clickPlat.script = this.GetComponent<PlatformInformation>();
		}
	}
	void OnDestroy(){
		if(!ArraysEqual(currentList, testList)){
			// user changed order of platforms
			Debug.Log("platform order changed");
			// access.Save(ArrayToString(currentList), file);
		}
	}
	public void SwapUs(){
		SwapPlatformPositions(swap1, swap2);
		swap1 = 0;
		swap2 = 0;
	}
	void ShowPlatform(string name, Vector3 pos, Color col, int num){
		locations[num] = pos;
		// set the original position of object
		GameObject background = Instantiate(platformBackground, new Vector3(pos.x, pos.y, pos.z - 2), Quaternion.identity);
		if(num == 0){
			// don't allow default platform to be moved!
			Destroy(background.GetComponent<DragHandler>());
		}
		background.GetComponent<Renderer>().material.color = col;
		GameObject text = Instantiate(platformName, pos, Quaternion.identity);
		TextParentFollow script = text.GetComponent<TextParentFollow>();
		script.parent = background;
		script.xOffset = 0;
		text.transform.SetParent(tCanvas.transform);
		Text ourText = text.GetComponent<Text>();
		ourText.text = name;
		GameObject numText = Instantiate(platformName, pos, Quaternion.identity);
		numText.transform.SetParent(tCanvas.transform);
		script = numText.GetComponent<TextParentFollow>();
		script.parent = background;
		script.xOffset = (background.transform.localScale.x / 2) - .15f;
		ourText = numText.GetComponent<Text>();
		ourText.text = (num+1).ToString();
		ourText.fontSize = 19;
		platformParents[num] = background;
	}
	bool SwapPlatformPositions(int num1, int num2){
		if(num1 == 0 || num2 == 0){
			// first element is off limits to swap
			return false;
		}
		// switch the current locations of platforms as well as their originally set locations
		Vector3 temp = locations[num1];
		platformParents[num1].transform.position = locations[num2];
		platformParents[num2].transform.position = temp;
		locations[num1] = locations[num2];
		locations[num2] = temp;
		// int temp = currentList[num1];
		// currentList[num1] = currentList[num2];
		// currentList[num2] = temp;
		// // swap the stored position of these platforms
		return true;
	}
	string ArrayToString(int[] temp){
		string item = "";
		for(int i=0; i < temp.Length; i++){
			item += temp[i];
		}
		return item;
	}
	int[] StringToArray(string temp){
		char[] numbers = temp.ToCharArray();
		int nLengthy = numbers.Length;
		int[] arr = new int[nLengthy];
		for(int z=0; z < nLengthy; z++){
			arr[z] = (int)char.GetNumericValue(numbers[z]);
		}
		return arr;
	}
	public void ClickLoad(){
		// go to load screen
		SceneManager.LoadScene(0);
	}
	public void ClickQuit(){
		Application.Quit();
	}
	static bool ArraysEqual<T>(T[] a1, T[] a2){
		if (ReferenceEquals(a1, a2))
			return true;

		if (a1 == null || a2 == null)
			return false;

		if (a1.Length != a2.Length)
			return false;

		EqualityComparer<T> comparer = EqualityComparer<T>.Default;
		for (int i = 0; i < a1.Length; i++){
			if (!comparer.Equals(a1[i], a2[i])) return false;
		}
		return true;
	}
}
