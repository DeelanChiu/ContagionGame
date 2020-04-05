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

    void Awake(){

        text_go = new GameObject();
        text_go.transform.parent = GameController.instance.canvas_go.transform;
        text_go.name = "BlocksLeftInfo";

        text = text_go.AddComponent<Text>();
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
       //text.text = ""+blocks;
        text.fontSize = 26;
        text.color = Color.white;
        text.alignment = TextAnchor.MiddleCenter;

        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(390, 350, 0);
        rectTransform.sizeDelta = new Vector2(60, 40);
        rectTransform.localScale = new Vector3(1f,1f,1f);



    }

    // Use this for initialization
    void Start()
    {
        

        
    }
    public void setXY(int xpos, int ypos) {
        base.setXY(xpos, ypos);

        //test code

      // Text

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
