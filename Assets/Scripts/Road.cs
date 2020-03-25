using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEditor;
using UnityEngine.UI;

public enum RoadType
{
    HORIZONTAL,
    VERTICAL,
    DIAGUP,
    DIAGDOWN
}

public class Road : GameItem
{
    
    public RoadType rdtype;

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
