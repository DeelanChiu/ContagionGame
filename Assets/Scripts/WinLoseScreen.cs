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
        /*
        foreach (int percent in GameController.instance.gamestate.survivalPercentLevels){
            string prefabName = "Prefabs/Heart";
            Object heart = Resources.Load<GameObject>(prefabName);
            if (GameController.instance.gamestate.survivalPercent >= percent){
                prefabName+="/heart";
            } else {
                prefabName+="/heartBlack";
            }


        }
        */

        string prefabName = "Prefabs/Heart";
        Object heart = Resources.Load<GameObject>(prefabName);

        Vector3 pos1 = new Vector3(-3f, -0.8f, 0);
        GameObject heart_go1 = (GameObject)Object.Instantiate(heart, pos1, Quaternion.identity);
        heart_go1.transform.parent = itemCanvas_go.transform;

        GameObject heartCanvas_go = new GameObject();
        heartCanvas_go.AddComponent<Canvas>();
        heartCanvas_go.transform.parent = heart_go1.transform;
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
        heartText_go.transform.parent = heartCanvas_go.transform;
        heartText_go.name = "HeartInfo";

        Text heartText = heartText_go.AddComponent<Text>();
        heartText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        heartText.text = "60%";
        heartText.fontSize = 35;
        heartText.color = Color.black;
        heartText.alignment = TextAnchor.MiddleCenter;

        RectTransform heartTextRectTransform = heartText.GetComponent<RectTransform>();
        heartTextRectTransform.localPosition = new Vector3(0, 0, 0);
        heartTextRectTransform.sizeDelta = new Vector2(500, 200);
        heartTextRectTransform.localScale = new Vector3(0.015f,0.015f,0.015f);


        Vector3 pos2 = new Vector3(0f, -0.8f, 0);
        GameObject heart_go2 = (GameObject)Object.Instantiate(heart, pos2, Quaternion.identity);
        heart_go2.transform.parent = itemCanvas_go.transform;

        Vector3 pos3 = new Vector3(3f, -0.8f, 0);
        GameObject heart_go3 = (GameObject)Object.Instantiate(heart, pos3, Quaternion.identity);
        heart_go3.transform.parent = itemCanvas_go.transform;
        
       // Heart pa = heart_go.GetComponent<Heart>();
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
