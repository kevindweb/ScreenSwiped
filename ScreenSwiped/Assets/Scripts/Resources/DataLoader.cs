using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEditor;

public class DataLoader : ScriptableObject {
	// allows us to save and load any type of data from game
	public string projectPath;
	public string dataPath;
	private string extension = ".dat";
	private BinaryFormatter bf;
	private FileStream fs;

	public void OnEnable(){
		projectPath = Application.persistentDataPath;
		dataPath = projectPath + "/Data/";
		if(!Directory.Exists(dataPath)){
			//if it doesn't, create it
			Directory.CreateDirectory(dataPath);
		}
		bf = new BinaryFormatter();
	}
	public string Save<T>(T data){
		//  serialize and save any object
		// save to random file name and return it
		var file = "file" + Random.Range(0, 1000);
		while(File.Exists(dataPath + file + extension)){
			file += "1";
			// making random file name
		}
		file += extension;
		fs = File.Create(dataPath + file);
		bf.Serialize(fs, data);
		fs.Close();
		return file;
	}
	public void Save<T>(T data, string file){
		//  serialize and save any object
		if(file.Substring(file.Length - 4, 4) != extension){
			// add file extension if not there
			file += extension;
		}
		fs = File.Create(dataPath + file);
		bf.Serialize(fs, data);
		fs.Close();
	}
	public T Load<T>(T data, string file){
		// use uninstantiated data to cast deserialized object
		if(file.Substring(file.Length - 4, 4) != extension){
			// add file extension if not there
			file += extension;
		}
		if(File.Exists(dataPath + file)){
			fs = File.Open(dataPath + file, FileMode.Open);
			T newData = (T)bf.Deserialize(fs);
			fs.Close();
			return newData;
		} else{
			Debug.Log("could not find file: " + file);
			// Debug.Break();
			return default(T);
		}

	}
}
