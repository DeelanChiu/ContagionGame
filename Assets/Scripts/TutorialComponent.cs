using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEditor;
using UnityEngine.UI;

public class TutorialComponent : GameItem
{

    public string prefab;

    private SpriteRenderer spriteRenderer;

    private float t;
    private float dur;

    new void Awake(){

    }

    IEnumerator toggleOrder() 
    {
        if (prefab.Equals("offlineTownTutorial") || prefab.Equals("blocksLeftTutorial") || prefab.Equals("popAliveTutorial")){
            while(GameController.instance.gamestate == null || GameController.instance.gamestate.blksLeft.getBlocksLeft()>0
                || !GameController.instance.gamestate.tutorialRoadBlocked){
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return new WaitForSeconds(t);
        spriteRenderer.sortingOrder = 3;
        yield return new WaitForSeconds(dur);
        yield return new WaitForSeconds(0.1f);
        if (prefab.Equals("blockRoadTutorial")){
            while(GameController.instance.gamestate == null || GameController.instance.gamestate.blksLeft.getBlocksLeft()>0){
                yield return new WaitForSeconds(0.1f);
            }
            GameController.instance.gamestate.tutorialRoadBlocked = true;
        }
        spriteRenderer.sortingOrder = 0;
        GameController.instance.gamestate.pendingTimers -= 1;
        GameController.instance.gamestate.EndGame = 
                GameController.instance.gamestate.infectedTowns == 0 && GameController.instance.gamestate.pendingTimers == 0;

    }

    // Use this for initialization
    void Start()
    {
        

    }

    public void setTimer(float t, float dur){
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 0;

        this.t = t;
        this.dur = dur;
        StartCoroutine("toggleOrder");
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
