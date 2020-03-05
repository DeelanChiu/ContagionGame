using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TownState
{
    HEALTHY,
    WARNING,
    INFECTED,
    OVERRUN
}

public class Town : GameItem
{
    public TownState state;

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
        else if (s.ToUpper() == "OVERRUN")
        {
            state = TownState.OVERRUN;
        }
    }

    public TownState getTownState()
    {
        return state;
    }


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

    }

    public void OnMouseOver()
    {
        
    }

    public void OnMouseExit()
    {
        
    }

}
