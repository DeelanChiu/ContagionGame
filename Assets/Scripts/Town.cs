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
    private TownState prevState;
    private Animator animator;
    public int population = 100;
    public int infectionProb = 50;
    private int currInfectionProb;

    List<Town> neighbors;

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
        prevState  =  TownState.HEALTHY;
        state = TownState.HEALTHY;

        neighbors = new List<Town>();
        currInfectionProb = infectionProb;


        //StartCoroutine("increaseInfectionProb");

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
        text.color = Color.black;
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

    IEnumerator increaseInfectionProb() 
    {
        Debug.Log("increasing");
        while (currInfectionProb < 100) {
            Debug.Log(""+currInfectionProb);
            currInfectionProb += 5;
            yield return new WaitForSeconds(1);
        }
    }

    void stateChange(){
        if (state == TownState.HEALTHY)
        {
            animator.SetInteger("TownState", 0);
            currInfectionProb = infectionProb;
            StopCoroutine("increaseInfectionProb");
        }
        else if (state == TownState.WARNING)
        {
            animator.SetInteger("TownState", 1);
            StartCoroutine("increaseInfectionProb");
        }
        else if (state == TownState.INFECTED)
        {
            animator.SetInteger("TownState", 2);
        }
        else if (state == TownState.OFFLINE)
        {
            animator.SetInteger("TownState", 3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (prevState != state){
            prevState = state;
            stateChange();
        }

        if (state == TownState.HEALTHY)
        {
            text.text = ""+population;
        }
        else if (state == TownState.WARNING)
        {
            text.text = population+"\n"+currInfectionProb;
        }
        else if (state == TownState.INFECTED)
        {
            text.text = ""+population;
        }
        else if (state == TownState.OFFLINE)
        {
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
