using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    public GameState gamestate;

    public string levelDataJson;

    public int goToLevel;

    public int currLevel;

    public int reachedLevel;

    public int numLevels;

    public LevelSelect levelselect;

    public int[] levelHearts;

    public GameState gs()
    {
        return gamestate;
    }

    private void Start()
    {
        
        if (PlayerPrefs.HasKey("reachedLevel")){
            reachedLevel = PlayerPrefs.GetInt("reachedLevel");
            
        } else {
            reachedLevel = 1;
            PlayerPrefs.SetInt("reachedLevel", 1);
        }
        
        //reachedLevel = 1;
        currLevel = 1;

        levelDataJson = GameObject.Find("LevelData").GetComponent<LevelData>().jsonString;

        numLevels = JSON.Parse(levelDataJson)["Levels"];

        levelHearts = new int[numLevels];

        loadCover();
        /*
        string scenename = SceneManager.GetActiveScene().name;

        if (scenename.Substring(0, 5).Equals("Level")) {
          int level = System.Convert.ToInt32(scenename.Substring(5));
          LoadLevel(level);
          
        }
        else if (scenename.Equals("GameMenu")) {
          GameObject.Find("Level_Select_Canvas").GetComponent<Canvas>().enabled = false;
        }
        */
        /*
        if (scenename.Equals("GameMenu")) {
          GameObject.Find("Level_Select_Canvas").GetComponent<Canvas>().enabled = false;
        } else {
          LoadLevel(currLevel);
 
        }
        */
        
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
        /*
        if (gamestate != null && gamestate.EndGame && !gamestate.levelEnd){
            
            gamestate.setupWinLoseScreen();
        }
        */
        
    }
    
    IEnumerator checkLevelOver(){

        while (gamestate == null || !gamestate.EndGame){
            
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1.5f);
        gamestate.setupWinLoseScreen();

    }
    

    public void loadCover(){
        GameObject coverCanvas_go = new GameObject();
        coverCanvas_go.name = "CoverCanvas";
        coverCanvas_go.AddComponent<Canvas>();

        Canvas coverCanvas = coverCanvas_go.GetComponent<Canvas>();
        coverCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        coverCanvas.worldCamera = Camera.main.GetComponent<Camera>();
        coverCanvas.sortingOrder = 1;

        Object cover = Resources.Load<GameObject>("Prefabs/Cover");
        Vector3 cover_pos = new Vector3(0, 0.6f, 100);
        GameObject cover_go = (GameObject)Object.Instantiate(cover, cover_pos, Quaternion.identity);
        cover_go.transform.parent = coverCanvas_go.transform;

        Object playButton = Resources.Load<GameObject>("Prefabs/PlayButton");
        Vector3 playButton_pos = new Vector3(-4.2f, -4.9f, 100);
        GameObject playButton_go = (GameObject)Object.Instantiate(playButton, playButton_pos, Quaternion.identity);
        playButton_go.transform.parent = coverCanvas_go.transform;

        Object levelSelectButton = Resources.Load<GameObject>("Prefabs/LevelSelectButton");
        Vector3 levelSelectButton_pos = new Vector3(4.2f, -4.9f, 100);
        GameObject levelSelectButton_go = (GameObject)Object.Instantiate(levelSelectButton, levelSelectButton_pos, Quaternion.identity);
        levelSelectButton_go.transform.parent = coverCanvas_go.transform;

    }

    private void cleanCanvas(){
        Destroy (GameObject.Find ("LevelCanvas"));
        Destroy (GameObject.Find ("ResultCanvas"));
        Destroy(GameObject.Find ("CoverCanvas"));
        Destroy(GameObject.Find ("LevelSelectCanvas"));
    }

    public void LoadLevelSelect(){
        cleanCanvas();

        levelselect = new LevelSelect();

    }

    public void LoadLevel(int level) {
        currLevel = level;
        //reachedLevel = Mathf.Max(reachedLevel, currLevel);

        cleanCanvas();

        if (gamestate != null){
            gamestate = null;
        }

        gamestate = new GameState(currLevel); //result
        StartCoroutine("checkLevelOver");

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
