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
    public GameObject canvas_go;
    private Canvas townCanvas;
    private GameObject townCanvas_go;
    public TownState state;
    private TownState prevState;
    private Animator animator;
    public int population;
    public int infectionProb;
    private int currInfectionProb;

    public List<Town> neighbors;
    List <Town> infectedNeighbors;
    int infectedNum;

    private GameObject text_go;
    private Text text;
    private RectTransform rectTransform;

    private float infectTime;
    

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

    public void placeText(){
        text_go = new GameObject();
        text_go.transform.parent = canvas_go.transform;
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

    void Awake(){
        itemtype = ItemType.TOWN;

        animator = GetComponent<Animator>();
        prevState  =  TownState.HEALTHY;
        state = TownState.HEALTHY;

        neighbors = new List<Town>();
        infectedNeighbors = new List<Town>();
        //infectedNum = 0;

        population = 100;
        infectionProb = 0;
        currInfectionProb = infectionProb;

        townCanvas_go = new GameObject();
        townCanvas_go.name = "townCanvas";
        townCanvas_go.AddComponent<Canvas>();
        townCanvas_go.transform.parent = this.transform;

        townCanvas = townCanvas_go.GetComponent<Canvas>();
        //townCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        //townCanvas.worldCamera = Camera.main.GetComponent<Camera>();;
        townCanvas.sortingOrder = 3;
        townCanvas_go.AddComponent<CanvasScaler>();
        townCanvas_go.AddComponent<GraphicRaycaster>();

        RectTransform townCanvasRect = townCanvas_go.GetComponent<RectTransform>();
        townCanvasRect.localPosition = new Vector3(0, -0.75f, 0);
        townCanvasRect.sizeDelta = new Vector2(3f, 1f);
        
        text_go = new GameObject();
        text_go.transform.parent = townCanvas_go.transform;
        text_go.name = "TownInfo";

        text = text_go.AddComponent<Text>();
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.text = population+"\n"+infectionProb;
        text.fontSize = 30;
        text.color = Color.black;
        text.lineSpacing = 0.9f;
        text.alignment = TextAnchor.MiddleCenter;

        // Text position
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.sizeDelta = new Vector2(400, 200);
        rectTransform.localScale = new Vector3(0.015f,0.015f,0.015f);
        

    }

    // Use this for initialization
    void Start()
    {
        
        //StartCoroutine("increaseInfectionProb");

        //Debug.Log("In Town Start");
        
    }


    public void addNeighbor(Town town){
        neighbors.Add(town);
    }

    IEnumerator infectExposure() 
    {
        while (infectedNeighbors.Count  > 0) {
            int infectInc = GameController.instance.gamestate.infectionPlusInc * infectedNeighbors.Count;
            currInfectionProb += infectInc;
            //Debug.Log(""+currInfectionProb);
            float diceRoll = Random.Range(0.0f, 1.0f);
            //Debug.Log(diceRoll+" "+(float)currInfectionProb / 100f);
            if (diceRoll <= (float)currInfectionProb / 100f){
                state = TownState.INFECTED;
                yield return null;
            }
            yield return new WaitForSeconds(1);
        }

        state = TownState.HEALTHY; //got rid of infected neighbor(s)
        yield return null;
        
    }

    IEnumerator evacuatePopulation() 
    {
        while (neighbors.Count > 0 && population > 0) {
            //Debug.Log(neighbors.Count);
            foreach ( Town neighborTown in neighbors){
                int evacInc = 10;
                population -= evacInc;
                neighborTown.population += evacInc;
            }

            yield return new WaitForSeconds(1);
        }

        yield return null;

    }

    IEnumerator checkOffline() 
    {
        yield return new WaitForSeconds(0.2f);
        while (neighbors.Count > 0) {
            //Debug.Log(neighbors.Count);
            yield return new WaitForSeconds(0.1f);
        }
        state = TownState.OFFLINE;
        yield return null;

    }

    void neighborInfected(Town infectedNeighbor){
        if (state == TownState.HEALTHY){
            state = TownState.WARNING;
        }
        //infectedNum += 1;
        neighbors.Remove(infectedNeighbor);
        infectedNeighbors.Add(infectedNeighbor);
    }

    public void cutOff(Town neighbor){
        /*
        if (neighbor.state == TownState.INFECTED || neighbor.state == TownState.OFFLINE){
            infectedNum -= 1;
        }
        */
        infectedNeighbors.Remove(neighbor);
        neighbors.Remove(neighbor);
    }

    
    IEnumerator infectTimer() 
    {
        yield return new WaitForSeconds(infectTime);
        GameController.instance.gamestate.pendingTimers -= 1;
        state = TownState.INFECTED;
    }

    public void setInfectTimer(float time){
        infectTime = time;
        //GameController.instance.gamestate.pendingTimers += 1;
        StartCoroutine("infectTimer");
    }

    void stateChange(){
        if (state == TownState.HEALTHY)
        {
            animator.SetInteger("TownState", 0);
            currInfectionProb = infectionProb;
            StopCoroutine("infectExposure");
        }
        else if (state == TownState.WARNING)
        {
            animator.SetInteger("TownState", 1);
            StartCoroutine("infectExposure");
        }
        else if (state == TownState.INFECTED)
        {
            animator.SetInteger("TownState", 2);
            StopCoroutine("infectExposure");

            foreach ( Town neighborTown in neighbors){
                neighborTown.neighborInfected(this);
            }
            foreach ( Town neighborTown in infectedNeighbors){
                neighborTown.neighborInfected(this);
            }

            StartCoroutine("checkOffline");
            StartCoroutine("evacuatePopulation");

            GameController.instance.gamestate.infectedTowns += 1;

        }
        else if (state == TownState.OFFLINE)
        {
            animator.SetInteger("TownState", 3);
            StopCoroutine("evacuatePopulation");
            StopCoroutine("checkOffline");
            GameController.instance.gamestate.currPopulation -= population;
            Debug.Log(GameController.instance.gamestate.currPopulation);

            GameController.instance.gamestate.infectedTowns -= 1;
            GameController.instance.gamestate.EndGame = 
                GameController.instance.gamestate.infectedTowns == 0 && GameController.instance.gamestate.pendingTimers == 0;
            Debug.Log(GameController.instance.gamestate.EndGame);
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
            text.text = population+"\n  "+currInfectionProb+"%";
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
