using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEditor;
using UnityEngine.UI;

public class PopAlive : GameItem
{
    private Animator animator;

    //int remainingPop;

    private GameObject text_go;
    private Text text;
    private RectTransform rectTransform;

    new void Awake(){
        base.Awake();

        itemCanvas_go.name = "popAliveCanvas";
        itemCanvas.sortingOrder = 2;
        itemCanvasRect.localPosition = new Vector3(0, 0, 0);
        itemCanvasRect.sizeDelta = new Vector2(3f, 1f);
    }

    // Use this for initialization
    void Start()
    {
        text_go = new GameObject();
        //text_go.transform.parent = itemCanvas_go.transform;
        text_go.transform.SetParent(itemCanvas_go.transform);
        text_go.name = "PopAliveInfo";

        text = text_go.AddComponent<Text>();
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.text = "";
        text.fontSize = 26;
        text.color = Color.white;
        text.alignment = TextAnchor.MiddleCenter;

        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.sizeDelta = new Vector2(400, 200);
        rectTransform.localScale = new Vector3(0.015f,0.015f,0.015f);

        //remainingPop = GameController.instance.gamestate.totalPopulation;
        //Debug.Log(remainingPop);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance != null && GameController.instance.gamestate != null)
            text.text = GameController.instance.gamestate.currPopulation+" / "+GameController.instance.gamestate.totalPopulation;

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
