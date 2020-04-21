using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEditor;
using UnityEngine.UI;

public class LevelSelectButton : GameItem
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
        GameController.instance.LoadLevelSelect();
        GameController.instance.audioSource.PlayOneShot(GameController.instance.click, 1);

    }

    public void OnMouseOver()
    {
        
    }

    public void OnMouseExit()
    {
        
    }

}
