using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEditor;
using UnityEngine.UI;

public class PopAlive : GameItem
{
    private Canvas popAliveCanvas;
    private GameObject popAliveCanvas_go;
    private Animator animator;

    int remainingPop;

    private GameObject text_go;
    private Text text;
    private RectTransform rectTransform;

    new void Awake(){
        popAliveCanvas_go = new GameObject();
        popAliveCanvas_go.name = "BlocksLeftCanvas";
        popAliveCanvas_go.AddComponent<Canvas>();
        popAliveCanvas_go.transform.parent = this.transform;

        popAliveCanvas = popAliveCanvas_go.GetComponent<Canvas>();
        popAliveCanvas.sortingOrder = 3;
        popAliveCanvas_go.AddComponent<CanvasScaler>();
        popAliveCanvas_go.AddComponent<GraphicRaycaster>();

        RectTransform popAliveRect = popAliveCanvas_go.GetComponent<RectTransform>();
        popAliveRect.localPosition = new Vector3(0, 0, 0);
        popAliveRect.sizeDelta = new Vector2(3f, 1f);
    }

    // Use this for initialization
    void Start()
    {
        text_go = new GameObject();
        text_go.transform.parent = GameController.instance.gamestate.canvas_go.transform;
        text_go.name = "PopAliveInfo";

        text = text_go.AddComponent<Text>();
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.text = "";
        text.fontSize = 26;
        text.color = Color.white;
        text.alignment = TextAnchor.MiddleCenter;

        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(190, 350, 0);
        rectTransform.sizeDelta = new Vector2(150, 40);
        rectTransform.localScale = new Vector3(1f,1f,1f);

        remainingPop = GameController.instance.gamestate.totalPopulation;
        Debug.Log(remainingPop);
        
    }

    // Update is called once per frame
    void Update()
    {
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
