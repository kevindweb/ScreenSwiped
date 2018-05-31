using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	public GameObject itemHolder;
	public GameObject itemParent;
	public GameObject keyItem;
	public GameObject tCanvas;
	public Font myFont;
	private GUIStyle myStyle;
	private List<GameObject> platforms;
	// list of usable platforms
	private DataLoader access;
	private float score = 0.0f;
	private int highScore;
	private int newHighScore;
	private string difficulty;
	private string difficultyFile = "difficulty.dat";
	private string scoreFile = "score.dat";
	private string prevScoreFile = "prevScore.dat";
	private string currScoreFile = "currScore.dat";
	private Color defaultItemBackground;
	private Dictionary<int, Tuple<float, float>> cooldownCall = new Dictionary<int, Tuple<float, float>>();
	private List<int> removeNumbers = new List<int>();
	void Start(){
		myStyle = new GUIStyle();
		myStyle.font = myFont;
		access = ScriptableObject.CreateInstance("DataLoader") as DataLoader;
		difficulty = access.Load("", difficultyFile);
		highScore = access.Load(0, scoreFile);
		if(difficulty == null){
			// set to default normal difficulty
			difficulty = "normal";
			access.Save(difficulty, difficultyFile);
		}
		newHighScore = highScore;
		float yPos = 2.0f;
		int number = 1;
		platforms = new List<GameObject>();
		Renderer rendItem = itemParent.GetComponent<Renderer>();
		defaultItemBackground = rendItem.material.color;
		// set item list on left side of screen
		CreateItem(Color.black, number.ToString(), yPos, itemParent.transform.position.x);
		yPos -= itemHolder.transform.localScale.y + 0.3f;
		number++;
		CreateItem(Color.green, number.ToString(), yPos, itemParent.transform.position.x);
		yPos -= itemHolder.transform.localScale.y + 0.3f;
		number++;
		CreateItem(Color.blue, number.ToString(), yPos, itemParent.transform.position.x);
	}
	void Update(){
		float currPos = transform.position.x;
		if(currPos > score)
			score = currPos;
			if(score > newHighScore)
				newHighScore = (int) score;
			// score changes when player (camera) moves
		if(cooldownCall.Count > 0){
			foreach(KeyValuePair<int, Tuple<float, float>> key in cooldownCall){
				CoolDown(key.Key, key.Value.Item1, key.Value.Item2);
			}
		}
		if(removeNumbers.Count > 0){
			foreach(int i in removeNumbers){
				cooldownCall.Remove(i);
			}
			removeNumbers.Clear();
			// empty queue
		}
	}
	void OnDestroy(){
		if(newHighScore > highScore){
			// save the new high score
			access.Save(newHighScore, scoreFile);
		}
		access.Save(highScore, prevScoreFile);
		access.Save((int)score, currScoreFile);
		// overwrite previous score and set current
	}
	void OnGUI(){
		int h = 30;
		int difficultyWidth = 200;
		int scoreWidth = 100;
		GUI.color = Color.red;
		GUI.Label(new Rect(0, Screen.height - (3*h), difficultyWidth, h), "Difficulty: " + difficulty, myStyle);
		GUI.Label(new Rect(0, Screen.height - (2*h), scoreWidth, h), "Score: " + (int) score, myStyle);
		GUI.Label(new Rect(0, Screen.height - h, scoreWidth, h), "High Score: " + (int) newHighScore, myStyle);
		// put difficulty setting, score, and high score in bottom left of screen
	}
	void CreateItem(Color itemColor, string numberKey, float yPos, float xPos){
		// create a visual to show player choice of items
		// consists of outerbox, innerbox(color of platform), and a text element to show what key to press
		GameObject item = Instantiate(itemHolder, new Vector3(xPos, yPos, 0), Quaternion.identity);
		platforms.Add(item);
		GameObject child = item.transform.GetChild(0).gameObject;
		Renderer rend = child.GetComponent<Renderer>();
		rend.material.SetColor("_Color", itemColor);
		item.transform.parent = itemParent.transform;
		GameObject number = Instantiate(keyItem, tCanvas.transform.position, Quaternion.identity);
		number.transform.SetParent(tCanvas.transform);
		Vector3 position = new Vector3(item.transform.position.x, item.transform.position.y - 0.3f, item.transform.position.z);
		Vector3 point = Camera.main.WorldToScreenPoint(position);
		number.transform.position = point;
		Text ourText = number.GetComponent<Text>();
		ourText.text = numberKey;
		// place text inside the item on left side of screen
	}
	public void ChangeColor(int platformNum, bool selected){
		// when selected, show player which platform is selected
		GameObject platform = platforms[platformNum - 1];
		Renderer rendItem = platform.GetComponent<Renderer>();
		if(selected){
			rendItem.material.SetColor("_Color", Color.red);
		} else{
			rendItem.material.SetColor("_Color", defaultItemBackground);
		}
	}
	public void CoolDown(int platformNum, float cooldown, float startTime){
		// when we select certain platforms, we need to cooldown before using them again
		GameObject platform = platforms[platformNum - 1];
		if(cooldown == 0){
			platform.GetComponent<Renderer>().material.color = Color.Lerp(Color.red, defaultItemBackground, 1);
			return;
		}
		float t = (Time.time - startTime) / cooldown;
		platform.GetComponent<Renderer>().material.color = Color.Lerp(Color.red, defaultItemBackground, t);
		if(!cooldownCall.ContainsKey(platformNum)){
			Tuple<float, float> items = new Tuple<float, float>(cooldown, startTime);
			cooldownCall.Add(platformNum, items);
		}
		if(Time.time - startTime >= cooldown){
			// cooldown is over
			removeNumbers.Add(platformNum);
		}
	}
}
class Tuple<T,U>{
    public T Item1 { get; private set; }
    public U Item2 { get; private set; }

    public Tuple(T item1, U item2)
    {
        Item1 = item1;
        Item2 = item2;
    }
}
