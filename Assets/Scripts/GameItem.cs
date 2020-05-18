using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEditor;
using UnityEngine.UI;


public enum ItemType {
    TOWN,
    ROAD
}

public abstract class GameItem : MonoBehaviour {

    public Canvas itemCanvas;
    public GameObject itemCanvas_go;
    public RectTransform itemCanvasRect;
    public ItemType itemtype;
    public int xpos;
    public int ypos;

    public GameObject go;

    public ItemType getType() {
        return itemtype;
    }

/*
    public void changeLoc(int xpos, int ypos) {

      x = xpos;
      y = ypos;
      //change on GUI
      target = new Vector3(xpos * 20 - 20, ypos*(-20) + 20, 99);
    }
*/

    public void Awake(){
        itemCanvas_go = new GameObject();
        itemCanvas_go.AddComponent<Canvas>();
        itemCanvas_go.transform.SetParent(this.transform);

        itemCanvas = itemCanvas_go.GetComponent<Canvas>();
        itemCanvas_go.AddComponent<CanvasScaler>();
        itemCanvas_go.AddComponent<GraphicRaycaster>();

        itemCanvasRect = itemCanvas_go.GetComponent<RectTransform>();

    }

    public void setXY(int xpos, int ypos) {
      this.xpos = xpos;
      this.ypos = ypos;
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
