using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEditor;
using UnityEngine.UI;

public class BlocksLeft : GameItem
{
    private Canvas blocksLeftCanvas;
    private GameObject blocksLeftCanvas_go;
    private Animator animator;

    private int blocks;

    private GameObject text_go;
    private Text text;
    private RectTransform rectTransform;

    void Awake(){
        blocksLeftCanvas_go = new GameObject();
        blocksLeftCanvas_go.name = "BlocksLeftCanvas";
        blocksLeftCanvas_go.AddComponent<Canvas>();
        blocksLeftCanvas_go.transform.parent = this.transform;

        blocksLeftCanvas = blocksLeftCanvas_go.GetComponent<Canvas>();
        //townCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        //townCanvas.worldCamera = Camera.main.GetComponent<Camera>();;
        blocksLeftCanvas.sortingOrder = 3;
        blocksLeftCanvas_go.AddComponent<CanvasScaler>();
        blocksLeftCanvas_go.AddComponent<GraphicRaycaster>();

        RectTransform blocksLeftCanvasRect = blocksLeftCanvas_go.GetComponent<RectTransform>();
        blocksLeftCanvasRect.localPosition = new Vector3(0, 0, 0);
        blocksLeftCanvasRect.sizeDelta = new Vector2(3f, 1f);
    }

    // Use this for initialization
    void Start()
    {
        text_go = new GameObject();
        //text_go.transform.parent = GameController.instance.gamestate.canvas_go.transform;
        text_go.transform.parent = blocksLeftCanvas_go.transform;
        text_go.name = "BlocksLeftInfo";

        text = text_go.AddComponent<Text>();
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.text = ""+blocks;
        text.fontSize = 26;
        text.color = Color.white;
        text.alignment = TextAnchor.MiddleCenter;

        rectTransform = text.GetComponent<RectTransform>();
        //rectTransform.localPosition = new Vector3(390, 350, 0);
        rectTransform.localPosition = new Vector3(-0.3f, 0, 0);
        rectTransform.sizeDelta = new Vector2(60, 40);
        //rectTransform.localScale = new Vector3(1f,1f,1f);
        rectTransform.localScale = new Vector3(0.015f,0.015f,0.015f);


        
    }

    public void setBlocksLeft (int n){
        blocks = n;
    }

    public bool useBlock (){
        if (blocks > 0){
            blocks--;
            return true;
        } else {
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.text = ""+blocks;

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
