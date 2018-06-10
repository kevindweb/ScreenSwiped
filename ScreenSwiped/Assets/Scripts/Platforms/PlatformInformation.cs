using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlatformInformation : MonoBehaviour{
	public Button backButton;
	public Button quitButton;
	public Button[] upDown;
	public GameObject lineSeperator;
	public GameObject platformHolder;
	public GameObject[] platformList;
	public GameObject tCanvas;
	public GameObject platformName;
	public GameObject topLayer;
	public GameObject platformBackground;
	public string[] platformNames;
	public Color[] platformColors;
	private GameObject[] platformParents;
	private TextParentFollow[] numbersFollow;
	private DataLoader access;
	private string temp;
	private int[] currentList;
	private int[] testList;
	private Vector3[] locations;
	private string file = "platformlist.dat";
	private int swap1 = 0;
	private int swap2 = 0;
	private int currentClickNum;
	private int startListIndex = 4;
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
				Debug.Log("updated list!");
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
			}
		}
		locations = new Vector3[lengthy];
		numbersFollow = new TextParentFollow[lengthy];
		// platformColors = new Color[] {Color.black, Color.green, Color.blue, Color.magenta};
		// platformNames = new string[] {"Default", "Magnet", "Destroyer", "Healthy"};
		int num = 0;
		platformParents = new GameObject[lengthy];
		Vector3 pos = platformHolder.transform.position;
		ShowPlatform(platformNames[num], pos, platformColors[num], num);
		float change = .85f;
		if(lengthy > 1){
			if(lengthy < 4){
				for(int x = 1; x < lengthy; x++){
					num++;
					pos = new Vector3(pos.x, pos.y - change, pos.z);
					ShowPlatform(platformNames[currentList[num]], pos, platformColors[currentList[num]], num);
					change = .6f;
				}
				Destroy(upDown[0]);
				Destroy(upDown[1]);
			} else{
				for(int x = 1; x < 4; x++){
					num++;
					pos = new Vector3(pos.x, pos.y - change, pos.z);
					ShowPlatform(platformNames[currentList[num]], pos, platformColors[currentList[num]], num);
					change = .6f;
				}
				// ran function on 4 platforms, now put the rest in a list to choose from at the bottom
				Vector3 linePos = lineSeperator.transform.position;
				Instantiate(lineSeperator, new Vector3(linePos.x, pos.y - .6f, linePos.z), lineSeperator.transform.rotation);
				num++;
				currentClickNum = num;
				ShowPlatform(platformNames[currentList[num]], new Vector3(pos.x, pos.y - 1, pos.z), platformColors[currentList[num]], num);
			}
		}
	}
	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			// press back button
			backButton.onClick.Invoke();
		} else if(Input.GetKeyDown(KeyCode.Q)){
			// press back button
			quitButton.onClick.Invoke();
		} else if(Input.GetKeyDown(KeyCode.UpArrow)){
			// press back button
			upDown[0].onClick.Invoke();
		} else if(Input.GetKeyDown(KeyCode.DownArrow)){
			// press back button
			upDown[1].onClick.Invoke();
		}
		int lengthy = platformList.Length;
		lengthy = (lengthy > 4) ? 5 : lengthy;
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
		} else if(clickPlat != null)
			clickPlat.hovering = false;
	}
	void OnDestroy(){
		if(!ArraysEqual(currentList, testList)){
			// user changed order of platforms
			access.Save(ArrayToString(currentList), file);
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
		GameObject layer = Instantiate(topLayer, new Vector3(pos.x, pos.y, pos.z - 3), Quaternion.identity);
		GameObject background = Instantiate(platformBackground, new Vector3(pos.x, pos.y, pos.z - 2), Quaternion.identity);
		background.GetComponent<Renderer>().material.color = col;
		BackgroundFollow backgroundScript = background.GetComponent<BackgroundFollow>();
		backgroundScript.parent = layer;
		GameObject text = Instantiate(platformName, pos, Quaternion.identity);
		TextParentFollow script = text.GetComponent<TextParentFollow>();
		script.parent = layer;
		script.xOffset = 0;
		text.transform.SetParent(tCanvas.transform);
		Text ourText = text.GetComponent<Text>();
		ourText.text = name;
		GameObject numText = Instantiate(platformName, pos, Quaternion.identity);
		numText.transform.SetParent(tCanvas.transform);
		script = numText.GetComponent<TextParentFollow>();
		script.parent = layer;
		float platformWidth = background.transform.localScale.x * 0.5f;
		script.xOffset = (platformWidth) - .15f;
		numbersFollow[num] = script;
		ourText = numText.GetComponent<Text>();
		ourText.text = (num+1).ToString();
		ourText.fontSize = 19;
		if(num == 0){
			// don't allow default platform to be moved!
			Destroy(layer.GetComponent<DragHandler>());
			Destroy(background.GetComponent<BackgroundFollow>());
		} else if(num > 3){
			upDown[0].transform.position = Camera.main.WorldToScreenPoint(new Vector3(pos.x + platformWidth + .3f, pos.y, pos.z));
			upDown[1].transform.position = Camera.main.WorldToScreenPoint(new Vector3(pos.x - platformWidth - .3f, pos.y, pos.z));
		}
		LayerChildren layerScript = layer.GetComponent<LayerChildren>();
		layerScript.background = background;
		layerScript.text = text;
		layerScript.numText = numText;
		platformParents[num] = layer;
	}
	bool SwapPlatformPositions(int num1, int num2){
		if(num1 == 0 || num2 == 0){
			Debug.Log("errors with default platform?");
			// first element is off limits to swap
			return false;
		} else if(num1 == currentClickNum){
			startListIndex = num2;
			currentClickNum = num2;
		} else if(num2 == currentClickNum){
			startListIndex = num1;
			currentClickNum = num1;
		}
		// switch the current locations of platforms as well as their originally set locations
		Vector3 temp = locations[num1];
		platformParents[num1].transform.position = locations[num2];
		platformParents[num2].transform.position = temp;
		locations[num1] = locations[num2];
		locations[num2] = temp;
		int tempNum = currentList[num1];
		currentList[num1] = currentList[num2];
		currentList[num2] = tempNum;
		// swap the stored position of these platforms
		TextParentFollow scriptTemp = numbersFollow[num1];
		GameObject followTemp = scriptTemp.parent;
		numbersFollow[num1].parent = numbersFollow[num2].parent;
		numbersFollow[num1] = numbersFollow[num2];
		numbersFollow[num2].parent = followTemp;
		numbersFollow[num2] = scriptTemp;
		// change where the numbers associated with platform are
		GameObject tempText = platformParents[num1].GetComponent<LayerChildren>().numText;
		platformParents[num1].GetComponent<LayerChildren>().numText = platformParents[num2].GetComponent<LayerChildren>().numText;
		platformParents[num2].GetComponent<LayerChildren>().numText = tempText;
		// swaps reference to number
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
	public void ClickUp(){
		// DO THIS
		if(currentClickNum < 5){
			// can't go up anymore
			Debug.Log("beginning of list");
			return;
		}
		UpDownClicked(currentClickNum, --currentClickNum);
	}
	public void ClickDown(){
		// DO THIS
		if(currentClickNum >= platformList.Length - 1){
			// at the end of the list
			Debug.Log("end of list");
			return;
		}
		UpDownClicked(currentClickNum, ++currentClickNum);
	}
	void UpDownClicked(int curr, int next){
		GameObject singleClicked = platformParents[startListIndex];
		LayerChildren script = singleClicked.GetComponent<LayerChildren>();
		script.background.GetComponent<Renderer>().material.color = platformColors[currentList[next]];
		script.text.GetComponent<Text>().text = platformNames[currentList[next]];
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
