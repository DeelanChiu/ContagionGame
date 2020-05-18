using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour {

	public string jsonString;
    //public GameObject startButton;

	// Use this for initialization
	void Awake () {
        //startButton.SetActive(true);
		DontDestroyOnLoad(gameObject);
		StartCoroutine(loadStreamingAsset("levels.json"));

	}

	IEnumerator<WWW> loadStreamingAsset(string fileName)
	{
		// Path.Combine combines strings into a file path
		// Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
		string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);

		if (filePath.Contains("://") || filePath.Contains(":///"))
		{
				WWW www = new WWW(filePath);
				yield return www;
				jsonString = www.text;
				doneLoading();

		}
		else
		{
				jsonString = System.IO.File.ReadAllText(filePath);
				doneLoading();
		}
	}

	private void doneLoading() {
        //Debug.Log("done loading");

        //create start game button;
        //Vector3 pos = new Vector3(0, 0, 100);
        //Object startbtn = Resources.Load<Object>("Prefabs/StartButton_prefab");
        //GameObject start_go = (GameObject)Object.Instantiate(startbtn, pos, Quaternion.identity);
        //start_go.name = "StartButton";

        //startButton.SetActive(true);
	}

	// Update is called once per frame
	void Update () {

	}
}
