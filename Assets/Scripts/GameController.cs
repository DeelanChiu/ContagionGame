using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    public GameState gamestate;

    public Canvas canvas;
    public GameObject canvas_go;

    public int goToLevel;

    public int currLevel;

    public int reachedLevel;

    public int numLevels = 1;

    public GameState gs()
    {
        return gamestate;
    }

    private void Start()
    {
        reachedLevel = 1;
        currLevel = 1;

        // Canvas
        canvas_go = new GameObject();
        canvas_go.name = "Canvas";
        canvas_go.AddComponent<Canvas>();

        canvas = canvas_go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main.GetComponent<Camera>();;
        canvas.sortingOrder = 2;
        canvas_go.AddComponent<CanvasScaler>();
        canvas_go.AddComponent<GraphicRaycaster>();

        string scenename = SceneManager.GetActiveScene().name;
        if (scenename.Substring(0, 5).Equals("Level")) {
          int level = System.Convert.ToInt32(scenename.Substring(5));
          LoadLevel(level);
          
        }
        else if (scenename.Equals("GameMenu")) {
          GameObject.Find("Level_Select_Canvas").GetComponent<Canvas>().enabled = false;
        }
        
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject.GetComponent<GameController>();
            //instance = GameObject.GetComponent<GameController>();
        }
    }

    private void Update()
    {
        //Debug.Log(LoggingManager.instance.playerAB);
        if (gamestate.EndGame && !gamestate.levelEnd){
            gamestate.setupWinLoseScreen();
        }
    }

    public void LoadLevel(int level) {
        currLevel = level;
        reachedLevel = Mathf.Max(reachedLevel, currLevel);

        string result = GameObject.Find("LevelData").GetComponent<LevelData>().jsonString;
        gamestate = new GameState(currLevel, result); //result

    }

    public void endLevel(int nextlevel, bool frommenu)
    {
        Debug.Log("Scene is ending, move to " + nextlevel);
        if (!frommenu) {
            Debug.Log("Recorded end of level " + (nextlevel-1));
            //LoggingManager.instance.RecordLevelEnd();
        }
        if (nextlevel > numLevels) {
          //end of levels
          SceneManager.LoadScene("GameMenu", LoadSceneMode.Single);
        }
        else {
          SceneManager.LoadScene("Level" + (nextlevel), LoadSceneMode.Single);
          Debug.Log("Recorded start of level " + nextlevel);
          //LoggingManager.instance.RecordLevelStart(nextlevel);
        }
    }

    public int getCurrLevel() {
        return currLevel;
    }

    public void changeReachedLevel() {
        PlayerPrefs.SetInt("LevelReached", Mathf.Max(currLevel+1, PlayerPrefs.GetInt("LevelReached", 1)));

    }
}
