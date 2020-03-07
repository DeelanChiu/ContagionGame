using System.Collections.Generic;
using UnityEngine;
//using SimpleJSON;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class GameState
{
    private int cols;
    private int rows;

    public bool LoseGame;
    public bool EndGame;
    public int level;

    // PlayerPrefs: LoseGame: 0 = in progress, 1 = win game, 2 = stuck, 3 = eaten, 4 = burned
    public GameState(int l, string jsonString)
    {
        /*
        foodCount = 0;
        level = l;
        click1 = null;
        NoMoreValidMoves = false;
        EndGame = false;
        onFire = false;

        loadData(jsonString);

        //show hint text
        GameController.instance.setHint(hintString);

        //set camera
        //GameObject.Find("MainCamera").GetComponent<MainCamera>().setPosition(cols, rows);
        Camera.main.transform.position =
        new Vector3((((float)cols)/2 - 0.5f) * 20 - 20, (((float)rows)/2 - 0.5f) * (-20) + 20, -10);
        Camera.main.orthographicSize = 65;
        if (rows >= 5) {
          Camera.main.orthographicSize = 70;
        }
        if (rows >= 7) {
          Camera.main.orthographicSize = 80;
        }
        Color bgColor = new Color();
        ColorUtility.TryParseHtmlString("#007C13", out bgColor);
        Camera.main.backgroundColor = bgColor;

        if (l==1) {
            //show hint arrow
            Vector3 pos = new Vector3(-33, 0, 100);
            Object startarrow = Resources.Load<Object>("Prefabs/StartArrow");
            GameObject starta_go = (GameObject)Object.Instantiate(startarrow, pos, Quaternion.identity);
            starta_go.name = "StartArrow";
        }

        initColor = GameObject.Find("Square00").GetComponent<SpriteRenderer>().color;
        */

        Vector3 pos = new Vector3(0, 0, 100);
        Object town = null;
        string prefabName = "Prefabs/Town";
        town = Resources.Load<GameObject>(prefabName);
        GameObject town_go = (GameObject)Object.Instantiate(town, pos, Quaternion.identity);
        town_go.name = "Town1";
        Town tn = town_go.GetComponent<Town>();

        GameObject canvas_go;
        GameObject text_go;
        Canvas canvas;
        Text text;
        RectTransform rectTransform;

        // Canvas
        canvas_go = new GameObject();
        canvas_go.name = "Canvas";
        canvas_go.AddComponent<Canvas>();

        canvas = canvas_go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main.GetComponent<Camera>();;
        canvas.sortingOrder = 2;
        canvas_go.AddComponent<CanvasScaler>();
        canvas_go.AddComponent<GraphicRaycaster>();

        // Text
        text_go = new GameObject();
        text_go.transform.parent = canvas_go.transform;
        text_go.name = "wibble";

        text = text_go.AddComponent<Text>();
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.text = "wobble";
        text.fontSize = 30;
        text.alignment = TextAnchor.MiddleCenter;

        // Text position
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, -40, 0);
        rectTransform.sizeDelta = new Vector2(400, 200);
        rectTransform.localScale = new Vector3(1f,1f,1f);
        /*
        Canvas canv;
        GameObject ngo = new GameObject("myTextGO");
        ngo.transform.SetParent(GameObject.Find("Canvas").GetComponent<Canvas>.transform);

        Text myText = ngo.AddComponent<Text>();
        myText.text = "Ta-dah!";
        */
        /*
        GameObject canvas_go = (GameObject)GameObject.Find("Canvas").GetComponent<Canvas>();
        Text text = canvas_go.AddComponent<Text>();
        text.text = "hello";
        
        /*
        Object townInfo = Resources.Load<GameObject>("Prefabs/TownInfo");
        Debug.Log(townInfo == null);
        GameObject townInfo_go = (GameObject)Object.Instantiate(townInfo, pos, Quaternion.identity);
        Text townInfo_txt = townInfo_go.GetComponent<Text>();
        townInfo_txt.text = "hello";
        */



    }

}
