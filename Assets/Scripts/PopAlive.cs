using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEditor;
using UnityEngine.UI;

public class PopAlive : GameItem
{
    

    private Animator animator;

    int remainingPop;

    private GameObject text_go;
    private Text text;
    private RectTransform rectTransform;

    void Awake(){

        text_go = new GameObject();
        text_go.transform.parent = GameController.instance.canvas_go.transform;
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



    }

    // Use this for initialization
    void Start()
    {
        
        remainingPop = GameController.instance.gamestate.totalPopulation;
        Debug.Log(remainingPop);
        
    }

    public void setXY(int xpos, int ypos) {
        base.setXY(xpos, ypos);

        //test code

      // Text

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
