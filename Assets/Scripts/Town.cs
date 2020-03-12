using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEditor;
using UnityEngine.UI;

public enum TownState
{
    HEALTHY,
    WARNING,
    INFECTED,
    OFFLINE
}

public class Town : GameItem
{
    public TownState state;
    private Animator animator;
    public int population = 100;
    public int infectionProb = 50;

    private GameObject text_go;
    private Text text;
    private RectTransform rectTransform;
    

    public void setTownState(string s)
    {
        if (s.ToUpper() == "HEALTHY")
        {
            state = TownState.HEALTHY;
        }
        else if (s.ToUpper() == "WARNING")
        {
            state = TownState.WARNING;
        }
        else if (s.ToUpper() == "INFECTED")
        {
            state = TownState.INFECTED;
        }
        else if (s.ToUpper() == "OFFLINE")
        {
            state = TownState.OFFLINE;
        }
    }

    public TownState getTownState()
    {
        return state;
    }


    // Use this for initialization
    void Start()
    {
        itemtype = ItemType.BRIDGE;

        animator = GetComponent<Animator>();
        state = TownState.HEALTHY;

        //Debug.Log("In Town Start");
        
    }

    public void setXY(int xpos, int ypos) {
        base.setXY(xpos, ypos);

      // Text


        text_go = new GameObject();
        text_go.transform.parent = GameController.instance.canvas_go.transform;
        text_go.name = "TownInfo";

        text = text_go.AddComponent<Text>();
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.text = population+"\n"+infectionProb;
        text.fontSize = 30;
        text.lineSpacing = 0.9f;
        text.alignment = TextAnchor.MiddleCenter;

        float x = 0;
        float y = 0;
        if (xpos == 0){
            x = -415f;
        } else if (xpos == 1){
            x = -140f;
        } else if (xpos == 2){
            x = 140f;
        } else if (xpos == 3){
            x = 415f;
        }

        if (ypos == 0){
            y = 175f;
        } else if (ypos == 1){
            y = -80f;
        } else if (ypos == 2){
            y = -337f;
        }

        // Text position
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(x, y, 0);
        rectTransform.sizeDelta = new Vector2(400, 200);
        rectTransform.localScale = new Vector3(1f,1f,1f);

    }

    // Update is called once per frame
    void Update()
    {
        if (state == TownState.HEALTHY)
        {
            animator.SetInteger("TownState", 0);
            text.text = ""+population;
        }
        else if (state == TownState.WARNING)
        {
            animator.SetInteger("TownState", 1);
            text.text = population+"\n"+infectionProb;
        }
        else if (state == TownState.INFECTED)
        {
            animator.SetInteger("TownState", 2);
            text.text = ""+population;
        }
        else if (state == TownState.OFFLINE)
        {
            animator.SetInteger("TownState", 3);
            text.text = "";
        }

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
