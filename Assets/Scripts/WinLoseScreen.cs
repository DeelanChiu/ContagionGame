using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEditor;
using UnityEngine.UI;

public class WinLoseScreen : GameItem
{
    private Animator animator;

    public GameObject winScreen_go;
    public GameObject loseScreen_go;

    private GameObject text_go;
    private Text text;
    private RectTransform rectTransform;

    new void Awake(){
        base.Awake();
        itemCanvas_go.name = "winLoseScreenCanvas";
        itemCanvas.sortingOrder = 2;
        itemCanvasRect.localPosition = new Vector3(0, 0, 0);
        itemCanvasRect.sizeDelta = new Vector2(3f, 1f);
    }

    void displayHearts(){
        Vector3 pos = new Vector3(-6f, -0.8f, 0);
        int hearts = 0;
        foreach (int percent in GameController.instance.gamestate.survivalPercentLevels){
            string prefabName = "Prefabs/";
            if (GameController.instance.gamestate.survivalPercent >= percent){
                prefabName+="heart";
                hearts+=1;
            } else {
                prefabName+="heartBlack";
            }
            Object heart = Resources.Load<GameObject>(prefabName);

            pos = new Vector3(pos.x + 3f, -0.8f, 0);

            GameObject heart_go = (GameObject)Object.Instantiate(heart, pos, Quaternion.identity);
            heart_go.transform.parent = itemCanvas_go.transform;

            GameObject heartCanvas_go = new GameObject();
            heartCanvas_go.AddComponent<Canvas>();
            //heartCanvas_go.transform.parent = heart_go.transform;
            heartCanvas_go.transform.SetParent(heart_go.transform);
            Canvas heartCanvas = heartCanvas_go.GetComponent<Canvas>();
            heartCanvas_go.AddComponent<CanvasScaler>();
            heartCanvas_go.AddComponent<GraphicRaycaster>();
            RectTransform heartCanvasRect = heartCanvas_go.GetComponent<RectTransform>();

            heartCanvas_go.name = "heartCanvas";
            heartCanvas.sortingOrder = 3;
            heartCanvasRect.localPosition = new Vector3(0.1f, -1.1f, 0);
            heartCanvasRect.sizeDelta = new Vector2(3f, 1f);

            GameObject heartText_go = new GameObject();
            //text_go.transform.parent = GameController.instance.gamestate.canvas_go.transform;
            //heartText_go.transform.parent = heartCanvas_go.transform;
            heartText_go.transform.SetParent(heartCanvas_go.transform);
            heartText_go.name = "HeartInfo";

            Text heartText = heartText_go.AddComponent<Text>();
            heartText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            heartText.text = ""+percent+"%";
            heartText.fontSize = 35;
            heartText.color = Color.black;
            heartText.alignment = TextAnchor.MiddleCenter;

            RectTransform heartTextRectTransform = heartText.GetComponent<RectTransform>();
            heartTextRectTransform.localPosition = new Vector3(0, 0, 0);
            heartTextRectTransform.sizeDelta = new Vector2(500, 200);
            heartTextRectTransform.localScale = new Vector3(0.015f,0.015f,0.015f);

        }
        int levelNum  = GameController.instance.gamestate.level;
        int newHeartNum = Mathf.Max(GameController.instance.levelHearts[levelNum-1], hearts);
        GameController.instance.levelHearts[levelNum-1] = newHeartNum;

        string levelHeartStr = PlayerPrefs.GetString("levelHearts");
        string strA = levelHeartStr.Substring(0, levelNum-1);
        string strB =  levelHeartStr.Substring(levelNum, levelHeartStr.Length - levelNum);
        string newLevelHeartStr = strA + newHeartNum + strB;
        PlayerPrefs.SetString("levelHearts", newLevelHeartStr);
        //Debug.Log(newLevelHeartStr);
        
    }

    void displayButtons(){
        Object retryLevelButton = Resources.Load<GameObject>("Prefabs/retryButton");
        Vector3 retryLevelButtonPos = new Vector3(-5f, -4f, 100);
        GameObject retryLevelButton_go = (GameObject)Object.Instantiate(retryLevelButton, retryLevelButtonPos, Quaternion.identity);
        //retryLevelButton_go.transform.parent = itemCanvas_go.transform;
        retryLevelButton_go.transform.SetParent(itemCanvas_go.transform);

        if (GameController.instance.currLevel < GameController.instance.numLevels && GameController.instance.gamestate.levelPass){
            Object nextLevelButton = Resources.Load<GameObject>("Prefabs/nextLevelButton");
            Vector3 nextLevelButtonPos = new Vector3(0, -4f, 100);
            GameObject nextLevelButton_go = (GameObject)Object.Instantiate(nextLevelButton, nextLevelButtonPos, Quaternion.identity);
            //nextLevelButton_go.transform.parent = itemCanvas_go.transform;
            nextLevelButton_go.transform.SetParent(itemCanvas_go.transform);
        }

        Object levelSelectButton = Resources.Load<GameObject>("Prefabs/LevelSelectButton");
        Vector3 levelSelectButtonPos = new Vector3(5f, -4f, 100);
        GameObject levelSelectButton_go = (GameObject)Object.Instantiate(levelSelectButton, levelSelectButtonPos, Quaternion.identity);
        //levelSelectButton_go.transform.parent = itemCanvas_go.transform;
        levelSelectButton_go.transform.SetParent(itemCanvas_go.transform);
    }

    // Use this for initialization
    void Start()
    {
        text_go = new GameObject();
        //text_go.transform.parent = GameController.instance.gamestate.canvas_go.transform;
        text_go.transform.parent = itemCanvas_go.transform;
        text_go.name = "BlocksLeftInfo";

        text = text_go.AddComponent<Text>();
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.text = ""+GameController.instance.gamestate.survivalPercent+"% survived";
        text.fontSize = 50;
        text.color = Color.black;
        text.alignment = TextAnchor.MiddleCenter;

        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(-0.3f, 1.7f, 0);
        rectTransform.sizeDelta = new Vector2(500, 200);
        rectTransform.localScale = new Vector3(0.015f,0.015f,0.015f);

        displayHearts();
        displayButtons();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        

    }

    public void OnMouseOver()
    {
        
    }

    public void OnMouseExit()
    {
        
    }

}
