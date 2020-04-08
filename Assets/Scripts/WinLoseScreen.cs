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

    // Use this for initialization
    void Start()
    {
        text_go = new GameObject();
        //text_go.transform.parent = GameController.instance.gamestate.canvas_go.transform;
        text_go.transform.parent = itemCanvas_go.transform;
        text_go.name = "BlocksLeftInfo";

        text = text_go.AddComponent<Text>();
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        int survivalPercent = (int)(GameController.instance.gamestate.survivalPercent * 100f);
        text.text = ""+survivalPercent+"% survived";
        text.fontSize = 50;
        text.color = Color.black;
        text.alignment = TextAnchor.MiddleCenter;

        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(-0.3f, 1.7f, 0);
        rectTransform.sizeDelta = new Vector2(500, 200);
        rectTransform.localScale = new Vector3(0.015f,0.015f,0.015f);

        string prefabName = "Prefabs/Heart";
        Object heart = Resources.Load<GameObject>(prefabName);

        Vector3 pos1 = new Vector3(-3f, -0.8f, 0);
        GameObject heart_go1 = (GameObject)Object.Instantiate(heart, pos1, Quaternion.identity);
        heart_go1.transform.parent = itemCanvas_go.transform;

        Vector3 pos2 = new Vector3(0f, -0.8f, 0);
        GameObject heart_go2 = (GameObject)Object.Instantiate(heart, pos2, Quaternion.identity);
        heart_go2.transform.parent = itemCanvas_go.transform;

        Vector3 pos3 = new Vector3(3f, -0.8f, 0);
        GameObject heart_go3 = (GameObject)Object.Instantiate(heart, pos3, Quaternion.identity);
        heart_go3.transform.parent = itemCanvas_go.transform;
        
       // Heart pa = heart_go.GetComponent<Heart>();

        
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
