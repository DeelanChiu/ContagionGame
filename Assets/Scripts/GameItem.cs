using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    TOWN,
    ROAD
}

public abstract class GameItem : MonoBehaviour {
    public ItemType itemtype;
    public int x;
    public int y;

    public GameObject go;

    public ItemType getType() {
        return itemtype;
    }

    public int[] getLoc() {
        int[] loc = {this.x, this.y};
        return loc;
    }
/*
    public void changeLoc(int xpos, int ypos) {

      x = xpos;
      y = ypos;
      //change on GUI
      target = new Vector3(xpos * 20 - 20, ypos*(-20) + 20, 99);
    }
*/
    public void setXY(int xpos, int ypos) {
      this.x = xpos;
      this.y = ypos;
    }
    public void reset() {

    }

    // Use this for initialization
	void Start () {
        go = this.gameObject;
	}

	// Update is called once per frame
	void Update () {

	}
}
