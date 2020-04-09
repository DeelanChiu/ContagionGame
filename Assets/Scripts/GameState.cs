using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class GameState
{
    public Canvas canvas;
    public GameObject canvas_go;

    public bool levelPass;
    public bool EndGame;
    public bool levelEnd;
    public int level;
    public int survivalPercent;

    public int infectionPlusInc;

    public BlocksLeft blksLeft;

    public int[] survivalPercentLevels =  {0, 0, 0};
    public int currPopulation;
    public int totalPopulation;

    public int pendingTimers;

    public int infectedTowns;

    Town[,] matrix;

    // PlayerPrefs: LoseGame: 0 = in progress, 1 = win game, 2 = stuck, 3 = eaten, 4 = burned

    private void loadData(string jsonString){
        var gamedata = JSON.Parse(jsonString);
        var leveldata = gamedata["Level" + level];

        infectionPlusInc = leveldata["infectionPlusInc"].AsInt;
        var survivalPercentLevelsJSON = leveldata["survivalPercentLevels"];
        for (int k = 0; k < 3; k++){
            survivalPercentLevels[k] = survivalPercentLevelsJSON[k].AsInt;
        }
        Debug.Log(infectionPlusInc);

    }

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

        // Canvas
        canvas_go = new GameObject();
        canvas_go.name = "LevelCanvas";
        canvas_go.AddComponent<Canvas>();

        canvas = canvas_go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main.GetComponent<Camera>();
        canvas.sortingOrder = 2;
        canvas_go.AddComponent<CanvasScaler>();
        canvas_go.AddComponent<GraphicRaycaster>();

        levelPass = false;
        EndGame = false;
        levelEnd = false;

        pendingTimers = 0;
        infectedTowns = 0;
        totalPopulation = 0;
        currPopulation = 0;

        matrix = new Town[4,3];

        level = l;
        loadData(jsonString);

        //infectionPlusInc = 2;
        //survivalPercentLevels[0] = 60;
        //survivalPercentLevels[1] = 80;
        //survivalPercentLevels[2] = 95;

        createTown(1, 1, 100, 0f);
        createTown(1, 2, 80, 5.0f);
        createTown(2, 1, 50, 0f);
        createTown(0, 0, 50, 0f);
        createTown(3, 2, 50, 0f);


        createRoad(1, 1, 1, 2);
        createRoad(1, 1, 2, 1);
        
        blksLeft = showBlocksLeft(1);
        showPopAlive();
        
    }
    
    private Town createTown(int coordX, int coordY, int townPop, float infectTime/*JSONNode currTown*/)
    {
        totalPopulation += townPop;
        currPopulation += townPop;

        //int coordX = 0;
        //int coordY = 0;

        float x = 0;
        float y = 0;
        
        if (coordX == 0){
            x = -6.5f;
        } else if (coordX == 1){
            x = -2.2f;
        } else if (coordX == 2){
            x = 2.2f;
        } else if (coordX == 3){
            x = 6.5f;
        }

        if (coordY == 0){
            y = 3.5f;
        } else if (coordY == 1){
            y = -0.5f;
        } else if (coordY == 2){
            y = -4.5f;
        }
        
        /*
        if (coordX == 0){
            x = -416f;
        } else if (coordX == 1){
            x = -140.8f;
        } else if (coordX == 2){
            x = 140.8f;
        } else if (coordX == 3){
            x = 416f;
        }

        if (coordY == 0){
            y = 224f;
        } else if (coordY == 1){
            y = -32f;
        } else if (coordY == 2){
            y = -288f;
        }
        */

        string prefabName = "Prefabs/Town";
        Object town = Resources.Load<GameObject>(prefabName);
        Vector3 pos = new Vector3(x, y, 100);
        GameObject town_go = (GameObject)Object.Instantiate(town, pos, Quaternion.identity);
        town_go.transform.parent = canvas_go.transform;
        
        //set XY
        Town tn = town_go.GetComponent<Town>();
        
        tn.setXY(coordX, coordY);
        tn.population = townPop;

        if (infectTime > 0f){
            pendingTimers += 1;
            tn.setInfectTimer(infectTime);
        }

        matrix[coordX, coordY] = tn;

        return tn;

    }

    private Road createRoad(int coordX1, int coordY1, int coordX2, int coordY2){
        Town tnA = matrix[coordX1, coordY1];
        Town tnB = matrix[coordX2, coordY2];

        //RoadType rt = RoadType.HORIZONTAL;
        string prefabName = "Prefabs/";

        tnA.neighbors.Add(tnB);
        tnB.neighbors.Add(tnA);

        //left to right, up to down;

        float x = 0;
        float y = 0;

        if (coordX1 == coordX2) { //vertical
            //rt = RoadType.VERTICAL;
            prefabName += "vertical";
            if (coordX1 == 0){
                x = -6.5f;
            } else if (coordX1 == 1){
                x = -2.2f;
            } else if (coordX1 == 2){
                x = 2.2f;
            } else if (coordX1 == 3){
                x = 6.5f;
            }

            if (coordY1 == 0){
                y = 1.1f;
            } else if (coordY1 == 1){
                y = -3.0f;
            }

        } else if (coordY1 == coordY2) { //horizontal
            //rt = RoadType.HORIZONTAL;
            prefabName += "horizontal";
            if (coordX1 == 0){
                x = -4.4f;
            } else if (coordX1 == 1){
                x = 0.2f;
            } else if (coordX1 == 2){
                x = 4.5f;
            }

            if (coordY1 == 0){
                y = 3.3f;
            } else if (coordY1 == 1){
                y = -0.9f;
            } else if (coordY1 == 2){
                y = -5.2f;
            }


        } else {
            if (coordY1 + 1 == coordY2){ //diagdown
                //rt = RoadType.DIAGDOWN;
                prefabName += "diagdown";

                if (coordY1 == 0){
                    y = 1.1f;
                } else if (coordY1 == 1){
                    y = -3.0f;
                }

            } else if (coordY1 - 1 == coordY2){ //diagup
                //rt = RoadType.DIAGUP;
                prefabName += "diagup";

                if (coordY1 == 1){
                    y = 1.1f;
                } else if (coordY1 == 2){
                    y = -3.0f;
                }

            }

            if (coordX1 == 0){
                x = -4.4f;
            } else if (coordX1 == 1){
                x = 0.2f;
            } else if (coordX1 == 2){
                x = 4.5f;
            }

        } 

        prefabName += "Road";
        Object road = Resources.Load<GameObject>(prefabName);
        Vector3 pos = new Vector3(x, y, 100);
        GameObject road_go = (GameObject)Object.Instantiate(road, pos, Quaternion.identity);
        
        //set XY
        Road rd = road_go.GetComponent<Road>();
        road_go.transform.parent = canvas_go.transform;
        //rd.rdtype = rt;
        rd.setXY(coordX1, coordY1);
        rd.town1 = tnA;
        rd.town2 = tnB;

        return rd;
    }

    BlocksLeft showBlocksLeft (int blocks){
        string prefabName = "Prefabs/BlocksLeft";
        Object blocksleft = Resources.Load<GameObject>(prefabName);
        Vector3 pos = new Vector3(6.4f, 5.4f, 100);
        GameObject blocksleft_go = (GameObject)Object.Instantiate(blocksleft, pos, Quaternion.identity);
        blocksleft_go.transform.parent = canvas_go.transform;
        
        //set XY
        BlocksLeft blc = blocksleft_go.GetComponent<BlocksLeft>();
        blc.setBlocksLeft(blocks);

        return blc;
    }

    PopAlive showPopAlive(){
        string prefabName = "Prefabs/PopAlive";
        Object popAlive = Resources.Load<GameObject>(prefabName);
        Vector3 pos = new Vector3(3.0f, 5.4f, 100);
        GameObject popalive_go = (GameObject)Object.Instantiate(popAlive, pos, Quaternion.identity);
        popalive_go.transform.parent = canvas_go.transform;
        
        //set XY
        PopAlive pa = popalive_go.GetComponent<PopAlive>();
        return pa;
    }

    public WinLoseScreen setupWinLoseScreen(){

        GameObject resultCanvas_go = new GameObject();
        resultCanvas_go.name = "ResultCanvas";
        resultCanvas_go.AddComponent<Canvas>();

        Canvas resultCanvas = resultCanvas_go.GetComponent<Canvas>();
        resultCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        resultCanvas.worldCamera = Camera.main.GetComponent<Camera>();
        resultCanvas.sortingOrder = 3;
        resultCanvas_go.AddComponent<CanvasScaler>();
        resultCanvas_go.AddComponent<GraphicRaycaster>();

        levelEnd = true;
        string prefabName = "Prefabs/";
        survivalPercent = (int)((float)currPopulation/(float)totalPopulation * 100f);
        if (survivalPercent >= survivalPercentLevels[0]){
            prefabName += "winScreen";
            levelPass = true;
        } else {
            prefabName += "loseScreen";
        }
        Object winScreen = Resources.Load<GameObject>(prefabName);
        Vector3 pos = new Vector3(0f, -0.6f, 100);
        GameObject winScreen_go = (GameObject)Object.Instantiate(winScreen, pos, Quaternion.identity);
        winScreen_go.transform.parent = resultCanvas_go.transform;
        
        //set XY
        WinLoseScreen wls = winScreen_go.GetComponent<WinLoseScreen>();
        //winScreen_go.SetActive(false);
        //wls.winScreen_go = winScreen_go;

        return wls;
    }
    

}
