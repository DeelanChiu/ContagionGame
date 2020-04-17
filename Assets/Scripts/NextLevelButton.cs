using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEditor;
using UnityEngine.UI;

public class NextLevelButton : GameItem
{
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void OnMouseDown()
    {
        GameController.instance.LoadLevel(GameController.instance.currLevel+1);

    }

    public void OnMouseOver()
    {
        
    }

    public void OnMouseExit()
    {
        
    }

}