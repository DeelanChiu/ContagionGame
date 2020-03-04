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

public class Animal : GameItem
{
    private Animator animator;
    public AnimalType type;
    public float speed;
    public Vector3 target;
    public AudioClip jumpAudio;
    public AudioSource audioSource;
    public bool checkTiger = false;
    public Texture2D cursor;
    public Texture2D animalCursor;
    private bool tigerEat = false;
    public AudioSource tigerHungry;
    public Animal animalEaten_TigerTypeOnly;
    public Square foodSquare;

    public void setAnimalType(string s)
    {
        if (s.ToUpper() == "RABBIT")
        {
            type = AnimalType.RABBIT;
        }
        else if (s.ToUpper() == "CAT")
        {
            type = AnimalType.CAT;
        }
        else if (s.ToUpper() == "DOG")
        {
            type = AnimalType.DOG;
        }
        else if (s.ToUpper() == "TIGER")
        {
            type = AnimalType.TIGER;
        }
    }

    public AnimalType getAnimalType()
    {
        return type;
    }

    public void moveTo(int xpos, int ypos, Square foodSq)
    {
        foodSquare = foodSq;
        LoggingManager.instance.RecordEvent(1, this.name + " moved to (" + xpos + " " + ypos + ")");
        //play audio
        if (tigerEat == false) {
          playJumpSound();
        }
        if (tigerEat) {
          Debug.Log("eating!");
        }

        if (xpos < x){ //going left
          //Debug.Log("Left");
          animator.SetInteger("moveDir", 4);
        } else if (xpos > x){ //going right
          //Debug.Log("Right");
          animator.SetInteger("moveDir", 2);
        } else if (ypos < y){ //going up
          //Debug.Log("Up");
          animator.SetInteger("moveDir", 1);
        } else { //going down
          //Debug.Log("Down");
          animator.SetInteger("moveDir", 3);
        }

        setX(xpos);
        setY(ypos);
        target = new Vector3(xpos * 20 - 20, ypos * (-20) + 20, 99);
        GameController.instance.midMove = true;

        animator.SetBool("midMove", true);

    }

    // Use this for initialization
    void Start()
    {
        target = transform.position;
        speed = 80;
        animator = GetComponent<Animator>();
        // only rabbit and tiger has animator right now

        audioSource = GetComponent<AudioSource>();
        cursor = Resources.Load<Texture2D>("Cursor/cursor");
        //square = Resources.Load<GameObject>("Prefabs/MidBottomSquare");
        animalCursor = Resources.Load<Texture2D>("Cursor/cursor2");

        //AB testing
        if (LoggingManager.instance.playerAB == 1) {
          //always show highlightes
          animator.SetBool("selected", true);
        }

    }

    void playJumpSound() {
      audioSource.Play(0);
    }

    public void setTigerEat(bool t) {
      tigerEat = t;
    }

    // Update is called once per frame
    void Update()
    {
        if (!transform.position.Equals(target)
        && GameController.instance.midMove
        && (GameController.instance.gs().click1.Equals(this) || tigerEat))
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            GameController.instance.movingAnimalPos = transform.position;
            if (transform.position.Equals(target))
            {
                GameController.instance.midMove = false;

                animator.SetBool("midMove", false);

                //fire
                GameController.instance.gs().burn();

                if (foodSquare != null && transform.position.x <= foodSquare.x * 20 && transform.position.x >= foodSquare.x * 20 - 40
    && transform.position.x <= foodSquare.y * (-20) + 40 && transform.position.x >= foodSquare.y * (-20))
                {
                    foodSquare.removeItem();                     foodSquare = null;                 }
                if (checkTiger) {
                  GameController.instance.gs().checkAndMoveTiger();
                  checkTiger = false;
                }


                if (type == AnimalType.TIGER && animalEaten_TigerTypeOnly != null && transform.position.Equals(target))
                {                     Debug.Log(transform.position.Equals(target));                     animalEaten_TigerTypeOnly.go.SetActive(false);                     animalEaten_TigerTypeOnly = null;                 }

            }
        }

    }

    void OnMouseDown()
    {
        if (!GameController.instance.midMove)
        {
            GameController.instance.gs().clickAnimal(this);
            GameController.instance.gs().findValidMoves(this);

            //gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            animator.SetBool("selected", true);

            if (GameController.instance.currLevel == 1) {
                GameController.instance.setHint("Click the darkened square eat the Carrot!");

                //move start arrow when clicked on in first level
                GameObject sa = GameObject.Find("StartArrow");
                sa.transform.position = new Vector3(33, 0, 100);
                SpriteRenderer sR = sa.GetComponent<SpriteRenderer>();
                sR.flipX = true;
            }
        }
    }

    public void OnMouseOver()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(animalCursor, Vector2.zero, CursorMode.Auto);
    }

    public void deselect()
    {
        if (LoggingManager.instance.playerAB != 1) {
          //not always highlighted
          animator.SetBool("selected", false);
        }

    }

    public void setCheckTiger(bool t) {
      checkTiger = t;
    }

    /** helper function for making idle animation
     * highlighted after restart.
    */
    public void playerABRestart() {
      if (LoggingManager.instance.playerAB == 1) {
        animator.SetBool("selected", true);
      }
    }

}
