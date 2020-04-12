using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEditor;
using UnityEngine.UI;

public class TutorialComponent : GameItem
{

    public GameObject this_go;

    private SpriteRenderer spriteRenderer;

    private float t;
    private float dur;

    new void Awake(){

    }

    IEnumerator toggleOrder() 
    {
        yield return new WaitForSeconds(t);
        spriteRenderer.sortingOrder = 3;
        yield return new WaitForSeconds(dur);
        spriteRenderer.sortingOrder = 0;

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
