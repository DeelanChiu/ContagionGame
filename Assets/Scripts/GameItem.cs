using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    TOWN,
    BRIDGE
}

public abstract class GameItem : MonoBehaviour {
    public ItemType itemtype;
    public int x;
    public int y;
    private int originalX;
    private int originalY;

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
    public void setX(int xpos) {
      x = xpos;
    }

    public void setY(int ypos) {
      y = ypos;
    }

    public void setXY(int xpos, int ypos) {
      this.setX(xpos);
      this.setY(ypos);
      transform.position = new Vector3(xpos * 20 - 20, ypos*(-20) + 20, 99);
    }

    public void setOriginalXY(int xpos, int ypos) {
      setXY(xpos, ypos);
      originalX = xpos;
      originalY = ypos;
    }

    public void reset() {
        /*/
      if (itemtype != ItemType.FIRE) {
        go.SetActive(true);
        GameController.instance.gs().getSquare(originalX, originalY).addItem(this);
        Animal thisA = this as Animal;
        if (thisA != null) {
          thisA.target = transform.position;
        }
        if (itemtype == ItemType.FOOD) {
          Food thisF = this as Food;
          thisF.reappear();
        }
      } else if (itemtype.Equals(ItemType.FIRE)) {
        //fire reset
        Destroy(go);
        */
      }
    }

    /*
    public void setItemType(string s) {
      if (s == "Animal" || s == "animal" || s == "ANIMAL") {
        itemtype = ItemType.ANIMAL;
      } else if (s == "Food" || s == "food" || s == "FOOD") {
        itemtype = ItemType.FOOD;
      } else if (s == "" || s == "None" || s == "NONE" || s == "none") {
        itemtype = ItemType.NONE;
      }
    }
    */

    // Use this for initialization
	void Start () {
        go = this.gameObject;
	}

	// Update is called once per frame
	void Update () {

	}
}
