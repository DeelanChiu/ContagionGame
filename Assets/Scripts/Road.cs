using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEditor;
using UnityEngine.UI;

public class Road : GameItem
{
    
    public Town town1;
    public Town town2;

    private Animator animator;


    void Awake(){
        itemtype = ItemType.ROAD;
        //animator = GetComponent<Animator>();

    }

    // Use this for initialization
    void Start()
    {
        

        
        //StartCoroutine("increaseInfectionProb");

        //Debug.Log("In Town Start");
        
    }

    public void setXY(int xpos, int ypos) {
        base.setXY(xpos, ypos);

        //test code

      // Text

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (rdtype == RoadType.HORIZONTAL){
            animator.SetInteger("RoadType", 0);
        } else if (rdtype == RoadType.VERTICAL){
            animator.SetInteger("RoadType", 1);
        } else if (rdtype == RoadType.DIAGUP) {
            animator.SetInteger("RoadType", 2);
        } else if (rdtype == RoadType.DIAGDOWN) {
            animator.SetInteger("RoadType", 3);
        }
        */
        

    }

    void separate() {

    }

    void OnMouseDown()
    {
        Debug.Log("Pressed");

        town1.cutOff(town2);
        town2.cutOff(town1);

    }

    public void OnMouseOver()
    {
        
    }

    public void OnMouseExit()
    {
        
    }

}
