using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class LevelSelect{

    public Canvas canvas;
    public GameObject canvas_go;

    private int page;

    private int pageLimit = 8;

    public LevelSelect(){
        page = 0;

        canvas_go = new GameObject();
        canvas_go.name = "LevelSelectCanvas";
        canvas_go.AddComponent<Canvas>();

        canvas = canvas_go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main.GetComponent<Camera>();
        canvas.sortingOrder = 1;

        loadPage(page);

    }

    private void loadPage(int pg){

        for (int k = pageLimit * pg; k < pageLimit * (pg+1) && k < GameController.instance.numLevels; k++){

            int x = k % (pageLimit / 2);
            int y = k / (pageLimit / 2);
            int levelNum = k + 1;

            int xcoord = -6 + (4 * x);
            int ycoord = 2 - (4 * y);
            //x: -6  -2 2 6
            //y: -2  2

            Object levelselect = Resources.Load<GameObject>("Prefabs/LevelSelectPanel");
            Vector3 levelselect_pos = new Vector3(xcoord, ycoord, 100);
            GameObject levelselect_go = (GameObject)Object.Instantiate(levelselect, levelselect_pos, Quaternion.identity);
            levelselect_go.transform.parent = canvas_go.transform;

            LevelSelectPanel lsp = levelselect_go.GetComponent<LevelSelectPanel>();
            lsp.setLevelNumber(levelNum);
            lsp.setXY(xcoord, ycoord);


        }


    }

}