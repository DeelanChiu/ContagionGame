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
    public int evacInc;

    public float incTiming;

    public float infectionPlusInc;

    public BlocksLeft blksLeft;

    public int[] survivalPercentLevels =  {0, 0, 0};
    public int currPopulation;
    public int totalPopulation;

    public int pendingTimers;

    public int infectedTowns;

    public bool tutorialRoadBlocked;

    Town[,] matrix;

    // PlayerPrefs: LoseGame: 0 = in progress, 1 = win game, 2 = stuck, 3 = eaten, 4 = burned

    private void loadData(){
        var gamedata = JSON.Parse(GameController.instance.levelDataJson);
        var leveldata = gamedata["Level" + level];
        infectionPlusInc = leveldata["infectionPlusInc"].AsFloat;
        blksLeft = showBlocksLeft(leveldata["blocksAllowed"].AsInt);
        evacInc = leveldata["evacInc"].AsInt;
        incTiming = leveldata["incTiming"].AsFloat;

        var survivalPercentLevelsJSON = leveldata["survivalPercentLevels"];
        for (int k = 0; k < 3; k++){
            survivalPercentLevels[k] = survivalPercentLevelsJSON[k].AsInt;
        }

        var towns = leveldata["towns"];
        for (int k = 0; k < towns.Count; k++){
            var townData = towns[k];
            createTown(townData["coordX"].AsInt,  townData["coordY"].AsInt, 
                townData["townPop"].AsInt, townData["infectTime"].AsInt);
        }

        var roads = leveldata["roads"];
        for (int k = 0; k < roads.Count; k++){
            var roadData = roads[k];
            createRoad(roadData["coordX1"].AsInt, roadData["coordY1"].AsInt, 
                roadData["coordX2"].AsInt, roadData["coordY2"].AsInt);
        }
        

    }

    public GameState(int l)
    {
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
        loadData();



        if (level == 1){//tutorial
            float[] frameLengths = {6f, 8f, 10f, 5f, 8f, 6f, 5f};
            tutorialRoadBlocked = false;

            float offset = 0;
            displayTutorialPart("townTutorial", 2.16f, 1.34f, offset, frameLengths[0]);
            displayTutorialPart("townPopTutorial", 5.81f, -1.24f, offset, frameLengths[0]);
            displayTutorialPart("roadTutorial", -2.04f, -2.92f, offset, frameLengths[0]);
            offset+=frameLengths[0];

            displayTutorialPart("infectedTownTutorial", 5.72f, -2.92f, offset, frameLengths[1]);
            offset+=frameLengths[1];

            displayTutorialPart("infectionChanceTutorial", -5.7f, 0.45f, offset, frameLengths[2]);
            displayTutorialPart("warningTownTutorial", -2.13f, -3.38f, offset, frameLengths[2]);
            offset+=frameLengths[2];

            displayTutorialPart("blockRoadTutorial", 0f, -2.9f, offset, frameLengths[3]);
            offset=0;

            displayTutorialPart("offlineTownTutorial", 5.7f, -2.9f, offset, frameLengths[4]);
            offset+=frameLengths[4];

            displayTutorialPart("blocksLeftTutorial", 5.9f, 3.5f, offset, frameLengths[5]);
            offset+=frameLengths[5];

            displayTutorialPart("popAliveTutorial", 3f, 3.5f, offset, frameLengths[6]);
            offset+=frameLengths[6];


        }
        
        showPopAlive();
        showLevelSign();
        
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
        //town_go.transform.parent = canvas_go.transform;
        town_go.transform.SetParent(canvas_go.transform);
        
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

    private void displayTutorialPart(string prefab, float x, float y, float t1, float t2){
        string prefabName = "Prefabs/Tutorial/"+prefab;

        Object tutorial = Resources.Load<GameObject>(prefabName);
        Vector3 pos = new Vector3(x, y, 100);
        GameObject tutorial_go = (GameObject)Object.Instantiate(tutorial, pos, Quaternion.identity);
        //tutorial_go.transform.parent = canvas_go.transform;
        tutorial_go.transform.SetParent(canvas_go.transform);

        TutorialComponent tc = tutorial_go.GetComponent<TutorialComponent>();
        tc.prefab = prefab;
        pendingTimers += 1;
        tc.setTimer(t1, t2);

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
                y = -2.8f;
            }

        } else if (coordY1 == coordY2) { //horizontal
            //rt = RoadType.HORIZONTAL;
            prefabName += "horizontal";
            if (coordX1 == 0){
                x = -4.4f;
            } else if (coordX1 == 1){
                x = 0f;
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
        //road_go.transform.parent = canvas_go.transform;
        road_go.transform.SetParent(canvas_go.transform);
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
        //blocksleft_go.transform.parent = canvas_go.transform;
        blocksleft_go.transform.SetParent(canvas_go.transform);

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
        //popalive_go.transform.parent = canvas_go.transform;
        popalive_go.transform.SetParent(canvas_go.transform);

        //set XY
        PopAlive pa = popalive_go.GetComponent<PopAlive>();
        return pa;
    }

    LevelSign showLevelSign (){
        string prefabName = "Prefabs/LevelSign";
        Object levelsign = Resources.Load<GameObject>(prefabName);
        Vector3 pos = new Vector3(-6f, 5.4f, 100);
        GameObject levelsign_go = (GameObject)Object.Instantiate(levelsign, pos, Quaternion.identity);
        //levelsign_go.transform.parent = canvas_go.transform;
        levelsign_go.transform.SetParent(canvas_go.transform);

        //set XY
        LevelSign lvs = levelsign_go.GetComponent<LevelSign>();

        return lvs;
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
            GameController.instance.reachedLevel = Mathf.Max(GameController.instance.reachedLevel, level+1);
            GameController.instance.reachedLevel = Mathf.Min(GameController.instance.reachedLevel, GameController.instance.numLevels);
            PlayerPrefs.SetInt("reachedLevel", Mathf.Max(PlayerPrefs.GetInt("reachedLevel"), GameController.instance.reachedLevel));
        
            GameController.instance.audioSource.PlayOneShot(GameController.instance.pass, 5);
        } else {
            prefabName += "loseScreen";
             GameController.instance.audioSource.PlayOneShot(GameController.instance.alarm, 1);
        }
        Object winScreen = Resources.Load<GameObject>(prefabName);
        Vector3 pos = new Vector3(0f, -0.6f, 100);
        GameObject winScreen_go = (GameObject)Object.Instantiate(winScreen, pos, Quaternion.identity);
        //winScreen_go.transform.parent = resultCanvas_go.transform;
        winScreen_go.transform.SetParent(resultCanvas_go.transform);
        
        //set XY
        WinLoseScreen wls = winScreen_go.GetComponent<WinLoseScreen>();
        //winScreen_go.SetActive(false);
        //wls.winScreen_go = winScreen_go;

        return wls;
    }

    

}
