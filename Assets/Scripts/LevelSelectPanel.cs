using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEditor;
using UnityEngine.UI;

public class LevelSelectPanel : GameItem
{
    private Animator animator;

    private int levelNum = 0;

    private GameObject text_go;
    private Text text;
    private RectTransform rectTransform;
    private bool locked;

    new void Awake(){
        base.Awake();

        itemCanvas_go.name = "levelSelectCanvas";
        itemCanvas.sortingOrder = 2;
        itemCanvasRect.localPosition = new Vector3(0, 0, 0);
        itemCanvasRect.sizeDelta = new Vector2(3f, 1f);
    }
    // Use this for initialization
    void Start()
    {
        text_go = new GameObject();
        text_go.transform.parent = itemCanvas_go.transform;
        text_go.name = "LevelSelectInfo";

        text = text_go.AddComponent<Text>();
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.text = ""+levelNum;
        text.fontSize = 170;
        text.color = Color.black;
        text.lineSpacing = 0.9f;
        text.alignment = TextAnchor.MiddleCenter;

        // Text position
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.sizeDelta = new Vector2(400, 200);
        rectTransform.localScale = new Vector3(0.015f,0.015f,0.015f);

        /*
        GameObject lock_go = new GameObject();
        lock_go.transform.parent = itemCanvas_go.transform;
        SpriteRenderer lockSprite = lock_go.AddComponent<SpriteRenderer>();
        lockSprite.sprite = Resources.Load<Sprite>("Prefabs/Lock");
        lockSprite.sortingOrder = 2;
        */
    
        locked = levelNum > GameController.instance.reachedLevel;

        if (locked){
            Object lockPic = Resources.Load<GameObject>("Prefabs/Lock");
            GameObject lock_go = (GameObject)Object.Instantiate(lockPic, new Vector3(xpos, ypos, 100), Quaternion.identity);
            lock_go.transform.parent = itemCanvas_go.transform;
        
        }

        int heartNum = GameController.instance.levelHearts[levelNum-1];
        Object smallHeart = Resources.Load<GameObject>("Prefabs/SmallHeart");
        float heartPos = xpos-1;
        for (int k = 0; k < heartNum; k++){
            GameObject smallHeart_go = (GameObject)Object.Instantiate(smallHeart, new Vector3(heartPos, ypos-1.95f, 100), Quaternion.identity);
            smallHeart_go.transform.parent = itemCanvas_go.transform;
            heartPos+=1;
        }

    }

    public void setLevelNumber(int n){
        levelNum = n;
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void OnMouseDown()
    {
        if (!locked){
            GameController.instance.LoadLevel(levelNum);
        }

    }

    public void OnMouseOver()
    {
        
    }

    public void OnMouseExit()
    {
        
    }

}
